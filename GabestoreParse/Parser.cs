using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace GabestoreParse
{
    class Parser
    {
        private HttpClient Client;
        public Parser()
        {
            Client = new HttpClient();
        }
        public async Task<string> ParseGameCatalogHtmlAsync(int pageNumber) //со страницы возвращается строка, содержая html в Json 
        {
            var url = "https://gabestore.ru/search/next?series=&ProductFilter%5Bavailable%5D=0&ProductFilter%5Bavailable%5D=1&ProductFilter%5BsortName%5D=views&page=" + pageNumber.ToString();
            try 
            {
                var response = await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var document = await response.Content.ReadAsStringAsync();
                return document;
            }
            catch(Exception e)
            {
                return e.Message;  
            }

        }
        public string GetHtmlFromJson(string jsonString)
        {
            var json = JsonDocument.Parse(jsonString);
            if (json.RootElement.TryGetProperty("html", out var html))
            {
                return html.GetString();
            }
            else
            {
                return "json incorrect";
            }
        }

        public async Task<Game> ParseGamePageAsync(string url)
        {
            try
            {
                var response = await Client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var document = await response.Content.ReadAsStringAsync();
                return await HTMLHandler.ExtractGameInfoFromPageAsync(document);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Game> ParseGamePageAsync(AngleSharp.Dom.IElement url)
        {
            try
            {
                var response = await Client.GetAsync(url.TextContent);
                response.EnsureSuccessStatusCode();
                var document = await response.Content.ReadAsStringAsync();
                return await HTMLHandler.ExtractGameInfoFromPageAsync(document);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
