using Final_Test.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Final_Test.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        public SearchResultRenderingModel GetModel(string searchQuery)
        {
            var model = new SearchResultRenderingModel();
            var parentItem = Sitecore.Context.Database.GetItem("/sitecore/content/Home");
            if (string.IsNullOrEmpty(searchQuery) || searchQuery.Equals(" "))
            {
                model.Items = new List<Item>();
                return model;
            }

            model.Items = GetArticles(parentItem, searchQuery);
            return model;
        }

        private List<Item> GetArticles(Item parentItem, string searchQuery = null)
        {
            if (parentItem == null)
            {
                return Array.Empty<Item>().ToList();
            }

            var items = parentItem.Axes.GetDescendants().Where(x => x.TemplateID.Equals(Templates.MiniArticle.TemplateId)).ToList();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                items = items.Where(x => x[Templates.MiniArticle.Title.ToString()].Contains(searchQuery)).ToList();
            }

            return items;
        }
    }
}