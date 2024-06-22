using System.Web;
using System.Web.Configuration;

namespace DetectingDeviceCapabilities.Infrastructure
{
    public class KindleCapabilities : HttpCapabilitiesProvider
    {
        public override HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request) {

            HttpCapabilitiesProvider defaults = new HttpCapabilitiesDefaultProvider();

            HttpBrowserCapabilities caps = defaults.GetBrowserCapabilities(request);

            if(request.UserAgent.Contains("Kindle Fire"))
            {
                caps.Capabilities["Browser"] = "Silk";
                caps.Capabilities["IsMobileDevice"] = "true";
                caps.Capabilities["MobileDeviceManufacturer"] = "Amazon";
                caps.Capabilities["MobileDeviceModel"] = "Kindle Fire";

                if(request.UserAgent.Contains("Kindle Fire HD"))                
                    caps.Capabilities["MobileDeviceModel"] = "Kindle Fire HD";
                
            }
            return caps;
        }
    }
}