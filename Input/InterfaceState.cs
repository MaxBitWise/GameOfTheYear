using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTrench
{
    public struct Button
    {
        public string Name;
        public ButtonsIndexes EnumIndex;
        public Point Position; //Top left point for rectangle, center for circle
        public Point Shape; //If rectangle V2 = (X,Y), if circle (Radius)
        public Texture2D ButtonTex;
        public Texture2D ButtonTexSelected;
        public Texture2D CurrTex;
        public MouseMode OnClickMode;
    }

    public enum ButtonsIndexes
    {
        Nothing = 0,
        Select25 = 1,
        Select50 = 2,
        Select75 = 3,
        Select100 = 4,
        Machinegun = 5,
        Bunker = 6,
        ArtilleryStrike = 7,
        MenuButton = 8,
        TrenchUp = 9,
        MachinegunUp = 10,
        BunkerUp = 11,
        ArtileryStrikeUp = 12
    }
    public static class InterfaceState
    {
        public static ButtonsIndexes SelectedButton = ButtonsIndexes.Nothing;

        public static Button[] ButtonsArr;
        public static void InitButtons()
        {
            ButtonsArr = new Button[] {
             new Button
            {
                Name = "Nothing",
                EnumIndex = ButtonsIndexes.Select25,
                Position = new Point(-100, -100),
                Shape = new Point(1, 1),
                ButtonTex = Globals.Select25Tex,
                ButtonTexSelected = Globals.Select25SelectedTex,
                CurrTex = Globals.Select25Tex,
                OnClickMode = MouseMode.Default

            },
            new Button
            {
                Name = "Select25",
                EnumIndex = ButtonsIndexes.Select25,
                Position = new Point(10, 100),
                Shape = new Point(100, 40),
                ButtonTex = Globals.Select25Tex,
                ButtonTexSelected = Globals.Select25SelectedTex,
                CurrTex = Globals.Select25Tex,
                OnClickMode = MouseMode.Select25

            },
            new Button
            {
                Name = "Select50",
                EnumIndex = ButtonsIndexes.Select50,
                Position = new Point(130, 100),
                Shape = new Point(100, 40),
                ButtonTex = Globals.Select50Tex,
                ButtonTexSelected = Globals.Select50SelectedTex,
                CurrTex = Globals.Select50Tex,
                OnClickMode = MouseMode.Select50

            },
            new Button
            {
                Name = "Select75",
                EnumIndex = ButtonsIndexes.Select75,
                Position = new Point(250, 100),
                Shape = new Point(100, 40),
                ButtonTex = Globals.Select75Tex,
                ButtonTexSelected = Globals.Select75SelectedTex,
                CurrTex = Globals.Select75Tex,
                OnClickMode = MouseMode.Select75

            },
            new Button
            {
                Name = "Select100",
                EnumIndex = ButtonsIndexes.Select100,
                Position = new Point(370, 100),
                Shape = new Point(100, 40),
                ButtonTex = Globals.Select100Tex,
                ButtonTexSelected = Globals.Select100SelectedTex,
                CurrTex = Globals.Select100Tex,
                OnClickMode = MouseMode.Select100

            },
            new Button
            {
                Name = "Machinegun",
                EnumIndex = ButtonsIndexes.Machinegun,
                Position = new Point(650, 8),
                Shape = new Point(132, 132),
                ButtonTex = Globals.MachinegunIconTex,
                ButtonTexSelected = Globals.MachinegunIconSelectedTex,
                CurrTex = Globals.MachinegunIconTex,
                OnClickMode = MouseMode.SetMG

            },
            new Button
            {
                Name = "Bunker",
                EnumIndex = ButtonsIndexes.Bunker,
                Position = new Point(802, 8),
                Shape = new Point(132, 132),
                ButtonTex = Globals.BunkerIconTex,
                ButtonTexSelected = Globals.BunkerIconSelectedTex,
                CurrTex = Globals.BunkerIconTex,
                OnClickMode = MouseMode.SetBunker

            },
            new Button
            {
                Name = "ArtilleryStrike",
                EnumIndex = ButtonsIndexes.ArtilleryStrike,
                Position = new Point(954, 8),
                Shape = new Point(132, 132),
                ButtonTex = Globals.ArtilleryStrikeIconTex,
                ButtonTexSelected = Globals.ArtilleryStrikeIconSelectedTex,
                CurrTex = Globals.ArtilleryStrikeIconTex,
                OnClickMode = MouseMode.SetArtillery

            },
            new Button
            {
                Name = "MenuButton",
                EnumIndex = ButtonsIndexes.MenuButton,
                Position = new Point(10, 8),
                Shape = new Point(200, 70),
                ButtonTex = Globals.MenuIconTex,
                ButtonTexSelected = Globals.MenuIconSelectedTex,
                CurrTex = Globals.MenuIconTex,
                OnClickMode = MouseMode.MenuButton

            },
            new Button
            {
                Name = "TrenchUp",
                EnumIndex = ButtonsIndexes.TrenchUp,
                Position = new Point(1200, 8),
                Shape = new Point(60, 60),
                ButtonTex = Globals.TrenchUpIconTex,
                ButtonTexSelected = Globals.TrenchUpIconSelectedTex,
                CurrTex = Globals.TrenchUpIconTex,
                OnClickMode = MouseMode.TrenchUp

            },
            new Button
            {
                Name = "MachinegunUp",
                EnumIndex = ButtonsIndexes.MachinegunUp,
                Position = new Point(1200, 78),
                Shape = new Point(60, 60),
                ButtonTex = Globals.MachinegunIconTex,
                ButtonTexSelected = Globals.MachinegunIconSelectedTex,
                CurrTex = Globals.MachinegunIconTex,
                OnClickMode = MouseMode.MGUp

            },
            new Button
            {
                Name = "BunkerUp",
                EnumIndex = ButtonsIndexes.BunkerUp,
                Position = new Point(1270, 8),
                Shape = new Point(60, 60),
                ButtonTex = Globals.BunkerIconTex,
                ButtonTexSelected = Globals.BunkerIconSelectedTex,
                CurrTex = Globals.BunkerIconTex,
                OnClickMode = MouseMode.BunkerUp

            },
            new Button
            {
                Name = "ArtileryStrikeUp",
                EnumIndex = ButtonsIndexes.ArtileryStrikeUp,
                Position = new Point(1270, 78),
                Shape = new Point(60, 60),
                ButtonTex = Globals.ArtilleryStrikeIconTex,
                ButtonTexSelected = Globals.ArtilleryStrikeIconSelectedTex,
                CurrTex = Globals.ArtilleryStrikeIconTex,
                OnClickMode = MouseMode.ArtilleryStrikeUp

            },

            };

        }
       // TrenchUp = 9,
       // MachinegunUp = 10,
       // BunkerUp = 11,
       // ArtileryStrikeUp = 12

        public static void InterfaceClick(MouseAdapted Mouse)
        {
            bool missClick = true;
            foreach( Button button in ButtonsArr)
            {
                if(Mouse.X > button.Position.X && 
                   Mouse.X < button.Position.X + button.Shape.X &&
                   Mouse.Y > button.Position.Y &&
                   Mouse.Y < button.Position.Y + button.Shape.Y)
                {
                    ButtonsArr[(int)SelectedButton].CurrTex = ButtonsArr[(int)SelectedButton].ButtonTex;
                    SelectedButton = button.EnumIndex;
                    ButtonsArr[(int)SelectedButton].CurrTex = ButtonsArr[(int)SelectedButton].ButtonTexSelected;
                    missClick = false;
                    MouseInput.CurrMode = button.OnClickMode;
                    break;
                }

            }
            if(missClick)
            {
                ButtonsArr[(int)SelectedButton].CurrTex = ButtonsArr[(int)SelectedButton].ButtonTex;
                SelectedButton = ButtonsIndexes.Nothing;
                MouseInput.CurrMode = MouseMode.Default;
            }
        }

        public static void Deselect()
        {
            ButtonsArr[(int)SelectedButton].CurrTex = ButtonsArr[(int)SelectedButton].ButtonTex;
            SelectedButton = ButtonsIndexes.Nothing;
        }

        
        
        public static void DrawButtons(GraphicsDevice device)
        {
            Globals._spriteBatch.Draw(Globals.InterfaceBackgroundTex, new Rectangle(0,0, (int)(1920 *Resolution.DetermineDrawScaling().X), (int)(200 * Resolution.DetermineDrawScaling().Y)), Color.White);
            var buttonBack = new Texture2D(device, 1, 1);
            Color buttonBackColor = new Color(0, 128, 255, 20); // default color gray
            buttonBack.SetData(new[] { buttonBackColor });
            foreach (Button button in ButtonsArr)
            {
                Globals._spriteBatch.Draw(button.CurrTex, new Rectangle(Resolution.ScaledPoint(button.Position), Resolution.ScaledPoint(button.Shape)), Color.White);
            }
        }

        public static void DrawText(GraphicsDevice device)
        {
         
            var buttonBack = new Texture2D(device, 1, 1);
            Color buttonBackColor = new Color(0, 128, 255, 20); // default color gray
            buttonBack.SetData(new[] { buttonBackColor });
            Globals._spriteBatch.DrawString(Globals.font, new String("Balance OS: \n" + Globals.MoneyBalance.ToString()), new Vector2(450, 20), Color.Black);

            foreach (Button button in ButtonsArr)
            {
                Globals._spriteBatch.Draw(button.CurrTex, new Rectangle(Resolution.ScaledPoint(button.Position), Resolution.ScaledPoint(button.Shape)), Color.White);
            }
        }


    }
}
