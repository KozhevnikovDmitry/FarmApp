using FarmApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FarmApp.DAL.Repositories
{
    /// <summary>
    /// Реализация репозитория в контексте EF
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /*
     * CR-1
     * Make class internal and sealed, see comments for IRepository regarding type constraints, asynchronicity and using Expression<Func<T, bool>> instead of Func<T, bool> 
     * Add comment to class methods
     */
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /*
	     * CR-1
         * Make field readonly
         * Add comment to field
	     */
        DbContext _context;

        /*
	     * CR-1
         * Make field readonly
         * Add comment to field
	     */
        DbSet<TEntity> _dbSet;

        /*
	     * CR-1 - add comments for ctor
	     */
        public EFRepository(DbContext context)
		{
		    /*
             * CR-1 - check argument for null
             */
            _context = context;
			_dbSet = context.Set<TEntity>();
		}

		public IEnumerable<TEntity> Get()
		{
			return _dbSet;
		}

		public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
		{
		    /*
             * CR-1 - check argument for null
             */
            return _dbSet.Where(predicate);
		}
		public TEntity FindById(int id)
		{
		    /*
             * CR-1 - add exception handling, logging and checking found value for null
             */
            return _dbSet.Find(id);
		}

		public void Create(TEntity item)
		{
		    /*
             * CR-1 -check argument for null
             */
            _dbSet.Add(item);			
		}
		public void Update(TEntity item)
		{
		    /*
             * CR-1 - check argument for null
             */
            _context.Entry(item).State = EntityState.Modified;
		}
		public void Remove(TEntity item)
		{
		    /*
             * CR-1 - check argument for null
             */
            _dbSet.Remove(item);
		}
	}
}
