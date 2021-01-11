using System.Collections.Generic;

namespace Taco.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public int RestaurantId { get; set; }

        public virtual List<MenuItem> MenuItems { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
