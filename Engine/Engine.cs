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
            UpdateGroupInTrenchHum();
            UpdateGroupInTrenchAI();
        }

        public static void updateBullets()
        {
            var deadBullets = new List<int>();
            for(int i = 0; i < Globals.Bullets.Count; i++)
            {
                Globals.Bullets[i].UpdateBullet();
                if (Globals.Bullets[i].dead) { deadBullets.Add(i); }//Globals.Bullets.Remove(bullet); }
            }
            if (deadBullets.Count > 0)
            {
                for (int i = deadBullets.Count-1; i > 0; i--)
                {
                    Globals.Bullets.RemoveAt(deadBullets[i]);

                }
            }
        }

        public static void UpdateEngine(GraphicsDevice device)
        {
            Resolution.Update(Globals._graphics);
            MouseInput.Update(device);
            moneyUpdate();

            foreach(ArtilleryStrike Strike in Globals.Strikes)
            {
                Strike.UpdateArtilleryStrike();
            }

            if (Globals.humanunitsCount < 882)
            {
                spawnHumanunits();
            }
            if (Globals.aiunitsCount < 882)
            {
                spawnAiunits();
            }

            for (int i = 0; i < Globals.groups.Count; i++)
                updateGroup(i);

            updateSoldiers();
            updateBullets();

            if (MouseInput.CurrMode == MouseMode.Selected)
            {
                KeyboardInput.checkCreationGroup();
                if (Globals.creatGroup)
                {
                    selectionSoldiersHum();
                    Globals.creatGroup = false;
                    
                }
                
            }
        }

        private static int IncomeCounter = Globals.IncomeTimer;
        static void moneyUpdate()
        {
            if (IncomeCounter == 0)
            {
                Globals.MoneyBalance += Globals.IncomeValue;
                IncomeCounter = Globals.IncomeTimer;
            }
            else IncomeCounter--;
        }
        static void selectionSoldiersHum()
        {
            Tuple<GroupStates, List<Unit>, bool, Vector3> newGroup = new Tuple<GroupStates, List<Unit>, bool, Vector3>();
            newGroup.Second = new List<Unit>();
            var delSoldFromTrench = new List<int>();

            for (int i = 0; i < Globals.humanunits.Count; i++)
            {
                if (Globals.humanunits[i].position.X <= Globals.recOfLastSelection.Z &&
                    Globals.humanunits[i].position.X >= Globals.recOfLastSelection.X &&
                    Globals.humanunits[i].position.Y >= Globals.recOfLastSelection.Y &&
                    Globals.humanunits[i].position.Y <= Globals.recOfLastSelection.W)
                {
                    newGroup.Second.Add(Globals.humanunits[i]);
                    delSoldFromTrench.Add(i);
                }
            }
            if (newGroup.Second.Count != 0)
            {
                newGroup.First = GroupStates.Order;
                newGroup.Third = false;
                newGroup.Fourth = new Vector3(Globals.recOfLastSelection.Z, Globals.recOfLastSelection.Y, Globals.recOfLastSelection.W);
                Globals.groups.Add(newGroup);
            }
            if (delSoldFromTrench.Count > 0)
            {
                for (int i = delSoldFromTrench.Count - 1; i >= 0; i--)
                {
                    Globals.humanunits.RemoveAt(delSoldFromTrench[i]);
                }
            }    
        }
        static void rightGroup(int numberGroup)
        {
            if (Globals.groups.Count != 0)
            {
                int heightSelection = (int)(Globals.recOfLastSelection.W - Globals.recOfLastSelection.Y);
                int weightSelection = (int)(Globals.recOfLastSelection.Z - Globals.recOfLastSelection.X);
                int soldInLineStep = heightSelection / Globals.groups[numberGroup].Second.Count;               
                if (soldInLineStep < 8)
                {
                    soldInLineStep = 8;
                }
                int xInGroup = 130 - (soldInLineStep);
                int yInGroup = (int)Globals.recOfLastSelection.Y + (soldInLineStep / 2);
                int numberOfSoldier = 0;
                int soldiersLeft = Globals.groups[numberGroup].Second.Count;
                while (numberOfSoldier < Globals.groups[numberGroup].Second.Count)
                {
                    if (yInGroup < Globals.recOfLastSelection.W)
                    {
                        Globals.groups[numberGroup].Second[numberOfSoldier].destination.X = xInGroup;
                        Globals.groups[numberGroup].Second[numberOfSoldier].destination.Y = yInGroup;
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
                        Globals.groups[numberGroup].Second[numberOfSoldier].destination.X = xInGroup;
                        Globals.groups[numberGroup].Second[numberOfSoldier].destination.Y = yInGroup;
                        soldiersLeft--;
                    }
                    numberOfSoldier++;
                }
            }
            Globals.groups[numberGroup].Third = true;
        }
        
        static void moveAttackGroup(int numberGroup)
        {
            int numberOfSoldier = 0;
            while (numberOfSoldier < Globals.groups[numberGroup].Second.Count)
            {
                Globals.groups[numberGroup].Second[numberOfSoldier].destination.X += 1800;
                numberOfSoldier++;
            }
            Globals.groups[numberGroup].Third = true;
        }

        static void updateGroup(int numberGroup)
        {
            Globals.groups[numberGroup].Fourth = new Vector3(Globals.groups[numberGroup].Second[0].position.X, 
                Globals.groups[numberGroup].Fourth.Y, Globals.groups[numberGroup].Fourth.Z);
            if (Globals.groups[numberGroup].First == GroupStates.Order)
            {
                if (!Globals.groups[numberGroup].Third)
                    rightGroup(numberGroup);
                bool allInPlaces = true;
                foreach (Soldier sold in Globals.groups[numberGroup].Second) 
                {
                    if (sold.position.X != sold.destination.X ||
                        sold.position.Y != sold.destination.Y)
                    {
                        allInPlaces = false;
                        break;
                    }
                }
                if (allInPlaces)
                {
                    Globals.groups[numberGroup].Third = false;
                    Globals.groups[numberGroup].First = GroupStates.MoveAttack;
                }
                foreach (Unit sold in Globals.groups[numberGroup].Second)
                    sold.UpdateUnit();
            }
            else if (Globals.groups[numberGroup].First == GroupStates.MoveAttack)
            {
                if (!Globals.groups[numberGroup].Third)
                    moveAttackGroup(numberGroup);
                bool allInPlaces = true;
                foreach (Soldier sold in Globals.groups[numberGroup].Second)
                {
                    if (sold.position.X != sold.destination.X ||
                        sold.position.Y != sold.destination.Y)
                    {
                        allInPlaces = false;
                        break;
                    }
                }
                if (allInPlaces)
                {
                    Globals.groups[numberGroup].Third = false;
                    Globals.groups[numberGroup].First = GroupStates.Stand;
                }

                var RealIndex = new List<int>();

                for (int i = 0; i < Globals.groupsAI.Count; i++)
                {
                    float dXUp = Globals.groups[numberGroup].Fourth.X - Globals.groupsAI[i].Fourth.X;
                    float dYUp = Globals.groups[numberGroup].Fourth.Y - Globals.groupsAI[i].Fourth.Y;

                    float hypotenuseUp = (float)Math.Sqrt(dXUp * dXUp + dYUp * dYUp);
                    if (hypotenuseUp < Globals.SoldierRange)
                    {
                        RealIndex.Add(i);
                    }
                    float dXDown = Globals.groups[numberGroup].Fourth.X - Globals.groupsAI[i].Fourth.X;
                    float dYDown = Globals.groups[numberGroup].Fourth.Z - Globals.groupsAI[i].Fourth.Y;

                    float hypotenuseDown = (float)Math.Sqrt(dXDown * dXDown + dYDown * dYDown);
                    if (hypotenuseDown < Globals.SoldierRange)
                    {
                        RealIndex.Add(i);
                    }
                    
                }
                if (1790 - Globals.groups[numberGroup].Fourth.X < Globals.SoldierRange)
                {
                    RealIndex.Add(-1);
                }
                foreach (Unit sold in Globals.groups[numberGroup].Second)
                    sold.UpdateUnit(RealIndex);
            }
            else if (Globals.groups[numberGroup].First == GroupStates.Stand)
            {

            }
        }

        public static void Draw(GraphicsDevice device)
        {
            DrawRecOfMouse(device);
            DrawSoldiers();
            InterfaceState.DrawButtons(device);
            InterfaceState.DrawText(device);
            foreach (Bullet bullet in Globals.Bullets)
            {
                bullet.DrawBullet(device);
            }
            foreach (ArtilleryStrike Strike in Globals.Strikes)
            {
                Strike.DrawBlasts(device);
            }

        }

        public static void DrawRecOfMouse(GraphicsDevice device)
        {
            MouseInput.Draw(device);
        }
        public static void DrawSoldiers()
        {
            for (int i = 0; i < Globals.groups.Count; i++)
            {
                foreach (Unit sold in Globals.groups[i].Second)
                    Globals._spriteBatch.Draw(sold.UnitTex, new Rectangle(Resolution.ScaledPoint(new Point((int)sold.position.X, (int)sold.position.Y)),
                        Resolution.ScaledPoint(sold.drawSize)), Color.White);
            }
            
            foreach (Unit sold in Globals.humanunits)
                Globals._spriteBatch.Draw(sold.UnitTex, new Rectangle(Resolution.ScaledPoint(new Point((int)sold.position.X, (int)sold.position.Y)), 
                    Resolution.ScaledPoint(sold.drawSize)), Color.White);
            foreach (Unit sold in Globals.aiunits)
                Globals._spriteBatch.Draw(sold.UnitTex, new Rectangle(Resolution.ScaledPoint(new Point((int)sold.position.X, (int)sold.position.Y)),
                    Resolution.ScaledPoint(sold.drawSize)), Color.White);
        }
        

        public static void UpdateGroupInTrenchHum()
        {
            var RealIndex = new List<int>();
            for (int i = 0; i < Globals.groupsAI.Count; i++)
            {
                if (Globals.groupsAI[i].Fourth.X - 130 < Globals.SoldierRange)
                {
                    RealIndex.Add(i);
                }
            }
            foreach (Unit sold in Globals.humanunits) sold.UpdateUnit(RealIndex);
        }

        public static void UpdateGroupInFieldHum()
        {
            for (int i = 0; i < Globals.groups.Count; i++)
            {
                foreach (Unit sold in Globals.groups[i].Second)
                {
                    sold.UpdateUnit();
                }
            }
        }

        public static void UpdateGroupInTrenchAI()
        {
            var RealIndex = new List<int>();
            for (int i = 0; i < Globals.groupsAI.Count; i++)
            {
                if (Globals.groupsAI[i].Fourth.X - 130 < Globals.SoldierRange)
                {
                    RealIndex.Add(i);
                }
            }
            foreach (Unit sold in Globals.aiunits) sold.UpdateUnit(RealIndex);
        }

        public static void UpdateGroupInFieldAI()
        {
            for (int i = 0; i < Globals.groups.Count; i++)
            {
                foreach (Unit sold in Globals.groups[i].Second)
                {
                    sold.UpdateUnit();
                }
            }
        }
    }
}
