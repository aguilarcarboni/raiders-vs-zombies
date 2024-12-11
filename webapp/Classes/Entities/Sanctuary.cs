namespace ZombieSimulation.Entities{
    public class Sanctuary : AbstractSafety // Sanctuary class that extends the AbstractSafety class
    {
        private const int SANCTUARY_CAPACITY = 3; // Constant to store the capacity of the sanctuary
        private readonly ISimulationConfig _config;

        public Sanctuary(ISimulationConfig config) : base(SANCTUARY_CAPACITY){
            _config = config;
            setInitialHealth();
            ColorLogger.CyanLog($"Created Sanctuary with health: {getHealth()}");
        }

        public Sanctuary(Position position, ISimulationConfig config) : base(position, SANCTUARY_CAPACITY){
            _config = config;
            setInitialHealth();
            ColorLogger.CyanLog($"Created Sanctuary at position with health: {getHealth()}");
        }

        private void setInitialHealth() {
            // First set health to 0
            while (getHealth() > 0) {
                decreaseHealth(getHealth());
            }
            // Then set it to the config value
            increaseHealth(_config.SanctuaryStamina);
        }

        public override string getBody(){ // Method to return the body of the sanctuary
            return "Sanctuary"; // Return the string "Sanctuary"
        }

        public bool isDestroyed(){
            return getHealth() <= 0;
        }
    }
}