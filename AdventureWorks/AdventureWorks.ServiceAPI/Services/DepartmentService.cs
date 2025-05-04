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
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
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
            .ThenInclude(h => h.BusinessEntity) // Include related Employee data if needed
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
                //BusinessEntityId = h.BusinessEntityId,// Adjust based on your entity structure
                EmployeeDetails = h.BusinessEntity != null ? new List<BusinessEntityDTO>
                {
                    new BusinessEntityDTO
                    {
                        BusinessEntityId = h.BusinessEntity.BusinessEntityId,
                        LoginId = h.BusinessEntity.LoginId,
                        JobTitle = h.BusinessEntity.JobTitle,
                        BirthDate = h.BusinessEntity.BirthDate
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
            .ThenInclude(h => h.BusinessEntity)
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
                EmployeeDetails = h.BusinessEntity != null ? new List<BusinessEntityDTO>
               {
                   new BusinessEntityDTO
                   {
                       BusinessEntityId = h.BusinessEntity.BusinessEntityId,
                       LoginId = h.BusinessEntity.LoginId,
                       JobTitle = h.BusinessEntity.JobTitle,
                       BirthDate = h.BusinessEntity.BirthDate
                   }
               } : new List<BusinessEntityDTO>(),
                StartDate = h.StartDate,
                EndDate = h.EndDate
            }).ToList()
        };

        return departmentDto;
    }

    public Task AddDepartmentAsync(Department department)
    {
        //_context.Departments.Add(department);
        _context.Entry(department).State = EntityState.Added;
        return _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(Department department)
    {
        _context.Entry(department).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDepartmentAsync(int departmentId)
    {
        var department = await _context.Departments.FindAsync(departmentId);

        if (department != null)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}