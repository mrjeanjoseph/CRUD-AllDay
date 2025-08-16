using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPE.Domain.Aggregates.Person
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
    }
}