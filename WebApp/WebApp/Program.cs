using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
        });
}

app.UseAuthorization();

app.MapControllers();

app.Run();