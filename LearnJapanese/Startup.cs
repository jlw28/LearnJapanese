using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnJapanese.Startup))]
namespace LearnJapanese
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
