using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DestinyTracker.Helpers
{
    public class DestinyHttpHandler
    {
        private static string ApiKey = "7dfc32f355204b9dae3c806b21eaad31";
        private static string ApiBase = "http://www.bungie.net/platform/destiny/1/";

        public static async Task<string> GetData(string type, string command)
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpResponseMessage response;

            httpClient.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
            var url = ApiBase;

            switch (type)
            {
                case "stats":
                    url = url.Substring(0, url.Length - 2) + command;
                    break;
                case "account":
                    url += command;
                    break;
            }

            response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode == false)
            {
                throw new HttpRequestException(((int)response.StatusCode).ToString());
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
