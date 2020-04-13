using System.Web;
using System.Web.Optimization;

namespace ISO20K
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


			bundles.Add(new ScriptBundle("~/bundles/src").Include(
					  "~/Scripts/src/sigma.core.js",
					  "~/Scripts/src/conrad.js",
					  "~/Scripts/src/utils/sigma.*",
					  "~/Scripts/src/sigma.settings.js",
					  "~/Scripts/src/sigma.core.js",
					  "~/Scripts/src/sigma.core.js",
					  
					  "~/Scripts/src/renderers/sigma.*",
					  "~/Scripts/src/renderers/canvas/sigma.*",
					  "~/Scripts/src/renderers/svg/sigma.*",
					  "~/Scripts/src/renderers/webgl/sigma.*",
					  "~/Scripts/src/misc/sigma.*",
					  "~/Scripts/src/middlewares/sigma.*",
					  "~/Scripts/src/classes/sigma.*",
					  "~/Scripts/src/captors/sigma.*"));

			bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
					  "~/Scripts/plugins/sigma.plugins.neighborhoods/sigma.plugins.neighborhoods.js",
					  "~/Scripts/plugins/sigma.layout.forceAtlas2/supervisor.js",
					  "~/Scripts/plugins/igma.layout.forceAtlas2/worker.js",
					  "~/Scripts/plugins/sigma.parsers.json/sigma.parsers.json.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
			bundles.Add(new ScriptBundle("~/bundles/sigma").Include(
						"~/Scripts/sigma.js"));

					

		}
    }
}
