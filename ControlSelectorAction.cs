using Unit06.Game.Casting;
using Unit06.Game.Services;


namespace Unit06.Game.Scripting
{
    public class ControlSelectorAction : Action
    {
        private KeyboardService keyboardService;

        public ControlSelectorAction(KeyboardService keyboardService)
        {
            this.keyboardService = keyboardService;
        }

       
        ///Moves Selector and stops movement
        public void Execute(Cast cast, Script script, ActionCallback callback)
        {

            Selector selector = (Selector)cast.GetFirstActor(Constants.SELECTOR_GROUP);
            Selector selector2 = (Selector)cast.GetActors(Constants.SELECTOR_GROUP)[1];
            if (keyboardService.IsKeyReleased(Constants.P1UP))
            {
                selector.MoveUp();
            }
             else if (keyboardService.IsKeyReleased(Constants.P1DOWN))
            {
                selector.MoveDown();
            }
            else
            {
                selector.StopMoving();  
            } 
            if (keyboardService.IsKeyReleased(Constants.P2UP))
            {
                selector2.MoveUp();
            }
             else if (keyboardService.IsKeyReleased(Constants.P2DOWN))
            {
                selector2.MoveDown();
            }
            else
            {
                selector2.StopMoving();  
            }           
        }
    }
}