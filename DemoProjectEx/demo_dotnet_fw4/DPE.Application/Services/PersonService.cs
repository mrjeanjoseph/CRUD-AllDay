using DPE.Application.DTOs;
using DPE.Application.Interfaces;
using DPE.Domain.Aggregates.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DPE.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return null;

            return new PersonDto
            {
                Id = person.Id,
                Title = person.Name.Title,
                FirstName = person.Name.FirstName,
                MiddleName = person.Name.MiddleName,
                LastName = person.Name.LastName,
                Suffix = person.Name.Suffix,
                PersonType = person.PersonType,
                NameStyle = person.NameStyle,
                EmailPromotion = person.EmailPromotion
            };
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var people = await _personRepository.GetAllAsync();
            return people.Select(p => new PersonDto
            {
                Id = p.Id,
                Title = p.Name.Title,
                FirstName = p.Name.FirstName,
                MiddleName = p.Name.MiddleName,
                LastName = p.Name.LastName,
                Suffix = p.Name.Suffix,
                PersonType = p.PersonType,
                NameStyle = p.NameStyle,
                EmailPromotion = p.EmailPromotion
            });
        }

        public async Task CreateAsync(PersonDto dto)
        {
            var name = new Name(dto.Title, dto.FirstName, dto.MiddleName, dto.LastName, dto.Suffix);
            var person = new Person(dto.Id, name, dto.PersonType, dto.NameStyle, dto.EmailPromotion);
            await _personRepository.AddAsync(person);
        }
    }
}