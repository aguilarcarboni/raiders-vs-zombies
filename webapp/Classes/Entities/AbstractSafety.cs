using ZombieSimulation.StatsInterfaces;

namespace ZombieSimulation.Entities{
    public abstract class AbstractSafety : AbstractEntity, ICapacity{ // AbstractSafety class that extends AbstractEntity and implements IProtect interface
        private readonly int _capacity; // Property to store the capacity of the entity
        private readonly List<IEntity> _occupants = new(); // Property to store the occupants of the entity
        public AbstractSafety(int capacity) : base(){ // Constructor for random position
            _capacity = capacity; // Set the capacity
        } // Constructor for random position

        public AbstractSafety(Position position, int capacity) : base(position){ // Constructor for specific position
            _capacity = capacity; // Set the capacity
        } // Constructor for specific position

        public int GetCapacity(){ // Method to return the capacity of the entity
            return _capacity; // Return the capacity
        }

        public bool IsFull(){ // Method to check if the entity is full
            return _occupants.Count >= _capacity; // Return true if the number of occupants is greater than or equal to the capacity
        }

        public bool AddOccupant(IEntity entity){ // Method to add an occupant to the entity
            if (IsFull() || _occupants.Contains(entity)) // Check if the entity is full or already contains the entity
                return false; // Return false if the entity is full or already contains the entity

            _occupants.Add(entity); // Add the entity to the occupants
            return true; // Return true
        }

        public bool RemoveOccupant(IEntity entity){ // Method to remove an occupant from the entity
            return _occupants.Remove(entity); // Return true if the occupant was removed
        }

        public IReadOnlyList<IEntity> GetOccupants() // Method to return the occupants of the entity
        {
            return _occupants.AsReadOnly(); // Return the occupants as a read-only list
        }
    }
}