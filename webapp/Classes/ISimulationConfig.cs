namespace ZombieSimulation;

public interface ISimulationConfig // Interface for the simulation config
{
    int InitialHumans { get; } // Property to get the initial number of humans
    int HumanStamina { get; } // Property to get the stamina
    int HumanSpeed { get; } // Property to get the speed

    int InitialZombies { get; } // Property to get the initial number of zombies
    int ZombieStamina { get; } // Property to get the stamina
    int ZombieSpeed { get; } // Property to get the speed
    int ZombieCaptureRadius { get; } // Property to get the capture radius

    int InitialFood { get; } // Property to get the initial number of food
    int FoodStamina { get; } // Property to get the stamina

    int InitialSanctuaries { get; } // Property to get the initial number of sanctuaries
    int SanctuaryStamina { get; } // Property to get the sanctuary stamina
    
    int SpawnInterval { get; } // Property to get the spawn interval
    int UpdateInterval { get; } // Property to get the update interval

}