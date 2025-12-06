using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetManagement.API.Services;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Identity.Queries.GetUserByEmail;
using TimesheetManagement.Application.Identity.Shared;
using TimesheetManagement.Domain.Identity;

namespace TimesheetManagement.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IQueryHandler<GetUserByEmailQuery, UserDto> _getUserByEmailHandler;
    private readonly IJwtTokenService _jwtService;
    private readonly IPasswordHashingService _passwordService;

    public AuthController(
        IQueryHandler<GetUserByEmailQuery, UserDto> getUserByEmailHandler,
        IJwtTokenService jwtService,
        IPasswordHashingService passwordService)
    {
        _getUserByEmailHandler = getUserByEmailHandler;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    /// <summary>
    /// Login with email and password
    /// </summary>
    /// <param name="request">Login credentials</param>
    /// <returns>JWT token and user information</returns>
    /// <response code="200">Login successful</response>
    /// <response code="400">Invalid credentials</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var query = new GetUserByEmailQuery(request.Email);
            var user = await _getUserByEmailHandler.Handle(query, cancellationToken);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // TODO: Verify password against stored hash
            // For now, this is a placeholder - you'll need to implement password verification
            // bool isValidPassword = _passwordService.VerifyPassword(request.Password, user.PasswordHash);
            
            // Temporary password check (remove in production)
            bool isValidPassword = request.Password == "password123"; // Replace with actual verification

            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

            return Ok(new LoginResponse
            {
                Token = token,
                User = user,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60) // Should match JWT expiry
            });
        }
        catch (KeyNotFoundException)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "An error occurred during login", details = ex.Message });
        }
    }

    /// <summary>
    /// Refresh JWT token
    /// </summary>
    /// <returns>New JWT token</returns>
    /// <response code="200">Token refreshed</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost("refresh")]
    [Authorize]
    [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var emailClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var roleClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (userIdClaim == null || emailClaim == null || roleClaim == null)
        {
            return Unauthorized(new { message = "Invalid token claims" });
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Invalid user ID in token" });
        }

        var newToken = _jwtService.GenerateToken(userId, emailClaim, roleClaim);

        return Ok(new RefreshTokenResponse
        {
            Token = newToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        });
    }

    /// <summary>
    /// Get current user information from token
    /// </summary>
    /// <returns>Current user details</returns>
    /// <response code="200">User information retrieved</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<UserDto> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var emailClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var roleClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (userIdClaim == null || emailClaim == null || roleClaim == null)
        {
            return Unauthorized(new { message = "Invalid token claims" });
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Invalid user ID in token" });
        }

        return Ok(new UserDto(userId, "", emailClaim, roleClaim)); // Username can be fetched if needed
    }
}

/// <summary>
/// Login request model
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Email address
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public required string Password { get; set; }
}

/// <summary>
/// Login response model
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// JWT access token
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// User information
    /// </summary>
    public required UserDto User { get; set; }

    /// <summary>
    /// Token expiration time
    /// </summary>
    public required DateTime ExpiresAt { get; set; }
}

/// <summary>
/// Refresh token response model
/// </summary>
public class RefreshTokenResponse
{
    /// <summary>
    /// New JWT access token
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Token expiration time
    /// </summary>
    public required DateTime ExpiresAt { get; set; }
}