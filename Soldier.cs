using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace GameTrench
{
    public class Soldier : Unit
    {
        public Vector2 destination = new Vector2(500,500);
        private Vector2 nextPosition;
        public int speed = 2;
        public Soldier(bool side) 
        {
            Random rand = new Random();
            position.Y = rand.Next(1080);
            if (side == true) position.X = 10;
            else position.X = 1900;
        }

        private Vector2 CalculateNextPosition()
        {
            Vector2 result = new Vector2(0, 0);
            float dX = destination.X - position.X;
            float dY = destination.Y - position.Y;

            float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);

            float relation = speed / hypotenuse;
            if (relation <= 1)
            {
                result.X = dX * relation;
                result.Y = dY * relation;
            }
            return result;
        }
        public void UpdateSoldier()
        {
            nextPosition = CalculateNextPosition();
            position.X += nextPosition.X;
            position.Y += nextPosition.Y;
        }
    }
}
