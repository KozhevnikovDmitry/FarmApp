using System;
/*
 * CR-1 - remove redundant usings below
 */
using System.Collections.Generic;
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
    /// Интерфейс паттерна Unit of work
    /// </summary>
    /*
     * CR-1 - add comments to members
     */
    public interface IUnitOfWork : IDisposable
	{
		IRepository<Agriculture> Agricultures { get; }

		IRepository<Crop> Crops { get; }

		IRepository<Farm> Farms { get; }

		IRepository<Farmer> Farmers { get; }

		IRepository<Region> Regions { get; }

	    /*
         * CR-1 - make method asynchronous
         */
        void Save();
	}
}
