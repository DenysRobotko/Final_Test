using Final_Test.Controllers;
using Final_Test.Models;
using Final_Test.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Final_Test
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(ISearchResultRepository), typeof(SearchResultRepository));
        }
    }
}