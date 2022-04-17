using EBSystem.Authentication.API.Contracts;
using EBSystem.Authentication.API.DBContexts;
using EBSystem.Authentication.API.Models;
using EBSystem.Authentication.API.Repositories;
using EBSystem.User.API.Swagger.Configuration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using EBSystem.Authentication.API.Handlers;
using EBSystem.Authentication.API.Helpers;
using Serilog;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// ====================================Serilog configuration =============================


builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});


// ==============================Service configuration ===========================

builder.Services.AddControllers();



// ============================= Cross Origin Service ===========================
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});


//  =========================JWT Authentication Service================================

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
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


// ===========================Google Authentication Services=========================

//builder.Services.AddIdentity<IdentityUser, IdentityRole>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
        .AddGoogle(options =>
        {
            options.ClientId = "473645421288-192r2fjahgshbp2s4isqta4m59jrlp3j.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });



// ================= Dependency Injection For the Objects =========================

builder.Services.AddDbContext<AuthenticateContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Authentication")));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Authentication")));


builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


builder.Services.AddTransient<ITokenRepository, TokenRepository>();


builder.Services.AddScoped<GoogleHelper>();

builder.Services.AddScoped<JWTHelper>();



// ===============================Api Versioning Service==========================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{

    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{

    options.GroupNameFormat = "'v'VVV";

    options.SubstituteApiVersionInUrl = true;
});


// ============================Swagger Services=========================================

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerDefaultValues>();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearer"
                                }
                            },
                            new string[] {}
                    }
                });

    var xmlFile = $"EBSystem.Authentication.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});




// ===========================================PipeLine Configuration=========================

var app = builder.Build();


// ==========================================Developer Exception Pipeline=====================
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// ==========================Logger middleware Pipeline ====================================

app.UseSerilogRequestLogging();


//================================Cross Origin Request pipeline=========================================
app.UseCors("EnableCORS");




//============================= Swagger pipeline  ======================================================
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


// ============================Static files pipeline configuration============================
app.UseStaticFiles();


// ================================Authentication Pipeline ===========================
app.UseAuthentication();


// ================================Authorization Pipeline================================
app.UseAuthorization();

app.MapControllers();

app.Run();
