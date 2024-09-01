using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Domain.DataAccessLayer;

public partial class AdWDbContext : DbContext {
    #region Person Entities
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<AddressType> AddressTypes { get; set; }
    public virtual DbSet<BusinessEntity> BusinessEntities { get; set; }
    public virtual DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
    public virtual DbSet<BusinessEntityContact> BusinessEntityContacts { get; set; }
    public virtual DbSet<ContactType> ContactTypes { get; set; }
    public virtual DbSet<CountryRegion> CountryRegions { get; set; }
    public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
    public virtual DbSet<Password> Passwords { get; set; }
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<PersonPhone> PersonPhones { get; set; }
    public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
    public virtual DbSet<StateProvince> StateProvinces { get; set; }
    public virtual DbSet<VAdditionalContactInfo> VAdditionalContactInfos { get; set; }
    public virtual DbSet<VStateProvinceCountryRegion> VStateProvinceCountryRegions { get; set; }

    #endregion

}
