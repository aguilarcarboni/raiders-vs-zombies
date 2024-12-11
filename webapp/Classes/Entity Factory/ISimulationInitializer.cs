using ZombieSimulation.Entities;

namespace ZombieSimulation.EntityFactories{
    public interface ISimulationInitializer{ // Interface for a simulation initializer
        List<IEntity> CreateInitialEntities(int numHumans, int numZombies, int numFood, int numSanctuaries); // Method to create the initial entities
        void CreateEntitiesAtPosition(List<IEntity> entities, EntityType type, Position position); // Method to create the entities at specific positions
        void CreateEntities(List<IEntity> entities, EntityType type, int count); // Method to create the entities
    }
}