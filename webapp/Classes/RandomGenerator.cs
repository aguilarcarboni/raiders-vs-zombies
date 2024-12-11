namespace ZombieSimulation
{
    public class RandomGenerator : IRandomGenerator // RandomGenerator class implements the IRandomGenerator interface
    {
        private readonly Random _random; // Random object

        public RandomGenerator() // Constructor
        {
            _random = new Random(); // Initialize the random object
        }

        public int Random(int min, int max) // Method to get a random integer between min and max
        {
            ValidateParameters(min, max); // Validate the parameters
            return _random.Next(min, max); // Return the random integer
        }

        private void ValidateParameters(int min, int max) // Method to validate the parameters
        {
            if (min > max) // If min is greater than max
            {
                throw new ArgumentException("Min cannot be greater than max"); // Throw an exception
            }
        }
    }
}