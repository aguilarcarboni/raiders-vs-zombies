using ZombieSimulation.StatsInterfaces;
using ZombieSimulation.Moves;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Entities
{
    public abstract class AbstractCreature : AbstractEntity, IMove, ISpeed // AbstractCreature class that extends AbstractEntity, implements IMove and ISpeed interfaces
    {
        private int _speed; // Property to store the speed of the creature
        private static readonly Random _random = new Random(); // Property to store the random number generator
        private readonly IMovementStrategy _movementStrategy; // Property to store the movement strategy of the creature
        private readonly IDistanceCalculator _distanceCalculator; // Property to store the distance calculator of the creature

        protected IDistanceCalculator DistanceCalculator => _distanceCalculator; // Property to access the distance calculator of the creature

        // Constructor for random position
        protected AbstractCreature(IDistanceCalculator distanceCalculator) : base() // Calls AbstractEntity with no position, can only be called by subclasses
        {
            _distanceCalculator = distanceCalculator ?? throw new ArgumentNullException(nameof(distanceCalculator)); // Initialize the distance calculator of the creature
            _movementStrategy = CreateMovementStrategy(); // Initialize the movement strategy of the creature
        }

        // Constructor for specific position
        protected AbstractCreature(Position position, IDistanceCalculator distanceCalculator) : base(position)  // Calls AbstractEntity with position, can only be called by subclasses
        {
            _distanceCalculator = distanceCalculator ?? throw new ArgumentNullException(nameof(distanceCalculator)); // Initialize the distance calculator of the creature
            _movementStrategy = CreateMovementStrategy(); // Initialize the movement strategy of the creature
        }

        // Add protected setter for speed
        protected void SetSpeed(int speed)
        {
            _speed = speed;
        }

        public int getSpeed(){ // Method to return the speed of the creature
            return _speed; // Return the speed of the creature
        }

        public void move(List<IEntity> nearbyEntities, double deltaTime){ // Method to move the creature to a new position

            if (nearbyEntities == null){ // If the nearby entities list is null
                throw new ArgumentNullException(nameof(nearbyEntities)); // Throw an exception
            }

            Position newPosition = _movementStrategy.DecideNextMove(this, nearbyEntities); // Move the creature to a new position
            
            Position = GetNewPosition(Position, newPosition, deltaTime); // Update the position of the creature
        }

        private Position GetNewPosition(Position current, Position next, double deltaTime) // Method to calculate the distance between the current and next positions
        {
            double dx = next.x - current.x; // Calculate the difference in the x-coordinates
            double dy = next.y - current.y; // Calculate the difference in the y-coordinates

            double distance = Math.Sqrt(dx * dx + dy * dy); // Calculate the distance between the current position and the new position

            if (distance <= 0){ // If the distance is 0
                return current;
            }

            double normalizedDx = dx / distance; // Calculate the normalized difference in the x-coordinates
            double normalizedDy = dy / distance; // Calculate the normalized difference in the y-coordinates

            double moveDistance = Math.Min(distance,_speed * deltaTime); // Calculate the move distance

            int newX = Position.x + (int)(normalizedDx * moveDistance); // Calculate the new x-coordinate
            int newY = Position.y + (int)(normalizedDy * moveDistance); // Calculate the new y-coordinate

            newX = Math.Clamp(newX, 0, SimulationController.GetGridSize() - 1); // Clamp the new x-coordinate
            newY = Math.Clamp(newY, 0, SimulationController.GetGridSize() - 1); // Clamp the new y-coordinate

            return new Position(newX, newY); // Return the new position
        }

        protected abstract IMovementStrategy CreateMovementStrategy(); // Abstract method to create the movement strategy of the creature
    }
}