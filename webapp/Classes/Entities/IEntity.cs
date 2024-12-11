namespace ZombieSimulation.Entities{
    public interface IEntity{ // Interface for an entity
        string getBody(); // Method to return the body of the entity
        Position Position {get; set;} // Property to get and set the position of the entity
    }
}