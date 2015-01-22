using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SpaUserList
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/toaster.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/userDataService.js",
                "~/Scripts/app/userCtrl.js",
                "~/Scripts/app/favoriteFilter.js"));

            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/toaster.css"));
        }
    }
}