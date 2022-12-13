using Unit06.Game.Casting;
using Unit06.Game.Services;
using System.Collections.Generic;

namespace Unit06.Game.Scripting
{
    /// <summary>
    /// Handles the outcomes when two or more fighters from opposing
    /// sides collide or battle each other.
    /// </summary>
    public class CollideFightersAction : Action
    {
        private AudioService audioService;
        private PhysicsService physicsService;

        public CollideFightersAction(PhysicsService physicsService, AudioService audioService)
        {
            this.physicsService = physicsService;
            this.audioService = audioService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Stats stats = (Stats)cast.GetFirstActor(Constants.STATS_GROUP);

            ///Collision logic for fighters. Each fighter is compared to all the fighters in the list and lives are removed and actor if dead. Team is a variable in fighters.
            List<Actor> fighters = cast.GetActors(Constants.FIGHTER_GROUP);
            
            foreach (Fighter f1 in fighters) 
            {
                foreach (Fighter f2 in fighters)
                {
                    if (f1 != f2 && f1.GetTeam() != f2.GetTeam())
                    {
                        Body b1 = f1.GetBody();
                        Body b2 = f2.GetBody();
                        if (physicsService.HasCollided(b1, b2))
                        {
                            f1.ReduceHealth(f2.GetDamage());
                            if (f1.IsDead())
                            {
                                cast.RemoveActor(Constants.FIGHTER_GROUP, f1);
                            }
                            else if (f2.IsDead())
                            {
                                cast.RemoveActor(Constants.FIGHTER_GROUP, f2);
                            }
                        }
                    }
                }
            }
        }
    }
}