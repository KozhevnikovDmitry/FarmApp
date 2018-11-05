using FarmApp.DAL.EF;
using FarmApp.DAL.Interfaces;
using System;
/*
 * CR-1 - remove redundant usings below
 */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.DAL.Repositories
{
    /// <summary>
    /// Реализация паттерна uow
    /// </summary>
    /*
     * CR-1
     * Make class internal and sealed, see comments for IUnitOfWork regarding type constraints and asynchronicity
     * Add comment to class methods
     *
     * Implemented UnitofWork approach (in combination with per request lifetime in autofac) is ok  for small CRUD application.
     * In real-world system I would prefer to execute db transaction with excplicitly managed UnitOfWork instance.
     * Example code:
     *
     * using(IUnitOfWork uow = _factory.Create())
     * {
     *    uow.PerformSomething();
     *    uow.PerformAnotherOne();
     *    uow.Commit();
     * }
     */
    public class EFUnitOfWork : IUnitOfWork
    {
        /*
	     * CR-1
         * Make a public property without setter
         * Initialize in constructor
         * Add comment
	     */
        private FarmContext context;

        /*
	     * CR-1
         * Make a public property without setter
         * Initialize in constructor
         * Add comment
	     */
        private IRepository<Agriculture> agricultures;

        /*
	     * CR-1
         * Make a public property without setter
         * Initialize in constructor
         * Add comment
	     */
        private IRepository<Crop> crops;

        /*
	     * CR-1
         * Make a public property without setter
         * Initialize in constructor
         * Add comment
	     */
        private IRepository<Farm> farms;

        /*
	     * CR-1
         * Make a public property without setter
         * Initialize in constructor
         * Add comment
	     */
        private IRepository<Farmer> farmers;

        /*
	     * CR-1
         * Make a public property without setter
         * Initialize in constructor
         * Add comment
	     */
        private IRepository<Region> regions;

        /*
	     * CR-1
         * Implement private ctor, that receives FarmContext
         * Call it from public constructors
         * Implement right away initializing of repositories - it is not costly operation
         * Target code here:
         *
         * public EFUnitOfWork(string connectionString)
         *     this(new FarmContext(connectionString)) { }
         *
         * public EFUnitOfWork()
         *     this(new FarmContext()) { }
         *
         * private EFUnitOfWork(FarmContext context)
         * {
         *   _context = context;
         *   Farms = new EFRepository<Farm>(_context);
         * }
         *
         * Add comments to ctor
	     */
        public EFUnitOfWork(string connectionString)
        { 
            /*
	         * CR-1 - check argument for null
	         */
            context = new FarmContext(connectionString);
		}

		public EFUnitOfWork()
		{
			context = new FarmContext();
		}




        /*
	     * CR-1 - remove this property, see comment to field agricultures
	     */
        public IRepository<Agriculture> Agricultures
		{
			get
			{
				if (agricultures == null)
				{
					agricultures = new EFRepository<Agriculture>(context);
				}
				return agricultures;
			}
		}

        /*
	     * CR-1 - remove this property, see comment to field crops
	     */
        public IRepository<Crop> Crops
		{
			get
			{
				if (crops == null)
				{
					crops = new EFRepository<Crop>(context);
				}
				return crops;
			}
		}

        /*
	     * CR-1 - remove this property, see comment to field farms
	     */
        public IRepository<Farm> Farms
		{
			get
			{
				if (farms == null)
				{
					farms = new EFRepository<Farm>(context);
				}
				return farms;
			}
		}

        /*
	     * CR-1 - remove this property, see comment to field farmers
	     */
        public IRepository<Farmer> Farmers
		{
			get
			{
				if (farmers == null)
				{
					farmers = new EFRepository<Farmer>(context);
				}
				return farmers;
			}
		}

        /*
	     * CR-1 - remove this property, see comment to field regions
	     */
        public IRepository<Region> Regions
		{
			get
			{
				if (regions == null)
				{
					regions = new EFRepository<Region>(context);
				}
				return regions;
			}
		}
		
		public void Save()
		{
		    /*
             * CR-1
		     * Add exception handling and logging
             */
            context.SaveChanges();
		}

        /*
         * CR-1
         * IDisposable implementation could be simplified for sealed class without unmanaged resources
         * Possible code:
         * public void Dispose()
         * {
         *     // check context for unfinished transaction
         *     _context.Dispose();
         * }
         *
         * However current implementation is ok and wide spreaded
         */
        private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
