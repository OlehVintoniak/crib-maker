using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CribMaker.Startup))]
namespace CribMaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
