using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Exo_Base.Web;
using Exo_Base.Bootstrapper;
using Exo_Base.Data;
using Exo_Base.Core.Data;
using Exo_Base.Core.Services;
using Exo_Base.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DIConfig), "RegisterDependencies")]
namespace Exo_Base.Bootstrapper
{
    // DI stands for DependencyInjection ( also called Ioc i.e. Inversion of control)
    //Autofac = addictive Ioc(Inversion of control) container
    public class DIConfig 
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            const string nameOrConnectionString = "name=DefaultConnection";
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            //builder.RegisterGeneric(typeof(MapperExtension)).As(typeof(IMappingProvider)).InstancePerRequest();
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerRequest();

            builder.RegisterType(typeof(NavigationService)).As(typeof(INavigationService)).InstancePerRequest();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.Register<IEntitiesContext>(b =>
            {
                //var logger = b.Resolve<ILogger>();
                var context = new ApplicationDbContext(nameOrConnectionString);
                return context;
            }).InstancePerRequest();
            ////builder.Register(b => NLogLogger.Instance).SingleInstance();
            builder.RegisterModule(new IdentityModule());

            

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
