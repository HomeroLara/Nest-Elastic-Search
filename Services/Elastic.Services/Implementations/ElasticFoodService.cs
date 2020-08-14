using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Services.Interfaces;
using Elastic.Models.Foods;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Nest;

namespace Elastic.Services.Implementations
{
    public class ElasticFoodService : IElasticFoodService
    {
        private readonly IElasticClient _elasticClient;
        public ElasticFoodService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public async Task<bool> GetFoodById(int id)
        {
            var response = await _elasticClient
                .GetAsync<Food>(id, ec => ec.Index("foods"));

            return response.Found;
        }

        public Task SaveFood(Food food)
        {
            return _elasticClient.IndexDocumentAsync<Food>(food);
        }
    }
}
