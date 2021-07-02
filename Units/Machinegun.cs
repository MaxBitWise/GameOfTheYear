using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameTrench.Units
{
    class Machinegun : Unit
    {
        Machinegun(bool sidein, float Y)
        {
            if (sidein) position = new Vector2(10, Y);
            else position = new Vector2(1000, Y);
            side = sidein;
            hp = Globals.MGHP;
        }
    }
}
