using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Repository;
using Repository.Common;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Server_side_API.Utils
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //DI for Mapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RepositoryProfile());
                cfg.AddProfile(new ServiceProfile());
            })).AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            });

            //Moduls
            builder.RegisterModule<ServiceAutofacModule>();
            builder.RegisterModule<RepositoryAutofacModule>();
            Container = builder.Build();

            return Container;
        }
    }
}