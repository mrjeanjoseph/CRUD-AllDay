using AdventureWorks.Domain.Models;
using AdventureWorks.Domain.Repository;

namespace AdventureWorks.ServiceAPI.Services {
    public class DepartmentServiceTesting {
        private readonly IDataRepository _dataRepository;

        public DepartmentServiceTesting(IDataRepository dataRepository) {
            _dataRepository = dataRepository;
        }

        public Department? TestDepartmentService(short id) {
            return _dataRepository.DepartmentRepository.GetAll().SingleOrDefault(d => d.DepartmentId == id);
        }
    }
}
