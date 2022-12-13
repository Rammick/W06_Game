namespace Unit06.Game.Casting
{
    /// <summary>
    /// A thing that participates in the game. Spawns fighters according to position
    /// </summary>
    public class Selector : Actor
    {
        private Body body;
        private Animation animation;
        
        /// <summary>
        /// Constructs a new instance of Selector.
        /// </summary>
        public Selector(Body body, Animation animation, bool debug) : base(debug)
        {
            this.body = body;
            this.animation = animation;
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
        /// Gets the body.
        /// </summary>
        /// <returns>The body.</returns>
        public Body GetBody()
        {
            return body;
        }

        /// <summary>
        /// Moves the Selector to its next position. A more specific method is used with moveup and movedown.
        /// </summary>
        public void MoveNext()
        {
            
            return;
        }

        /// <summary>
        /// Moves the selector up 1 row.
        /// </summary>
        public void MoveUp()
        {
            if (body.GetPosition().GetY() - Constants.TRACK_DISTANCE < 0)
                return;

            Point pDes = new Point(body.GetPosition().GetX(),body.GetPosition().GetY() - Constants.TRACK_DISTANCE);

            body.GetPosition().Jumpto(pDes);
            
        }

        /// <summary>
        /// Moves the Selector down 1 row.
        /// </summary>
        public void MoveDown()
        {
            if (body.GetPosition().GetY() + Constants.TRACK_DISTANCE > Constants.SCREEN_HEIGHT)
                return;

            Point pDes = new Point(body.GetPosition().GetX(),body.GetPosition().GetY() + Constants.TRACK_DISTANCE);

            body.GetPosition().Jumpto(pDes);
        }

        /// <summary>
        /// Stops the Selector from moving.
        /// </summary>
        public void StopMoving()
        {
            Point velocity = new Point(0, 0);
            body.SetVelocity(velocity);
        }
        
    }
}