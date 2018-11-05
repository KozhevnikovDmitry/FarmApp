using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmApp.BLL.DTO;
using FarmApp.BLL.Infrastructure;
using FarmApp.BLL.Interfaces;
using FarmApp.DAL;
using FarmApp.DAL.Interfaces;

namespace FarmApp.BLL.Services
{
    /*
     * CR-1 - rewrite comment in terms of class responsibility. For instance "CRUD service for farms"
     */
    /// <summary>
    /// Реализует операции бизнес-логики
    /// </summary>
    /*
     * CR-1 - make class internal and sealed
     */
    public class FarmService : IFarmService
    {
        /*
	     * CR-1
         * Make field readonly
         * Add prefix '_' to name of the privare field
         * Add comment to field
	     */
        private IUnitOfWork database;

        /*
	     * CR-1
         * Make field readonly
         * Add prefix '_' to name of the privare field
         * Add comment to field
	     */
        private IMapper mapper;

        /*
         * CR-1 - add comment to ctor and its arguments
         */
        public FarmService(IUnitOfWork uow, IMapper map)
        {
            database = uow ?? throw new ArgumentNullException("uow");
            mapper = map ?? throw new ArgumentNullException("map");
        }

        /*
         * CR-1 - check comments to IFarmService regarding the naming and comments for methods AddFarmCrop(), DeleteFarm(), GetFarms(), GetFarm()
         */
        public void AddFarmCrop(FarmCropDto farmCrop)
        {
            /*
	         * CR-1 - check argument for null
	         */


            /*
             * CR-1 - move validation rules into class FarmCropViewModel
             */
            if (farmCrop == null)
                throw new ValidationException($"Параметр farmCorp равен null", "");

            if (farmCrop.AgricultureId < 1)
                throw new ValidationException($"Неправильное значение: {farmCrop.AgricultureId }", "AgricultureId");

            if (farmCrop.FarmerId < 1)
                throw new ValidationException($"Неправильное значение: {farmCrop.FarmerId }", "FarmerId");

            if (farmCrop.RegionId < 1)
                throw new ValidationException($"Неправильное значение: {farmCrop.RegionId }", "RegionId");

            if (string.IsNullOrEmpty(farmCrop.Name))
                throw new ValidationException($"Не задано имя фермы", "Name");

            if (farmCrop.Gather < 0)
                throw new ValidationException($"Значение урожайности не может быть отрицательным", "Gather");
            /*
             * CR-1 - change validation condtition from "<=" to "<"
             */
            if (farmCrop.Area <= 0)
                throw new ValidationException($"Значение площади должно быть больше нуля", "Area");

            try
            {

                var farm = mapper.Map<FarmCropDto, Farm>(farmCrop);

                /*
                 * CR-1 - set up AutoMapper fill inner crops collection of farm instance, get rid of crop variable
                 */
                var crop = mapper.Map<FarmCropDto, Crop>(farmCrop);


                /*
                 * CR-1
                 * Rewrite two lines below as adding farm, instead of adding crop
                 * Target code here:
                 * database.Farms.Create(farm);
                 */
                crop.CropFarm = farm;
                database.Crops.Create(crop);
                database.Save();
            }
            /*
             * CR-1 - move handling of data access exceptions to repository class
             */
            catch (Exception ex)
            {
                throw new Exception("Ошибка сохранения информации об урожае на ферме", ex);
            }

        }

        public void DeleteFarm(int Id)
        {
            /*
             * CR-1 - use IRepository<T>.FindById() instead of Get 
             */
            var farmToRemove = database.Farms.Get(f => f.Id == Id).FirstOrDefault();
            if (farmToRemove == null)
                /*
                 * CR-1 - move handling of data access exceptions to repository class
                 */
                throw new Exception($"Ферма с Id {Id} не найдена");
            try
            {
                database.Farms.Remove(farmToRemove);
                database.Save();
            }
            /*
             * CR-1 - move handling of data access exceptions to repository class
             */
            catch (Exception ex)
            {
                throw new Exception("Ошибка удаления фермы", ex);
            }
        }

        /*
         * CR-1 - check comments to IFarmService regarding the separating of methods GetAgricultures(), GetFarmers(), GetRegions() into another class
         */
        public IEnumerable<AgricultureDto> GetAgricultures()
        {
            return database.Agricultures.Get().AsQueryable().ProjectTo<AgricultureDto>(mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<FarmerDto> GetFarmers()
        {
            return database.Farmers.Get().AsQueryable().ProjectTo<FarmerDto>(mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<FarmDto> GetFarms()
        {
            return database.Farms.Get().AsQueryable().ProjectTo<FarmDto>(mapper.ConfigurationProvider).ToList();
        }

        public FarmDto GetFarm(int Id)
        {
            /*
             * CR-1 - use IRepository<T>.FindById() instead of Get 
             */
            var farm = database.Farms.Get(f => f.Id == Id).FirstOrDefault();
            return mapper.Map<FarmDto>(farm);
        }

        public IEnumerable<RegionDto> GetRegions()
        {
            return database.Regions.Get().AsQueryable().ProjectTo<RegionDto>(mapper.ConfigurationProvider).ToList();
        }

        /*
         * CR-1
         * IDisposable implementation could be simplified for sealed class without unmanaged resources
         * Possible code:
         * public void Dispose()
         * {
         *     _context.Dispose();
         * }
         *
         * However current implementation is ok and wide spreaded
         */
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                    database.Dispose();
                    database = null;
                    mapper = null;
                }
                disposed = true;
            }
        }
    }
}
