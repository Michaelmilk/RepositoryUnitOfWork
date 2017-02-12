using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace unitWork.Models
{
    public interface IUnitOfWorks
    {
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;

        IQueryable<T> All<T>() where T : class;

        int Count<T>() where T : class;

        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Find<T>(object id) where T : class;

        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Add<T>(T t) where T : class;

        IEnumerable<T> Add<T>(IEnumerable<T> items) where T : class;

        void Update<T>(T t) where T : class;

        void Delete<T>(T t) where T : class;

        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Clear<T>() where T : class;

        int SaveChanges();

        //void Config(IConfiguration settings);
    }
}
