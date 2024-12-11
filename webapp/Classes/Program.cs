using Microsoft.Extensions.DependencyInjection;
using ZombieSimulation.Moves;
using ZombieSimulation.Entities;
using ZombieSimulation.EntityFactories;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation
{
    public class Program
    {
        private readonly ISimulationController _simulationController; // Simulation controller
        private readonly ISimulationConfig _config; // Simulation config
        private readonly IRandomGenerator _randomGenerator; // Random generator
        private int _lastSpawnTime; // Last spawn time
        private bool _isSimulationRunning; // Flag to check if the simulation is running
        private int _timeStep; // Time step
        public Program(ISimulationController simulationController, ISimulationConfig config, IRandomGenerator randomGenerator) // Constructor
        {
            _simulationController = simulationController ?? throw new ArgumentNullException(nameof(simulationController)); // Initialize the simulation controller
            _config = config ?? throw new ArgumentNullException(nameof(config)); // Initialize the simulation config
            _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator)); // Initialize the random generator
        }
        public static void Main() // Main method
        {
            var services = ConfigureServices(); // Configure services
            var program = services.GetRequiredService<Program>(); // Get the program
            program.Run(); // Run the program
        }

        private static IServiceProvider ConfigureServices() // Configure services
        {
            var services = new ServiceCollection(); // Create a service collection
            ServiceConfiguration.ConfigureServices(services); // Configure the services
            services.AddSingleton<Program>(); // Add the program
            return services.BuildServiceProvider(); // Build the service provider
        }

        public void Run() // Run the program
        {
            try{
                InitializeSimulation(); // Initialize the simulation
                RunSimulationLoop(); // Run the simulation loop
                DisplayFinalStats(); // Display the final stats
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}"); // Display the error message
            }
        }

        private void InitializeSimulation() // Initialize the simulation
        {
            _simulationController.Initialize(_config.InitialZombies, _config.InitialHumans, _config.InitialSanctuaries, _config.InitialFood); // Initialize the simulation controller
            Console.WriteLine("Simulation started! Press Spacebar to stop, 'S' for stats."); // Display the message
            _isSimulationRunning = true; // Set the flag to true
            _timeStep = 0; // Set the time step to 0
            _lastSpawnTime = 0; // Set the last spawn time to 0
        }

        private void RunSimulationLoop() // Run the simulation loop
        {
            while (_isSimulationRunning) // While the simulation is running
            {
                UpdateSimulation(); // Update the simulation
                HandleSpawning(); // Handle the spawning
                HandleUserInput(); // Handle the user input
                CheckSimulationEnd(); // Check the simulation end
                Thread.Sleep(_config.UpdateInterval); // Sleep for the update interval
            }
        }

        private void UpdateSimulation() // Update the simulation
        {
            _simulationController.Update(); // Update the simulation controller
            _timeStep++; // Increment the time step
        }

        private void HandleSpawning() // Handle the spawning
        {
            if (_timeStep - _lastSpawnTime >= _config.SpawnInterval) // If the time step minus the last spawn time is greater than or equal to the spawn interval
            {
                SpawnEntities(); // Spawn the entities
                _lastSpawnTime = _timeStep; // Set the last spawn time to the time step
            }
        }

        private void HandleUserInput() // Handle the user input
        {
            if (!Console.KeyAvailable) // If the console key is not available
                return; // Return
            var key = Console.ReadKey(true).Key; // Get the key
            switch (key) // Switch the key
            {
                case ConsoleKey.Spacebar: // If the key is the spacebar
                    _isSimulationRunning = false; // Set the flag to false
                    break; // Break
                case ConsoleKey.S: // If the key is the 'S' key
                    DisplayStats(); // Display the stats
                    break; // Break
            }
        }

        private void CheckSimulationEnd() // Check the simulation end
        {
            if (!_simulationController.GetEntities().Any(entity => entity is Human)) // If there are no humans left
            {
                Console.WriteLine("All humans are dead! Simulation ended."); // Display the message
                _isSimulationRunning = false; // Set the flag to false
            }
        }

        private void SpawnEntities() // Spawn the entities
        {
            var counts = new Dictionary<EntityType, int> // Create a dictionary to store the counts
            {
                { EntityType.Human, _randomGenerator.Random(1, 4) }, // Add the humans
                { EntityType.Zombie, _randomGenerator.Random(1, 4) }, // Add the zombies
                { EntityType.Sanctuary, _randomGenerator.Random(1, 4) }, // Add the sanctuaries
                { EntityType.Food, _randomGenerator.Random(1, 4) } // Add the food
            };
            foreach (var (type, count) in counts) // For each type and count in the dictionary
            {
                _simulationController.AddEntity(type, count); // Spawn the entities
            }
            LogSpawnedEntities(counts); // Log the spawned entities
        }

        private void DisplayStats() // Display the stats
        {
            var entities = _simulationController.GetEntities(); // Get the entities
            Console.WriteLine("\nCurrent Statistics:"); // Display the message
            Console.WriteLine($"Humans: {entities.Count(e => e is Human)}"); // Display the humans
            Console.WriteLine($"Zombies: {entities.Count(e => e is Zombie)}"); // Display the zombies
            Console.WriteLine($"Food: {entities.Count(e => e is Food)}"); // Display the food
            Console.WriteLine($"Sanctuaries: {entities.Count(e => e is Sanctuary)}"); // Display the sanctuaries
        }

        private void DisplayFinalStats() // Display the final stats
        {
            Console.WriteLine("\nFinal Stats:"); // Display the message
            DisplayStats(); // Display the stats
        }

        private void LogSpawnedEntities(Dictionary<EntityType, int> counts) // Log the spawned entities
        {
            Console.WriteLine($"\nSpawned new entities: " + // Display the message
               $"{counts[EntityType.Human]} Humans, " + // Display the humans
               $"{counts[EntityType.Zombie]} Zombies, " + // Display the zombies
               $"{counts[EntityType.Food]} Food, " + // Display the food
               $"{counts[EntityType.Sanctuary]} Sanctuaries"); // Display the sanctuaries
        }
    }
}