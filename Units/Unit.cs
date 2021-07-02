using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace GameTrench
{
    public class Unit
    {



        public Vector2 position;
        public Vector2 destination;
        public bool side;
        public int hp;
        public int cooldown;
        public int FireRate;
        public int FireDmg;
        public int FireRange;
        public int FireAccuracy;
        public Unit()
        {
            
        }

        public void FindFireTarget()
        {
            if(side)
            {
                //var PossibleTargets = new List<Unit>();
                var RealIndex = new List<int>();
                for(int i = 0; i < Globals.aiunits.Count; i++ )
                { 
                    float dX = position.X - Globals.aiunits[i].position.X;
                    float dY = position.Y - Globals.aiunits[i].position.Y;

                    float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                    if (hypotenuse < FireRange)
                    {
                       // PossibleTargets.Add(Globals.aiunits[i]);
                        RealIndex.Add(i);
                    }
                }
                if(RealIndex.Count > 0)
                {
                    Random rand = new Random();
                    int targetIndexInRange = rand.Next((int)(RealIndex.Count));
                    Fire(RealIndex[targetIndexInRange]);
                }
            }
            if (false)
            {
                //var PossibleTargets = new List<Unit>();
                var RealIndex = new List<int>();
                for (int i = 0; i < Globals.humanunits.Count; i++)
                {
                    float dX = position.X - Globals.humanunits[i].position.X;
                    float dY = position.Y - Globals.humanunits[i].position.Y;

                    float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                    if (hypotenuse < FireRange)
                    {
                        // PossibleTargets.Add(Globals.aiunits[i]);
                        RealIndex.Add(i);
                    }
                }
                if (RealIndex.Count > 0)
                {
                    Random rand = new Random();
                    int targetIndexInRange = rand.Next((int)(RealIndex.Count));
                    Fire(RealIndex[targetIndexInRange]);
                }
            }

        }

        public void Fire(int EnemyIndex)
        {
            if (side) { Globals.Bullets.Add(new Bullet(position, Globals.aiunits[EnemyIndex].position)); Globals.aiunits[EnemyIndex].Die();  }
            if (!side) { Globals.Bullets.Add(new Bullet(position, Globals.humanunits[EnemyIndex].position)); Globals.humanunits[EnemyIndex].Die(); }
            cooldown = 60 / FireRate;
            
        }

        public void Die()
        {
            if (side) position.X = 10;
            else position.X = 1910;
            position.Y = 500;
        }

    }
}
