using System.Text;

namespace SympliDevelopment.Api.Models
{
    // Hi there. Inject me for unit testing purposes. Expected result:-

    // Google: 2 (from top 100 listing/s)
    // Bing: 4 (from top 4 listing/s)

    public class SearchEngineResponseFile : ISearchEngineResponse
    {
        public Task<string> GetResponseString(string UrlGetString)
        {
            string fileContent = String.Empty;
            string fileName = String.Empty;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            for (int i = 0; i < 4; i++) // step up 4 levels
            {
                // bad memory management
                baseDirectory = Directory.GetParent(baseDirectory).FullName;
            }

            if (UrlGetString.Contains("google"))
            {
                fileName = baseDirectory + @"\Test\GoogleTopX.txt";
            }
            else if (UrlGetString.Contains("bing"))
            {
                fileName = baseDirectory + @"\Test\BingTopX.txt";
            }

            fileContent = System.IO.File.ReadAllText(fileName, Encoding.UTF8);
            return Task.Run(() => fileContent);
        }
    }
}
