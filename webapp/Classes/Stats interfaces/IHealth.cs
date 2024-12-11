namespace ZombieSimulation.StatsInterfaces
{
    public interface IHealth // Interface for health
    {
        double getHealth(); // Method to return the health of the entity
        void decreaseHealth(double amount); // Method to decrease the health of the entity
        void increaseHealth(double amount); // Method to increase the health of the entity
    }
}