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
        public static async Task<List<Game>> ExtractGameInfoFromPageAsync(string htmlGamePage)
        {
            //извлекает из html страницы с конкретной игрой информацию о ней
            var document = await Context.OpenAsync(req => req.Content(htmlGamePage));
            var game = new Game();
            //название игры
            var title = document.QuerySelectorAll("h1").Where(title => title.ClassName.Contains("b-card__title")).Single();
            title.TextContent = title.TextContent.Replace("купить ", "");

            //цена
            var priceString = document.GetElementsByClassName("b-card__price-currentprice").Single().TextContent;
            priceString = priceString.Replace(" ₽", "");

            //поля с информацией о жанре, платформе, дате выхода, издателе и разработчике
            var gameInfoFields = document.QuerySelectorAll(".b-card__table").First().QuerySelectorAll(".b-card__table-item"); //список дивов b-card__table-item

            //жанр
            var genresList = gameInfoFields[0]//.QuerySelector(".b-card__table-item")
                .QuerySelector(".b-card__table-value").QuerySelectorAll("a");
            string genre = "";
            foreach (var g in genresList)
            {
                genre += g.TextContent;
                if (!g.IsLastChild()) genre += ", "; //в случае если у игры указано несколько жанров, то добавляет разделитель между ними
            }

            //Платформа
            var platform = gameInfoFields[1].QuerySelector(".b-card__table-value").TextContent;

            //дата выхода
            var realeseDate = DateTime.Parse(gameInfoFields[2].QuerySelector(".b-card__table-value").TextContent).ToString("d");
            Console.WriteLine($"{title.TextContent} {genre} цена: {priceString}, платформа {platform} дата выхода {realeseDate}");

            return new List<Game>();
        }
    }
}
