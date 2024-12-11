using ZombieSimulation.Entities;

namespace ZombieSimulation.StatsInterfaces
{
    public interface IMove // Interface for movement
    {
        void move(List<IEntity> nearbyEntities, double deltaTime); // Method to move the entity to a new position
    }
}