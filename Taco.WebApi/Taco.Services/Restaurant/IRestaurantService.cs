using System.Collections.Generic;
using System.Threading.Tasks;
using Taco.Core.DTO;
using Taco.Core.DTO.Filters;

namespace Taco.Services.Restaurant
{
    public interface IRestaurantService
    {
        public Task<List<RestaurantDto>> GetByFilter(RestaurantFilterDto filter);
    }
}
