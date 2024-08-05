using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _Repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _Repositories = new Hashtable();
        }
        public Task<int> CompleteAsync()
        => _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if(!_Repositories.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(_dbContext);
                _Repositories.Add(type, Repository);
            }
            return (IGenericRepository < TEntity >) _Repositories[type];
        }
    }
}
