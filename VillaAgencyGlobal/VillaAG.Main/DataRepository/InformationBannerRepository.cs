using System.Collections.Generic;
using VillaAG.Main.Models;

namespace VillaAG.Main.DataRepository {
    public class InformationBannerRepository {
        private static List<InformationBanner> _inMemInfoBanners = new List<InformationBanner>{
            new InformationBanner { Id = 1, Name = "Pilot Knob", LocationCity = "Pilot Mountain", LocationState = "NC", QuickInfo = "Hurry! Get the Best Villa for you all" },
            new InformationBanner { Id = 2, Name = "Cozy Cottage", LocationCity = "Raleigh", LocationState = "NC", QuickInfo = "Be Quick! Get the best villa in town" },
            new InformationBanner { Id = 3, Name = "Modern Apartment", LocationCity = "Durham", LocationState = "NC", QuickInfo = "Act Now! Get the highest level penthouse" },
            new InformationBanner { Id = 4, Name = "Log House", LocationCity = "Pilot Mountain", LocationState = "NC", QuickInfo = "It's Yours! Enjoy this beautiful scenery" }
        };

        public ICollection<InformationBanner> GetAllInformationBanners() => _inMemInfoBanners;
        
        public InformationBanner GetInfoBannerById(int id) {
            foreach (var item in _inMemInfoBanners) {
                if (item.Id == id) {
                    return item;
                }
            }
            return null;
        }
    }
}