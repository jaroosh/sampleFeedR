using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FeedR.Commons.Interfaces;
using FeedR.Commons.Model;

namespace FeedR.Services.Repositories
{
    /// <summary>
    /// Implementation of the repository using in - memory storage;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> 
        where T : DtoObject
    {
        #region Members.

        // Keeping a dictionary will speed up
        private readonly IDictionary<Guid, T> _entities = new Dictionary<Guid, T>();
        // Here, probably lock would be ok, but...
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        #endregion

        /// <summary>
        /// Queries for an item.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            _lock.EnterReadLock();
            try
            {
                var items = _entities.Values.AsQueryable().Where(predicate);
                return items;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public T GetById(Guid id)
        {
            // We could stack IQueryable calls here instead of doing ienumerable and then single
            // but I dont like the idea of returning iQUeryable from the repository especially
            // if its directly taken from EF db.
            return Query(e => e.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Inserts new obj.
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(T obj)
        {
            _lock.EnterWriteLock();
            try
            {
                _entities.Add(obj.Id, obj);
            }
            finally
            {
                _lock.ExitWriteLock();
            };
        }

        /// <summary>
        /// Updates new obj.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(T obj)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                T keptObj;
                if (_entities.TryGetValue(obj.Id, out keptObj))               
                    if (keptObj.Equals(obj)) // As this works just like attach we want to use T's equals if defined or identity equality.
                        return;

                // This will upgrade the lock if necessary.
                // IN case of dictionary ADd will either add or update.
                Insert(obj); 
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Deletes an object.
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(T obj)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_entities.ContainsKey(obj.Id))
                    _entities.Remove(obj.Id);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
