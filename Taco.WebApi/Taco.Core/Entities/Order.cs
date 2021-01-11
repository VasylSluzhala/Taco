using System.Collections.Generic;

namespace Taco.Core.Entities
{
    public class Order : BaseEntity<int>
    {
        public double Total { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
