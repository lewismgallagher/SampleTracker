using DAL.Data;
using DAL.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using SampleTracker.Client.Pages;
using SampleTracker.Components;
using BAL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ISampleTrackerDbContext, SampleTrackerDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<RackConfigurationRepo, RackConfigurationRepo>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SampleTrackerDbContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("couldn't connect");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SampleTracker.Client._Imports).Assembly);

app.Run();
