using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace unitWork.Models
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// 获取所有的实体对象
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();

        /// <summary>
        /// 通过Lamda表达式过滤符合条件的实体对象
        /// </summary>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取实体总数
        /// </summary>
        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 通过键值查找并返回单个实体
        /// </summary>
        T Find(params object[] keys);

        /// <summary>
        /// 通过表达式查找复合条件的单个实体
        /// </summary>
        /// <param name="predicate"></param>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 创建实体对象
        /// </summary>
        T Create(T t);

        /// <summary>
        /// 删除实体对象
        /// </summary>
        void Delete(T t);

        /// <summary>
        /// 删除符合条件的多个实体对象
        /// </summary>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        T Update(T t);

        /// <summary>
        /// Clear all data items.
        /// </summary>
        /// <returns>Total clear item count</returns>
        void Clear();

        /// <summary>
        /// Save all changes.
        /// </summary>
        /// <returns></returns>
        int Submit();

        //#region IDisposable Support
        //private bool disposedValue = false; // 要检测冗余调用

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: 释放托管状态(托管对象)。
        //        }

        //        // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
        //        // TODO: 将大型字段设置为 null。

        //        disposedValue = true;
        //    }
        //}

        //// TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        //// ~IRepository() {
        ////   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        ////   Dispose(false);
        //// }

        //// 添加此代码以正确实现可处置模式。
        //public void Dispose()
        //{
        //    // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //    Dispose(true);
        //    // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
        //    // GC.SuppressFinalize(this);
        //}
        //#endregion
    }
}