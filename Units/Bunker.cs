using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTrench
{
    public class Bunker : Unit
    {
        public Bunker(bool sidein, float Y)
        {
            if (sidein) position = new Vector2(0, Y);
            else position = new Vector2(1870, Y);
            side = sidein;
            hp = Globals.BunkerHP;
            UnitTex = Globals.BunkerIconTex;
            FireRate = Globals.BunkerFireRate;
            FireDmg = Globals.BunkerDmg;
            FireRange = Globals.BunkerRange;
            FireAccuracy = Globals.BunkerAccuracy;
            cooldown = 0;
            drawSize = new Point(48, 48);
        }
           

        public override void UpdateUnit()
        {
            if (cooldown > 0) { cooldown--; }
            else
            {
                FindFireTarget();
            }
        }
    }
}
