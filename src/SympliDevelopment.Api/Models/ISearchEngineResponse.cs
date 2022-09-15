namespace SympliDevelopment.Api.Models
{
    public interface ISearchEngineResponse
    {
        Task<string> GetResponseString(string UrlGetString);
    }
}
