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
                        if (Globals.groupsAI[targetIndex].Second.Count > 0)
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
                        else
                        {
                            break;
                        }
                        
                    }
                }
                else
                {
                    Random rand = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        if (Globals.aiunits.Count > 0)
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
                        else
                        {
                            break;
                        }
                    }
                }

            }
            if (!side)
            {
                if (indexes[indexes.Count - 1] != -1)
                {

                    Random rand = new Random();
                    int targetIndex = rand.Next((int)(indexes.Count));
                    for (int i = 0; i < 10; i++)
                    {
                        if (Globals.groups[targetIndex].Second.Count > 0)
                        {
                            int targetIndexInGroup = rand.Next((int)(Globals.groups[targetIndex].Second.Count - 1));
                            float dX = position.X - Globals.groups[targetIndex].Second[targetIndexInGroup].position.X;
                            float dY = position.Y - Globals.groups[targetIndex].Second[targetIndexInGroup].position.Y;

                            float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                            if (hypotenuse < FireRange)
                            {
                                FireToGroup(targetIndex, targetIndexInGroup);
                            }
                        }else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Random rand = new Random();
                    for (int i = 0; i < 10; i++)
                    {
                        if (Globals.humanunits.Count > 0)
                        {
                            int target = rand.Next((int)(Globals.humanunits.Count - 1));
                            float dX = position.X - Globals.humanunits[target].position.X;
                            float dY = position.Y - Globals.humanunits[target].position.Y;

                            float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                            if (hypotenuse < FireRange)
                            {
                                Fire(target);
                            }
                        }else
                        {
                            break;
                        }

                    }
                }
            }
        }

        public void FindFireTarget()
        {

            if (side)
            {
                if (Globals.groupsAI.Count > 0)
                {
                    Random rand = new Random();
                    int indGroup = 0;
                    int indSold = 0;
                    if (Globals.groupsAI.Count == 1)
                    {
                        indGroup = 0;
                        indSold = rand.Next((int)(Globals.groupsAI[indGroup].Second.Count - 1));
                    }
                    else
                    {
                        indGroup = rand.Next((int)(Globals.groupsAI.Count - 1));
                        indSold = rand.Next((int)(Globals.groupsAI[indGroup].Second.Count - 1));
                    }


                    float dX = position.X - Globals.groupsAI[indGroup].Second[indSold].position.X;
                    float dY = position.Y - Globals.groupsAI[indGroup].Second[indSold].position.Y;

                    float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                    if (hypotenuse < FireRange)
                    {
                        FireToGroup(indGroup, indSold);
                    }
                }
            }
            if (!side)
            {
                if (Globals.groups.Count > 0)
                {
                    Random rand = new Random();
                    int indGroup = 0;
                    int indSold = 0;
                    if (Globals.groups.Count == 1)
                    {
                        indGroup = 0;
                        indSold = rand.Next((int)(Globals.groups[indGroup].Second.Count - 1));
                    }
                    else
                    {
                        indGroup = rand.Next((int)(Globals.groups.Count - 1));
                        indSold = rand.Next((int)(Globals.groups[indGroup].Second.Count - 1));
                    }


                    float dX = position.X - Globals.groups[indGroup].Second[indSold].position.X;
                    float dY = position.Y - Globals.groups[indGroup].Second[indSold].position.Y;

                    float hypotenuse = (float)Math.Sqrt(dX * dX + dY * dY);
                    if (hypotenuse < FireRange)
                    {
                        FireToGroup(indGroup, indSold);
                    }
                }
            }
        }

        public void Fire(int EnemyIndex)
        {
            if (side) { Globals.Bullets.Add(new Bullet(position, Globals.aiunits[EnemyIndex].position)); Globals.aiunits[EnemyIndex].Die(EnemyIndex);  }
            if (!side) { Globals.Bullets.Add(new Bullet(position, Globals.humanunits[EnemyIndex].position)); Globals.humanunits[EnemyIndex].Die(EnemyIndex); }
            cooldown = 60 / FireRate;
            
        }
        public void FireToGroup(int groupIndex, int EnemyIndex)
        {
            if (side) 
            { 
                Globals.Bullets.Add(new Bullet(position, Globals.groupsAI[groupIndex].Second[EnemyIndex].position)); 
                Globals.groupsAI[groupIndex].Second[EnemyIndex].Die(EnemyIndex, groupIndex); 
            }
            if (!side) 
            {
                Globals.Bullets.Add(new Bullet(position, Globals.groups[groupIndex].Second[EnemyIndex].position));
                Globals.groups[groupIndex].Second[EnemyIndex].Die(EnemyIndex, groupIndex);
            }
            cooldown = 60 / FireRate;

        }
        public abstract void UpdateUnit(List<int> indexes);
        public abstract void UpdateUnit();

            
     
        public void Die(int index, int indexGroup)
        {
            if (side)
            {
                Globals.groups[indexGroup].Second.RemoveAt(index);
            }      
            else
            {
                Globals.groupsAI[indexGroup].Second.RemoveAt(index);
            } 
        }
        public void Die(int index)
        {
            if (side)
            {
                Globals.humanunits.RemoveAt(index);
            }
            else
            {
                Globals.aiunits.RemoveAt(index);
            }
        }

    }
}
