using ZombieSimulation.StatsInterfaces;

namespace ZombieSimulation.Entities
{
    public abstract class AbstractResource : AbstractEntity, IConsumable // Abstract class that extends the AbstractEntity class and implements the IConsumable interface
    {
        private int _staminaValue; // Property to store the stamina value of the resource
        private int _healthValue; // Property to store the health value of the resource
        private readonly Random _random = new Random(); // Property to store the random number generator
        private const int MIN_STAMINA_VALUE = 30; // Minimum stamina value
        private const int MAX_STAMINA_VALUE = 60; // Maximum stamina value
        private const int MIN_HEALTH_VALUE = 10; // Minimum health value
        private const int MAX_HEALTH_VALUE = 100; // Maximum health value

        protected AbstractResource() : base() // Constructor for random position
        {
            InitializeValues(); // Initialize the values of the resource
        }

        protected AbstractResource(Position position) : base(position) // Constructor for specific position
        {
            InitializeValues(); // Initialize the values of the resource
        }

        private void InitializeValues()
        {
            lock(_random) // Lock the random number generator
            {
                _staminaValue = _random.Next(MIN_STAMINA_VALUE, MAX_STAMINA_VALUE); // Initialize the stamina value of the resource to a random number between MIN_STAMINA_VALUE and MAX_STAMINA_VALUE
                _healthValue = _random.Next(MIN_HEALTH_VALUE, MAX_HEALTH_VALUE); // Initialize the health value of the resource to a random number between MIN_HEALTH_VALUE and MAX_HEALTH_VALUE
            }
        }

        public int getStaminaValue() // Method to return the stamina value of the resource
        {
            return _staminaValue; // Return the stamina value of the resource
        }

        public int getHealthValue() // Method to return the health value of the resource
        {
            return _healthValue; // Return the health value of the resource
        }
    }
}