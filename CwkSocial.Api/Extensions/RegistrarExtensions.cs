using CwkSocial.Api.Registrars;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CwkSocial.Api.Extensions
{
    public static class RegistrarExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
        {
            //var register = scanningType.Assembly.GetTypes()
            //      .Where(t => t.IsAssignableTo<IWebApplicationBuilderRegistrar>());
        }

        public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
        {

        }
    }
}
