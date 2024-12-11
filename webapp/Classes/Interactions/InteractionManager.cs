using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;
using ZombieSimulation.EntityFactories;
namespace ZombieSimulation.Moves
{
    public class InteractionManager : IInteractionManager
    {
        private readonly IReadOnlyList<IInteractionHandler> _handlers; // Array of interaction handlers

        public InteractionManager(IEnumerable<IInteractionHandler> handlers) // Constructor for the interaction manager
        {
            if (handlers == null) // Check if the handlers are null
                throw new ArgumentNullException(nameof(handlers)); // Throw an exception if the handlers are null
            if (!handlers.Any()) // Check if the handlers are empty
                throw new ArgumentException("Handlers cannot be empty"); // Throw an exception if the handlers are empty
            _handlers = handlers.ToList().AsReadOnly(); // Initialize the array of interaction handlers
        }

        public void HandleInteractions(IReadOnlyList<IEntity> entities, IEntityManager entityManager) // Method to handle the interactions
        {
            if (entities == null) // Check if the entities are null
                throw new ArgumentNullException(nameof(entities)); // Throw an exception if the entities are null
            if (entityManager == null) // Check if the entity manager is null
                throw new ArgumentNullException(nameof(entityManager)); // Throw an exception if the entity manager is null
            var entitiesToRemove = new List<IEntity>(); // List to store the entities to remove

            ProcessInteractions(entities, entitiesToRemove); // Process the interactions
            foreach (var entity in entitiesToRemove) // Remove the entities from the list
            {
                entityManager.RemoveEntity(entity); // Remove the entity from the list
            }
        }

        private void HandleEntityInteractions(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove) // Method to handle the interaction
        {
            foreach (var handler in _handlers) // Iterate through the handlers
            {
                if (handler.CanHandle(entity1, entity2)) // Check if the handler can handle the interaction
                {
                    handler.Handle(entity1, entity2, entitiesToRemove); // Handle the interaction
                    return; // Return if the interaction is handled
                }
            }
        }
        private bool AreInSamePosition(IEntity entity1, IEntity entity2) // Method to check if the entities are in the same position
        {
            return (entity1.Position.x == entity2.Position.x) && (entity1.Position.y == entity2.Position.y); // Return true if the entities are in the same position
        }
        private void ProcessInteractions(IReadOnlyList<IEntity> entities, List<IEntity> entitiesToRemove) // Method to process the interactions
        {
            for (int i = 0; i < entities.Count; i++) // Get one entity
            {
                for (int j = i + 1; j < entities.Count; j++) // Use the i entity to compare with all the other entities
                {
                    if (AreInSamePosition(entities[i], entities[j])) // Check if the entities are in the same position
                    {
                        HandleEntityInteractions(entities[i], entities[j], entitiesToRemove); // Handle the interaction
                    }
                }
            }
        }
    }
}