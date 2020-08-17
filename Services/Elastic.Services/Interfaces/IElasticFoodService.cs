using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Models.Foods;

namespace Elastic.Services.Interfaces
{
    public interface IElasticFoodService
    {
        Task<bool> SaveFood(Food food);
        Task<bool> GetFoodById(int id);
    }
}
