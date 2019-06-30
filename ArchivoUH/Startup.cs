using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArchivoUH.Startup))]
namespace ArchivoUH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
