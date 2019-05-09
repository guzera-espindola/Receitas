using System.Web.Http;
using OnHelp.Api.Receitas.Configuration;
using Microsoft.Owin;
using Owin;
using OnHelp.Api.Receitas.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace OnHelp.Api.Receitas.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
  

            app.UseWebApi(config);
            app.RegisterWebApi(config);
            app.RegisterMediaTypeFormatter(config);
            app.ConfigureDependencyInjection(config);

        }
    }
}