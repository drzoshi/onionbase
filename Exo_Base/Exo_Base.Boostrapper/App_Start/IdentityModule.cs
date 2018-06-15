using Autofac;
using Exo_Base.Core.Identity;
using Exo_Base.Data;
using Exo_Base.Data.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Exo_Base.Bootstrapper
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(AppUserManager)).As(typeof(IApplicationUserManager)).InstancePerRequest();
            builder.RegisterType(typeof(AppRoleManager)).As(typeof(IApplicationRoleManager)).InstancePerRequest();
            builder.RegisterType(typeof(AppSignInManager)).As(typeof(IApplicationSignInManager)).InstancePerRequest();
            builder.RegisterType(typeof(ApplicationAuthenticationManager)).As(typeof(IApplicationAuthenticationManager)).InstancePerRequest();
            builder.RegisterType(typeof(ApplicationIdentityUser)).As(typeof(IUser<int>)).InstancePerRequest();
            
            builder.Register(b => b.Resolve<IEntitiesContext>() as DbContext).InstancePerRequest();

            builder.Register(b =>IdentityFactory.CreateUserManager(b.Resolve<IEntitiesContext>() as ApplicationDbContext, Startup.DataProtectionProvider)).InstancePerRequest();
            builder.Register(b => IdentityFactory.CreateRoleManager(b.Resolve<IEntitiesContext>() as ApplicationDbContext));
            builder.Register(b => IdentityFactory.CreateSignInManager(b.Resolve<ApplicationUserManager>(), HttpContext.Current.GetOwinContext().Authentication)).InstancePerRequest();
            builder.Register(b => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();

            
            //builder.RegisterType(typeof(ApplicationRole)).As(typeof(IRole<int>)).InstancePerRequest();

            //builder.Register(b =>
            //{
            //    var manager = IdentityFactory.CreateUserManager(b.Resolve<DbContext>());
            //    if (Startup.DataProtectionProvider != null)
            //    {
            //        manager.UserTokenProvider =
            //            new DataProtectorTokenProvider<ApplicationIdentityUser, int>(
            //                Startup.DataProtectionProvider.Create("ASP.NET Identity"));
            //    }
            //    return manager;
            //}).InstancePerHttpRequest();
        }
    }
}
