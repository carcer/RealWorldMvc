using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RealWorldMvc3.Core.Castle.Components;

namespace RealWorldMvc3.Core.Castle.Installers
{
    // http://kozmic.pl/2010/05/22/contextual-controller-injection-in-asp-net-mvc-with-castle-windsor/
    public class MvcComponentsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(
                    Component.For<ControllerContextHost>().LifeStyle.PerWebRequest,
                    Component.For<RequestContextHost>().LifeStyle.PerWebRequest,

                    Component.For<RequestContext>()
                        .UsingFactoryMethod(k => k.Resolve<RequestContextHost>().GetContext()),

                    Component.For<HttpContextBase>()
                        .UsingFactoryMethod(k => new HttpContextWrapper(HttpContext.Current)),

                    Component.For<HttpSessionStateBase>()
                        .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Session),

                    Component.For<HttpRequestBase>()
                        .UsingFactoryMethod(k => k.Resolve<HttpContextBase>().Request),

                    Component.For<Func<ControllerContext>>()
                        .UsingFactoryMethod<Func<ControllerContext>>(k => k.Resolve<ControllerContextHost>().GetContext),

                    Component.For<ITempDataProvider>()
                        .ImplementedBy<SessionStateTempDataProvider>().
                        LifeStyle.Transient,

                    Component.For<UrlHelper>().LifeStyle.PerWebRequest,
                    Component.For<HtmlHelper>().LifeStyle.PerWebRequest
                );
        }
    }
}