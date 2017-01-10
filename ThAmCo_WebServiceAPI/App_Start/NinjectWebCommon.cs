[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ThAmCo_WebServiceAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ThAmCo_WebServiceAPI.App_Start.NinjectWebCommon), "Stop")]

namespace ThAmCo_WebServiceAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Repository;
    using InventoryManagementModel;
    using System.Web.Http;
    using WebApiContrib.IoC.Ninject;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>(); // to the API project.




                // Web API settings - necessary as resolves dependencies of ApiController extention.
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);





                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProductRepository>().To<ProductRepository>(); // binding the product repository interface to the product repository class.
            //kernel.Bind<ModelInventoryManagement>().To<ModelInventoryManagement>(); // binding the Entity framework model to itself.
        }        
    }
}
