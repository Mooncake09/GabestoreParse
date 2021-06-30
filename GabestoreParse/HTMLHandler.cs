using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html;
using AngleSharp.Dom;

namespace GabestoreParse
{
    static class HTMLHandler
    {
        static IConfiguration Config = Configuration.Default.WithDefaultLoader();
        static IBrowsingContext Context = BrowsingContext.New(Config);
        public static async Task<IEnumerable<IElement>> ExtractLinksAsync(string html)
        {
            var document = await Context.OpenAsync(req => req.Content(html));
            var linkList = document.QuerySelectorAll("a").Where(link => link.ClassName.Contains("shop-item__name"));
            foreach (var link in linkList)
            {
                link.TextContent = @"https://gabestore.ru" + link.GetAttribute("href");
            }
            return linkList;
        }
    }
}
