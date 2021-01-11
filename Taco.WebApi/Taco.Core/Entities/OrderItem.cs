namespace Taco.Core.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        public int MenuItemId { get; set; }

        public virtual Order Order { get; set; }

        public virtual MenuItem MenuItem { get; set; }
    }
}
