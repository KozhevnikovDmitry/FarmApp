/*
 * CR-1 - remove redundant usings - System.Web, System.Web.UI, System.Threading.Tasks
 */
using AutoMapper;
using FarmApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FarmApp.ViewModels;
using System.Web.UI;
using System.Runtime.Caching;
using FarmApp.BLL.DTO;
using FarmApp.BLL.Infrastructure;
using System.Threading.Tasks;

namespace FarmApp.Controllers
{
    /// <summary>
    /// Работа с фермами
    /// </summary>
    /*
	 * CR-1
     * Make class sealed
     * Add exception handling and returning standard HTTP codes - 404, 500, etc.
     * Separated handling of known exceptions from classes from FarmApp.DAL and unexpected exceptions
	 */
    public class FarmController : Controller
    {
        /*
	     * CR-1
         * Make field readonly
         * Add prefix '_' to name of the privare field
         * Add comment to field
	     */
        private IFarmService farmService;

        /*
	     * CR-1
         * Make field readonly
         * Add prefix '_' to name of the privare field
         * Add comment to field
	     */
        private IMapper mapper;


        /*
	     * CR-1 - add comment to ctor and parameters
	     */
        public FarmController(IFarmService service, IMapper map)

            /*
             * CR-1 - remove redundant call of ctor of base class
             */
            : base()
        {
            /*
             * CR-1 - add check arguments fot null value
             */
            farmService = service;
            mapper = map;
        }

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns list of all farms"
         */
        /// <summary>
        /// Список ферм
        /// </summary>
        /// <returns></returns>
        /*
         * CR-1 - add [HttpGet] attribure
         */
        public ActionResult List()
        {
            var farms = farmService.GetFarms().AsQueryable().ProjectTo<FarmViewModel>(mapper.ConfigurationProvider).ToList();

            return View(farms);
        }

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Returns form for adding new farm"
         */
        /// <summary>
        /// Добавление фермы
        /// </summary>
        /// <returns></returns>
        /*
         * CR-1 - add [HttpGet] attribure
         */
        public ActionResult Create()
        {
            /*
             * CR-1 
             * Move work with cache to separated class, let's say ValuesProvider, use is as dependency of FarmController
             * ValuesProvider must not be dependent from FarmService, give it works repositories by itself 
             */

            MemoryCache cache = MemoryCache.Default;
            //если есть в кэше - берём из него, в противном случае запрашиваем из IFarmService и помещаем в кэш

            /*
             * CR-1 - rename variable from "rc" to "regions"
             */
            var rc = cache.Get("regions") as IEnumerable<NamedItemViewModel>;

            /*
             * CR-1 - rename variable from "fc" to "farmers"
             */
            var fc = cache.Get("farmers") as IEnumerable<NamedItemViewModel>;

            /*
             * CR-1 - rename variable from "ac" to "agricultures"
             */
            var ac = cache.Get("agricultures") as IEnumerable<NamedItemViewModel>;


            /*
             * CR-1 
             * Reduce code duplication on adding/getting values from cache
             * Move cache expiring time to configuration file
             */
            if (rc == null)
            {
                rc = farmService.GetRegions().AsQueryable().ProjectTo<NamedItemViewModel>(mapper.ConfigurationProvider);
                cache.Add("regions", rc, new DateTimeOffset(DateTime.Now.AddSeconds(120)));
            }

            if (fc == null)
            {
                fc = farmService.GetFarmers().AsQueryable().ProjectTo<NamedItemViewModel>(mapper.ConfigurationProvider);
                cache.Add("farmers", fc, new DateTimeOffset(DateTime.Now.AddSeconds(120)));
            }

            if (ac == null)
            {
                ac = farmService.GetAgricultures().AsQueryable().ProjectTo<NamedItemViewModel>(mapper.ConfigurationProvider);
                cache.Add("agricultures", ac, new DateTimeOffset(DateTime.Now.AddSeconds(120)));
            }

            /*
             * CR-1 - move these collections from ViewBag to class FarmCropViewModel
             */
            ViewBag.Regions = rc;
            ViewBag.Farmers = fc;
            ViewBag.Agricultures = ac;

            return View(new FarmCropViewModel());
        }

        /*
         * CR-1 - add comment to action
         */
        [HttpPost]
        public ActionResult Create(FarmCropViewModel model)
        {
            /*
             * CR-1 - add check argument for null
             */

            /*
             * CR-1 
             * Reduce code duplication with previous method, use ValueProvider class from comments above
             * Add cached values only in case of redirection back to create form because of failed validation
             */
            var cache = MemoryCache.Default;
            //если есть в кэше - берём из него, в противном случае запрашиваем из IFarmService
            var rc = (cache.Get("regions") as IEnumerable<NamedItemViewModel>) ?? farmService.GetRegions().AsQueryable().ProjectTo<NamedItemViewModel>(mapper.ConfigurationProvider);
            var fc = (cache.Get("farmers") as IEnumerable<NamedItemViewModel>) ?? farmService.GetFarmers().AsQueryable().ProjectTo<NamedItemViewModel>(mapper.ConfigurationProvider);
            var ac = cache.Get("agricultures") as IEnumerable<NamedItemViewModel> ?? farmService.GetAgricultures().AsQueryable().ProjectTo<NamedItemViewModel>(mapper.ConfigurationProvider);
            ViewBag.Regions = rc;
            ViewBag.Farmers = fc;
            ViewBag.Agricultures = ac;

            /*
             * CR-1
             * Gather all validation rules into one place - class FarmCropViewModel, let it validate its data,
             * Implement method FarmCropViewModel.Validate that returns dictionary with validation errors instead of exceptions
             * Check validation results here in controller. If there are no errors, save farm to DB.
             * If there are an errors, add them to ModelState and redirect back to form
             */

            /*
             * CR-1 - change condition from "<=" to "<". 
             */
            if (model.Area <= 0)
            {
                ModelState.AddModelError("Area", "Площадь должна быть больше нуля");
            }

            if (model.Gather < 0)
            {
                ModelState.AddModelError("Gather", "Урожай не может быть меньше нуля");
            }

            if (!ModelState.IsValid)
            {
                /*
                * CR-1 - add regions, farmers and agricultures only here
                */
                return View(model);
            }

            /*
            * CR-1 - get rid of exception based logic, use validation results from FarmCropViewModel
            */
            try
            {
                var farmCrop = mapper.Map<FarmCropDto>(model);
                farmService.AddFarmCrop(farmCrop);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View(model);
            }

            return RedirectToAction("List");
        }

        /*
         * CR-1 - rewrite comment in terms of what action does. For instance "Shows delete farm form"
         */
        /// <summary>
        /// Удаление фермы
        /// </summary>
        
        /// <param name="Id"></param>
        /// <returns></returns>
        /*
         * CR-1 
         * Rename argumetn from "Id" to "id", add description of argument to comment
         * Add [HttpGet] attribure
         */
        public ActionResult Delete(int Id)
        {
            var farm = farmService.GetFarm(Id);

            var farmModel = mapper.Map<FarmViewModel>(farm);

            return View(farmModel);
        }

        /*
         * CR-1 - add a comment to action and arguments
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
         * CR-1
         * Rename argumetn from "Id" to "id"
         * It is probably would be better to introduced model class for delete post action and get rid of argument formCollection 
         */
        public ActionResult Delete(int Id, FormCollection formCollection)
        {
            farmService.DeleteFarm(Id);

            return RedirectToAction("List");
        }
    }
}