using Unit06.Game.Casting;
using Unit06.Game.Services;


namespace Unit06.Game.Scripting
{
    /// <summary>
    /// Implements the consequences of a fighter colliding with 
    /// or passing through the left or right side of the screen.
    /// </summary>
    public class CollideBordersAction : Action
    {
        private AudioService audioService;
        private PhysicsService physicsService;
        
        public CollideBordersAction(PhysicsService physicsService, AudioService audioService)
        {
            this.physicsService = physicsService;
            this.audioService = audioService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Sound victorySound = new Sound(Constants.CHEER_SOUND);
            Sound swordStrike = new Sound(Constants.SWORD_CLASH_SOUND);

            foreach (Fighter fighter in cast.GetActors(Constants.FIGHTER_GROUP))
            {
                //loops through each fighter on screen and checks if they cross either
                // the right or left border. The affected player loses a life per fighter crossed.

                Body body = fighter.GetBody();
                Point position = body.GetPosition();
                int x = position.GetX();
                int y = position.GetY();
                Stats stats = (Stats)cast.GetFirstActor(Constants.STATS_GROUP);

                if (x < Constants.FIELD_LEFT)
                {
                    
                    stats.RemovePlayer1Life();

                    if (stats.GetPlayer1Lives() <= 0)
                    {
                        // Do stuff to show that Player 1 Wins!
                        callback.OnNext(Constants.P2_DID_WIN);
                        //clears all actors from screen for new game
                        cast.ClearActors(Constants.FIGHTER_GROUP); 
                        audioService.PlaySound(victorySound);
                    }
                    else
                    {
                        audioService.PlaySound(swordStrike);
                        cast.RemoveActor(Constants.FIGHTER_GROUP, fighter);
                    }
                    
                }
                else if (x >= Constants.FIELD_RIGHT - Constants.SWORD_FIGHTER_WIDTH)
                {
                    stats.RemovePlayer2Life();
                    
                    if (stats.GetPlayer2Lives() <= 0)
                    {
                        // Do stuff to show that Player 2 Wins!
                        callback.OnNext(Constants.P1_DID_WIN);
                        //clears all actors from screen for new game
                        cast.ClearActors(Constants.FIGHTER_GROUP); 
                        audioService.PlaySound(victorySound);
                    }
                    else
                    {
                        audioService.PlaySound(swordStrike);
                        cast.RemoveActor(Constants.FIGHTER_GROUP, fighter);
                    }

                }
            }
        }
    }
}