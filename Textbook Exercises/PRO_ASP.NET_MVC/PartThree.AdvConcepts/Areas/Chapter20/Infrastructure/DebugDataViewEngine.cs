﻿using System;
using System.Web.Mvc;

namespace Chapter20.RazorPagesAndViewEngine.Infrastructure
{
    public class DebugDataViewEngine : IViewEngine
    {
        public ViewEngineResult FindPartialView(
            ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return new ViewEngineResult(new string[] { "No View (Debug Data View Engine)" });
        }

        public ViewEngineResult FindView(
            ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (viewName == "DebugData")
                return new ViewEngineResult(new DebugDataView(), this);
            else
                return new ViewEngineResult(new string[] { "No View (Debug Data View Engine)" });
        }

        public void ReleaseView(
            ControllerContext controllerContext, IView view)
        {
            // Do nothing here for now.
        }
    }
}