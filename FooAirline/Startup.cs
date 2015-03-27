using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FooAirline.Startup))]
namespace FooAirline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
