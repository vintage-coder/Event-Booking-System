using Microsoft.OpenApi.Models;
using EBSystem.Services.Contracts;
using EBSystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using EBSystem.Models.DBContexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<EMSDBContext>(opts =>
opts.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));



builder.Services.AddTransient<IEventService, EventService>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();





//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
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

    });

    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "EB System API v1",
        Description = "A simple example for EB System v2 swagger api information",
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

    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}




app.UseSwagger();
app.UseSwaggerUI(c =>
{
    
    c.DocumentTitle = "Event Booking System UI";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Booking System API v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Event Booking System API v2");

    c.InjectStylesheet("/swagger/custom.css");

    c.InjectJavascript("/swagger/custom.js");

    c.RoutePrefix = string.Empty;

    
    

});

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
