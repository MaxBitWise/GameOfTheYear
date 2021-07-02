using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameTrench
{
    public abstract class Unit
    {

        public Point drawSize;
        public Texture2D UnitTex;
        public Vector2 position;
        public Vector2 destination;
        public bool side;
        public int hp;
        public int cooldown;
        public int FireRate;
        public int FireDmg;
        public int FireRange;
        public int FireAccuracy;
        public int SelfInvul = 0;
        Random rand = new Random();
        public Unit()
        {
            
        }

        public void FindFireTarget(List<int> indexes)
        {
            if (side)
            {
                if (indexes[indexes.Count - 1] != -1)
                {

                    Random rand = new Random();
                    int targetIndex = rand.Next((int)(indexes.Count));
                    for (int i = 0; i < 10; i++)
                    {
                        int targetIndexInGroup = rand.Next((int)(Globals.groupsAI[targetIndex].Second.Count - 1));
                        float dX = position.X - Globals.groupsAI[targetIndex].Second[targetIndexInGroup].position.X;
                        float dY = position.Y - Globals.groupsAI[targetIndex].Second[targetIndexInGroup].position.Y;

                        float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                        if (hypotenuse < FireRange)
                        {
                            FireToGroup(targetIndex, targetIndexInGroup);
                        }
                    }
                }
                else
                {
                    Random rand = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        int target = rand.Next((int)(Globals.aiunits.Count - 1));
                        float dX = position.X - Globals.aiunits[target].position.X;
                        float dY = position.Y - Globals.aiunits[target].position.Y;

                        float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                        if (hypotenuse < FireRange)
                        {
                            Fire(target);
                        }
                    }
                }

            }







            /*if(side)
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
            }*/

        }

        public void Fire(int EnemyIndex)
        {
            if (side) 
            { 
                
                int chance = FireAccuracy - Globals.aiunits[EnemyIndex].SelfInvul;
                if (rand.Next(100) < chance)
                {
                    Globals.aiunits[EnemyIndex].Die();
                    Globals.Bullets.Add(new Bullet(position, Globals.aiunits[EnemyIndex].position, true));
                }
                else
                {
                    Globals.Bullets.Add(new Bullet(position, Globals.aiunits[EnemyIndex].position, false));
                }
                
            }
            if (!side)
            {
                int chance = FireAccuracy - Globals.humanunits[EnemyIndex].SelfInvul;
                if (rand.Next(100) < chance)
                {
                    Globals.humanunits[EnemyIndex].Die();
                    Globals.Bullets.Add(new Bullet(position, Globals.humanunits[EnemyIndex].position, true));
                }
                else
                {
                    Globals.Bullets.Add(new Bullet(position, Globals.humanunits[EnemyIndex].position, false));
                }
                
            }
            cooldown = 60 / FireRate;
            
        }
        public void FireToGroup(int groupIndex, int EnemyIndex)
        {
            
            if (side) 
            { 
               
                int chance = FireAccuracy;
                if(rand.Next(100) < chance)
                {
                    Globals.groupsAI[groupIndex].Second[EnemyIndex].Die();
                    Globals.Bullets.Add(new Bullet(position, Globals.groupsAI[groupIndex].Second[EnemyIndex].position, true));
                }
                else
                {
                    Globals.Bullets.Add(new Bullet(position, Globals.groupsAI[groupIndex].Second[EnemyIndex].position, false));
                }

                //Globals.groupsAI[groupIndex].Second[EnemyIndex].Die(); 
            }
            if (!side) 
            {
                int chance = FireAccuracy;
                if (rand.Next(100) < chance)
                {
                    Globals.groups[groupIndex].Second[EnemyIndex].Die();
                    Globals.Bullets.Add(new Bullet(position, Globals.groups[groupIndex].Second[EnemyIndex].position, true));
                }
                else
                {
                    Globals.Bullets.Add(new Bullet(position, Globals.groups[groupIndex].Second[EnemyIndex].position, false));
                }

            }
            cooldown = 60 / FireRate;

        }
        public abstract void UpdateUnit(List<int> indexes);
        public abstract void UpdateUnit();

            
     
        public void Die()
        {
            Globals.corpses.Add(position);
            if (side) position.X = 10;
            else position.X = 1910;
            position.Y = 500;
        }

    }
}
