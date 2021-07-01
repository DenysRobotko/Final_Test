using Sitecore.IO;
using Sitecore.Pipelines.HttpRequest;

namespace FinalTest
{
    public class NotFoundHandler
    {
        public void Process(HttpRequestArgs args)
        {
            if (Sitecore.Context.Site == null)
                return;

            if (Sitecore.Context.Item != null)
                return;

            var notFoundPath = Sitecore.Context.Site.Properties["notFoundItem"];

            if (string.IsNullOrEmpty(notFoundPath))
                return;

            var sitePath = Sitecore.Context.Site.StartPath;
            var path = FileUtil.MakePath(sitePath, notFoundPath);
            var notFoundItem = Sitecore.Context.Database.GetItem(path);

            if (notFoundItem != null)
            {
                Sitecore.Context.Item = notFoundItem;
                args.HttpContext.Response.StatusCode = 404;
            }
        }
    }
}