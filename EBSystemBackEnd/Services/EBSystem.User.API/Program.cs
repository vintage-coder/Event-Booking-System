using EBSystem.Authentication.API.Contracts;
using EBSystem.Authentication.API.DBContexts;
using EBSystem.Authentication.API.Repositories;
using EBSystem.User.API.Swagger.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gowthamSecretKey@345"))
        };
    });




builder.Services.AddDbContext<AuthenticateContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Authentication")));


builder.Services.AddEndpointsApiExplorer();




builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


builder.Services.AddTransient<ITokenRepository, TokenRepository>();


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


    var xmlFile = $"EBSystem.User.API.xml";
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


    c.DocumentTitle = "EBS User Service";

    var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();



    
    foreach (var description in provider.ApiVersionDescriptions)
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", "EBS User Service API " + description.GroupName.ToUpperInvariant());


    c.InjectStylesheet("/swagger/ui/Custom.css");

    c.InjectJavascript("/swagger/ui/Custom.js");

    c.RoutePrefix = string.Empty;
});



app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
