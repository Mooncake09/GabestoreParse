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
        const int PAGE_NUMBERS = 118;
        static async Task Main(string[] args)
        {
            var container = new Container();
            var parser = new Parser();
            for (int i = 1; i <= PAGE_NUMBERS; i++)
            {
                Thread.Sleep(2000);
                var document = await parser.ParseRawDataAsync(i);
                var html = parser.GetHtml(document);
                container.Links.AddRange(await HTMLHandler.ExtractLinksAsync(html));
            }
            foreach (var link in container.Links)
            {
                Console.WriteLine(link.TextContent);
            }

        }
    }
}
