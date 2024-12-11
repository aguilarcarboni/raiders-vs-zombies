using ZombieSimulation.Entities;

namespace ZombieSimulation.StatsInterfaces
{
    public interface ICapacity{ // Interface for capacity
        int GetCapacity(); // Method to return the capacity of the entity
        bool IsFull(); // Method to check if the entity is full
        bool AddOccupant(IEntity entity); // Method to add an occupant to the entity
        bool RemoveOccupant(IEntity entity); // Method to remove an occupant from the entity
        IReadOnlyList<IEntity> GetOccupants(); // Method to return the occupants of the entity
    }
}