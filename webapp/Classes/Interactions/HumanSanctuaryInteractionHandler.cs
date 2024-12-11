using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves
{
    public class HumanSanctuaryInteractionHandler : IInteractionHandler
    {
        private const double PROTECTION_RADIUS = 10.0; // Protection radius for sanctuaries
        private const int MAX_SANCTUARY_STAY_SECONDS = 30; // Changed to seconds for testing
        private const double FORCE_MOVE_DISTANCE = PROTECTION_RADIUS * 2; // Move them outside the protection radius
        private const double MIN_STAMINA_FOR_SANCTUARY = 10.0; // Minimum stamina required to enter sanctuary

        public bool CanHandle(IEntity entity1, IEntity entity2) // Method to check if the interaction can be handled
        {
            var (human, sanctuary) = GetEntities(entity1, entity2); // Get the human and sanctuary entities
            return human != null && sanctuary != null && sanctuary.getHealth() > 0; // Return true if the human and sanctuary entities are not null and the sanctuary's health is greater than 0
        }

        public void Handle(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove) // Method to handle the interaction
        {
            ValidateParameters(entity1, entity2, entitiesToRemove); // Validate the parameters
            var (human, sanctuary) = GetEntities(entity1, entity2); // Get the human and sanctuary entities
            if (human == null || sanctuary == null) // Check if the human or sanctuary entities are null
            {
                throw new ArgumentException("Invalid entity types. Expected Human and Sanctuary."); // Throw an exception if the human or sanctuary entities are not found
            }
            HandleHumanSanctuaryInteraction(human, sanctuary, entitiesToRemove); // Handle the interaction
        }

        private (Human? human, Sanctuary? sanctuary) GetEntities(IEntity entity1, IEntity entity2) // Method to get the human and sanctuary entities
        {
            Human? human = null; // Initialize the human entity to null
            Sanctuary? sanctuary = null; // Initialize the sanctuary entity to null

            if (entity1 is Human h1 && entity2 is Sanctuary s1) // Check if the first entity is a human and the second entity is a sanctuary
            {
                human = h1; // Set the human entity to the first entity
                sanctuary = s1; // Set the sanctuary entity to the second entity
            }
            else if (entity1 is Sanctuary s2 && entity2 is Human h2) // Check if the first entity is a sanctuary and the second entity is a human
            {
                human = h2; // Set the human entity to the second entity
                sanctuary = s2; // Set the sanctuary entity to the first entity
            }

            return (human, sanctuary); // Return the human and sanctuary entities
        }

        private void HandleHumanSanctuaryInteraction(Human human, Sanctuary sanctuary, List<IEntity> entitiesToRemove) // Method to handle the interaction
        {
            if (sanctuary.getHealth() <= 0) // Check if the sanctuary's health is less than or equal to 0
            {
                HandleDestroyedSanctuary(sanctuary, human, entitiesToRemove); // Handle the destroyed sanctuary
                return;
            }

            // Check time limit first, before position check
            if (human.IsInSanctuary() && human.GetSanctuaryEntryTime().HasValue)
            {
                var timeInSanctuary = DateTime.UtcNow - human.GetSanctuaryEntryTime().Value;
                ColorLogger.YellowLog("Time in sanctuary: " + timeInSanctuary.TotalSeconds + " seconds"); // Debug logging
                if (timeInSanctuary.TotalSeconds >= MAX_SANCTUARY_STAY_SECONDS)
                {
                    sanctuary.RemoveOccupant(human);
                    human.SetIsInSanctuary(false);
                    
                    // Force move them away from sanctuary
                    var angle = new Random().NextDouble() * 2 * Math.PI;
                    var newX = human.Position.x + FORCE_MOVE_DISTANCE * Math.Cos(angle);
                    var newY = human.Position.y + FORCE_MOVE_DISTANCE * Math.Sin(angle);
                    human.Position = new Position((int)newX, (int)newY);
                    ColorLogger.YellowLog("Human forced to leave sanctuary at position: (" + (int)newX + ", " + (int)newY + ")"); // Debug logging
                    return;
                }
            }

            bool isInSamePosition = AreInSamePosition(human, sanctuary); // Check if the human and sanctuary are in the same position
            
            UpdateSanctuaryOccupancy(human, sanctuary, isInSamePosition); // Update the sanctuary's occupancy
        }

        private void HandleDestroyedSanctuary(Sanctuary sanctuary, Human human, List<IEntity> entitiesToRemove) // Method to handle the destroyed sanctuary
        {
            entitiesToRemove.Add(sanctuary); // Add the sanctuary to the entities to remove list
            sanctuary.RemoveOccupant(human); // Remove the human from the sanctuary
            human.SetIsInSanctuary(false); // Set the human's is in sanctuary property to false
        }

        private void UpdateSanctuaryOccupancy(Human human, Sanctuary sanctuary, bool isInSamePosition)
        {
            if (isInSamePosition && !sanctuary.IsFull()) // Check if the human and sanctuary are in the same position and the sanctuary is not full
            {
                // Only allow entry if human has enough stamina
                if (!human.IsInSanctuary() && human.getStamina() >= MIN_STAMINA_FOR_SANCTUARY)
                {
                    sanctuary.AddOccupant(human);
                    human.SetIsInSanctuary(true);
                    ColorLogger.YellowLog("Human enters sanctuary with stamina: " + human.getStamina());
                }
            }
            else if (!isInSamePosition) // Check if the human and sanctuary are not in the same position
            {
                sanctuary.RemoveOccupant(human); // Remove the human from the sanctuary
                human.SetIsInSanctuary(false); // Set the human's is in sanctuary property to false
            }
        }

        private bool AreInSamePosition(IEntity entity1, IEntity entity2) // Method to check if the entities are within protection radius
        {
            double dx = entity1.Position.x - entity2.Position.x;
            double dy = entity1.Position.y - entity2.Position.y;
            double distanceSquared = dx * dx + dy * dy;
            return distanceSquared <= PROTECTION_RADIUS * PROTECTION_RADIUS; // Return true if the entities are within the protection radius
        }

        private void ValidateParameters(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove) // Method to validate the parameters
        {
            if (entitiesToRemove == null) // Check if the entities to remove list is null
                throw new ArgumentNullException(nameof(entitiesToRemove)); // Throw an exception if the entities to remove list is null
            if (entity1 == null) // Check if the first entity is null
                throw new ArgumentNullException(nameof(entity1)); // Throw an exception if the first entity is null
            if (entity2 == null) // Check if the second entity is null
                throw new ArgumentNullException(nameof(entity2)); // Throw an exception if the second entity is null
        }
    }
}