using System.Configuration;

namespace PlatformServices.Infrastructure
{
    public class CustomDefaults : ConfigurationSectionGroup
    {
        public NewUserDefaultSection NewUserDefaults
        {
            get { return (NewUserDefaultSection)Sections["newUserDefaults"]; }
        }

        public PlaceSection Places
        {
            get { return (PlaceSection)Sections["places"]; }
        }

    }
}