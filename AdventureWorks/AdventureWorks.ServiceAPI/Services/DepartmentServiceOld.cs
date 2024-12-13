using AdventureWorks.Domain.Models;
using AdventureWorks.Domain.Repository;
using System.Linq;

namespace AdventureWorks.ServiceAPI.Services;

public class DepartmentServiceOld {
    private readonly IDataRepository _departmentRepo;
    public DepartmentServiceOld(IDataRepository dataRepository) {
        _departmentRepo = dataRepository;
    }

    public async Task<IEnumerable<Department>> GetAllDepartments() {
        return await Task.FromResult(_departmentRepo.DepartmentRepository.GetAll().ToList());
    }

    public async Task<Department> GetDepartmentById(int id) {
        return await Task.FromResult(_departmentRepo.DepartmentRepository.GetAll().FirstOrDefault(d => d.DepartmentId == id));
    }

    public async Task<Department> AddDepartment(Department department) {
        return await Task.FromResult(_departmentRepo.DepartmentRepository.AddOrInsert(department));
    }

    public async Task<Department> UpdateDepartment(Department department) {
        return await Task.FromResult(_departmentRepo.DepartmentRepository.AddOrInsert(department));
    }

    public async Task DeleteDepartment(int id) {
        var department = await Task.FromResult(_departmentRepo.DepartmentRepository.GetAll().FirstOrDefault(d => d.DepartmentId == id));
        if (department != null) {
            _departmentRepo.DepartmentRepository.Remove(department);
            await Task.FromResult(_departmentRepo.Context.SaveChangesAsync());
        }        
    }

    //public async Task DeleteDepartment(Department department) {
    //    _departmentRepo.DepartmentRepository.Remove(department);
    //}
}
