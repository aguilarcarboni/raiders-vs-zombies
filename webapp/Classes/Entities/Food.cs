namespace ZombieSimulation.Entities{
    public class Food : AbstractResource // Food class that extends the AbstractResource class and implements the IConsumable interface
    {
        private static readonly Random _random = new Random(); // Property to store the random number generator
        private readonly FoodType _foodType; // Property to store the food type
        public Food() : base(){ // Constructor for random position
            _foodType = GetRandomFoodType();
        }

        public Food(Position position) : base(position){ // Constructor for specific position
            _foodType = GetRandomFoodType(); // Set the food type
        }

        public override string getBody(){ // Method to return the body of the food
            return _foodType.ToString(); // Return the food type as a string
        }

        public Food(FoodType type) : base() // Constructor for specific food type
        {
            _foodType = type; // Set the food type
        }

        public Food(Position position, FoodType type) : base(position) // Constructor for specific position and food type
        {
            _foodType = type; // Set the food type
        }

        public FoodType GetFoodType(){ // Method to return the food type
            return _foodType; // Return the food type
        }

        private FoodType GetRandomFoodType(){ // Method to return a random food type
            var foodTypes = Enum.GetValues<FoodType>(); // Get the values of the FoodType enum
            return foodTypes[_random.Next(foodTypes.Length)]; // Return a random food type
        }
    }
}