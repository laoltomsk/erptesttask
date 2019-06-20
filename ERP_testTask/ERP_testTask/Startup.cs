using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP_testTask.Startup))]
namespace ERP_testTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
