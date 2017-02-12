using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace unitWork.Models
{
    public class EntityRepository<TContext, TObject> : IRepository<TObject>
        where TContext : DbContext
        where TObject : class
    {
        protected TContext context;
        protected DbSet<TObject> dbSet;
        protected bool IsOwnContext = true;

        public EntityRepository(TContext dbContext)
        {
            context = dbContext;
            dbSet = context.Set<TObject>();
        }


        /// <summary>
        /// Gets the data context object.
        /// </summary>
        protected virtual TContext Context { get { return context; } }

        /// <summary>
        /// Gets the current DbSet object.
        /// </summary>
        protected virtual DbSet<TObject> DbSet { get { return dbSet; } }

        /// <summary>
        /// Dispose the class.
        /// </summary>
        public void Dispose()
        {
            if ((IsOwnContext) && (Context != null))
                Context.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get all objects.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Gets objects by specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate object.</param>
        /// <returns>return an object collection result.</returns>
        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TObject>();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            if (IsOwnContext)
                Context.SaveChanges();
            return newEntry;
        }

        public virtual void Delete(TObject TObject)
        {
            var entry = Context.Entry(TObject);
            DbSet.Remove(TObject);
            if (IsOwnContext)
                Context.SaveChanges();
        }

        public virtual TObject Update(TObject TObject)
        {
            var entry = Context.Entry(TObject);
            DbSet.Attach(TObject);
            entry.State = EntityState.Modified;
            if (IsOwnContext)
                Context.SaveChanges();
            return TObject;
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = DbSet.Where(predicate).ToList();
            foreach (var obj in objects)
                DbSet.Remove(obj);

            if (IsOwnContext)
                return Context.SaveChanges();
            return objects.Count();
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual int Count(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public int Submit()
        {
            return Context.SaveChanges();
        }

        public virtual void Clear()
        {

        }
    }
}