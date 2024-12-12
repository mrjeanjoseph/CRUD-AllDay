using AdventureWorks.Domain.Models;
using AdventureWorks.ServiceAPI.Models;
using AutoMapper;

namespace AdventureWorks.ServiceAPI;
public class MappingProfile : Profile {
    public MappingProfile() {

        CreateMap<Department, DepartmentDTO>();
        CreateMap<DepartmentDTO, Department>();

    }
}
