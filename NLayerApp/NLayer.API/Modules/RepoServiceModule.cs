using System.Reflection;
using Autofac;
using NLayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnifOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using Module = Autofac.Module;

namespace NLayer.API.Modules;

public class RepoServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(ServiceWithDto<,>)).As(typeof(IServiceWithDto<,>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<ProductServiceWithDto>().As<IProductServiceWithDto>().InstancePerLifetimeScope();
           

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        
        var apiAssembly = Assembly.GetExecutingAssembly();
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));
        
        builder.RegisterAssemblyTypes(repoAssembly,serviceAssembly,apiAssembly)
          .Where(t => t.Name.EndsWith("Repository"))
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(serviceAssembly,apiAssembly,repoAssembly)
         .Where(t => t.Name.EndsWith("Service"))
         .AsImplementedInterfaces()
         .InstancePerDependency();

        builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
        
        
        
        base.Load(builder);
    }
}