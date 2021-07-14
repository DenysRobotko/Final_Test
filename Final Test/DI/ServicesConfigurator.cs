using FinalTest.Controllers;
using FinalTest.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace FinalTest.DI
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(ISearchResultRepository), typeof(SearchResultRepository));
            serviceCollection.AddTransient(typeof(SearchResultController), typeof(SearchResultController));
        }
    }
}