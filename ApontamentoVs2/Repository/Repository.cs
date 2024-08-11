using ApontamentoVs2.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApontamentoVs2.Repository
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly ApplicationDataContext _dataContext;
        protected readonly DbSet<TEntity> _entity;

        public Repository(ApplicationDataContext context)
        {
            _dataContext = context ?? throw new ArgumentNullException(nameof(context));
            _entity = _dataContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _entity.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public virtual List<TEntity> GetAll()
        {
            return _entity.ToList();
        }

        public virtual TEntity GetById(TKey id)
        {
            return _entity.Find(id);
        }

        public virtual void Update(TEntity entity)
        {
            _entity.Update(entity);
        }
        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }
    }
}
