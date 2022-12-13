using Unit06.Game.Casting;
using Unit06.Game.Services;


namespace Unit06.Game.Scripting
{
    public class DrawSelectorAction : Action
    {
        private VideoService videoService;
        
        public DrawSelectorAction(VideoService videoService)
        {
            this.videoService = videoService;
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            foreach (Selector selector in cast.GetActors(Constants.SELECTOR_GROUP))
            {
                Body body = selector.GetBody();

                if (selector.IsDebug())
                {
                    Rectangle rectangle = body.GetRectangle();
                    Point size = rectangle.GetSize();
                    Point pos = rectangle.GetPosition();
                    videoService.DrawRectangle(size, pos, Constants.BLACK, false);
                }
                else
                {
                    Animation animation = selector.GetAnimation();
                    Image image = animation.NextImage();
                    Point position = body.GetPosition();
                    videoService.DrawImage(image, position);
                }
            }
            
        }
    }
}