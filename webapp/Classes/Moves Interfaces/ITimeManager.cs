namespace ZombieSimulation.MovesInterfaces
{
    public interface ITimeManager
    {
        double GetDeltaTime(); // Method to get the delta time
        void UpdateTime(); // Update the time of the game
        double GetTotalTime(); // Get the total time of the game
    }
}