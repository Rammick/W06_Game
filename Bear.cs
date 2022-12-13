using System;
using System.Collections.Generic;

namespace Unit06.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Bear is to appear on the screen. 
    /// </para>
    /// </summary>
    public class Bear : Fighter
    {
       
        // /// Constructs a new instance of Bear.
        // /// </summary>
        public Bear(Point position, int team, bool debug = false) : base(debug) 
        {

            //sets the pixel size and velocity values to size and velocity
            Point size = new Point(Constants.BEAR_WIDTH, Constants.BEAR_HEIGHT);
            Point velocity = new Point(Constants.BEAR_VELOCITY, 0);
            
            //Setting some member variables
            this.team = team;
            this.health = Constants.BEAR_HP;
            this.damage = Constants.BEAR_ATTACK_DAMAGE;

            //sets the point, size, and velocity to the bear fighter and according to team
            this.body = new Body(position, size, velocity);

            if (team == 1)
            {
                this.animation = new Animation(Constants.BEAR1_IMAGES, 0, Constants.ANIMATION_RATE);
            }
            else
            {
                this.animation = new Animation(Constants.BEAR2_IMAGES, 0, Constants.ANIMATION_RATE);
                this.ReverseVelocity();
            }
            
        }
        public override void AttackAction()
        {
            return;
        }
    }
}
