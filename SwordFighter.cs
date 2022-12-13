using System;
using System.Collections.Generic;

namespace Unit06.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Swordfighter is to appear on the screen. 
    /// </para>
    /// </summary>
    public class SwordFighter : Fighter
    {
        // /// Constructs a new instance of Swordfighter.
        // /// </summary>
        public SwordFighter(Point position, int team, bool debug = false) : base(debug) 
        {

            //sets the pixel size and velocity values to size and velocity
            Point size = new Point(Constants.SWORD_FIGHTER_WIDTH, Constants.SWORD_FIGHTER_HEIGHT);
            Point velocity = new Point(Constants.SWORD_FIGHTER_VELOCITY, 0);

            //Setting some member variables
            this.team = team;
            this.health = Constants.SWORD_FIGHTER_HP;
            this.damage = Constants.SWORD_DAMAGE;

            //sets the point, size, and velocity to the  swordfighter according to team
            this.body = new Body(position, size, velocity);

            if (team == 1)
            {
                this.animation = new Animation(Constants.SWORD_FIGHTER_IMAGES, 0, Constants.ANIMATION_RATE);
            }
            else
            {
                this.animation = new Animation(Constants.SWORD_FIGHTER2_IMAGES, 0, Constants.ANIMATION_RATE);
                this.ReverseVelocity();
            }
            
        }
        public override void AttackAction()
        {
            return;
        }        

        }
}