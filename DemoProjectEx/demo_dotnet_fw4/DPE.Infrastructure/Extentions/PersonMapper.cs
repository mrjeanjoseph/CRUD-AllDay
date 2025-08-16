using DPE.Domain.Aggregates.Person;
using DPE.Infrastructure.Entities;
using System;

namespace DPE.Infrastructure.Mapping
{
    public static class PersonMapper
    {
        public static Person ToDomain(this PersonEntity entity)
        {
            if (entity == null) return null;

            var name = new Name(
                title: null, // Entity does not have Title
                firstName: entity.FirstName,
                middleName: entity.MiddleName,
                lastName: entity.LastName,
                suffix: null // Entity does not have Suffix
            );

            return new Person(
                id: entity.BusinessEntityID,
                name: name,
                personType: entity.PersonType,
                nameStyle: entity.NameStyle,
                emailPromotion: entity.EmailPromotion ? 1 : 0 // Entity uses bool, domain uses int
            );
        }

        public static PersonEntity ToEntity(this Person domain)
        {
            if (domain == null) return null;

            return new PersonEntity
            {
                BusinessEntityID = domain.Id,
                FirstName = domain.Name?.FirstName,
                MiddleName = domain.Name?.MiddleName,
                LastName = domain.Name?.LastName,
                PersonType = domain.PersonType,
                NameStyle = domain.NameStyle,
                EmailPromotion = domain.EmailPromotion > 0, // Domain uses int, entity uses bool
                ModifiedDate = DateTime.UtcNow
            };
        }
    }
}