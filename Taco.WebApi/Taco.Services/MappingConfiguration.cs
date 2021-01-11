using AutoMapper;
using Taco.Core.DTO;

namespace Taco.Services
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Core.Entities.MenuItem, MenuItemDto>();

            CreateMap<Core.Entities.Restaurant, RestaurantDto>();

            CreateMap<Core.Entities.Category, CategoryDto>()
                .ForMember(dest => dest.MenuItems, x => x.MapFrom(src => src.MenuItems));
        }
    }
}
