namespace SympliDevelopment.Api.Models
{
    public class SearchEngineRepository
    {
        public List<SearchEngine> Engines { get; set; }
        public SearchEngineRepository(string SearchTerm)
        {
            Engines = new List<SearchEngine>()
            {
                new SearchEngine("Google", "www.google.com.au", SearchTerm, "num", "(<div class=\\\"egMi0 kCrYT\\\"><a href=\\\"\\/url\\?q=)"),
                new SearchEngine("Bing", "www.bing.com.au", SearchTerm, "count", "<div class=\\\"b_algo\\\"><p><a href=\\\"")
            };
        }
    }
}
