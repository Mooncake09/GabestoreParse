using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AngleSharp.Dom;
namespace GabestoreParse
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            var parser = new Parser();
            for (int i = 1; i <= 1; i++)
            {
                var document = await parser.ParseGameCatalogHtmlAsync(i);
                var html = parser.GetHtmlFromJson(document);
                var links = await HTMLHandler.ExtractLinksAsync(html);
                if (links != null) Container.Links.AddRange(links);
                Thread.Sleep(2000);
            }

            foreach (var link in Container.Links)
            {
                Container.Games.Add(await parser.ParseGamePageAsync(link));
                Console.WriteLine("Игра добавлен в контейнер");
            }
            
            foreach (var game in Container.Games.ToArray())
            {
                Console.WriteLine($"{game.Title} цена: {game.Price} дата релиза: {game.ReleaseDate}");
            }
            Console.WriteLine($"В каталоге {Container.Games.Count} игр");

        }
    }
}
