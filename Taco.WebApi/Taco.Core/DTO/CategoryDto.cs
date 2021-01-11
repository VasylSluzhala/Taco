using System.Collections.Generic;

namespace Taco.Core.DTO
{
    public class CategoryDto
    {
        public string Name { get; set; }

        public List<MenuItemDto> MenuItems { get; set; }
    }
}
