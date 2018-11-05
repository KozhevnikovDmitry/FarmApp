using FarmApp.BLL.DTO;
using System;
using System.Collections.Generic;
/*
 * CR-1 - remove redundant using below
 */
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.BLL.Interfaces
{
    /*
     * CR-1 -  rewrite comment in terms of abstraction, which interface covers. For instance "Abstraction of CRUD service for farms"
     */
    /// <summary>
    /// Предоставляет набор операций бизнес-логики
    /// </summary>
    public interface IFarmService : IDisposable
    { 
        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns list of all farms"
         */
        /// <summary>
        /// Получить информацию по фермам
        /// </summary>
        /// <returns></returns>
        IEnumerable<FarmDto> GetFarms();

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns farm by identifier"
         */
        /// <summary>
        /// Получить информацию по ферме с заданным Id
        /// </summary>
        /// <param name="Id">Id фермы</param>
        /// <returns></returns>
        /*
         * CR-1 - rename argument "Id" to "id"
         */
        FarmDto GetFarm(int Id);


        /*
         * CR-1
         * Separate GetFarmers(), GetRegions(),  GetAgricultures() into another class, let's say ValueProvider
         * Encapsulate caching within ValueProvider
         * See comments to method FarmController.Create() 
         */

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns list of all farmers"
         */
        /// <summary>
        /// Получить информацию по фермерам
        /// </summary>
        /// <returns></returns>
        IEnumerable<FarmerDto> GetFarmers();

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns list of all farmers"
         */
        /// <summary>
        /// Получить информацию по регионам
        /// </summary>
        /// <returns></returns>
        IEnumerable<RegionDto> GetRegions();

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns list of all agricultures"
         */
        /// <summary>
        /// Получить информацию по с/х культурам
        /// </summary>
        /// <returns></returns>
        IEnumerable<AgricultureDto> GetAgricultures();

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Deletes farm by identifier"
         */
        /// <summary>
        /// Удалить ферму
        /// </summary>
        /// <param name="Id">Идентификатор удаляемой фермы</param>
        /*
         * CR-1 - rename argument "Id" to "id"
         */
        void DeleteFarm(int Id);

        /*
         * CR-1
         * Rewrite comment in terms of what action does. For instance "Adds new farm"
         * Rename method to AddFarm
         * Add comment to argument
         */
        /// <summary>
        /// Добавление сведений ферме и урожае на ней
        /// </summary>
        /// <param name="farmCrop"></param>
        void AddFarmCrop(FarmCropDto farmCrop);
    }
}
