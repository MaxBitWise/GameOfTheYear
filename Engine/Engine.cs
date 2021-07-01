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
            if (Globals.wasSelected)
            {
                KeyboardInput.checkCreationGroup();
                if (Globals.creatGroup)
                {
                    selectionSoldiersHum();
                    rightGroup();
                    //Globals.writeTextForGroup = true;
                    Globals.creatGroup = false;
                    
                }
                Globals.wasSelected = false;
            }
        }


        static void selectionSoldiersHum()
        {
            List<Soldier> newGroup = new List<Soldier>();
            for (int i = 0; i < Globals.humanunits.Count; i++)
            {
                if (Globals.humanunits[i].position.X <= Globals.recOfLastSelection.Z &&
                    Globals.humanunits[i].position.X >= Globals.recOfLastSelection.X &&
                    Globals.humanunits[i].position.Y >= Globals.recOfLastSelection.Y &&
                    Globals.humanunits[i].position.Y <= Globals.recOfLastSelection.W)
                {
                    newGroup.Add(Globals.humanunits[i]);
                }
            }
            Globals.groups.Add(newGroup);
        }
        static void rightGroup()
        {
            if (Globals.groups.Count != 0)
            {
                int heightSelection = (int)(Globals.recOfLastSelection.W - Globals.recOfLastSelection.Y);
                int weightSelection = (int)(Globals.recOfLastSelection.Z - Globals.recOfLastSelection.X);
                int soldInLineStep = heightSelection / Globals.groups[Globals.groups.Count - 1].Count;               
                if (soldInLineStep < 8)
                {
                    soldInLineStep = 8;
                }
                int xInGroup = 130 - (soldInLineStep);
                int yInGroup = (int)Globals.recOfLastSelection.Y + (soldInLineStep / 2);
                int numberOfSoldier = 0;
                int soldiersLeft = Globals.groups[Globals.groups.Count - 1].Count;
                while (numberOfSoldier < Globals.groups[Globals.groups.Count-1].Count)
                {
                    if (yInGroup < Globals.recOfLastSelection.W)
                    {
                        Globals.groups[Globals.groups.Count - 1][numberOfSoldier].destination.X = xInGroup;
                        Globals.groups[Globals.groups.Count - 1][numberOfSoldier].destination.Y = yInGroup;
                        soldiersLeft--;
                        yInGroup += soldInLineStep;
                    }else if (yInGroup >= Globals.recOfLastSelection.W)
                    {
                        if (heightSelection / soldiersLeft > 8)
                        {
                            soldInLineStep = heightSelection / soldiersLeft;
                        }
                        yInGroup = (int)Globals.recOfLastSelection.Y + soldInLineStep/2;
                        xInGroup -= 8;
                        Globals.groups[Globals.groups.Count - 1][numberOfSoldier].destination.X = xInGroup;
                        Globals.groups[Globals.groups.Count - 1][numberOfSoldier].destination.Y = yInGroup;
                        soldiersLeft--;
                    }
                    numberOfSoldier++;
                }
            }
        }

        static void moveCreatedGroup()
        {
            int numberOfSoldier = 0;
            while (numberOfSoldier < Globals.groups[Globals.groups.Count - 1].Count)
            {
                Globals.groups[Globals.groups.Count - 1][numberOfSoldier].destination.X += 640;
                numberOfSoldier++;
            }
        }


        public static void Draw(GraphicsDevice device)
        {
            DrawRecOfMouse(device);
            DrawSoldiers();

        }

        public static void DrawRecOfMouse(GraphicsDevice device)
        {
            MouseInput.Draw(device);
        }
        public static void DrawSoldiers()
        {
            foreach (Soldier sold in Globals.humanunits)
                Globals._spriteBatch.Draw(Globals.texture, new Rectangle(Resolution.ScaledPoint(new Point((int)sold.position.X, (int)sold.position.Y)), 
                    Resolution.ScaledPoint(new Point(8,8))), Color.White);
            foreach (Soldier sold in Globals.aiunits)
                Globals._spriteBatch.Draw(Globals.texture, new Rectangle(Resolution.ScaledPoint(new Point((int)sold.position.X, (int)sold.position.Y)),
                    Resolution.ScaledPoint(new Point(8, 8))), Color.White);
        }
/*
        public static void DrawTextForCreationGroup()
        {
            if (Globals.writeTextForGroup && Globals.creatGroup)
            {
                Globals._spriteBatch.DrawString(Globals.fontBold, new String("ГРУППА БЫЛА СОЗДАНА"), new Vector2(940, 1000), Color.Green);
                Globals.wasSelected = false;
                Globals.writeTextForGroup = false;
                Globals.creatGroup = false;
            }   
            else if (Globals.writeTextForGroup && !Globals.creatGroup)
            {
                Globals._spriteBatch.DrawString(Globals.fontBold, new String("ГРУППА НЕ БЫЛА СОЗДАНА"), new Vector2(940, 1000), Color.Red);
                Globals.wasSelected = false;
                Globals.creatGroup = false;
                Globals.writeTextForGroup = false;
            }
        }
*/
    }
}
