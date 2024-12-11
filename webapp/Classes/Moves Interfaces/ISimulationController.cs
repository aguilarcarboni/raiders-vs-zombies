using ZombieSimulation.Entities;

namespace ZombieSimulation.MovesInterfaces
{
    public interface ISimulationController
    {
        void Initialize(int numHumans, int numZombies, int numFood, int numSanctuaries); // Method to initialize the entity
        void Update(); // Method to update the simulation
        void AddEntity(EntityType type, int count); // Method to add an entity to the simulation
        IReadOnlyList<IEntity> GetEntities(); // Method to get the entities
        void AddEntityAtPosition(EntityType type, Position position); // Method to add an entity at a specific position
    }
}