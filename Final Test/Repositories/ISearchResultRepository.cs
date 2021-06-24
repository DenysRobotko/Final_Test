using FinalTest.Models;

namespace FinalTest.Repositories
{
    public interface ISearchResultRepository
    {
        SearchResultRenderingModel GetModel(string searchQuery);
    }
}
