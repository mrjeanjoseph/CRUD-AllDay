﻿using System.Web.Mvc;

namespace Chapter22.TemplatedHelperMethods
{
    public class Chapter22AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter22";
            }
        }

        public override void RegisterArea(AreaRegistrationContext routes) 
        {
            routes.MapRoute(
                "",
                "TemplatedHelperMethods/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "FormRouteTwo",
                "app/forms/{controller}/{action}"
            );
        }
    }
}