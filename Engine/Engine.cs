using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameTrench
{
    static class Engine
    {
        public static void spawnsoldiers()
        {
            for (int i = 0; i < 300; i++)
            {
                Globals.humanunits.Add(new Soldier(true));
                Globals.aiunits.Add(new Soldier(false));
            }
        }
        public static void updateSoldiers()
        {
            foreach (Soldier sold in Globals.humanunits) sold.UpdateSoldier();
            foreach (Soldier sold in Globals.aiunits) sold.UpdateSoldier();
        }
        public static void UpdateEngine(GraphicsDevice device)
        {
            MouseInput.Update(device);
        }
        public static void DrawRecOfMouse(GraphicsDevice device)
        {
            MouseInput.Draw(device);
        }
    }
}
