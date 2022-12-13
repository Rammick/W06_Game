using System;
using System.Collections.Generic;

namespace Unit06.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Fighter is to keep track of its appearance, position and velocity in 2d. Fighter inherits from actor
    /// space.
    /// </para>
    /// </summary>
    public abstract class Fighter : Actor
    {
        // private Point position;
        // private Point size;
        //protected static Random random = new Random();
        protected Body body;
        protected Animation animation;
        protected int health;
        protected int team;
        protected int damage;
        protected Image image;

        //  /// <summary>
        // /// Constructs a new instance of Fighter.
        // /// </summary>
        public Fighter(bool debug = false) : base(debug) 
        {

        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <returns>The body.</returns>
        public Body GetBody()
        {
            return body;
        }

        /// <summary>
        /// Sets the body.
        /// </summary>
        /// <returns>The body.</returns>
        public void SetBody(Body body)
        {
            this.body = body;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>The image.</returns>
        public Image GetImage()
        {
            return image;
        }

        /// <summary>
        /// Gets the animation.
        /// </summary>
        /// <returns>The animation.</returns>
        public Animation GetAnimation()
        {
            return animation;
        }

        /// <summary>
        /// Moves the fighter to its next position.
        /// </summary>
        public void MoveNext()
        {
            Point position = body.GetPosition();
            Point velocity = body.GetVelocity();
            Point newPosition = position.Add(velocity);
            body.SetPosition(newPosition);
        }
        /// <summary>
        /// Stops the fighter from moving.
        /// </summary>
        public void StopMoving()
        {
            Point velocity = new Point(0, 0);
            body.SetVelocity(velocity);
        }

        public virtual void AttackAction()
        {
            return;
        }

        public int GetHealth()
        {
            return health;
        }

        public bool IsDead()
        {
            // return health <= 0; Protip

            if (health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReduceHealth(int attackerDamage)
        {
            health -= attackerDamage;
        }

        public int GetDamage()
        {
            return damage;
        }

        public int GetTeam()
        {
            return team;
        }
        ///creates method for changing direction for velocity for player 1 or 2
        public void ReverseVelocity()
        {
            Point velocity = body.GetVelocity();
            Point newVelocity = new Point(velocity.GetX() * -1, velocity.GetY());

            body.SetVelocity(newVelocity);
        }

    }
}