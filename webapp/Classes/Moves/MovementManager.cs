using ZombieSimulation.Entities;
using ZombieSimulation.StatsInterfaces;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves
{
    public class MovementManager : IMovementManager
    {
        private const int VISION_RANGE = 30; // Constant to store the vision range of the entities
        private readonly IStaminaManager _staminaManager; // Private field to store the stamina manager
        private readonly IDistanceCalculator _distanceCalculator; // Private field to store the distance calculator

        public MovementManager(IStaminaManager staminaManager, IDistanceCalculator distanceCalculator) // Constructor for the movement manager
        {
            _staminaManager = staminaManager ?? throw new ArgumentNullException(nameof(staminaManager)); // Initialize the stamina manager
            _distanceCalculator = distanceCalculator ?? throw new ArgumentNullException(nameof(distanceCalculator)); // Initialize the distance calculator
        }

        public void UpdatePositions(List<IEntity> entities, double deltaTime) // Method to update the positions of the entities
        {
            if (entities == null) // Check if the entities list is null
                throw new ArgumentNullException(nameof(entities)); // Throw an exception if the entities list is null
            if (deltaTime <= 0) // Check if the delta time is less than or equal to 0
                throw new ArgumentException("Delta time must be greater than 0", nameof(deltaTime)); // Throw an exception if the delta time is less than or equal to 0
            
            foreach (var entity in entities) // Loop through the entities
            {
                UpdateEntityPosition(entity, entities,deltaTime); // Update the position of the entity
            }
        }

        private void UpdateEntityPosition(IEntity entity, List<IEntity> entities, double deltaTime) // Method to update the position of the entity
        {
            var nearbyEntities = GetNearbyEntities(entity.Position, VISION_RANGE, entities); // Get the nearby entities
            if (entity is IMove moveable) // Check if the entity is moveable
                moveable.move(nearbyEntities, deltaTime); // Move the entity
            _staminaManager.DecreasingStamina(entity, deltaTime); // Decrease the stamina of the entity
        }

        public List<IEntity> GetNearbyEntities(Position center, int range, List<IEntity> entities) // Method to get the nearby entities within the range
        {
            if (entities == null) // Check if the entities list is null
                throw new ArgumentNullException(nameof(entities)); // Throw an exception if the entities list is null
            if (range < 0) // Check if the range is less than 0
                throw new ArgumentException("Range must be positive", nameof(range)); // Throw an exception if the range is less than 0
            return entities.Where(e => IsEntityInRange(center, e.Position, range)).ToList();
        }

        private bool IsEntityInRange(Position center, Position position, int range) // Method to check if the entity is within the range
        {
            return _distanceCalculator.CalculateDistance(center, position) <= range; // Return true if the entity is within the range, otherwise return false
        }
    }
}