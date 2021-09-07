using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.WriteLine(link.TextContent);
            }
            return linkList;
        }
        public static async Task<List<Game>> ExtractGameInfoFromPage(string htmlGamePage)
        {
            var document = await Context.OpenAsync(req => req.Content(htmlGamePage));
            var game = new Game();
            var title = document.QuerySelectorAll("h1").Where(title => title.ClassName.Contains("b-card__title")).Single();
            title.TextContent = title.TextContent.Replace("купить ", "");
            var priceString = document.GetElementsByClassName("b-card__price-currentprice").Single().TextContent;
            priceString = priceString.Replace(" ₽", "");
            var genre = document.QuerySelectorAll(".b-card__table")[0].ChildNodes.GetElementsByClassName("b-card__table-item")[0].ChildElementCount;
            Console.WriteLine(genre);
            return new List<Game>();
        }
    }
}
