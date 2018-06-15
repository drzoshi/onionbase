using Exo_Base.Core.DomainModels;
using Exo_Base.Data.EFConfigs;
using Exo_Base.Data.EntityConfig;
using Exo_Base.Data.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Exo_Base.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>
        ,IEntitiesContext
    {
        public static readonly object Lock = new object();
        private static bool _dbInitialized;
        //public ApplicationDbContext() : base("DefaultConnection")
        //{

        //}

        public ApplicationDbContext(string nameOrConnetionString):base(nameOrConnetionString)
        {
            //Database.Log = 
            if (_dbInitialized)
                return;

            lock(Lock)
            {
                if (!_dbInitialized)
                {
                    Database.SetInitializer(new ApplicationDbInitializer());
                    _dbInitialized = true;
                }
            }
        }

        public DbSet<Entities.NavigationTypeEntity> NavigationTypes { get; set; }
        public DbSet<Entities.NavigationMenuEntity> NavigationMenus { get; set; }
        public DbSet<Entities.RoleNavigationMenuEntity> RoleNavigationMenus { get; set; }

        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //EFConfig.ConfigureEF(modelBuilder);
            IdentityEntityConfig.ConfigureIdentity(modelBuilder);
            modelBuilder.Configurations.Add(new NavigationTypeEntityConfig());
            modelBuilder.Configurations.Add(new NavigationMenuEntityConfig());
            modelBuilder.Configurations.Add(new RoleNavigationEntityConfig());
        }

        #region EntityContext
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }
        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }
        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }
        public void BeginTransaction()
        {
            _objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (_objectContext.Connection.State == System.Data.ConnectionState.Open)
            {
                return;
            }
            _objectContext.Connection.Open();
            _transaction = _objectContext.Connection.BeginTransaction();
        }
        public int Commit()
        {
            var saveChanges = SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }
        public Task<int> CommitAsync()
        {
            var saveChangesAsync = SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }
        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_objectContext != null && _objectContext.Connection.State == System.Data.ConnectionState.Open)
                {
                    _objectContext.Connection.Close();
                }
                if (_objectContext != null)
                {
                    _objectContext.Dispose();
                }
                if (_transaction != null)
                {
                    _transaction.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
