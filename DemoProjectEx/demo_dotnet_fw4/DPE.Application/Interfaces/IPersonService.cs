using DPE.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPE.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> GetByIdAsync(int id);
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task CreateAsync(PersonDto personDto);
    }
}
