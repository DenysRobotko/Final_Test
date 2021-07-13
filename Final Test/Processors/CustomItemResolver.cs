using Sitecore;
using Sitecore.Abstractions;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Sites;
using System;
using System.IO;

namespace FinalTest.Processors
{
    // Sitecore.Pipelines.HttpRequest.SiteResolver

    /// <summary>
    /// Updates the paths in the <see cref="T:Sitecore.Pipelines.HttpRequest.HttpRequestArgs" /> and <see cref="P:Sitecore.Context.Site" />.
    /// Note: this processor does not resolve a site anymore. <see cref="T:Sitecore.Pipelines.PreAuthenticateRequest.SiteResolver" /> resolves a site.
    /// </summary>
    public class CustomItemResolver : HttpRequestProcessor
    {
        /// <summary>
        /// Gets the site context factory.
        /// </summary>
        /// <value>
        /// The site context factory.
        /// </value>
        protected BaseSiteContextFactory SiteContextFactory { get; private set; }

        /// <summary>
        /// Gets a value indicating whether take 'site.config' configuration file into account.
        /// </summary>
        /// <value>
        /// <c>true</c> if enable site configuration file; otherwise, <c>false</c>.
        /// </value>
        protected bool EnableSiteConfigFiles { get; private set; }

        /// <summary>
        /// Gets the site query string key.
        /// </summary>
        /// <value>
        /// The site query string key.
        /// </value>
        protected string SiteQueryStringKey => "sc_site";

        /// <summary>
        /// Runs the processor.
        /// </summary>
        /// <param name="args">The args.</param>
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            SiteContext siteContext = Context.Site ?? ResolveSiteContext(args);
            UpdatePaths(args, siteContext);
            SetSiteToRequestContext(siteContext);
            Log.Info("The custom site resolver process is running", this);
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected string GetFilePath(HttpRequestArgs args, SiteContext context)
        {
            return GetPath(context.PhysicalFolder, args.Url.FilePath, context);
        }

        /// <summary>
        /// Gets the item path.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected string GetItemPath(HttpRequestArgs args, SiteContext context)
        {
            return GetPath(context.StartPath, args.Url.ItemPath, context);
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="path">The path.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected virtual string GetPath(string basePath, string path, SiteContext context)
        {
            string virtualFolder = context.VirtualFolder;
            if (virtualFolder.Length > 0 && virtualFolder != "/")
            {
                string text = StringUtil.EnsurePostfix('/', virtualFolder);
                string text2 = StringUtil.EnsurePostfix('/', path);
                if (text2.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
                {
                    path = StringUtil.Mid(path, text.Length);
                }
            }
            if (basePath.Length > 0 && basePath != "/")
            {
                path = FileUtil.MakePath(basePath, path, '/');
            }
            if (path.Length > 0 && path[0] != '/')
            {
                path = "/" + path;
            }
            return path;
        }

        /// <summary>
        /// Extracts the site configuration file path for requested directory.
        /// <para>Uses file path to <see cref="P:Sitecore.Pipelines.HttpRequest.HttpRequestArgs.Url" /> to get directory that should contain 'site.config' file.</para>
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>File path to the requested directory site configuration;<c>null</c> in case file does not exist.</returns>
        protected virtual string ExtractSiteConfigPathForRequestedDirectory(HttpRequestArgs args)
        {
            string @string = StringUtil.GetString(Path.GetDirectoryName(args.Url.FilePath));
            string part = FileUtil.NormalizeWebPath(@string);
            string text = FileUtil.MakePath(part, "site.config");
            if (!File.Exists(text))
            {
                return null;
            }
            return text;
        }

        /// <summary>
        /// Resolves the site context.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        [Obsolete("This processor is not used for site resolving anymore. Use Sitecore.Pipelines.PreAuthenticateRequest.SiteResolver processor.")]
        protected virtual SiteContext ResolveSiteContext(HttpRequestArgs args)
        {
            string queryString = GetQueryString(SiteQueryStringKey, args);
            SiteContext siteContext;
            if (queryString.Length > 0)
            {
                siteContext = SiteContextFactory.GetSiteContext(queryString);
                Assert.IsNotNull(siteContext, "Site from query string was not found: " + queryString);
                return siteContext;
            }
            if (EnableSiteConfigFiles)
            {
                string text = ExtractSiteConfigPathForRequestedDirectory(args);
                if (!string.IsNullOrEmpty(text))
                {
                    siteContext = SiteContextFactory.GetSiteContextFromFile(text);
                    Assert.IsNotNull(siteContext, "Site from site.config was not found: " + text);
                    return siteContext;
                }
            }
            Uri requestUrl = args.RequestUrl;
            siteContext = SiteContextFactory.GetSiteContext(requestUrl.Host, args.Url.FilePath, requestUrl.Port);
            Assert.IsNotNull(siteContext, "Site from host name and path was not found. Host: {0}, path: {1}", requestUrl.Host, args.Url.FilePath);
            return siteContext;
        }

        /// <summary>
        /// Sets the resolved site to request context.
        /// </summary>
        /// <param name="site">The site to be used in current request.</param>
        protected virtual void SetSiteToRequestContext(SiteContext site)
        {
            Context.Site = site;
        }

        /// <summary>
        /// Updates the paths.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="site">The site.</param>
        protected virtual void UpdatePaths(HttpRequestArgs args, SiteContext site)
        {
            if (!string.IsNullOrEmpty(args.HttpContext.Request.PathInfo))
            {
                string filePath = args.Url.FilePath;
                int num = filePath.LastIndexOf('.');
                int num2 = filePath.LastIndexOf('/');
                args.Url.ItemPath = ((num < 0) ? filePath : ((num >= num2) ? filePath.Substring(0, num) : filePath));
            }
            args.StartPath = site.StartPath;
            args.Url.ItemPath = GetItemPath(args, site);
            site.Request.ItemPath = args.Url.ItemPath;
            args.Url.FilePath = GetFilePath(args, site);
            site.Request.FilePath = args.Url.FilePath;
        }
    }
}