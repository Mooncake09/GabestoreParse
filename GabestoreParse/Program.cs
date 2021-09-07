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
            for (int i = 1; i <= Container.PAGE_NUMBERS; i++)
            {
                var document = await parser.ParseRawDataAsync(i);
                var html = parser.GetHtmlFromJson(document);
                var links = await HTMLHandler.ExtractLinksAsync(html);
                if (links != null) Container.Links.AddRange(links);
                foreach (var link in Container.Links)
                {
                    var game = await parser.ParseGameInfoAsync(link.TextContent);
                }
                Thread.Sleep(2000);
            }

           //await parser.ParseGameDataAsync(@"https://gabestore.ru/game/red-dead-redemption-2");


        }
    }
}
