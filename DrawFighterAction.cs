using Unit06.Game.Casting;
using Unit06.Game.Services;


namespace Unit06.Game.Scripting
{
    public class DrawFighterAction : Action
    {
        private VideoService videoService;
        
        public DrawFighterAction(VideoService videoService)
        {
            this.videoService = videoService;
        }
        ///Method to draw fighter on the screen
        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            
            foreach (Fighter fighter in cast.GetActors(Constants.FIGHTER_GROUP))
            {
                Body body = fighter.GetBody();

                if (fighter.IsDebug())
                {
                    Rectangle rectangle = body.GetRectangle();
                    Point size = rectangle.GetSize();
                    Point pos = rectangle.GetPosition();
                    videoService.DrawRectangle(size, pos, Constants.PURPLE, false);
                }
                else
                {
                    Animation animation = fighter.GetAnimation();
                    Image image = animation.NextImage();
                    Point position = body.GetPosition();
                    videoService.DrawImage(image, position);
                }
            }

        }
    }
}