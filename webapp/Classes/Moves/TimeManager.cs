namespace ZombieSimulation.Moves;
using ZombieSimulation.MovesInterfaces;

public class TimeManager : ITimeManager // Class for the time manager
{
    private const double DEFAULT_DELTA_TIME = 0.016; // 60 FPS
    private const double MIN_DELTA_TIME = 0.001; // Minimum delta time
    private const double MAX_DELTA_TIME = 0.1; // Maximum delta time
    private readonly object _lock = new(); // Lock object to synchronize access to the time manager
    private DateTime _lastUpdateTime; // Last update time
    private double _deltaTime; // Delta time
    private double _totalTime; // Total time

    public TimeManager() // Constructor for the time manager
    {
        _lastUpdateTime = DateTime.Now; // Initialize the last update time
        _deltaTime = DEFAULT_DELTA_TIME; // Initialize the delta time
        _totalTime = 0; // Initialize the total time
    }

    public void UpdateTime() // Method to update the time
    {
        lock(_lock) // Lock the access to the time manager
        {
            var currentTime = DateTime.Now; // Get the current time
            _deltaTime = CalculateDeltaTime(currentTime); // Calculate the delta time
            _totalTime += _deltaTime; // Update the total time
            _lastUpdateTime = currentTime; // Update the last update time
        }
    }

    public double GetDeltaTime() // Method to get the delta time
    {
        lock(_lock) // Lock the access to the time manager
        {
            return _deltaTime; // Return the delta time
        }
    }

    public double GetTotalTime() // Method to get the total time
    {
        lock(_lock) // Lock the access to the time manager
        {
            return _totalTime; // Return the total time
        }
    }

    private double CalculateDeltaTime(DateTime currentTime) // Method to calculate the delta time
    {
        double deltaTime = (currentTime - _lastUpdateTime).TotalSeconds; // Calculate the delta time
        return ClampDeltaTime(deltaTime); // Clamp the delta time
    }

    private double ClampDeltaTime(double deltaTime) // Method to clamp the delta time
    {
        return Math.Clamp(deltaTime, MIN_DELTA_TIME, MAX_DELTA_TIME); // Clamp the delta time
    }
}
