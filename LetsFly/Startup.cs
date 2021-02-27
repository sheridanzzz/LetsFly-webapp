using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LetsFly.Startup))]
namespace LetsFly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
