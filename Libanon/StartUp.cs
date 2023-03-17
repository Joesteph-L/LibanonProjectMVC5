using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Libanon.App_Start.StartUp))]
namespace Libanon.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}