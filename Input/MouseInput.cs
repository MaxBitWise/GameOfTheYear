using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace GameTrench
{ 
    public struct MouseAdapted
    {
        public float rawX;
        public float rawY;
        public bool LeftPressed;
        public bool RightPressed;
        public MouseAdapted(MouseState FullState)
        {
            rawX = FullState.X;
            rawY = FullState.Y;
            if (FullState.LeftButton == ButtonState.Pressed) { LeftPressed = true; }
            else LeftPressed = false;
            if (FullState.RightButton == ButtonState.Pressed) { RightPressed = true; }
            else RightPressed = false;
        }
        public Vector2 ScaledVector2()
        {
            return new Vector2(X * Resolution.DetermineDrawScaling().X, Y * Resolution.DetermineDrawScaling().Y); 
        }
        public Point ScaledPoint()
        {
            return new Point((int)(X * Resolution.DetermineDrawScaling().X), (int)(Y * Resolution.DetermineDrawScaling().Y));
        }
        public float X
        {
            get { return rawX / Resolution.DetermineDrawScaling().X; }
        }
        public float Y
        {
            get { return rawY / Resolution.DetermineDrawScaling().Y; }
        }
    }

    public enum MouseMode
    {
        Default = 0,
        Selection = 1,
        Selected = 2,
        Select25 = 3,
        Select50 = 4,
        Select75 = 5,
        Select100 = 6,
        SetMG = 7,
        SetBunker = 8,
        SetArtillery = 9,
        MGUp = 10,
        TrenchUp = 11,
        BunkerUp = 12,
        ArtilleryStrikeUp = 13,
        MenuButton = 14
    }
    static class MouseInput
    {
        static Texture2D rectangleBlock;
        static Vector2 startPosition;
        static Vector2 endPosition;

        public static MouseMode CurrMode = MouseMode.Default;

        public static void Update(GraphicsDevice device)
        {

            MouseAdapted currentMouseState = new MouseAdapted(Mouse.GetState());
            if (currentMouseState.LeftPressed  && currentMouseState.X <= 200 && currentMouseState.Y > 200 &&
                !Globals.lastMouseState.LeftPressed && CurrMode == MouseMode.Default)
            {
                startPosition.X = currentMouseState.X;
                startPosition.Y = currentMouseState.Y;
                endPosition.X = currentMouseState.X;
                endPosition.Y = currentMouseState.Y;
                CurrMode = MouseMode.Selection;
            }
            if (currentMouseState.LeftPressed &&
               Globals.lastMouseState.LeftPressed && CurrMode == MouseMode.Selection)
            {
                endPosition.X = currentMouseState.X;
                endPosition.Y = currentMouseState.Y;
                
            }
            if (!currentMouseState.LeftPressed && CurrMode == MouseMode.Selection)
            {
                CurrMode = MouseMode.Selected;
                if (startPosition.Y < endPosition.Y)
                {
                    Globals.recOfLastSelection.X = 50;
                    Globals.recOfLastSelection.Y = startPosition.Y;
                    Globals.recOfLastSelection.Z = 130;
                    Globals.recOfLastSelection.W = endPosition.Y;
                }else
                {
                    Globals.recOfLastSelection.X = 50;
                    Globals.recOfLastSelection.Y = endPosition.Y;
                    Globals.recOfLastSelection.Z = 130;
                    Globals.recOfLastSelection.W = startPosition.Y;
                }


            }
            if (currentMouseState.Y < 250 && currentMouseState.LeftPressed && !Globals.lastMouseState.LeftPressed && CurrMode == MouseMode.Default)
            {
                InterfaceState.InterfaceClick(currentMouseState);
            }
            if (currentMouseState.RightPressed && !Globals.lastMouseState.RightPressed)
            {
                InterfaceState.InterfaceClick(currentMouseState);
                CurrMode = MouseMode.Default;
            }


            Globals.lastMouseState = currentMouseState;
        }
        public static void Draw(GraphicsDevice device)
        {
            device.Clear(Color.Bisque);
            if (CurrMode == MouseMode.Selection)
            {
                rectangleBlock = new Texture2D(device, 1, 1);
                Color xnaColorBorder = new Color(0, 128, 255, 20); // default color gray
                rectangleBlock.SetData(new[] { xnaColorBorder });

                //Globals._spriteBatch.Begin();
                Point position = new Point((int)(50 *Resolution.DetermineDrawScaling().X), (int)(startPosition.Y* Resolution.DetermineDrawScaling().Y)); // position
                if (startPosition.Y > endPosition.Y)
                {
                    position = new Point((int)((int)50 * Resolution.DetermineDrawScaling().X), (int)(endPosition.Y));
                }
                Point size = new Point((int)(80 * Resolution.DetermineDrawScaling().X), (int)((int)Math.Abs((int)endPosition.Y - (int)startPosition.Y) * Resolution.DetermineDrawScaling().Y)); // size

                Globals._spriteBatch.DrawString(Globals.font, new String(endPosition.X.ToString() +" "+ endPosition.Y.ToString()), new Vector2(300, 70), Color.Blue);
                Rectangle rectangle = new Rectangle(position, size);
                Globals._spriteBatch.Draw(rectangleBlock, rectangle, Color.White);
                //Globals._spriteBatch.End();
            }
            
        }
    }
}
