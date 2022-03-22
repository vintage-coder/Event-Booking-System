using Microsoft.OpenApi.Models;
using EBSystem.Services.Contracts;
using EBSystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using EBSystem.Models.DBContexts;
using System.Reflection;
using EBSystem.API.Swagger.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);



builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:57589",
            ValidAudience = "http://localhost:57589",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });








builder.Services.AddDbContext<EMSDBContext>(opts =>
opts.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));




builder.Services.AddTransient<IEventService, EventService>();




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

   

    var xmlFile = $"EBSystemDocument.xml";
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
app.UseSwaggerUI(c =>
{
    
    c.DocumentTitle = "Event Booking System UI";


    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();



    
    foreach (var description in provider.ApiVersionDescriptions)
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json","EBSystem API "+ description.GroupName.ToUpperInvariant());


   

    c.InjectStylesheet("/swagger/custom.css");

    c.InjectJavascript("/swagger/custom.js");

    c.RoutePrefix = string.Empty;

    
    

});

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
