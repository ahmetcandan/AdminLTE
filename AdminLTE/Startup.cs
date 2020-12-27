using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminLTE.Startup))]
namespace AdminLTE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
