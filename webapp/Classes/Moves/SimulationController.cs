using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;
using ZombieSimulation.StatsInterfaces;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves
{
    public class SimulationController : ISimulationController // Class for the simulation controller
    {
        private const int GRID_SIZE = 100; // Constant to store the size of the grid
        private readonly IEntityManager _entityManager; // Property to store the entity manager
        private readonly IMovementManager _movementManager; // Property to store the movement manager
        private readonly IInteractionManager _interactionManager; // Property to store the interaction manager
        private readonly IHealthManager _healthManager; // Property to store the health manager
        private readonly ITimeManager _timeManager; // Property to store the time manager
        public SimulationController(IEntityManager entityManager, IMovementManager movementManager, IInteractionManager interactionManager, IHealthManager healthManager, ITimeManager timeManager){ // Private constructor to prevent direct instantiation
            _entityManager = entityManager ?? throw new ArgumentNullException(nameof(entityManager)); // Initialize the entity manager
            _movementManager = movementManager ?? throw new ArgumentNullException(nameof(movementManager)); // Initialize the movement manager
            _interactionManager = interactionManager ?? throw new ArgumentNullException(nameof(interactionManager)); // Initialize the interaction manager
            _healthManager = healthManager ?? throw new ArgumentNullException(nameof(healthManager)); // Initialize the health manager
            _timeManager = timeManager ?? throw new ArgumentNullException(nameof(timeManager)); // Initialize the time manager
        } // Constructor for the SimulationController

        public void Initialize(int numHumans, int numZombies, int numFood, int numSanctuaries) // Method to initialize the SimulationController
        {
            ValidateInitializationParameters(numHumans, numZombies, numFood, numSanctuaries); // Validate the initialization parameters
            _entityManager.InitializeList(numHumans, numZombies, numFood, numSanctuaries); // Initialize the entities list
        }

        private void ValidateInitializationParameters(int numHumans, int numZombies, int numFood, int numSanctuaries) // Method to validate the initialization parameters
        {
            if (numHumans < 0) // Check if the number of humans is less than 0
                throw new ArgumentException("Number of humans must be non-negative", nameof(numHumans)); // Throw an argument exception if the number of humans is less than 0
            if (numZombies < 0) // Check if the number of zombies is less than 0
                throw new ArgumentException("Number of zombies must be non-negative", nameof(numZombies)); // Throw an argument exception if the number of zombies is less than 0
            if (numFood < 0) // Check if the number of food is less than 0
                throw new ArgumentException("Number of food must be non-negative", nameof(numFood)); // Throw an argument exception if the number of food is less than 0
            if (numSanctuaries < 0) // Check if the number of sanctuaries is less than 0
                throw new ArgumentException("Number of sanctuaries must be non-negative", nameof(numSanctuaries)); // Throw an argument exception if the number of sanctuaries is less than 0
        }

        public void AddEntityAtPosition(EntityType type, Position position) // Method to add an entity at a specific position
        {
            ValidatePosition(position); // Validate the position
            _entityManager.AddEntityAtPosition(type, position); // Add the entity to the entities list
        }

        private void ValidatePosition(Position position) // Method to validate the position
        {
            if (position.x < 0 || position.x >= GRID_SIZE || // Check if the x position is less than 0 or greater than or equal to the grid size
                position.y < 0 || position.y >= GRID_SIZE) // Check if the y position is less than 0 or greater than or equal to the grid size
                throw new ArgumentException("Position must be within grid bounds"); // Throw an argument exception if the position is not within the grid bounds
        }

        public void Update() // Process each entity's movement and interactions
        {
            _timeManager.UpdateTime(); // Update the time
            var deltaTime = _timeManager.GetDeltaTime(); // Get the delta time
            var entities = _entityManager.GetEntities(); // Get the entities
            UpdateSimulationState(entities, deltaTime); // Update the simulation state
        }

        private void UpdateSimulationState(List<IEntity> entities, double deltaTime) // Method to update the simulation state
        {
            _healthManager.UpdateHealth(entities, deltaTime, _entityManager); // Update the health of the entities over the delta time
            _movementManager.UpdatePositions(entities, deltaTime); // Update the positions of the entities
            _interactionManager.HandleInteractions(entities, _entityManager); // Handle the interactions
        }

        public static int GetGridSize() // Method to get the size of the grid
        {
            return GRID_SIZE; // Return the size of the grid
        }

        public void AddEntity(EntityType type, int count) // Method to add an entity to the entities list
        {
            if (count < 0) // Check if the number of entities is less than 0
                throw new ArgumentException("Number of entities cannot be negative", nameof(count)); // Throw an argument exception if the number of entities is less than 0
            _entityManager.AddEntities(type, count); // Add the entity to the entities list
        }

        public IReadOnlyList<IEntity> GetEntities() // Method to get the entities
        {
            return _entityManager.GetEntities().AsReadOnly(); // Return the entities list
        }
    }
}