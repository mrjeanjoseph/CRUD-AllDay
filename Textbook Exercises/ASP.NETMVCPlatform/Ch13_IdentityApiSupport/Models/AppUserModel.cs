using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityApiSupport.Models
{
    public enum Cities
    {
        Franklinton, Lascahobas, Raleigh, BonRepos, Polokwane
    }

    public enum Countries
    {
        None, USA, Haiti, SouthAfrica
    }
    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }
        public Countries Country { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.Franklinton:
                    Country = Countries.USA;
                    break;
                case Cities.Lascahobas:
                    Country = Countries.Haiti;
                    break;
                case Cities.Raleigh:
                    Country = Countries.USA;
                    break;
                case Cities.BonRepos:
                    Country = Countries.Haiti;
                    break;
                case Cities.Polokwane:
                    Country = Countries.SouthAfrica;
                    break;
                default:
                    Country = Countries.None; 
                    break;
            }
        }
    }
}