using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;
namespace ZombieSimulation.Moves{
    public class ZombieMovementStrategy : MovementStrategy{
        private const double SANCTUARY_AVOIDANCE_RADIUS = 10.0; // Radius within which zombies will avoid sanctuaries

        public ZombieMovementStrategy(IDistanceCalculator distanceCalculator) : base(distanceCalculator){}
        
        public override Position DecideNextMove(IEntity entity, List<IEntity> nearbyEntities)
        {
            ValidateParameters(entity, nearbyEntities);
            var zombie = (Zombie)entity;

            // Check if near any sanctuary
            var nearestSanctuary = FindNearestEntity<Sanctuary>(zombie.Position, nearbyEntities);
            if (nearestSanctuary != null && IsNearSanctuary(zombie.Position, nearestSanctuary.Position))
            {
                return RandomMove(zombie.Position); // Move randomly if near sanctuary
            }

            var nearestHuman = FindNearestEntity<Human>(zombie.Position, nearbyEntities);
            return nearestHuman != null ? CalculateNewPosition(zombie.Position, nearestHuman.Position) : RandomMove(zombie.Position) ;
        }

        private bool IsNearSanctuary(Position zombiePos, Position sanctuaryPos)
        {
            double dx = zombiePos.x - sanctuaryPos.x;
            double dy = zombiePos.y - sanctuaryPos.y;
            double distanceSquared = dx * dx + dy * dy;
            return distanceSquared <= SANCTUARY_AVOIDANCE_RADIUS * SANCTUARY_AVOIDANCE_RADIUS;
        }

        private void ValidateParameters(IEntity entity, List<IEntity> nearbyEntities)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            
            if (entity is not Zombie)
                throw new ArgumentException("Entity must be a Zombie", nameof(entity));
            
            if (nearbyEntities == null)
                throw new ArgumentNullException(nameof(nearbyEntities));
        }

        private T? FindNearestEntity<T>(Position currentPosition, List<IEntity> entities)
            where T : class, IEntity
        {
            return entities.OfType<T>().OrderBy(e => CalculateDistance(currentPosition, e.Position)).FirstOrDefault();
        }
    }
}