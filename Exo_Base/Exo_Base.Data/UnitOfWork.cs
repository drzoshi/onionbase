using Exo_Base.Core.Data;
using Exo_Base.Core.DomainModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exo_Base.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IEntitiesContext _context;
        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(IEntitiesContext context)
        {
            _context = context;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }
            var repositoryType = typeof(EntityRepository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
            return (IRepository<TEntity>)_repositories[type];
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }
        public int Commit()
        {
            return _context.Commit();
        }
        public Task<int> CommitAsync()
        {
            return _context.CommitAsync();
        }
        public void Rollback()
        {
            _context.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if(!_disposed && disposing)
            {
                _context.Dispose();
                foreach(IDisposable repository in _repositories.Values)
                {
                    repository.Dispose();
                }
            }
            _disposed = true;
        }

    }
}
