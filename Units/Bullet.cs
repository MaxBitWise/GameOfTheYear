using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameTrench
{
    class Bullet
    {
        private Vector2 nextPosition;
        public Vector2 position;
        public Vector2 destination;
        public bool dead = false;
        public int speed = 40;
        Random rand = new Random();
        public Bullet(Vector2 posin, Vector2 destin, bool isAimed)
        {
            if (isAimed == false)
            {
                //speed = 1;
                destin.X = (int)(destin.X + rand.Next(20) - 10);
                destin.Y = (int)(destin.Y + rand.Next(20) - 10);
            }
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
                
            }
            else
            {
                result.X = destination.X - position.X;
                result.Y = destination.Y - position.Y;
                dead = true;
            }
            return result;
        }
        public void UpdateBullet()
        {
            nextPosition = CalculateNextPosition();
            position.X += nextPosition.X;
            position.Y += nextPosition.Y;

        }

       public void DrawBullet(GraphicsDevice device)
        {
           Globals._spriteBatch.Draw(Globals.BulletTex, new Rectangle(Resolution.ScaledPoint(new Point((int)position.X, (int)position.Y)),
                    Resolution.ScaledPoint(new Point(4, 4))), Color.White);
        }
    }
}
