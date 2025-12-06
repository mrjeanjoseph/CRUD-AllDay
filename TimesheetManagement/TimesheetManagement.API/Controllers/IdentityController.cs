using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimesheetManagement.API.Services;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Commands.AssignRole;
using TimesheetManagement.Application.Identity.Commands.ChangePassword;
using TimesheetManagement.Application.Identity.Commands.RegisterUser;
using TimesheetManagement.Application.Identity.Queries.GetUserByEmail;
using TimesheetManagement.Application.Identity.Queries.GetUserById;
using TimesheetManagement.Application.Identity.Shared;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.API.Controllers;

[ApiController]
[Route("api/identity")]
[Authorize]
public class IdentityController : ControllerBase
{
    private readonly IQueryHandler<GetUserByIdQuery, UserDto> _getUserByIdHandler;
    private readonly IQueryHandler<GetUserByEmailQuery, UserDto> _getUserByEmailHandler;
    private readonly ICommandHandler<RegisterUserCommand, Guid> _registerUserHandler;
    private readonly ICommandHandler<AssignRoleCommand, bool> _assignRoleHandler;
    private readonly ICommandHandler<ChangePasswordCommand, bool> _changePasswordHandler;
    private readonly IPasswordHashingService _passwordHashingService;

    public IdentityController(
        IQueryHandler<GetUserByIdQuery, UserDto> getUserByIdHandler,
        IQueryHandler<GetUserByEmailQuery, UserDto> getUserByEmailHandler,
        ICommandHandler<RegisterUserCommand, Guid> registerUserHandler,
        ICommandHandler<AssignRoleCommand, bool> assignRoleHandler,
        ICommandHandler<ChangePasswordCommand, bool> changePasswordHandler,
        IPasswordHashingService passwordHashingService)
    {
        _getUserByIdHandler = getUserByIdHandler;
        _getUserByEmailHandler = getUserByEmailHandler;
        _registerUserHandler = registerUserHandler;
        _assignRoleHandler = assignRoleHandler;
        _changePasswordHandler = changePasswordHandler;
        _passwordHashingService = passwordHashingService;
    }

    /// <summary>
    /// Get User by ID
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <returns>User details</returns>
    /// <response code="200">User found</response>
    /// <response code="404">User does not exist</response>
    /// <response code="401">Invalid token</response>
    /// <response code="403">Access denied</response>
    [HttpGet("users/{userId:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserDto>> GetUserById(Guid userId, CancellationToken cancellationToken = default)
    {
        // Authorization: Admin can access any user, users can only access themselves
        var currentUserId = GetCurrentUserId();
        var currentUserRole = GetCurrentUserRole();
        
        if (currentUserRole != "Admin" && currentUserRole != "SuperAdmin" && currentUserId != userId)
        {
            return Forbid();
        }

        try
        {
            var query = new GetUserByIdQuery(userId);
            var result = await _getUserByIdHandler.Handle(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "User not found" });
        }
    }

    /// <summary>
    /// Get User by Email
    /// </summary>
    /// <param name="email">User email address</param>
    /// <returns>User details</returns>
    /// <response code="200">User found</response>
    /// <response code="404">User not found</response>
    /// <response code="400">Invalid email format</response>
    /// <response code="401">Invalid token</response>
    [HttpGet("users/email/{email}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest(new { message = "Email is required" });
        }

        try
        {
            var query = new GetUserByEmailQuery(email);
            var result = await _getUserByEmailHandler.Handle(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "User not found" });
        }
    }

    /// <summary>
    /// Register User
    /// </summary>
    /// <param name="request">User registration details</param>
    /// <returns>Created user ID</returns>
    /// <response code="201">User registered successfully</response>
    /// <response code="400">Validation error</response>
    /// <response code="409">Username or email already exists</response>
    [HttpPost("users/register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Hash the password
            var passwordHash = _passwordHashingService.HashPassword(request.Password);
            
            // Parse role from string, default to User if not specified or invalid
            if (!Enum.TryParse<Role>(request.Role, ignoreCase: true, out var role))
            {
                role = Role.User; // Default role
            }

            var command = new RegisterUserCommand(request.Username, request.Email, passwordHash, role);
            var userId = await _registerUserHandler.Handle(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetUserById), 
                new { userId }, 
                new { id = userId });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Assign Role to User
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="request">Role assignment details</param>
    /// <returns>Assignment result</returns>
    /// <response code="200">Role assigned successfully</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">User not found</response>
    /// <response code="403">Insufficient role</response>
    [HttpPost("users/{userId:guid}/roles")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AssignRole(Guid userId, [FromBody] AssignRoleRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var currentUserId = GetCurrentUserId();
        if (currentUserId == userId)
        {
            return BadRequest(new { message = "Cannot assign role to yourself" });
        }

        // Parse role from string
        if (!Enum.TryParse<Role>(request.Role, ignoreCase: true, out var role))
        {
            return BadRequest(new { message = "Invalid role specified. Valid roles are: User, Admin, SuperAdmin" });
        }

        try
        {
            var command = new AssignRoleCommand(userId, role);
            var success = await _assignRoleHandler.Handle(command, cancellationToken);

            if (!success)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new { success = true });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Change User Password
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <param name="request">Password change details</param>
    /// <returns>Password change result</returns>
    /// <response code="200">Password changed successfully</response>
    /// <response code="400">Invalid current password</response>
    /// <response code="401">Insufficient privilege</response>
    /// <response code="404">User not found</response>
    [HttpPut("users/{userId:guid}/password")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Authorization check - self or admin
        var currentUserId = GetCurrentUserId();
        var currentUserRole = GetCurrentUserRole();
        
        if (currentUserId != userId && currentUserRole != "Admin" && currentUserRole != "SuperAdmin")
        {
            return Forbid();
        }

        try
        {
            // Hash the new password
            var newPasswordHash = _passwordHashingService.HashPassword(request.NewPassword);
            
            var command = new ChangePasswordCommand(userId, newPasswordHash);
            var success = await _changePasswordHandler.Handle(command, cancellationToken);

            if (!success)
            {
                return BadRequest(new { message = "Invalid current password or user not found" });
            }

            return Ok(new { success = true });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }

    private string GetCurrentUserRole()
    {
        return User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }
}

/// <summary>
/// User registration request model
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// Username (max 64 characters, required)
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Valid email address (required)
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Password (min 8 characters, required)
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Role (User, Admin, SuperAdmin) - defaults to User if not specified
    /// </summary>
    public string Role { get; set; } = "User";
}

/// <summary>
/// Role assignment request model
/// </summary>
public class AssignRoleRequest
{
    /// <summary>
    /// Role to assign (Admin, SuperAdmin, User)
    /// </summary>
    public required string Role { get; set; }
}

/// <summary>
/// Password change request model
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// Current password
    /// </summary>
    public required string CurrentPassword { get; set; }

    /// <summary>
    /// New password (min 8 characters)
    /// </summary>
    public required string NewPassword { get; set; }
}