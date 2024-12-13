using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.ServiceAPI.Services;

public interface IDepartmentService {
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<Department> GetDepartmentByIdAsync(int id);
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(int departmentId);
}

public class DepartmentService : IDepartmentService {
    private readonly AdWDbContext _context;

    public DepartmentService(AdWDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync() {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department> GetDepartmentByIdAsync(int id) {
        var department = await _context.Departments.FindAsync(id);

        if (department == null) 
            throw new KeyNotFoundException($"Department with id {id} not found.");
        
        return department;
    }

    public Task AddDepartmentAsync(Department department) {
        //_context.Departments.Add(department);
        _context.Entry(department).State = EntityState.Added;
        return _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(Department department) {
        _context.Entry(department).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDepartmentAsync(int departmentId) {
        var department = await _context.Departments.FindAsync(departmentId);

        if(department != null) {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }
}