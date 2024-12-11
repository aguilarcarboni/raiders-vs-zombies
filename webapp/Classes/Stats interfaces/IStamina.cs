namespace ZombieSimulation.StatsInterfaces
{
    public interface IStamina{ // Interface for stamina
        double getStamina(); // Method to return the stamina of the entity
        void decreaseStamina(double amount); // Method to decrease the stamina of the entity
        void increaseStamina(double amount); // Method to increase the stamina of the entity
    }
}