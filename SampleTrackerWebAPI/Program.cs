using DAL.Data;
using DAL.Data.Interfaces;
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.AspNetCore.Identity;
using SampleTrackerWebAPI.RackConfiguration;
using SampleTrackerWebAPI.Samples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200");
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ISampleTrackerDbContext, SampleTrackerDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SampleTrackerDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<RackConfigurationService, RackConfigurationService>();
builder.Services.AddScoped<SampleTypeConfigurationService, SampleTypeConfigurationService>();
builder.Services.AddScoped<SampleRackService, SampleRackService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    
}
app.UseCors("CORSPolicy");
//app.UseHttpsRedirection();

app.MapRackConfiguration();
app.MapSamples();

app.Run();

