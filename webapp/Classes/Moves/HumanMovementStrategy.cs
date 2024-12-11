using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves{
    public class HumanMovementStrategy : MovementStrategy{
        private const int LOW_HEALTH_THRESHOLD = 10; // Constant for the low health threshold
        public HumanMovementStrategy(IDistanceCalculator distanceCalculator) : base(distanceCalculator){}// Constructor for the human movement strategy
        
        public override Position DecideNextMove(IEntity entity, List<IEntity> nearbyEntities) // Method to move the human
        {   
            if (entity is not Human human) // Check if the entity is a Human
                throw new ArgumentException("Entity is not a Human"); // Throw an exception
            
            if (nearbyEntities == null) // Check if the nearby entities list is null
                throw new ArgumentNullException(nameof(nearbyEntities)); // Throw an exception

            var nearbyZombie = FindNearestEntity<Zombie>(human.Position, nearbyEntities); // Find the nearest zombie

            if (IsLowHealth(human)) // If the human's health is less than 50
            {
                ColorLogger.CyanLog("Human is low on health, searching for food");
                var nearestFood = FindNearestEntity<Food>(human.Position, nearbyEntities); // Find the nearest food

                if (nearestFood != null){ // If the nearest food is found
                    return CalculateNewPosition(human.Position, nearestFood.Position); // Move the human to the nearest food
                }
            }

            if (nearbyZombie != null){ // If the nearest zombie is found
                var nearestSanctuary = FindNearestEntity<Sanctuary>(human.Position, nearbyEntities); // Find the nearest sanctuary

                if (nearestSanctuary != null){ // If the nearest sanctuary is found
                    return CalculateNewPosition(human.Position, nearestSanctuary.Position); // Move the human to the nearest sanctuary
                }
            }

            return RandomMove(human.Position); // Move the human randomly
        }

        private bool IsLowHealth(Human human){ // Method to check if the human's health is less than 50
            return human.getStamina() < LOW_HEALTH_THRESHOLD; // Return true if the human's health is less than 50, otherwise return false
        }

        private T? FindNearestEntity<T>(Position currentPosition, List<IEntity> entities) where T : class, IEntity{ // Method to find the nearest entity of a given type
            return entities.OfType<T>().OrderBy(e => CalculateDistance(currentPosition, e.Position)).FirstOrDefault(); // Return the nearest entity of the given type, otherwise return null
        }
    }
}