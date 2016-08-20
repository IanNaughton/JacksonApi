using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using Autofac;
using JacksonApi.DataAccess.Interfaces;
using JacksonApi.DataAccess.Gateways;
using Autofac.Integration.WebApi;
using System.Web.Http.Dependencies;
using System.Reflection;
using JacksonApi.Interfaces;
using JacksonApi.Adapters;

namespace JacksonApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.EnableCors();

            // Emit all result sets as JSON
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Register dependencies with autofac container
            var builder = new ContainerBuilder();

            // Register controllers 
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register custom services
            builder.RegisterType<ActRepository>().As<IActRepository>();
            builder.RegisterType<VolumeRepository>().As<IVolumeRepository>();
            builder.RegisterType<VolumeTextRepository>().As<IVolumeTextRepository>();
            builder.RegisterType<ActAdapter>().As<IActAdapter>();
            builder.RegisterType<VolumeAdapter>().As<IVolumeAdapter>();
            builder.RegisterType<VolumeTextAdapter>().As<IVolumeTextAdapter>();

            IDependencyResolver resolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(builder.Build());

            config.DependencyResolver = resolver;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
