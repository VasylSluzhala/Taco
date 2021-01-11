using System.Collections.Generic;

namespace Taco.Core.Entities
{
    public class MenuItem : BaseEntity<int>
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string CategoryName { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
