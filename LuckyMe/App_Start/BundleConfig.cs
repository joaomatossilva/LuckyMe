using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LuckyMe
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundle/globalize").Include(
                        "~/Scripts/cldr.js",
                        "~/Scripts/globalize.js",
                        "~/Scripts/globalize/number.js",
                        "~/Scripts/globalize/currency.js"));

            var jqueryVal = new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*");
            jqueryVal.Orderer = new JqueryvalOrderer();
            bundles.Add(jqueryVal);


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }

        public class JqueryvalOrderer : IBundleOrderer 
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files.Select(f => new
                                  {
                                      file = f,
                                      height = f.VirtualFile.Name.Contains("globalize") ? 10 : 0
                                  }).OrderBy(f => f.height).ThenBy(f => f.file.VirtualFile.Name)
                    .Select(f => f.file);
            }
        }
    }
}
