using ZombieSimulation.Entities;

namespace ZombieSimulation.MovesInterfaces
{
    public interface IDistanceCalculator // Interface for calculating the distance between two positions
    {
        double CalculateDistance(Position a, Position b); // Method to calculate the distance between two positions
    }
}
