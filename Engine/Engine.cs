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
            for (int i = 0; Globals.aiunitsCount < 882; i++)
            {
                Globals.humanunits.Add(new Soldier(true));
                Globals.aiunits.Add(new Soldier(false));
                Globals.aiunitsCount++;
            }
        }
        public static void spawnHumanunits()
        {
            int j = 0;
            for (int i = 0; Globals.humanunitsCount < 882; i++)
            {
                if (Globals.trenchArrHum[j].Z == 0)
                {

                    Globals.humanunits.Add(new Soldier(true, new Vector2(Globals.trenchArrHum[j].X, Globals.trenchArrHum[j].Y)));
                    Globals.humanunitsCount++;
                    Globals.trenchArrHum.FindAll(s => s.X == Globals.trenchArrHum[j].X && s.Y == Globals.trenchArrHum[j].Y).
                        ForEach(x => x.Z = 1);
                    j++;
                }
            }
        }
        public static void spawnAiunits()
        {
            int j = 0;
            for (int i = 0; Globals.aiunitsCount < 882; i++)
            {
                if (Globals.trenchArrAi[j].Z == 0)
                {

                    Globals.aiunits.Add(new Soldier(false, new Vector2(Globals.trenchArrAi[j].X, Globals.trenchArrAi[j].Y)));
                    Globals.aiunitsCount++;
                    Globals.trenchArrAi.FindAll(s => s.X == Globals.trenchArrAi[j].X && s.Y == Globals.trenchArrAi[j].Y).
                        ForEach(x => x.Z = 1);
                    j++;
                }
            }
        }
        public static void createTrenches()
        {
            int x = 50;
            int y = 200;
            while (x < 130)
            {
                y = 200;
                while (y < 1080)
                {
                    Globals.trenchArrHum.Add(new Vector3(x, y, 0));
                    y += 9;
                }
                x += 9;
            }
            x = 1862;
            y = 200;
            while (x > 1788)
            {
                y = 200;
                while (y < 1080)
                {
                    Globals.trenchArrAi.Add(new Vector3(x, y, 0));
                    y += 9;
                }
                x -= 9;
            }
        }

        public static void updateSoldiers()
        {
            foreach (Soldier sold in Globals.humanunits) sold.UpdateSoldier();
            foreach (Soldier sold in Globals.aiunits) sold.UpdateSoldier();
        }

        public static void UpdateEngine(GraphicsDevice device)
        {
            Resolution.Update(Globals._graphics);
            MouseInput.Update(device);
            if (Globals.humanunitsCount < 882)
            {
                spawnHumanunits();
            }
            if (Globals.aiunitsCount < 882)
            {
                spawnAiunits();
            }
            updateSoldiers();
        }


/*        static void selectionSoldiersHum()
        {
            for(int i = 0; i < Globals.humanunits.Count; i++)
            {
                if 
            }
        }
*/

        public static void Draw(GraphicsDevice device)
        {
            DrawRecOfMouse(device);
            DrawSoldiers();
            InterfaceState.DrawButtons(device);
        }

        public static void DrawRecOfMouse(GraphicsDevice device)
        {
            MouseInput.Draw(device);
        }
        public static void DrawSoldiers()
        {
            foreach (Soldier sold in Globals.humanunits)
                Globals._spriteBatch.Draw(Globals.texture, sold.position, Color.White);
            foreach (Soldier sold in Globals.aiunits)
                Globals._spriteBatch.Draw(Globals.texture, sold.position, Color.White);
        }
    }
}
