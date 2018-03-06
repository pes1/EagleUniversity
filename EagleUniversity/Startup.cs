using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EagleUniversity.Startup))]
namespace EagleUniversity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
