using Sitecore.Abstractions;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;

namespace FinalTest.Processors
{
    public class CustomSiteResolver : SiteResolver
    {
        [System.Obsolete("Please use constructor with parameters.")]
        public CustomSiteResolver() { }

        public CustomSiteResolver(BaseSiteContextFactory siteContextFactory) : base(siteContextFactory) { }

        public override void Process(HttpRequestArgs args)
        {
            Log.Info("The custom site resolver process is running", this);
            base.Process(args);
        }

    }
}