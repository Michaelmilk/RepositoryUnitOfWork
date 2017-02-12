using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(unitWork.Startup))]
namespace unitWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
