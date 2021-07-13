using FinalTest.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace FinalTest.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        public SearchResultRenderingModel GetModel(string searchQuery)
        {
            var model = new SearchResultRenderingModel();
            if (string.IsNullOrEmpty(searchQuery) || searchQuery.Equals(" "))
            {
                model.Items = new List<Item>();
                return model;
            }

            model.Items = GetArticles(searchQuery).Items;

            return model;
        }

        private SearchResultRenderingModel GetArticles(string searchQuery = null)
        {
            var myResults = new SearchResultRenderingModel
            {
                Items = new List<Item>()
            };

            var searchIndex = ContentSearchManager.GetIndex("sitecore_master_index");

            using (var searchContext = searchIndex.CreateSearchContext())
            {
                var searchResults = searchContext.GetQueryable<SearchResultItem>().Where(item => item["Title"] == searchQuery).ToList();

                foreach(var element in searchResults)
                {
                    myResults.Items.Add(element.GetItem());
                }

                return myResults;
            }
        }
    }
}