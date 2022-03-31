using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot(builder.Configuration);


builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{

    config
    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
});


var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseOcelot();

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
