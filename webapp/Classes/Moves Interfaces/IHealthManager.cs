using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;

namespace ZombieSimulation.MovesInterfaces{
    public interface IHealthManager{ // Interface for decreasing the health of an entity
        void UpdateHealth(List<IEntity> entities, double deltaTime, IEntityManager entityManager); // Method to decrease the health of an entity
    }
}