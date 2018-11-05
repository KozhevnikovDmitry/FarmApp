using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using FarmApp.BLL;
using System.Web.Mvc;

namespace FarmApp.Util
{
    /*
     * CR-1 
     * Add a comment to class
     * Make class static
     */
    public class AutofacConfig
    {
        /*
         * CR-1 - add a comment to method
         */
        public static void ConfigureContainer()
		{
			// получаем экземпляр контейнера
			var builder = new ContainerBuilder();

			//регистрируем модуль из проекта BLL
			builder.RegisterModule(new AutofacModule());

            /*
             * CR-1
		     * Get rid of parameter injecting setting, let autofac provide mapper instance to controllers
		     * See comments to AutofacModule and AutoMapperConfig classes
		     * Target code here:
             *
		     * builder.RegisterModule(new AutofacModule());
             * builder.RegisterInstance<IMapper>(AutomapperConfig.GetMapper());
             * builder.RegisterControllers(typeof(MvcApplication).Assembly);
             */
            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly).WithParameter(
				(p, c) => p.ParameterType == typeof(IMapper), 
				(p, c) => AutoMapperConfig.GetMapper()
            );

			// создаем новый контейнер с теми зависимостями, которые определены выше
			var container = builder.Build();

			// установка сопоставителя зависимостей
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}