using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves
{
    public class HumanFoodInteractionHandler : IInteractionHandler
    {
        public bool CanHandle(IEntity entity1, IEntity entity2) // Method to check if the interaction can be handled
        {
            var (human, food) = GetEntities(entity1, entity2); // Get the human and food entities
            return human != null && food != null; // Return true if the human and food entities are not null, otherwise return false
        }

        public void Handle(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove) // Method to handle the interaction
        {
            ValidateParameters(entity1, entity2, entitiesToRemove); // Validate the parameters
            var (human, food) = GetEntities(entity1, entity2); // Get the human and food entities
            if (human == null || food == null) // Check if the human or food entities are null
                throw new ArgumentException("Human or food entity not found"); // Throw an exception
            HumanEatsFood(human, food); // Handle the human eating the food
            entitiesToRemove.Add(food); // Add the food to the list of entities to remove
        }

        private void ValidateParameters(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove) // Method to validate the parameters
        {
            if (entity1 == null) // Check if the entity1 is null
                throw new ArgumentNullException(nameof(entity1)); // Throw an exception
            if (entity2 == null) // Check if the entity2 is null
                throw new ArgumentNullException(nameof(entity2));
            if (entitiesToRemove == null) // Check if the entitiesToRemove list is null
                throw new ArgumentNullException(nameof(entitiesToRemove)); // Throw an exception
        }

        private (Human? human, Food? food) GetEntities(IEntity entity1, IEntity entity2) // Method to get the entities
        {
            Human? human = null; // Initialize the human entity to null
            Food? food = null; // Initialize the food entity to null

            if (entity1 is Human h1 && entity2 is Food f1) // Check if the first entity is a human and the second entity is a food
            {
                human = h1; // Set the human entity to the first entity
                food = f1; // Set the food entity to the second entity
            }
            else if (entity1 is Food f2 && entity2 is Human h2) // Check if the first entity is a food and the second entity is a human
            {
                human = h2; // Set the human entity to the second entity
                food = f2; // Set the food entity to the first entity
            }

            return (human, food); // Return the human and food entities
        }

        private void HumanEatsFood(Human human, Food food) // Method to handle the human eating the food
        {
            ColorLogger.YellowLog("Human eats food");
            human.increaseHealth(food.getHealthValue()); // Increase the health of the human
            human.increaseStamina(food.getStaminaValue()); // Increase the stamina of the human
        }
    }
}