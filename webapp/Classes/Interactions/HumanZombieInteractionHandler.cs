using ZombieSimulation.Entities;
using ZombieSimulation.MovesInterfaces;

namespace ZombieSimulation.Moves
{
    public class HumanZombieInteractionHandler : IInteractionHandler
    {
        public bool CanHandle(IEntity entity1, IEntity entity2)
        {
            var human = GetHuman(entity1, entity2);
            var zombie = GetZombie(entity1, entity2);
            
            return human != null && zombie != null && 
                   !human.IsInSanctuary() && 
                   zombie.IsInCaptureRange(human.Position);
        }

        public void Handle(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove)
        {
            ValidateParameters(entity1, entity2, entitiesToRemove);
            var human = GetHuman(entity1, entity2);
            if (human == null)
                throw new ArgumentException("Human entity not found");
            
            ColorLogger.YellowLog("Zombie eats human");
            entitiesToRemove.Add(human);
        }

        private Human? GetHuman(IEntity entity1, IEntity entity2)
        {
            if (entity1 is Human human1)
                return human1;
            if (entity2 is Human human2)
                return human2;
            return null;
        }

        private Zombie? GetZombie(IEntity entity1, IEntity entity2)
        {
            if (entity1 is Zombie zombie1)
                return zombie1;
            if (entity2 is Zombie zombie2)
                return zombie2;
            return null;
        }

        private void ValidateParameters(IEntity entity1, IEntity entity2, List<IEntity> entitiesToRemove)
        {
            if (entitiesToRemove == null)
                throw new ArgumentNullException(nameof(entitiesToRemove));
            if (entity1 == null)
                throw new ArgumentNullException(nameof(entity1));
            if (entity2 == null)
                throw new ArgumentNullException(nameof(entity2));
        }
    }
}