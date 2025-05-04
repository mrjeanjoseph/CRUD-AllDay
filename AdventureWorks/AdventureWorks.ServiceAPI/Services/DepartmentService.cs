using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using AdventureWorks.ServiceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.ServiceAPI.Services;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
    Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
    Task AddDepartmentAsync(DepartmentDTO department);
    Task UpdateDepartmentAsync(DepartmentDTO department);
    Task DeleteDepartmentAsync(int departmentId);
}

public class DepartmentService : IDepartmentService
{
    protected readonly AdWDbContext _context;
    protected ILogger<DepartmentService> _logger;

    public DepartmentService(AdWDbContext context, [NotNull] ILogger<DepartmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
    {
        _logger.LogInformation(_logger.IsEnabled(LogLevel.Information) ? "GetAllDepartmentsAsync called" : "GetAllDepartmentsAsync not called");
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Eagerly load EmployeeDepartmentHistories
        var departments = await _context.Departments
            .Include(d => d.EmployeeDepartmentHistories)
            .ThenInclude(h => h.EmployeeHistoryDetail)
            .ToListAsync();

        // Map entities to DTOs
        var departmentDtos = departments.Select(d => new DepartmentDTO
        {
            DepartmentId = d.DepartmentId,
            Name = d.Name,
            GroupName = d.GroupName,
            EmployeeDepartmentHistories = d.EmployeeDepartmentHistories.Select(h => new EmployeeDepartmentHistoryDto
            {
                DepartmentId = h.DepartmentId,
                EmployeeDetails = h.EmployeeHistoryDetail != null ? new List<EmpDepartmentDetailDTO>
                {
                    new EmpDepartmentDetailDTO
                    {
                        BusinessEntityId = h.EmployeeHistoryDetail.BusinessEntityId,
                        LoginId = h.EmployeeHistoryDetail.LoginId,
                        JobTitle = h.EmployeeHistoryDetail.JobTitle,
                        BirthDate = h.EmployeeHistoryDetail.BirthDate
                    }
                } : [],
                StartDate = h.StartDate,
                EndDate = h.EndDate
            }).ToList()
        }).ToList();

        _logger.LogTrace("Departments retrieved");
        stopwatch.Stop();
        _logger.LogInformation($"GetAllDepartmentsAsync took {stopwatch.ElapsedMilliseconds} ms");

        return departmentDtos;
    }

    public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
    {
        var department = await _context.Departments
            .Include(d => d.EmployeeDepartmentHistories)
            .ThenInclude(h => h.EmployeeHistoryDetail)
            .FirstOrDefaultAsync(d => d.DepartmentId == (short)id);

        if (department == null)
            throw new KeyNotFoundException($"Department with id {id} not found.");

        // Map entity to DTO  
        var departmentDto = new DepartmentDTO
        {
            DepartmentId = department.DepartmentId,
            Name = department.Name,
            GroupName = department.GroupName,
            EmployeeDepartmentHistories = department.EmployeeDepartmentHistories.Select(h => new EmployeeDepartmentHistoryDto
            {
                DepartmentId = h.DepartmentId,
                BusinessEntityId = h.BusinessEntityId,
                EmployeeDetails = h.EmployeeHistoryDetail != null ? new List<EmpDepartmentDetailDTO>
               {
                   new EmpDepartmentDetailDTO
                   {
                       BusinessEntityId = h.EmployeeHistoryDetail.BusinessEntityId,
                       LoginId = h.EmployeeHistoryDetail.LoginId,
                       JobTitle = h.EmployeeHistoryDetail.JobTitle,
                       BirthDate = h.EmployeeHistoryDetail.BirthDate
                   }
               } : new List<EmpDepartmentDetailDTO>(),
                StartDate = h.StartDate,
                EndDate = h.EndDate
            }).ToList()
        };

        return departmentDto;
    }

    public async Task AddDepartmentAsync(DepartmentDTO departmentDto)
    {
        if (departmentDto == null)
            throw new ArgumentNullException(nameof(departmentDto));

        // Check if a department with the same name already exists
        var existingDepartment = await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == departmentDto.Name);

        if (existingDepartment != null)        
            throw new InvalidOperationException($"A department with the name '{departmentDto.Name}' already exists.");        

        var department = new Department
        {
            Name = departmentDto.Name,
            GroupName = departmentDto.GroupName
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(DepartmentDTO? departmentDto = null)
    {
        if (departmentDto == null)
            throw new ArgumentNullException(nameof(departmentDto));

        // Find the existing department
        var existingDepartment = await _context.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentDto.DepartmentId);

        if (existingDepartment == null)
            throw new KeyNotFoundException($"Department with ID {departmentDto.DepartmentId} not found.");

        // Update the department fields
        existingDepartment.Name = departmentDto.Name;
        existingDepartment.GroupName = departmentDto.GroupName;

        _context.Entry(existingDepartment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDepartmentAsync(int departmentId)
    {
        // Find the department by ID
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

        if (department == null)
            throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}