using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

using daytot.core.contracts;
using daytot.core.caching;
using daytot.dal;

namespace daytot.bll.repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T: class
    {
        protected DbContext _dbContext;
        protected DbSet<T> _dbSet;
        internal IUow _uow;
        protected string _tableName { get; private set; }

        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext is null");
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _tableName = typeof(T).Name;

            _uow = StructureMap.ObjectFactory.GetInstance<IUow>();
        }

        #region IBaseRepository

        public virtual T Get(params object[] ids)
        {
            var e = _dbSet.Find(ids);
            _dbContext.Entry(e).State = EntityState.Detached;

            return e;
        }

        /// <summary>
        /// Lấy tất cả Entity trong hệ thống
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        /// <summary>
        /// Bổ sung một Entity vào trong hệ thống
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(entity);
            }
        }

        /// <summary>
        /// Thực hiện trước khi update
        /// </summary>
        public virtual void PreUpdate(T entity)
        {
        }

        /// <summary>
        /// Cập nhật thông tin một Entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            PreUpdate(entity);

            DbEntityEntry<T> dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                var keys = _dbContext.GetEntityKey<T>(entity);
                T attached = _dbSet.Find(keys);
                if (attached == null)
                {
                    _dbSet.Attach(entity);
                    dbEntityEntry.State = EntityState.Modified;
                }
                else
                {
                    _dbContext.Entry(attached).CurrentValues.SetValues(entity);
                }
            }
        }

        /// <summary>
        /// Xóa nhiều entity
        /// </summary>
        /// <param name="lstEntity"></param>
        public void Deletes(List<T> lstEntity)
        {
            foreach (var item in lstEntity)
            {
                Delete(item);
            }
        }

        /// <summary>
        /// Xóa một Entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Xóa một Entity theo mã
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        #endregion


        #region Cache

        #region GetCacheKey

        public string GetCacheKey(string key)
        {
            return _tableName + "_" + key;
        }

        public string GetCacheKey(int key)
        {
            return _tableName + "_" + key.ToString();
        }

        public string GetCacheKey(Guid key)
        {
            return _tableName + "_" + key.ToString();
        }

        public string GetCacheKey(string prefix, int key)
        {
            return _tableName + "_" + prefix + "_" + key.ToString();
        }

        public string GetCacheKey(params object[] keys)
        {
            string key = _tableName;
            for (int i = 0; i < keys.Length; i++)
            {
                key += "_" + keys[i].ToString();
            }
            return key;
        }

        #endregion


        /// <summary>
        /// Cập nhật Cache khi có sự thay đổi entity CRUD
        /// </summary>
        /// <param name="entity"></param>
        public virtual void UpdateCache(T entity){

        }

        public virtual void UpdateCache(T entity, bool delete)
        {
            // do nothing
        }

        /// <summary>
        /// Lấy Object đã lưu trong Cache theo key
        /// </summary>
        /// <param name="key">Khóa lưu cache</param>
        /// <returns></returns>
        public object GetCacheValue(string key)
        {
            return CacheManager.CacheClient.GetValue(key);
        }

        /// <summary>
        /// Lấy Object đã lưu trong Cache theo key, nếu không tồn tại thì trả về giá trị mặc định đưa vào.
        /// </summary>
        /// <param name="key">Khóa lưu cache</param>
        /// <param name="def">Đối tượng dạng string mặc định trà về nếu không tồn tại [key] trong cache</param>
        /// <returns></returns>
        public string GetCacheValue(string key, string def)
        {
            return CacheManager.CacheClient.GetValue(key, def);
        }

        /// <summary>
        /// Lấy Object đã lưu trong Cache theo key, nếu không tồn tại thì trả về giá trị mặc định đưa vào.
        /// </summary>
        /// <param name="key">Khóa lưu cache</param>
        /// <param name="def">Đối tượng dạng integer mặc định trà về nếu không tồn tại [key] trong cache</param>
        /// <returns></returns>
        public int GetCacheValue(string key, int def)
        {
            return CacheManager.CacheClient.GetValue(key, def);
        }

        /// <summary>
        /// Lấy Object đã lưu trong Cache theo key, nếu không tồn tại thì trả về giá trị mặc định đưa vào.
        /// </summary>
        /// <param name="key">Khóa lưu cache</param>
        /// <param name="def">Đối tượng dạng long mặc định trà về nếu không tồn tại [key] trong cache</param>
        /// <returns></returns>
        public long GetCacheValue(string key, long def)
        {
            return CacheManager.CacheClient.GetValue(key, def);
        }

        /// <summary>
        /// Lấy Object kiểu T đã lưu trong Cache theo key
        /// </summary>
        /// <typeparam name="T">Là một kiểu đối tượng đưa vào.</typeparam>
        /// <param name="key">Khóa lưu cache</param>
        /// <returns></returns>
        public TEntity GetCacheValue<TEntity>(string key)
        {
            return (TEntity)CacheManager.CacheClient.GetValue(key);
        }


        /// <summary>
        /// Lấy một danh sách đã cache trên CacheServer
        /// </summary>
        /// <typeparam name="T">Loại đối tượng trong danh sách lưu cache</typeparam>
        /// <param name="key">Khóa lưu cache</param>
        /// <returns></returns>
        public List<TEntity> GetCacheList<TEntity>(string key)
        {
            return (List<TEntity>)CacheManager.CacheClient.GetValue(key);
        }

        /// <summary>
        /// Xóa Cache từ Cache Server theo khóa
        /// </summary>
        /// <param name="key">Khóa lưu cache cần xóa</param>
        /// <returns></returns>
        public bool RemoveCache(string key)
        {
            return CacheManager.RemoveCache(key);
        }

        /// <summary>
        /// Xóa toàn bô dữ liệu đã cache trên Cache Server
        /// </summary>
        public void ClearCacheAll()
        {
            CacheManager.CacheClient.FlushAll();
        }

        #endregion
    }
}
