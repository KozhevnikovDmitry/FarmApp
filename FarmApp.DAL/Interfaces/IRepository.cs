using System;
using System.Collections.Generic;
/*
 * CR-1 - remove redundant usings below
 */
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.DAL.Interfaces
{
    /*
     * CR-1
     * Move this interface to FarmApp.BLL assembly
     * Leave implementation in FarmApp.DAL
     * Make FarmApp.DAL refer on FarmApp.BLL
     */
    /// <summary>
    /// Обобщённый репозиторий - набор операций над сущностью
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /*
     * CR-1 - add IEntity interface for generic type constraint, make entity class inherit it
     */
    public interface IRepository<TEntity> where TEntity : class
    {
        /*
         * CR-1
         * Add comments to all method
         * Make all methods asynchronous
         */
        void Create(TEntity item);

        /*
         * CR-1 - as long as all entities have identifiers of type int, it is ok, however it can be replaced with second type constraint
         */
        TEntity FindById(int id);

		IEnumerable<TEntity> Get();

        /*
         * CR-1 - use Expression<Func<TEntity>> instead of Func<TEntity>
         */
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

		void Remove(TEntity item);

		void Update(TEntity item);
	}
}
