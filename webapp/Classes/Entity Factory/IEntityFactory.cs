using ZombieSimulation.Entities;

namespace ZombieSimulation.EntityFactories{
    public interface IEntityFactory // Interface for an entity factory
    {
        IEntity CreateEntity(EntityType type); // Method to create an entity
        IEntity CreateEntityAtPosition(EntityType type, Position position); // Method to create an entity at a specific position
    }
}