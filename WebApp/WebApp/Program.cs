using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using WebApp.Context;
using WebApp.Services;
using WebApp.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISubsriptionService, SubscriptionService>();
builder.Services.AddScoped<IRevenueService, RevenueService>();

builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseInMemoryStorage());

builder.Services.AddHangfireServer();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Description = "<a href='/hangfire'>Hangfire</a>";
        
        document.Components ??= new OpenApiComponents();

        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        
        document.Components.SecuritySchemes!.Add("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Add token here",
        });

        document.Security ??= new List<OpenApiSecurityRequirement>();

        var requirement = new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        };
        document.Security.Add(requirement);
        return Task.CompletedTask;
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
    };
});

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
            options.InjectStylesheet("/assets/swagger.css");
        });
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    AppPath = "/swagger"
});

RecurringJob.AddOrUpdate<ContractExpirationJob>(
    "ContractExpiration",
    job => job.CancelExpiredContracts(),
    Cron.Daily
    );

RecurringJob.AddOrUpdate<SubscriptionExpirationJob>(
    "SubscriptionExpiration",
    job => job.CancelExpiredSubscriptions(),
    Cron.Daily
    );

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();