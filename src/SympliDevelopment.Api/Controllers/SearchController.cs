using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Memory;
using SympliDevelopment.Api.Models;
using System.Text.RegularExpressions;

namespace SympliDevelopment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        ISearchEngineResponse searchEngineResponse;
        IMemoryCache memoryCache;
        public SearchController(ISearchEngineResponse SearchEngineResponse, IMemoryCache MemoryCache)
        {
            this.searchEngineResponse = SearchEngineResponse;
            this.memoryCache = MemoryCache;
        }

        [HttpGet("keywords")]
        public async Task<IActionResult> GetResult([FromQuery] string keywords)
        {
            // please implement this method to return the result correctly.
            // the method receives an input keywords and then return the ranking result

            // url for dev is http://localhost:5180/Search/keywords?keywords=e-settlements

            string result = String.Empty;
            string pageContents;
            string hostToFind = "www.sympli.com.au"; // Amee confirms this can be loaded from within application
            int memoryCacheMinutes = 60; // Can be reduced for unit testing
            Listings listings;

            if (memoryCache.TryGetValue(keywords, out result))
            {
                return Ok(result);
            }

            SearchEngineRepository searchEngineRepo = new SearchEngineRepository(keywords);

            foreach (SearchEngine engine in searchEngineRepo.Engines)
            {
                pageContents = String.Empty;
                pageContents = await searchEngineResponse.GetResponseString(engine.UrlSearchString);

                listings = new Listings(hostToFind);

                foreach (Match match in Regex.Matches(pageContents, engine.HtmlSearchPattern))
                {
                    listings.AddUri(Regex.Replace(match.Value, engine.HtmlSearchPrefix, ""));
                }
                result = result + engine.Name + ": " + listings.GetUriIndexes() + " (from top " + listings.GetListingsCount() + " listing/s)" + Environment.NewLine;
            }

            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(memoryCacheMinutes));
            memoryCache.Set(keywords, result, cacheOptions);

            return Ok(result);
        }
    }
}