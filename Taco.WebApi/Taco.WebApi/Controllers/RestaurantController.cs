using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taco.Core.DTO.Filters;
using Taco.Services.Restaurant;

namespace Taco.WebApi.Controllers
{
    /// <summary>
    /// Controller for restaurant processing.
    /// </summary>
    public class RestaurantController : BaseController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Get list of restaurant by filter.
        /// </summary>
        /// <param name="filter">Restaurant filter.</param>
        /// <returns>Returns List&lt;RestaurantDto&gt;</returns>
        [HttpPost]
        public async Task<ActionResult> GetByFilter([FromBody] RestaurantFilterDto filter)
        {
            var result = await _restaurantService.GetByFilter(filter);
            return Ok(result);
        }
    }
}
