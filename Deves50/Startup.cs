using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Deves50.Startup))]
namespace Deves50
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
