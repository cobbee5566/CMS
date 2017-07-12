using System;
using System.Web.Mvc;
using System.Web.Optimization;

namespace CMS.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            //重新註冊bundle
            RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null) { throw new ArgumentNullException("bundles"); }

            bundles.Add(new ScriptBundle("~/bundles/Admin/jquery").Include(
                        "~/Areas/Admin/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Admin/jqueryval").Include(
                        "~/Areas/Admin/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Admin/modernizr").Include(
                        "~/Areas/Admin/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/Admin/bootstrap").Include(
                      "~/Areas/Admin/Scripts/bootstrap.js",
                      "~/Areas/Admin/Scripts/respond.js",
                      "~/Areas/Admin/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Areas/Admin/Scripts/Custom.js"));

            bundles.Add(new StyleBundle("~/Content/Admin/css").Include(
                      "~/Areas/Admin/Content/bootstrap.css",
                      "~/Areas/Admin/Content/site.css",
                      "~/Areas/Admin/Content/bootstrap-datetimepicker.min.css"));

            //// Bookings Bundles
            //bundles.Add(new ScriptBundle("~/bundles/bookkeeping/booking")
            //    .Include("~/Areas/bookkeeping/Scripts/booking.js"
            //));

            //bundles.Add(new StyleBundle("~/bookkeeping/css/booking")
            //    .Include("~/Areas/bookkeeping/Content/booking.css"));

        }
    }
}