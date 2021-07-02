using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameTrench
{
    class Bullet
    {
        private Vector2 nextPosition;
        public Vector2 position;
        public Vector2 destination;
        public bool dead = false;
        public int speed = 40;
        public Bullet(Vector2 posin, Vector2 destin)
        {
            position = posin;
            destination = destin;
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
                dead = true;
            }
            else
            {
                result.X = destination.X - position.X;
                result.Y = destination.Y - position.Y;
            }
            return result;
        }
        public void UpdateBullet()
        {
            nextPosition = CalculateNextPosition();
            position.X += nextPosition.X;
            position.Y += nextPosition.Y;

        }
    }
}
