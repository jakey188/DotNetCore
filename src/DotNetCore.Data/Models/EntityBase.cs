using DotNetCore.Data.Models;

namespace DotNetCore.Data
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
    }

    public abstract class EntityBase 
    {
    }
}
