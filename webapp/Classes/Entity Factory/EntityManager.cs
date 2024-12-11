using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;

namespace ZombieSimulation.EntityFactories
{

public class EntityManager : IEntityManager // Class for the entity manager
{
        private List<IEntity> _entities = new(); // List of entities
        private readonly ISimulationInitializer _initializer; // Simulation initializer

        public EntityManager(ISimulationInitializer initializer) // Constructor for the entity manager
        {
            _initializer = initializer ?? throw new ArgumentNullException(nameof(initializer)); // Initialize the initializer
        }

        public void InitializeList(int numHumans, int numZombies, int numFood, int numSanctuaries) // Method to initialize the list
        {
            var initialEntities = _initializer.CreateInitialEntities(numHumans, numZombies, numFood, numSanctuaries); // Create the initial entities
            _entities.Clear(); // Clear the entities list
            _entities.AddRange(initialEntities); // Add the initial entities to the entities list
        }

        public void AddEntities(EntityType type, int count) // Method to add entities to the list
        {
            if (count < 0) // Check if the number of entities is negative
                throw new ArgumentException("Number of entities cannot be negative"); // Throw an exception if the number of entities is negative
            _initializer.CreateEntities(_entities, type, count); // Add the entities to the entities list
        }

        public void AddEntityAtPosition(EntityType type, Position position) // Method to add an entity at a specific position
        {
            if (position == null) // Check if the position is null
                throw new ArgumentNullException(nameof(position)); // Throw an exception if the position is null
            _initializer.CreateEntitiesAtPosition(_entities, type, position); // Add the entities to the entities list
        }

        public List<IEntity> GetEntities() // Method to get the entities
        {
            return _entities; // Return the entities list
        }

        public void RemoveEntity(IEntity entity) // Method to remove an entity from the list
        {
            if (entity == null) // Check if the entity is null
                throw new ArgumentNullException(nameof(entity)); // Throw an exception if the entity is null
            _entities.Remove(entity); // Remove the entity from the entities list
        }

        public void Clear() // Method to clear the entities list
        {
            _entities.Clear(); // Clear the entities list
        }

        public int Count() // Method to count the entities
        {
            return _entities.Count; // Return the number of entities
        }
    }
}