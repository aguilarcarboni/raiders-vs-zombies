using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;
namespace ZombieSimulation.EntityFactories{
    public class EntityFactory : IEntityFactory // EntityFactory class that implements the IEntityFactory interface
    {
        private readonly IDistanceCalculator _distanceCalculator; // Property to store the distance calculator of the entity factory
        private readonly ISimulationConfig _config;

        public EntityFactory(IDistanceCalculator distanceCalculator, ISimulationConfig config) // Constructor for the entity factory
        {
            _distanceCalculator = distanceCalculator ?? throw new ArgumentNullException(nameof(distanceCalculator));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public IEntity CreateEntity(EntityType type){ // Factory method
            switch(type){ // Switch statement to create the correct entity
                case EntityType.Human: // Create a new Human object
                    return new Human(_distanceCalculator, _config);
                case EntityType.Zombie: // Create a new Zombie object
                    return new Zombie(_distanceCalculator, _config);
                case EntityType.Food: // Create a new Food object
                    return new Food();
                case EntityType.Sanctuary: // Create a new Sanctuary object
                    return new Sanctuary(_config);
                default: // If the entity type is invalid, throw an exception
                    throw new ArgumentException("Invalid entity type");
            }
        }

        public IEntity CreateEntityAtPosition(EntityType type, Position position){ // Factory method to create an entity at a specific position
            switch(type){ // Switch statement to create the correct entity
                case EntityType.Human: // Create a new Human object at the specified position
                    return new Human(position, _distanceCalculator, _config);
                case EntityType.Zombie: // Create a new Zombie object at the specified position
                    return new Zombie(position, _distanceCalculator, _config);
                case EntityType.Food: // Create a new Food object at the specified position
                    return new Food(position);
                case EntityType.Sanctuary: // Create a new Sanctuary object at the specified position
                    return new Sanctuary(position, _config);
                default: // If the entity type is invalid, throw an exception
                    throw new ArgumentException("Invalid entity type");
            }
        }
    }
}