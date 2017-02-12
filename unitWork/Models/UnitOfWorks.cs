using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace unitWork.Models
{
    public class UnitOfWorks<TDBContext> : IUnitOfWorks
        where TDBContext : DbContext
    {
        protected TDBContext dbContext;

        public UnitOfWorks(TDBContext context)
        {
             dbContext=context;
        }

        //构造通用的Repository
        private IDictionary<Type, object> repositoryTable = new Dictionary<Type, object>();

        //注册其它的Repository
        public void Register<T>(IRepository<T> repository) where T : class
        {
            var key = typeof(T);
            if (repositoryTable.ContainsKey(key))
                repositoryTable[key] = repository;
        }


        private IRepository<T> GetRepository<T>()
            where T : class
        {
            IRepository<T> repository = null;
            var key = typeof(T);

            if (repositoryTable.ContainsKey(key))
                repository = (IRepository<T>)repositoryTable[key];
            else
            {
                repository = GenericRepository<T>();
                repositoryTable.Add(key, repository);
            }

            return repository;
        }

        protected virtual IRepository<T> GenericRepository<T>() where T : class
        {
            return new EntityRepository<TDBContext, T>(dbContext);
        }

        public T Find<T>(object id) where T : class
        {
            return GetRepository<T>().Find(id);
        }

        public T Add<T>(T t) where T : class
        {
            return GetRepository<T>().Create(t);
        }

        public IEnumerable<T> Add<T>(IEnumerable<T> items) where T : class
        {
            var list = new List<T>();
            foreach (var item in items)
                list.Add(Add(item));
            return list;
        }

        public void Update<T>(T t) where T : class
        {
            GetRepository<T>().Update(t);
        }

        public void Delete<T>(T t) where T : class
        {
            GetRepository<T>().Delete(t);
        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            GetRepository<T>().Delete(predicate);
        }

        public int SaveChanges(bool validateOnSave = true)
        {
            if (!validateOnSave)
                dbContext.Configuration.ValidateOnSaveEnabled = false;

            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate)
            where T : class
        {
            return GetRepository<T>().Filter(predicate);
        }

        public T Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetRepository<T>().Find(predicate);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return GetRepository<T>().All();
        }

        public int Count<T>() where T : class
        {
            return GetRepository<T>().Count();
        }

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetRepository<T>().Count(predicate);
        }

        //public void Config(IConfiguration settings)
        //{
        //    var configuration = settings as DbConfiguration;
        //    if (configuration != null)
        //    {
        //        this.dbContext.Configuration.AutoDetectChangesEnabled = configuration.AutoDetectChangesEnabled;
        //        this.dbContext.Configuration.LazyLoadingEnabled = configuration.LazyLoadingEnabled;
        //        this.dbContext.Configuration.ProxyCreationEnabled = configuration.ProxyCreationEnabled;
        //        this.dbContext.Configuration.ValidateOnSaveEnabled = configuration.ValidateOnSaveEnabled;
        //    }
        //}

        public void Clear<T>() where T : class
        {
            GetRepository<T>().Clear();
        }

        int IUnitOfWorks.SaveChanges()
        {
            return this.SaveChanges();
        }
    }
}