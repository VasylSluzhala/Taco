namespace Taco.Core.Entities
{
    public class BaseEntity
    {
    }

    public class BaseEntity<T> : BaseEntity
    {
        public T Id { get; set; }
    }
}
