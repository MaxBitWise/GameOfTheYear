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
            if (Mouse.GetState().LeftButton == ButtonState.Pressed  && 
                Globals.lastMouseState.LeftButton == ButtonState.Released)
            {
                startPosition.X = currentMouseState.X;
                startPosition.Y = currentMouseState.Y;
                endPosition.X = currentMouseState.X;
                endPosition.Y = currentMouseState.Y;
                isSelect = true;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed  &&
                Globals.lastMouseState.LeftButton == ButtonState.Pressed && isSelect)
            {
                endPosition.X = currentMouseState.X;
                endPosition.Y = currentMouseState.Y;
                
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                isSelect = false;
            }
            Draw(device);
            Globals.lastMouseState = currentMouseState;
        }
        public static void Draw(GraphicsDevice device)
        {
            device.Clear(Color.CornflowerBlue);
            if (isSelect == true)
            {
               rectangleBlock = new Texture2D(device, 1, 1);
                 Color xnaColorBorder = new Color(128, 128, 128); // default color gray
                rectangleBlock.SetData(new[] { xnaColorBorder });

                Globals._spriteBatch.Begin();
                Point position = new Point((int)(startPosition.X), (int)(startPosition.Y)); // position
                Point size = new Point((int)endPosition.X-(int)startPosition.X, (int)endPosition.Y - (int)startPosition.Y); // size

                Globals._spriteBatch.DrawString(Globals.font, new String(endPosition.X.ToString() +" "+ endPosition.Y.ToString()), new Vector2(300, 10), Color.White);
                Rectangle rectangle = new Rectangle(position, size);
                Color color = new Color(255, 255, 0); // color yellow
                Globals._spriteBatch.Draw(rectangleBlock, rectangle, color);
                Globals._spriteBatch.End();
            }
            
            
        }
    }
}
