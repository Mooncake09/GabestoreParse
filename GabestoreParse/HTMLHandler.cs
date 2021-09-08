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
                Console.WriteLine(link.TextContent);
            }
            return linkList;
        }
        public static async Task<Game> ExtractGameInfoFromPageAsync(string htmlGamePage)
        {
            //извлекает из html страницы с конкретной игрой информацию о ней
            var document = await Context.OpenAsync(req => req.Content(htmlGamePage));
            //название игры
            var title = document.QuerySelectorAll("h1").Where(title => title.ClassName.Contains("b-card__title")).Single().TextContent;
            title = title.Replace("купить ", "");

            //цена
            var priceString = document.GetElementsByClassName("b-card__price-currentprice").Single().TextContent;
            priceString = priceString.Replace(" ₽", "");
            var price = float.Parse(priceString);
            

            //поля с информацией о жанре, платформе, дате выхода, издателе и разработчике
            var gameInfoFields = document.QuerySelectorAll(".b-card__table").First().QuerySelectorAll(".b-card__table-item"); //список дивов b-card__table-item

            string genre = "";
            string platform = "";
            DateTime realeseDate = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day);
            string publisher = "";
            string developer = "";
            foreach (var gameInfoField in gameInfoFields)
            {
                var infoFieldName = gameInfoField.QuerySelector(".b-card__table-title").TextContent;
                switch (infoFieldName)
                {
                    case "Жанр":
                        var genresList = gameInfoField.QuerySelector(".b-card__table-value").QuerySelectorAll("a"); //для нормализации БД создать отдельную таблицу с жанрами?
                        foreach (var g in genresList)
                        {
                            genre += g.TextContent;
                            if (!g.IsLastChild()) genre += ", "; //в случае если у игры указано несколько жанров, то добавляет разделитель между ними
                        }
                        break;

                    case "Платформа":
                        platform = gameInfoField.QuerySelector(".b-card__table-value").TextContent;
                        break;

                    case "Дата выхода":
                        realeseDate = DateTime.Parse(gameInfoField.QuerySelector(".b-card__table-value").TextContent);
                        break;

                    case "Издатель":
                        publisher = gameInfoField.QuerySelector(".b-card__table-value").QuerySelector("a").TextContent;
                        break;

                    case "Разработчик":
                        developer = gameInfoField.QuerySelector(".b-card__table-value").QuerySelector("a").TextContent;
                        break;
                }
            }

            //Console.WriteLine($"{title} {genre} цена: {priceString}, платформа {platform} дата выхода {realeseDate} Издатель {publisher} разработчик {developer}");

            return new Game(title, price, genre, platform, realeseDate, publisher, developer);
        }
    }
}
