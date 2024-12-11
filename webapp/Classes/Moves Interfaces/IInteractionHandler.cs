using ZombieSimulation.Entities;

namespace ZombieSimulation.MovesInterfaces
{
    public interface IInteractionHandler // Interface for the interaction handler
    {
        bool CanHandle(IEntity entity1, IEntity entity2); // Method to check if the interaction can be handled
        void Handle(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove); // Method to handle the interaction
    }
}
