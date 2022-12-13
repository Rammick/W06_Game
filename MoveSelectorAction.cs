using Unit06.Game.Casting;

namespace Unit06.Game.Scripting
{
    public class MoveSelectorAction : Action
    {
        public MoveSelectorAction()
        {
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            Selector selector = (Selector)cast.GetFirstActor(Constants.SELECTOR_GROUP);
            Body body = selector.GetBody();
            Point position = body.GetPosition();
            Point velocity = body.GetVelocity();
            int y = position.GetY();

            position = position.Add(velocity);
            if (y < 0)
            {
                position = new Point(position.GetX(), 0);
            }
            else if (y > Constants.SCREEN_HEIGHT - Constants.SELECTOR_HEIGHT)
            {
                position = new Point(Constants.SCREEN_HEIGHT - Constants.SELECTOR_HEIGHT, 
                    position.GetX());
            }

            body.SetPosition(position);       
        }
    }
}