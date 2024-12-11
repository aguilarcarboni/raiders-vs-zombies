using ZombieSimulation.MovesInterfaces;
using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;
namespace ZombieSimulation.Moves{
    public class HealthManager : IHealthManager{ // Class that implements the IHealthManager interface
        private const double HEALTH_DECREASE_RATE = 1; // Constant for the health decrease rate

        public void UpdateHealth(List<IEntity> entities, double deltaTime, IEntityManager entityManager) // Method to decrease the health of the entities
        {
            ValidateParameters(entities, deltaTime, entityManager); // Validate the parameters
            var entitiesToRemove = GetEntitiesToRemove(entities, deltaTime); // Get the entities to remove
            foreach (var entity in entitiesToRemove) // Iterate through the entities to remove
            {
                entityManager.RemoveEntity(entity); // Remove the entity from the entity manager
            }
        }

        private List<IEntity> GetEntitiesToRemove(List<IEntity> entities, double deltaTime) // Method to get the entities to remove
        {
            var entitiesToRemove = new List<IEntity>(); // List to store the entities to remove

            foreach (var entity in entities) // Iterate through the entities
            {
                if (entity is Human human)
                {
                    UpdateEntityHealth(human, deltaTime);
                    ColorLogger.RedLog($"Human stamina: {human.getStamina()}");
                    
                }
                if (ShouldUpdateEntityHealth(entity)) // Check if the entity should have its health updated
                {
                    if (entity is Zombie zombie)
                    {
                        UpdateEntityHealth(zombie, deltaTime);
                        zombie.decreaseStamina(HEALTH_DECREASE_RATE * deltaTime);
                        ColorLogger.RedLog($"Zombie stamina: {zombie.getStamina()}");
                        
                        if (zombie.isDead())
                        {
                            entitiesToRemove.Add(zombie);
                        }
                    }
                    else if (entity is Sanctuary sanctuary)
                    {
                        UpdateEntityHealth(sanctuary, deltaTime);
                        ColorLogger.RedLog($"Sanctuary stamina: {sanctuary.getHealth()}");
                        
                        if (sanctuary.isDestroyed())
                        {
                            entitiesToRemove.Add(sanctuary);
                        }
                    }
                }
            }
            ColorLogger.RedLog($"Entities to remove: {entitiesToRemove.Count}"); // Log the number of entities to remove
            return entitiesToRemove; // Return the list of entities to remove
        }

        private static bool ShouldUpdateEntityHealth(IEntity entity) // Method to check if the entity should have its health updated
        {
            return entity is Zombie || entity is Sanctuary; // Only update health for zombies and sanctuaries
        }

        private static void UpdateEntityHealth(AbstractEntity entity, double deltaTime) // Method to update the health of the entity
        {
            entity.decreaseHealth(HEALTH_DECREASE_RATE * deltaTime); // Decrease the health of the entity
        }

        private void ValidateParameters(List<IEntity> entities, double deltaTime, IEntityManager entityManager) // Method to validate the parameters
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            if (deltaTime <= 0)
                throw new ArgumentException("Delta time must be positive", nameof(deltaTime));
            if (entityManager == null)
                throw new ArgumentNullException(nameof(entityManager));
        }
    }
}