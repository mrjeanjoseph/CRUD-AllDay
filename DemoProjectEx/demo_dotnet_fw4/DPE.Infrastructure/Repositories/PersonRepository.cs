using DPE.Domain.Aggregates.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using DPE.Infrastructure.Persistence;
using DPE.Infrastructure.Mapping;

namespace DPE.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AdventureWorksDbContext _context;

        public PersonRepository(AdventureWorksDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var entity = await _context.Persons.FindAsync(id);
            return entity?.ToDomain(); // Extension method or mapper
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var entities = await _context.Persons.ToListAsync();
            return entities.Select(e => e.ToDomain());
        }

        public async Task AddAsync(Person person)
        {
            var efEntity = person.ToEntity(); // Map domain to EF entity
            _context.Persons.Add(efEntity);
            await _context.SaveChangesAsync();
        }
    }
}