using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CjbHome.Startup))]
namespace CjbHome
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
