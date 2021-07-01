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
    static class MouseInput
    {
        static Texture2D rectangleBlock;
        static Vector2 startPosition;
        static Vector2 endPosition;
        static bool isSelect = false;

        public static void Update(GraphicsDevice device)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (Mouse.GetState().Y < 250 && Mouse.GetState().LeftButton == ButtonState.Pressed && Globals.lastMouseState.LeftButton == ButtonState.Released)
            {
                InterfaceState.InterfaceClick(Mouse.GetState());
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed && Globals.lastMouseState.RightButton == ButtonState.Released)
            {
                InterfaceState.InterfaceClick(Mouse.GetState());
            }
          

            Globals.lastMouseState = currentMouseState;
        }
        public static void Draw(GraphicsDevice device)
        {
            device.Clear(Color.Bisque);
            if (isSelect == true && Globals.wasSelected == false)
            {
                rectangleBlock = new Texture2D(device, 1, 1);
                Color xnaColorBorder = new Color(0, 128, 255, 20); // default color gray
                rectangleBlock.SetData(new[] { xnaColorBorder });

                //Globals._spriteBatch.Begin();
                Point position = new Point((int)(50 *Resolution.DetermineDrawScaling().X), (int)(startPosition.Y)); // position
                if (startPosition.Y > endPosition.Y)
                {
                    position = new Point((int)((int)50 * Resolution.DetermineDrawScaling().X), (int)(endPosition.Y));
                }
                Point size = new Point((int)(80 * Resolution.DetermineDrawScaling().X), (int)Math.Abs((int)endPosition.Y - (int)startPosition.Y)); // size

                Globals._spriteBatch.DrawString(Globals.font, new String(endPosition.X.ToString() +" "+ endPosition.Y.ToString()), new Vector2(300, 70), Color.Blue);
                Rectangle rectangle = new Rectangle(position, size);
                Globals._spriteBatch.Draw(rectangleBlock, rectangle, Color.White);
                //Globals._spriteBatch.End();
            }
            
        }
    }
}
