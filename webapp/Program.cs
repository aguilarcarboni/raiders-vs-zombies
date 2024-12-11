using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorApp.Components;
using BlazorApp.Services;
using ZombieSimulation;
using ZombieSimulation.Moves;
using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;
using ZombieSimulation.MovesInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register simulation services
builder.Services.AddSingleton<ISimulationConfig, SimulationConfig>();
builder.Services.AddSingleton<IRandomGenerator, RandomGenerator>();
builder.Services.AddSingleton<IDistanceCalculator, DistanceCalculator>();
builder.Services.AddSingleton<IEntityFactory, EntityFactory>();
builder.Services.AddSingleton<ISimulationInitializer, SimulationInitializer>();
builder.Services.AddSingleton<IEntityManager, EntityManager>();
builder.Services.AddSingleton<IStaminaManager, StaminaManager>();

// Register interaction handlers
builder.Services.AddSingleton<IInteractionHandler, HumanZombieInteractionHandler>();
builder.Services.AddSingleton<IInteractionHandler, HumanFoodInteractionHandler>();
builder.Services.AddSingleton<IInteractionHandler, HumanSanctuaryInteractionHandler>();

builder.Services.AddSingleton<IMovementManager, MovementManager>();
builder.Services.AddSingleton<IInteractionManager, InteractionManager>();
builder.Services.AddSingleton<IHealthManager, HealthManager>();
builder.Services.AddSingleton<ITimeManager, TimeManager>();
builder.Services.AddSingleton<ISimulationController, SimulationController>();
builder.Services.AddSingleton<SimulationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
