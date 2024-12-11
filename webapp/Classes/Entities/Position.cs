namespace ZombieSimulation.Entities{
    public class Position{
        private int _x; // Private variable to store the x coordinate
        private int _y; // Private variable to store the y coordinate

        public int x{ // Public property to get and set the x coordinate
            get => _x; // Getter to return the x coordinate
            private set => _x = value; // Setter to set the x coordinate
        }

        public int y{ // Public property to get and set the y coordinate
            get => _y; // Getter to return the y coordinate
            private set => _y = value; // Setter to set the y coordinate
        }

        public Position(int x, int y){ // Constructor to initialize the x and y coordinates in an instance
            _x = x; // Set the x coordinate
            _y = y; // Set the y coordinate
        }
    }


}