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
                filter.Location = filter.Location.ToLower();
                restaurantList = restaurantList.Where(x => x.Suburb.ToLower().Contains(filter.Location) || x.City.ToLower().Contains(filter.Location)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                filter.Keyword = filter.Keyword.ToLower();
                restaurantList = restaurantList.Where(r => r.Categories.Any(c => c.Name.ToLower().Contains(filter.Keyword)) || r.Categories.Any(c => c.MenuItems.Any(m => m.Name.ToLower().Contains(filter.Keyword)))).ToList();

                foreach (var restaurant in restaurantList)
                {
                    restaurant.Categories = restaurant.Categories.Where(x => x.Name.ToLower().Contains(filter.Keyword) || x.MenuItems.Any(m => m.Name.ToLower().Contains(filter.Keyword))).ToList();

                    foreach (var category in restaurant.Categories)
                    {
                        if (!category.Name.ToLower().Contains(filter.Keyword))
                        {
                            category.MenuItems = category.MenuItems.Where(x => x.Name.ToLower().Contains(filter.Keyword)).ToList();
                        }
                    }
                }
            }

            restaurantList = restaurantList.OrderByDescending(x => x.Categories.SelectMany(x => x.MenuItems).Count()).ThenByDescending(x => x.Rank).ToList();

            return _mapper.Map<List<RestaurantDto>>(restaurantList);
        }
    }
}
