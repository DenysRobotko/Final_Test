using Final_Test.Models;

namespace Final_Test.Repositories
{
    public interface ISearchResultRepository
    {
        SearchResultRenderingModel GetModel(string searchQuery);
    }
}
