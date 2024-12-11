using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves{
    public class StaminaManager : IStaminaManager{
        private const double STAMINA_DECREASE_RATE = 1; // Constant for the stamina decrease rate

        public void DecreasingStamina(IEntity entity, double deltaTime){ // Method to decrease the stamina of an entity
            if (entity is Human human && human.getStamina() > 0) // Check if the entity is human and the stamina is greater than 0
            {
                human.decreaseStamina(STAMINA_DECREASE_RATE * deltaTime); // Decrease the stamina of the human
            }
        }
    }
}