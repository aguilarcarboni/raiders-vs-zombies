using ZombieSimulation.Entities;

namespace ZombieSimulation.MovesInterfaces
{
    public interface IMovementManager
    {
        void UpdatePositions(List<IEntity> entities, double deltaTime); // Method to update the positions of the entities
        List<IEntity> GetNearbyEntities(Position center, int range, List<IEntity> entities); // Method to get the nearby entities within the range
    }
}