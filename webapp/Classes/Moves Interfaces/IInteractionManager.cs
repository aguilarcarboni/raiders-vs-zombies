using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;
namespace ZombieSimulation.MovesInterfaces
{
    public interface IInteractionManager // Interface for the interaction manager
    {
        void HandleInteractions(IReadOnlyList<IEntity> entities, IEntityManager entityManager); // Method to handle the interactions
    }
}