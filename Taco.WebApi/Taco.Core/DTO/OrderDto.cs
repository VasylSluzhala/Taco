using System.Collections.Generic;

namespace Taco.Core.DTO
{
    public class OrderDto
    {
        public double Total { get; set; }

        public List<MenuItemDto> MenuItems { get; set; }
    }
}
