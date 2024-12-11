using ZombieSimulation.StatsInterfaces;

namespace ZombieSimulation.Entities
{
    public abstract class AbstractEntity : IEntity, IHealth // Abstract class that implements the IEntity and IHealth interfaces, used to create the position of the entity and manage the health of the entity
    {
        private static readonly Random _random = new Random(); // Property to store the random number generator
        private Position _position; // Property to store the position of the entity
        private double _health; // Property to store the health of the entity
        private const int MIN_HEALTH = 80; // Constant to store the minimum health of the entity
        private const int MAX_HEALTH = 100; // Constant to store the maximum health of the entity
        private const int MAX_X = 100; // Constant to store the maximum x coordinate of the entity
        private const int MAX_Y = 100; // Constant to store the maximum y coordinate of the entity

        protected AbstractEntity() // Generate a random position for the entity
        {
            lock(_random){ // Lock the random number generator to ensure thread safety
                int randomX = _random.Next(0, MAX_X); // Generate a random x coordinate
                int randomY = _random.Next(0, MAX_Y); // Generate a random y coordinate
                _position = new Position(randomX, randomY); // Initialize the position of the entity
                _health = _random.Next(MIN_HEALTH, MAX_HEALTH); // Initialize the health of the entity to a random number between MIN_HEALTH and MAX_HEALTH
            }
        }

        // Constructor with specific position, for more control over the position of the entity, to create entities at a specific position
        protected AbstractEntity(Position position) // Constructor with specific position
        {
            if (position.x < 0 || position.x >= MAX_X || position.y < 0 || position.y >= MAX_Y) // Check if the position is within the bounds
            {
                throw new ArgumentOutOfRangeException("Position is out of bounds"); // Throw an exception if the position is out of bounds
            }
            _position = position; // Initialize the position of the entity
            lock(_random){ // Lock the random number generator to ensure thread safety
                _health = _random.Next(MIN_HEALTH, MAX_HEALTH); // Initialize the health of the entity to a random number between MIN_HEALTH and MAX_HEALTH
            }
        }

        // Implementing IEntity interface
        public abstract string getBody(); // Abstract method to return the body of the entity
        
        public Position Position // Property to get and set the position of the entity
        {
            get => _position; // Getter to return the position of the entity
            set => _position = value; // Setter to set the position of the entity
        }

        public double getHealth(){ // Method to return the health of the entity
            return _health; // Return the health of the entity
        }

        public void decreaseHealth(double amount){
            _health = Math.Max(0, _health - amount); // Decrease the health of the entity, but not below 0
        }

        public void increaseHealth(double amount){
            _health = Math.Min(100, _health + amount); // Increase the health of the entity, but not above 100
        }
    }
}