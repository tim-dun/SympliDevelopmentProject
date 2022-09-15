using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace SympliDevelopment.Api.Models
{
    public class SearchEngine
    {
        private static int searchMax;
        private static int searchStart;
        private static string htmlSearchSuffix;
        public string Name { get; set; }
        public string HtmlSearchPrefix { get; set; }
        public string HtmlSearchPattern { get; set; }
        public string UrlSearchString { get; set; }

        public SearchEngine(string Name, string Host, string SearchTerm, string SearchParam, string HtmlSearchPrefix)
        {
            searchMax = 100;
            searchStart = 1;
            htmlSearchSuffix = "([^\\&][^\\\"]{1,2048})";

            this.Name = Name;
            SearchTerm = SearchTerm.Replace(" ", "+");

            UrlSearchString = $"http://{Host}/search?q={SearchTerm}&{SearchParam}={searchMax}";
            this.HtmlSearchPrefix = HtmlSearchPrefix;
            HtmlSearchPattern = HtmlSearchPrefix + htmlSearchSuffix;
        }
    }
}
