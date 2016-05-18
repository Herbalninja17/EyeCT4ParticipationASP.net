using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EyeCT4ParticipationASP.Startup))]
namespace EyeCT4ParticipationASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
