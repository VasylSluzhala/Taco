using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taco.Core.DTO;
using Taco.Core.DTO.Filters;
using Taco.DAL;
using Taco.DAL.DataProvider;

namespace Taco.Services.Restaurant
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMapper _mapper;

        public RestaurantService(IDataProvider dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        public async Task<List<RestaurantDto>> GetByFilter(RestaurantFilterDto filter)
        {
            var restaurantList = await _dataProvider.GetRestaurantList();

            if (!string.IsNullOrEmpty(filter.Location))
            {
                restaurantList = restaurantList.Where(x => x.Suburb.Contains(filter.Location) || x.City.Contains(filter.Location)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                restaurantList = restaurantList.Where(r => r.Categories.Any(c => c.Name.Contains(filter.Keyword)) || r.Categories.Any(c => c.MenuItems.Any(m => m.Name.Contains(filter.Keyword)))).ToList();

                foreach (var restaurant in restaurantList)
                {
                    restaurant.Categories = restaurant.Categories.Where(x => x.Name.Contains(filter.Keyword) || x.MenuItems.Any(m => m.Name.Contains(filter.Keyword))).ToList();

                    foreach (var category in restaurant.Categories)
                    {
                        if (!category.Name.Contains(filter.Keyword))
                        {
                            category.MenuItems = category.MenuItems.Where(x => x.Name.Contains(filter.Keyword)).ToList();
                        }
                    }
                }
            }

            return _mapper.Map<List<RestaurantDto>>(restaurantList);
        }
    }
}
