using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using RealWorldMvc3.Core.Castle.Components;

namespace RealWorldMvc3
{
    public class MvcApplication : HttpApplication, IContainerAccessor
    {
        private static readonly IWindsorContainer container = new WindsorContainer();

        public IWindsorContainer Container
        {
            get { return container; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            InstallContainer(Container);
            RegisterModelBinderProvider(Container);
            RegisterModelValidatorProvider(Container);
            SetControllerFactory(Container);
        }

        private void InstallContainer(IWindsorContainer windsorContainer)
        {
            windsorContainer.Install(FromAssembly.This());
        }

        private void RegisterModelValidatorProvider(IWindsorContainer windsorContainer)
        {
            if (!windsorContainer.Kernel.HasComponent(typeof (ModelValidatorProvider))) return;

            var modelValidatorProvider = windsorContainer.Resolve<ModelValidatorProvider>();
            ModelValidatorProviders.Providers.Add(modelValidatorProvider);
        }

        protected void Application_End()
        {
            container.Dispose();
        }

        private void SetControllerFactory(IWindsorContainer windsorContainer)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(windsorContainer));
        }

        private void RegisterModelBinderProvider(IWindsorContainer windsorContainer)
        {
            if (!windsorContainer.Kernel.HasComponent(typeof(IModelBinderProvider))) return;

            var modelBinderProvider = windsorContainer.Resolve<IModelBinderProvider>();
            ModelBinderProviders.BinderProviders.Add(modelBinderProvider);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
    }
}