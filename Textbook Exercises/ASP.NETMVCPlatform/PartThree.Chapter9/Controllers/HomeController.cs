using PlatformServices.Infrastructure;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PlatformServices.Configuration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            SystemWebSectionGroup sysWeb = (SystemWebSectionGroup)WebConfigurationManager
                .OpenWebConfiguration("/").GetSectionGroup("system.web");

            configData.Add("debug", sysWeb.Compilation.Debug.ToString());
            configData.Add("targetFramework", sysWeb.Compilation.TargetFramework);

            return View(configData);
        }
        public ActionResult FormerIndexActionSix()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            foreach (string key in WebConfigurationManager.AppSettings.AllKeys)
                configData.Add(key, WebConfigurationManager.AppSettings[key]);

            return View(configData);
        }

        public ActionResult FolderLevelConfig()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            AppSettingsSection appSettings = WebConfigurationManager
                .OpenWebConfiguration("~/Views/Home").AppSettings;
            int counter = 1;
            foreach (string key in appSettings.Settings.AllKeys)
                configData.Add($"{counter++}-{key}", appSettings.Settings[key].Value);

            return View("Index",configData);
        }
        public ActionResult OtherAction()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            foreach (string key in WebConfigurationManager.AppSettings.AllKeys)
                configData.Add(key, WebConfigurationManager.AppSettings[key]);

            return View("Index",configData);
        }
        public ActionResult FormerIndexActionFive()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            CustomDefaults cdefaults = (CustomDefaults)WebConfigurationManager
                .OpenWebConfiguration("/").GetSectionGroup("customDefaults");

            foreach (Place place in cdefaults.Places.Places)            
                configData.Add(place.Code, string.Format("{0} {1}", place.City, place.Country));

            return View(configData);
        }

        public ActionResult DisplaySingle()
        {
            PlaceSection section = WebConfigurationManager
                .GetWebApplicationSection("customDefaults/places") as PlaceSection;
            Place defaultPlace = section.Places[section.Default];

            return View((object)string.Format("The default place is: {0}", defaultPlace.City));
        }

        public ActionResult FormerIndexActionFour()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();
            PlaceSection section = WebConfigurationManager
                .GetWebApplicationSection("places") as PlaceSection;

            foreach (Place place in section.Places)            
                configData.Add(place.Code, string.Format("{0} {1}", place.City, place.Country));

            return View("Index",configData);
        }

        public ActionResult FormerDisplaySingleActionTwo()
        {
            PlaceSection section = WebConfigurationManager
                .GetWebApplicationSection("places") as PlaceSection;
            Place defaultPlace = section.Places[section.Default];

            return View((object)string.Format("The default place is: {0}", defaultPlace.City));
        }

        public ActionResult FormerIndexActionThree()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            NewUserDefaultSection newdefaults = WebConfigurationManager
                .GetWebApplicationSection("newUserDefaults") as NewUserDefaultSection;

            if(newdefaults != null)
            {
                configData.Add("City", newdefaults.City);
                configData.Add("Country", newdefaults.Country);
                configData.Add("Language", newdefaults.Language);
                configData.Add("Region", newdefaults.Region.ToString());
            };
            
            return View(configData);
        }

        public ActionResult FormerIndexActionTwo()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();
            var appSettingData = WebConfigurationManager.AppSettings;
            var connStringData = WebConfigurationManager.ConnectionStrings;

            foreach (string key in appSettingData)            
                configData.Add(key, appSettingData[key]);

            foreach (ConnectionStringSettings cs in connStringData)
                configData.Add(cs.Name, $"{cs.ProviderName} {cs.ConnectionString}");
            
            return View(configData);
        }

        public ActionResult FormerDisplaySingleActionOne()
        {
            string appSettingConfig = WebConfigurationManager.AppSettings["defaultLanguage"];
            string connString = WebConfigurationManager.ConnectionStrings["SportsStore"].ConnectionString;
            return View("DisplaySingle",(object)connString);
        }
    }
}