using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;
namespace ZombieSimulation.Moves
{
    public class DistanceCalculator : IDistanceCalculator // Class to calculate the distance between two positions
    {
        public double CalculateDistance(Position a, Position b) // Method to calculate the distance between two positions
        {
            if (PositionIsNull(a) || PositionIsNull(b)) // Check if the positions are null
                throw new ArgumentNullException("Position cannot be null"); // Throw an exception if the position is null
            int dx = a.x - b.x; // Calculate the difference in the x-coordinates
            int dy = a.y - b.y; // Calculate the difference in the y-coordinates
            return Math.Sqrt(dx * dx + dy * dy); // Return the distance between the two positions
        }

        private bool PositionIsNull(Position position) // Method to check if a position is null
        {
            return position == null; // Return true if the position is null, otherwise false
        }
    }
}