using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EBSystem.API.Swagger.Configuration
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
           
            if(description.GroupName=="v1")
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


//var info = new OpenApiInfo()
//{
//    Title = "Cool Web API",
//    Version = description.ApiVersion.ToString(),
//    Description = "A Cool Web API Sample.",
//    Contact = new OpenApiContact { Name = "Mosi Esmailpour", Email = "mo.esmp@gmail.com" },
//    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
//};

//if (description.IsDeprecated)
//    info.Description += " This API version has been deprecated.";

//return info;