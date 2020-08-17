using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Elastic.Models.Foods;
using Elastic.Services.Interfaces;
using Elastic.Services.Implementations;
using Elastic.Services.Helpers;

namespace Elastic.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FoodController : ControllerBase
    {
        private readonly IElasticFoodService _elasticFoodService;

        public FoodController(IElasticFoodService elasticFoodService)
        {
            _elasticFoodService = elasticFoodService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(int id)
        {
            var found = await _elasticFoodService.GetFoodById(id);
            return Ok(found);
        }

        [HttpPut()]
        public async Task<IActionResult> SaveFood(Food food)
        {
            var saved = await _elasticFoodService.SaveFood(food);
            return Ok(saved);
        }
    }
}
