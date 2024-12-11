using ZombieSimulation;
using ZombieSimulation.Moves;
using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;
using System.Timers;

namespace BlazorApp.Services;

public class SimulationService : IDisposable
{
    private readonly ISimulationController _simulationController;
    private readonly ISimulationConfig _config;
    private readonly Random _random = new Random();
    private System.Timers.Timer? _timer;
    private bool _isRunning;
    private int _timeStep;
    private int _lastSpawnTime;
    
    public event Action? OnSimulationUpdated;
    
    public SimulationService(ISimulationController simulationController, ISimulationConfig config)
    {
        _simulationController = simulationController;
        _config = config;
    }

    public void StartSimulation()
    {
        if (_isRunning) return;
        
        _simulationController.Initialize(_config.InitialHumans, _config.InitialZombies, _config.InitialFood, _config.InitialSanctuaries);
        _timeStep = 0;
        _lastSpawnTime = 0;
        
        _timer = new System.Timers.Timer(_config.UpdateInterval);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();
        
        _isRunning = true;
    }

    public void StopSimulation()
    {
        if (!_isRunning) return;
        
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;
        _isRunning = false;
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        var previousEntities = GetEntityPositions().ToList();
        _simulationController.Update();
        _timeStep++;
        
        HandleSpawning();
        
        var currentEntities = GetEntityPositions().ToList();
        if (HasEntitiesChanged(previousEntities, currentEntities))
        {
            OnSimulationUpdated?.Invoke();
        }
        
        CheckSimulationEnd();
    }

    private bool HasEntitiesChanged(IEnumerable<EntityPosition> previous, IEnumerable<EntityPosition> current)
    {
        if (previous.Count() != current.Count()) return true;
        
        var previousDict = previous.ToDictionary(e => e.Id);
        var currentDict = current.ToDictionary(e => e.Id);
        
        return previous.Any(p => !currentDict.ContainsKey(p.Id) || 
                               currentDict[p.Id].X != p.X || 
                               currentDict[p.Id].Y != p.Y);
    }

    private void HandleSpawning()
    {
        if (_timeStep - _lastSpawnTime >= _config.SpawnInterval)
        {
            SpawnEntities();
            _lastSpawnTime = _timeStep;
        }
    }

    private void SpawnEntities()
    {
        var counts = new Dictionary<EntityType, int>
        {
            { EntityType.Human, _random.Next(1, 4) },
            { EntityType.Zombie, _random.Next(1, 4) },
            { EntityType.Sanctuary, _random.Next(1, 4) },
            { EntityType.Food, _random.Next(1, 4) }
        };

        foreach (var (type, count) in counts)
        {
            _simulationController.AddEntity(type, count);
        }

        Console.WriteLine($"\nSpawned new entities: " +
            $"{counts[EntityType.Human]} Humans, " +
            $"{counts[EntityType.Zombie]} Zombies, " +
            $"{counts[EntityType.Food]} Food, " +
            $"{counts[EntityType.Sanctuary]} Sanctuaries");
    }

    private void CheckSimulationEnd()
    {
        if (!_simulationController.GetEntities().Any(entity => entity is Human))
        {
            StopSimulation();
        }
    }

    public SimulationStats GetStats()
    {
        var entities = _simulationController.GetEntities();
        var humans = entities.Count(e => e is Human);
        var zombies = entities.Count(e => e is Zombie);
        var food = entities.Count(e => e is Food);
        var sanctuaries = entities.Count(e => e is Sanctuary);
        var totalPopulation = humans + zombies;
        
        return new SimulationStats
        {
            Survivors = humans,
            Zombies = zombies,
            Food = food,
            Sanctuaries = sanctuaries,
            InfectionRate = totalPopulation > 0 ? (zombies * 100.0 / totalPopulation) : 0
        };
    }

    public IEnumerable<EntityPosition> GetEntityPositions()
    {
        return _simulationController.GetEntities()
            .Select(e => new EntityPosition
            {
                Type = e.GetType().Name,
                X = (e.Position.x * 100) / 99,
                Y = (e.Position.y * 100) / 99,
                Body = e.getBody(),
                Id = $"{e.GetType().Name}_{e.Position.x}_{e.Position.y}_{Guid.NewGuid()}"
            });
    }

    public void Dispose()
    {
        StopSimulation();
    }
}

public class SimulationStats
{
    public int Survivors { get; set; }
    public int Zombies { get; set; }
    public double InfectionRate { get; set; }
    public int Food { get; set; }
    public int Sanctuaries { get; set; }
}

public class EntityPosition
{
    public string Type { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }
    public string Body { get; set; } = "";
    public string Id { get; set; } = "";
} 