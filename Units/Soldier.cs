using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace GameTrench
{
    public class Soldier : Unit
    {
        private Vector2 nextPosition;
        public int speed = 20;
        public Soldier(bool sidein) 
        {
            Random rand = new Random();
            side = sidein;
            position.Y = rand.Next((int)(1080));
            if (side == true) 
                position.X = 10;
            else 
                position.X = 1900;

            hp = Globals.SoldierHP;
            cooldown = 60 / Globals.SoldierFireRate;
            FireRate = Globals.SoldierFireRate;
            FireDmg = Globals.SoldierDmg;
            FireRange = Globals.SoldierRange;
            FireAccuracy = Globals.SoldierAccuracy;
    }
        public Soldier(bool sidein, Vector2 dest)
        {
            destination.X = dest.X;
            destination.Y = dest.Y;
            Random rand = new Random();
            side = sidein;
            position.Y = rand.Next((int)(1080));
            if (side == true)
                position.X = 10;
            else
                position.X = 1900;
            hp = Globals.SoldierHP;
            cooldown = 60 / Globals.SoldierFireRate;
            FireRate = Globals.SoldierFireRate;
            FireDmg = Globals.SoldierDmg;
            FireRange = Globals.SoldierRange;
            FireAccuracy = Globals.SoldierAccuracy;
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
            }
            return result;
        }
        public void UpdateSoldier()
        {
            FindFireTarget();
            nextPosition = CalculateNextPosition();
            position.X += nextPosition.X;
            position.Y += nextPosition.Y;
        }
    }
}
