using AutoMapper;
using FarmApp.BLL.DTO;
using FarmApp.ViewModels;

/*
 * CR-1 - remove redundant usings below
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmApp.Util
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
         * Combine maping rules from FarmApp.Util.AutoMapperConfig and FarmApp.BLL.AutoMapperConfig in single configuration
         * Make configration settings from different assemblies be available from here and gather them
         *
         * Possible code here:
         * var config = new MapperConfiguration(cfg =>
         * {
         *   FarmApp.Util.AutoMapperConfig.AddConfig(cfg);
         *   FarmApp.BLL.AutoMapperConfig.AddConfig(cfg);
         * }
         *
         * Register result IMapper instance in Autofac container
         * For instance this class can be wrapped with another Autofac module
         */
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FarmDto, FarmViewModel>()
                    .ForMember(des => des.FarmerName, opt => opt.MapFrom(src => src.OwnerName))
                    .ForMember(des => des.RegionName, opt => opt.MapFrom(src => src.LocationName));
                cfg.CreateMap<RegionDto, NamedItemViewModel>();
                cfg.CreateMap<FarmerDto, NamedItemViewModel>();
                cfg.CreateMap<AgricultureDto, NamedItemViewModel>();
                cfg.CreateMap<FarmCropViewModel, FarmCropDto>();

            });
            return new Mapper(config);
        }
    }
}