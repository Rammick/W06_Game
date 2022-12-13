using System;
using System.Collections.Generic;

namespace Unit06.Game.Casting
{
    /// <summary>
    /// <para>A thing that participates in the game.</para>
    /// <para>
    /// The responsibility of Actor is to keep track of its appearance, position and velocity in 2d 
    /// space.
    /// </para>
    /// </summary>
    public class Field : Actor
    {
        //Field will just upload a background picture for the battlefield

        public Field(Image image, bool debug = false) : base(debug)
        {
            //constructor
        }
    }
}