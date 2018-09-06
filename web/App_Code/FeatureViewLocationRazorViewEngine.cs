using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace web
{
    public class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        public FeatureViewLocationRazorViewEngine()
        {
            var featureFolderViewLocationFormats = new[]
            {
                "~/Modules/{1}/{0}.cshtml",
                "~/Modules/Shared/{0}.cshtml",
            };

            base.ViewLocationFormats = featureFolderViewLocationFormats;
            base.MasterLocationFormats = featureFolderViewLocationFormats;
            base.PartialViewLocationFormats = featureFolderViewLocationFormats;
        }
    }
}
