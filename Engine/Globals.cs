using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameTrench
{
    static class Globals
    {
        public static MouseState lastMouseState;
        public static List<Unit> humanunits = new List<Unit>();
        public static List<Unit> aiunits = new List<Unit>();
        public static List<Vector2> testArray = new List<Vector2>();
        public static Texture2D texture;
        public static Texture2D animtexture;
        public static Texture2D bigtex;
        public static Texture2D trenchtexleft;
        public static double timer = 0;
        public static double distance = 0;
        public static Random rand = new Random();
        public static int currentFrame = 0;
        public static  Stopwatch stopWatch = new Stopwatch();
        public static SpriteFont font;
        public static int Width = 1920;
        public static int Height = 1080;

        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static void initGraphic(Game1 game)
        {
            _graphics = game._graphics;
        }

    }
}
