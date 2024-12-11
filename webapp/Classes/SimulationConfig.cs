namespace ZombieSimulation;

public class SimulationConfig : ISimulationConfig // SimulationConfig class implements the ISimulationConfig interface
{
    /*
    //Easy mode
    public int InitialHumans => 15;
    public int HumanStamina => 5;
    public int HumanSpeed => 50;

    public int InitialZombies => 15;
    public int ZombieStamina => 10;
    public int ZombieSpeed => 50;
    public int ZombieCaptureRadius => 20;

    public int InitialFood => 20;
    public int FoodStamina => 3;

    public int InitialSanctuaries => 3;
    public int SanctuaryStamina => 2;

    public int SpawnInterval => 5;
    public int UpdateInterval => 250;
    */

    /*
    //Hard mode
    public int InitialHumans => 5;
    public int HumanStamina => 3;
    public int HumanSpeed => 25;

    public int InitialZombies => 10;
    public int ZombieStamina => 15;
    public int ZombieSpeed => 75;
    public int ZombieCaptureRadius => 20;

    public int InitialFood => 15;
    public int FoodStamina => 2;

    public int InitialSanctuaries => 3;
    public int SanctuaryStamina => 1;

    public int SpawnInterval => 5;
    public int UpdateInterval => 100;
    */

    //Hardcore mode
    public int InitialHumans => 100;
    public int HumanStamina => 2;
    public int HumanSpeed => 100;

    public int InitialZombies => 2;
    public int ZombieStamina => 20;
    public int ZombieSpeed => 100;
    public int ZombieCaptureRadius => 20;

    public int InitialFood => 10;
    public int FoodStamina => 1;

    public int InitialSanctuaries => 1;
    public int SanctuaryStamina => 1;

    public int SpawnInterval => 5;
    public int UpdateInterval => 100;



}