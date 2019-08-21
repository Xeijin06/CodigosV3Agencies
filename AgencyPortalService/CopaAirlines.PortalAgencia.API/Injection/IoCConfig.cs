using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CopaAirlines.PortalAgencia.Interface;
using CopaAirlines.PortalAgencia.Logica;
using Portaldeagencias.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Portaldeagencias.Injection
{
    public class IoCConfig
    {
        public static void AddInjections(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(IoCConfig).Assembly);

            builder.RegisterType<AgentRequest>().As<IAgentRequest>().InstancePerRequest();

            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}