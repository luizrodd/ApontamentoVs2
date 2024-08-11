namespace ApontamentoVs2.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity id);
        int SaveChanges();
    }
}
