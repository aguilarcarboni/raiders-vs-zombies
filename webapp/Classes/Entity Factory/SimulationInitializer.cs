using ZombieSimulation.Entities;

namespace ZombieSimulation.EntityFactories{
    public class SimulationInitializer : ISimulationInitializer{ // SimulationInitializer class that implements the ISimulationInitializer interface
        private readonly IEntityFactory _entityFactory; // Property to store the entity factory
        public SimulationInitializer(IEntityFactory entityFactory) // Constructor for the SimulationInitializer
        {
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory)); // Initialize the entity factory
        }
        public List<IEntity> CreateInitialEntities(int numHumans, int numZombies, int numFood, int numSanctuaries){ // Method to create the initial entities
            ValidateEntityCounts(numHumans, numZombies, numFood, numSanctuaries); // Validate the entity counts
            var entities = new List<IEntity>(); // Initialize the entities list

            CreateEntities(entities, EntityType.Human, numHumans); // Create the human entities
            CreateEntities(entities, EntityType.Zombie, numZombies); // Create the zombie entities
            CreateEntities(entities, EntityType.Food, numFood); // Create the food entities
            CreateEntities(entities, EntityType.Sanctuary, numSanctuaries); // Create the sanctuary entities

            return entities; // Return the entities list
        }

        public void CreateEntities(List<IEntity> entities, EntityType type, int count) // Method to create the entities
        {
            if (entities == null) // Check if the entities list is null
                throw new ArgumentNullException(nameof(entities)); // Throw an exception if the entities list is null
            if (count < 0) // Check if the number of entities is negative
                throw new ArgumentException("Number of entities cannot be negative"); // Throw an exception if the number of entities is negative
            for(int i = 0; i < count; i++){ // Loop through the number of entities to create
                entities.Add(_entityFactory.CreateEntity(type)); // Add the entity to the entities list
            }
        }

        public void CreateEntitiesAtPosition(List<IEntity> entities, EntityType type, Position position) // Method to create the entities at specific positions
        {
            if (entities == null) // Check if the entities list is null
                throw new ArgumentNullException(nameof(entities)); // Throw an exception if the entities list is null
            if (position == null) // Check if the position is null
                throw new ArgumentNullException(nameof(position)); // Throw an exception if the position is null
            entities.Add(_entityFactory.CreateEntityAtPosition(type, position)); // Add the entity to the entities list
        }

        private void ValidateEntityCounts(int numHumans, int numZombies, int numFood, int numSanctuaries) // Method to validate the entity counts
        {
            if (numHumans < 0 || numZombies < 0 || numFood < 0 || numSanctuaries < 0) // Check if the number of entities is negative
                throw new ArgumentException("Number of entities cannot be negative"); // Throw an exception if the number of entities is negative
        }
    }
}