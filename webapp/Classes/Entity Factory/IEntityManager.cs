using ZombieSimulation.Entities;

namespace ZombieSimulation.EntityFactories{
    public interface IEntityManager
    {
        void InitializeList(int numHumans, int numZombies, int numFood, int numSanctuaries); // Method to initialize the list
        void AddEntities(EntityType type, int count); // Method to add entities to the list
        void AddEntityAtPosition(EntityType type, Position position); // Method to add an entity at a specific position
        List<IEntity> GetEntities(); // Method to get the entities
        void RemoveEntity(IEntity entity); // Method to remove an entity from the list
        void Clear(); // Method to clear the entities list
        int Count(); // Method to count the entities of the list
    }
}