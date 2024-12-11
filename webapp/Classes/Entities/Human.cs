using ZombieSimulation.StatsInterfaces;
using ZombieSimulation.Moves;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Entities{
    public class Human : AbstractCreature, IStamina{ // Human class that extends the AbstractCreature class and implements the IStamina interface

        private readonly ISimulationConfig _config;
        private double _stamina; // Private field to store the stamina of the human
        private bool _isInSanctuary = false; // Private field to store if the human is in a sanctuary
        private DateTime? _sanctuaryEntryTime = null; // Private field to store the sanctuary entry time of the human
        private const double SANCTUARY_EXIT_STAMINA_REDUCTION = 40.0; // Significant stamina reduction when forced to leave sanctuary
        private static readonly Random _random = new Random(); // Private static field to store the random number generator

        public Human(IDistanceCalculator distanceCalculator, ISimulationConfig config) : base(distanceCalculator) { // Constructor for random position
            _config = config;
            InitializeStamina(); // Initialize the stamina of the human
            SetSpeed(_config.HumanSpeed);
            ColorLogger.CyanLog($"Created Human with stamina: {getStamina()}");
        }

        public Human(Position position, IDistanceCalculator distanceCalculator, ISimulationConfig config) : base(position, distanceCalculator) { // Constructor for specific position
            _config = config;
            InitializeStamina(); // Initialize the stamina of the human
            SetSpeed(_config.HumanSpeed);
            ColorLogger.GreenLog($"Created Human at position with stamina: {getStamina()}");
        }

        private void InitializeStamina() { // Method to initialize the stamina of the human
            _stamina = _config.HumanStamina;
        }

        public override string getBody(){ // Method to return the body of the human
            return "Human"; // Return the string "Human"
        }

        public double getStamina(){ // Method to return the stamina of the human
            return _stamina; // Return the stamina of the human
        }

        public void decreaseStamina(double amount){ // Method to decrease the stamina of the human
            _stamina = Math.Max(0, _stamina - amount); // Decrease the stamina of the human, but not below 0
        }

        public void increaseStamina(double amount){ // Method to increase the stamina of the human
            _stamina = Math.Min(100, _stamina + amount); // Increase the stamina of the human, but not above 100
        }

        protected override IMovementStrategy CreateMovementStrategy(){ // Method to create the movement strategy of the human
            return new HumanMovementStrategy(DistanceCalculator); // Return the movement strategy of the human
        }

        public bool IsInSanctuary(){ // Method to return if the human is in a sanctuary
            return _isInSanctuary; // Return if the human is in a sanctuary
        }

        public void SetIsInSanctuary(bool isInSanctuary){ // Method to set if the human is in a sanctuary
            if (_isInSanctuary && !isInSanctuary) // If leaving sanctuary
            {
                decreaseStamina(SANCTUARY_EXIT_STAMINA_REDUCTION); // Make them hungry when leaving
            }
            _isInSanctuary = isInSanctuary;
            _sanctuaryEntryTime = isInSanctuary ? DateTime.UtcNow : null;
        }

        public DateTime? GetSanctuaryEntryTime(){ // Method to get the sanctuary entry time of the human
            return _sanctuaryEntryTime; // Return the sanctuary entry time of the human
        }
    }
}