namespace Unit06.Game.Casting
{
    /// <summary>
    /// A thing that participates in the game.
    /// </summary>
    public class Stats : Actor
    {
        private int lives1;
        private int lives2;

        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Stats(int lives1 = 10, int lives2 = 10, 
                bool debug = false) : base(debug)
        {
            this.lives1 = lives1;
            this.lives2 = lives2;
        }

        /// <summary>
        /// Replenishes one life.
        /// </summary>
        public void AddPlayer1Lives()
        {
            lives1++;
        }

        /// <summary>
        /// Replenishes one life.
        /// </summary>
        public void AddPlayer2Lives()
        {
            lives2++;
        }

        /// <summary>
        /// Gets player 1's lives.
        /// </summary>
        /// <returns>The level.</returns>
        public int GetPlayer1Lives()
        {
            return lives1;
        }

        /// <summary>
        /// Gets player 2's lives.
        /// </summary>
        /// <returns>The lives.</returns>
        public int GetPlayer2Lives()
        {
            return lives2;
        }

        /// <summary>
        /// Removes a life from player 1.
        /// </summary>
        public void RemovePlayer1Life()
        {
            lives1--;
            if (lives1 <= 0)
            {
                lives1 = 0;
            }
        }

        /// <summary>
        /// Removes a life from player 2.
        /// </summary>
        public void RemovePlayer2Life()
        {
            lives2--;
            if (lives2 <= 0)
            {
                lives2 = 0;
            }
        }
        
    }
}