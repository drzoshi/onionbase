using Exo_Base.Services;
using Owin;

//[assembly: OwinStartup(typeof(Exo_Base.Bootstrapper.Startup))]
//[assembly: OwinStartup("BootstrapperConfiguration", typeof(Exo_Base.Bootstrapper.Startup))]
namespace Exo_Base.Bootstrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
