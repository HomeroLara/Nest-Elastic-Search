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

        public async Task<bool> SaveFood(Food food)
        {
            var foodExists = await GetFoodById(food.Id);
            var saved = false;

            if(foodExists)
            {
                var updateResponse = await _elasticClient.UpdateAsync<Food>(food, u => u.Doc(food));
                if(updateResponse.ApiCall.Success
                    && updateResponse.ServerError is null
                    && !string.IsNullOrWhiteSpace(updateResponse.Id))
                {
                    saved = true;
                }
            }
            else
            {
                var savedResponse = await _elasticClient.IndexDocumentAsync<Food>(food);
                if (savedResponse.ApiCall.Success
                    && savedResponse.ServerError is null
                    && !string.IsNullOrWhiteSpace(savedResponse.Id))
                {
                    saved = true;
                }
            }

            return saved;

        }
    }
}
