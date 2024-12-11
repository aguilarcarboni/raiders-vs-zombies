using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves
{
    public abstract class MovementStrategy : IMovementStrategy // Abstract class for movement strategy
    {
        private readonly Random _random; // Random number generator
        private readonly IDistanceCalculator _distanceCalculator; // Distance calculator
        private const int GRID_SIZE = 100; // Grid size
        public MovementStrategy(IDistanceCalculator distanceCalculator) // Constructor for the movement strategy
        {
            _random = new Random(); // Initialize the random number generator
            _distanceCalculator = distanceCalculator ?? throw new ArgumentNullException(nameof(distanceCalculator)); // Initialize the distance calculator
        }

        public abstract Position DecideNextMove(IEntity entity, List<IEntity> nearbyEntities); // Abstract method to move the entity

        protected Position CalculateNewPosition(Position currentPosition, Position targetPosition) // Method to calculate the new position
        {
            var direction = GetMoveDirection(currentPosition, targetPosition); // Get the movement direction
            return CreateNewPosition(currentPosition, direction.x, direction.y); // Create the new position
        }

        protected Position RandomMove(Position currentPosition) // Method to move the entity randomly
        {
            int dx = _random.Next(-1, 2);
            int dy = _random.Next(-1, 2);
            return CreateNewPosition(currentPosition, dx, dy);
        }

        private (int x, int y) GetMoveDirection(Position currentPosition, Position targetPosition) // Method to get the movement direction
        {
            int dx = targetPosition.x - currentPosition.x; // Calculate the difference in the x-coordinate
            int dy = targetPosition.y - currentPosition.y; // Calculate the difference in the y-coordinate
            return (Math.Sign(dx), Math.Sign(dy)); // Return the direction
        }

        private Position CreateNewPosition(Position currentPosition, int dx, int dy) // Method to create the new position
        {
            int newX = Math.Clamp(currentPosition.x + dx, 0, GRID_SIZE - 1); // Clamp the new x-coordinate
            int newY = Math.Clamp(currentPosition.y + dy, 0, GRID_SIZE - 1); // Clamp the new y-coordinate
            return new Position(newX, newY); // Return the new position
        }

        protected double CalculateDistance(Position position1, Position position2) // Method to calculate the distance between two positions
        {
            return _distanceCalculator.CalculateDistance(position1, position2); // Calculate the distance
        }
    }
}