using Microsoft.Extensions.DependencyInjection;
using ZombieSimulation.Moves;
using ZombieSimulation.MovesInterfaces;
using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;

namespace ZombieSimulation
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services) // ConfigureServices is a method that takes an IServiceCollection as a parameter, IServiceCollection comes from Microsoft.Extensions.DependencyInjection
        {
            // IServiceCollection is like a registry or container where you list/register all the services your application needs
            // Core services
            services.AddSingleton<ISimulationConfig, SimulationConfig>(); // SimulationConfig is a singleton, new SimulationConfig() is a new instance
            services.AddSingleton<IRandomGenerator, RandomGenerator>(); // RandomGenerator is a singleton, new RandomGenerator() is a new instance
            
            services.AddSingleton<IStaminaManager>(_ => new StaminaManager()); // StaminaManager is a singleton, new StaminaManager() is a new instance

            services.AddSingleton<IDistanceCalculator>(_ => new DistanceCalculator()); // DistanceCalculator is a singleton, new DistanceCalculator() is a new instance

            services.AddSingleton<IMovementManager>(sp =>
                new MovementManager(sp.GetRequiredService<IStaminaManager>(), sp.GetRequiredService<IDistanceCalculator>())); // MovementManager is a singleton, new MovementManager(StaminaManager, DistanceCalculator) is a new instance, StaminaManager and DistanceCalculator are singletons

            services.AddSingleton<IInteractionHandler>(_ => new HumanFoodInteractionHandler()); // HumanFoodInteractionHandler is a singleton, new HumanFoodInteractionHandler() is a new instance
            services.AddSingleton<IInteractionHandler>(_ => new HumanZombieInteractionHandler()); // HumanZombieInteractionHandler is a singleton, new HumanZombieInteractionHandler() is a new instance
            services.AddSingleton<IInteractionHandler>(_ => new HumanSanctuaryInteractionHandler()); // HumanSanctuaryInteractionHandler is a singleton, new HumanSanctuaryInteractionHandler() is a new instance

            services.AddSingleton<IInteractionManager>(sp =>
                {
                    var handlers = sp.GetServices<IInteractionHandler>().ToArray();
                    var interactionManager = new InteractionManager(handlers);
                    return interactionManager;
                }); // InteractionManager is a singleton, new InteractionManager(handlers) is a new instance, handlers is an array of IInteractionHandler
            
            services.AddSingleton<IEntityFactory>(sp => new EntityFactory(
                sp.GetRequiredService<IDistanceCalculator>(),
                sp.GetRequiredService<ISimulationConfig>()
            )); // EntityFactory is a singleton, new EntityFactory(DistanceCalculator) is a new instance, DistanceCalculator is a singleton

            services.AddSingleton<ISimulationInitializer>(sp => 
                new SimulationInitializer(sp.GetRequiredService<IEntityFactory>())); // SimulationInitializer is a singleton, new SimulationInitializer(EntityFactory) is a new instance, EntityFactory is a singleton

            services.AddSingleton<IEntityManager>(sp =>
                new EntityManager(sp.GetRequiredService<ISimulationInitializer>())); // EntityManager is a singleton, new EntityManager(SimulationInitializer) is a new instance, SimulationInitializer is a singleton

            services.AddSingleton<IHealthManager>(_ => new HealthManager()); // HealthManager is a singleton, new HealthManager() is a new instance

            services.AddSingleton<ITimeManager>(_ => new TimeManager()); // TimeManager is a singleton, new TimeManager() is a new instance

            services.AddSingleton<ISimulationController>(sp =>
                new SimulationController(
                    sp.GetRequiredService<IEntityManager>(), // EntityManager parameter
                    sp.GetRequiredService<IMovementManager>(), // MovementManager parameter
                    sp.GetRequiredService<IInteractionManager>(), // InteractionManager parameter
                    sp.GetRequiredService<IHealthManager>(), // HealthManager parameter
                    sp.GetRequiredService<ITimeManager>() // TimeManager parameter
                )); // SimulationController is a singleton, new SimulationController(EntityManager, MovementManager, InteractionManager, HealthManager, TimeManager) is a new instance, EntityManager, MovementManager, InteractionManager, HealthManager, and TimeManager are singletons

            return services; // return the services
            }
        }
}
