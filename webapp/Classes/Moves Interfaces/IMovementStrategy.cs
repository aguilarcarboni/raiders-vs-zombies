using ZombieSimulation.Entities;

namespace ZombieSimulation.MovesInterfaces
{
    public interface IMovementStrategy{ // Interface for movement strategy
        Position DecideNextMove(IEntity entity, List<IEntity> nearbyEntities); // Method to move the entity
    }
}
