using System;
using System.Net;
using System.Text;

namespace SympliDevelopment.Api.Models
{
    public class SearchEngineResponseWeb : ISearchEngineResponse
    {
        public async Task<string> GetResponseString(string UrlGetString)
        {
            string responseString = string.Empty;

            HttpClient client = new HttpClient();
            byte[] responseBytes = await client.GetByteArrayAsync(UrlGetString);
            responseString = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length - 1);

            return responseString;
        }
    }
}
