namespace ZombieSimulation.StatsInterfaces
{
    public interface IConsumable // Interface for consumable resources
    {
        int getStaminaValue(); // Method to return the stamina value of the resource
        int getHealthValue(); // Method to return the health value of the resource
    }
}