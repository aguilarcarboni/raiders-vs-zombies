using ZombieSimulation.Entities;

namespace ZombieSimulation.MovesInterfaces{
    public interface IStaminaManager{ // Interface for managing the stamina of an entity
        void DecreasingStamina(IEntity entity, double deltaTime); // Method to decrease the stamina of an entity
    }
}