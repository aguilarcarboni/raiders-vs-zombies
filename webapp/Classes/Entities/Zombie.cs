using ZombieSimulation.MovesInterfaces;
using ZombieSimulation.Moves;

namespace ZombieSimulation.Entities{
    public class Zombie : AbstractCreature{ // Zombie class that extends the AbstractCreature class
        private readonly ISimulationConfig _config;
        private double _stamina;

        // Constructor for random position
        public Zombie(IDistanceCalculator distanceCalculator, ISimulationConfig config) : base(distanceCalculator){
            _config = config;
            _stamina = config.ZombieStamina;
            SetSpeed(_config.ZombieSpeed);
            // Override the random health initialization with config value
            setInitialHealth();
            ColorLogger.GreenLog($"Created Zombie with health: {getHealth()}");
        }

        // Constructor for specific position
        public Zombie(Position position, IDistanceCalculator distanceCalculator, ISimulationConfig config) : base(position, distanceCalculator){
            _config = config;
            _stamina = config.ZombieStamina;
            SetSpeed(_config.ZombieSpeed);
            // Override the random health initialization with config value
            setInitialHealth();
            ColorLogger.GreenLog($"Created Zombie at position with health: {getHealth()}");
        }

        private void setInitialHealth() {
            // First set health to 0
            while (getHealth() > 0) {
                decreaseHealth(getHealth());
            }
            // Then set it to the config value
            increaseHealth(_config.ZombieStamina);
        }

        public override string getBody(){ // Method to return the body of the zombie
            return "Zombie"; // Return the string "Zombie"
        }

        protected override IMovementStrategy CreateMovementStrategy(){ // Method to create the movement strategy of the zombie
            return new ZombieMovementStrategy(DistanceCalculator); // Return the movement strategy of the zombie
        }

        public double getStamina(){
            return _stamina;
        }

        public void decreaseStamina(double amount){
            _stamina = Math.Max(0, _stamina - amount);
        }

        public bool isDead(){
            return getHealth() <= 0 || _stamina <= 0;
        }

        public bool IsInCaptureRange(Position targetPosition)
        {
            if (targetPosition == null)
                return false;
            
            // Calculate distance between zombie and target
            int dx = Position.x - targetPosition.x;
            int dy = Position.y - targetPosition.y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // Check if target is within capture radius
            return distance <= _config.ZombieCaptureRadius;
        }
    }
}