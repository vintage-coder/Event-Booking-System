using EBSystem.Order.API.Swagger.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();




builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();



builder.Services.AddApiVersioning(options =>
{

    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{

    options.GroupNameFormat = "'v'VVV";

    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerDefaultValues>();


    var xmlFile = $"EBSystem.Order.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();

}

app.UseCors("EnableCORS");

app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/swagger.json"; });
app.UseSwaggerUI(c=>
{
    c.DocumentTitle = "EBS Order Service";

    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();




    foreach (var description in provider.ApiVersionDescriptions)
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", "EBS Order Service API " + description.GroupName.ToUpperInvariant());


    c.InjectStylesheet("/swagger/ui/Custom.css");

    c.InjectJavascript("/swagger/ui/Custom.js");

    c.RoutePrefix = string.Empty;
});


app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
