using Autofac;
/*
 * CR-1 - remove redundant using
 */
using AutoMapper;
using FarmApp.BLL.Interfaces;
using FarmApp.BLL.Services;
using FarmApp.DAL.Interfaces;
using FarmApp.DAL.Repositories;

namespace FarmApp.BLL
{

    /*
     * CR-1
     * Add a comment to class
     * Give class meaningfull name, for instance BusinessLogicModule
     * Make class sealed
     */
    public class AutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
		    /*
             * CR-1 - move registration of data access services to DataAccessModule, that is located in FarmApp.DAL assembly
             */
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().WithParameter("connectionString", "FarmContext").InstancePerRequest();

            /*
             * CR-1
             * IMapper instance must be registered in container directly.
             * See comments tot AutofacConfig and AutomapperConfig classes
             * Target code here:
             * builder.RegisterType<FarmService>().As<IFarmService>().InstancePerRequest();
             */
            builder.RegisterType<FarmService>().As<IFarmService>().WithParameter("map", AutoMapperConfig.GetMapper()).InstancePerRequest();

			base.Load(builder);
		}
	}
}
