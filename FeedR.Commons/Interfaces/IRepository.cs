using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FeedR.Commons.Model;

namespace FeedR.Commons.Interfaces
{
    /// <summary>
    /// Simple Generic repository. Not using UnitOfWork here or transactions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
        where T : DtoObject
    {
        /// <summary>
        /// Filter the store for items.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets by id.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetById(Guid id);

        /// <summary>
        /// Store object in the data store.
        /// </summary>
        /// <param name="obj"></param>
        void Insert(T obj);

        /// <summary>
        /// Works as Attach.
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);

        /// <summary>
        /// Instantly deletes an object.
        /// </summary>
        /// <param name="obj"></param>
        void Delete(T obj);
    }
}
