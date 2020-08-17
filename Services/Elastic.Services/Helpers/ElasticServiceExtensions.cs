using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Elastic.Models.Foods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elastic.Services.Helpers
{
    public static class ElasticServiceExtensions
    {
        public static void AddElasticFoodSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            var indexExistsResult = client
                .Indices
                .Exists(new IndexExistsRequest(defaultIndex));

            if (!indexExistsResult.Exists)
            {
                CreateIndex(client, defaultIndex);
            }
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client
                .Indices
                .Create(indexName,
                    index => index.Map<Food>(x => x.AutoMap()));
        }
    }
}
