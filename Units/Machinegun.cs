﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameTrench
{
    public class Machinegun : Unit
    {
        public Machinegun(bool sidein, float Y)
        {
            if (sidein) position = new Vector2(0, Y);
            else position = new Vector2(1000, Y);
            side = sidein;
            hp = Globals.MGHP;
            UnitTex = Globals.MGFieldTex;
            FireRate = Globals.MGFireRate;
            FireDmg = Globals.MGDmg;
            FireRange = 2000;//Globals.MGRange;
            FireAccuracy = Globals.MGAccuracy;
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
