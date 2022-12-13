using Unit06.Game.Casting;
using System.Collections.Generic;
namespace Unit06.Game.Scripting
{
    public class MoveFighterAction : Action
    {
        public MoveFighterAction()
        {
        }

        public void Execute(Cast cast, Script script, ActionCallback callback)
        {
            List<Actor> fighters = cast.GetActors(Constants.FIGHTER_GROUP);

            foreach (Fighter f in fighters)
            {
                f.MoveNext();
            }
        }
        
    }
}