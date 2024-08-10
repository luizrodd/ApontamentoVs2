namespace ApontamentoVs2.Domain
{
    public interface IEntity
    {
    }

    public abstract class Entity<TKey> : IEntity
    {
        public TKey Id { get; protected set; }
    }
}
