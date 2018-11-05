using AutoMapper;
using FarmApp.BLL.DTO;
using FarmApp.DAL;
/*
 * CR-1 - remove redundant usings below
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.BLL
{
    /*
     * CR-1 
     * Add a comment to class
     * Make class static
     */
    public class AutoMapperConfig
    {
        /*
         * CR-1 
         * Add a comment to method
         * See comments to FarmApp.Util.AutoMapperConfig
         */
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FarmCropDto, Farm>();
                cfg.CreateMap<FarmCropDto, Crop>();
                cfg.CreateMap<Agriculture, AgricultureDto>();
                cfg.CreateMap<Farmer, FarmerDto>();
                cfg.CreateMap<Region, RegionDto>();
            });
            return new Mapper(config);
        }
    }
}
