using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EBSystem.ApiGateWay.Swagger.Configuration
{

    
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

       
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;


        public void Configure(SwaggerGenOptions options)
        {
         
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {

            if (description.GroupName == "v1")
            {
                var info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EB System API v1",
                    Description = "A simple example for EB System v1 swagger api information",
                    TermsOfService = new Uri("http://tempuri.org/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gowthaman V",
                        Email = "gowthamanvai@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "EB System 1.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                };

                return info;
            }
            else
            {
                var info = new OpenApiInfo
                {
                    Version = "v2",
                    Title = "EB System API v2",
                    Description = "A simple example for EB System v2 swagger api information",
                    TermsOfService = new Uri("http://tempuri.org/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gowthaman V",
                        Email = "gowthamanvai@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "EB System 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                };
                return info;
            }

        }

    }
}
