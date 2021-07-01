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
        public static List<Soldier> humanunits = new List<Soldier>();
        public static int humanunitsCount = 0;
        public static List<Soldier> aiunits = new List<Soldier>();
        public static int aiunitsCount = 0;
        public static List<Vector2> testArray = new List<Vector2>();
        public static List<Vector3> trenchArrHum = new List<Vector3>();
        public static List<Vector3> trenchArrAi = new List<Vector3>();
        public static List<List<Soldier>> groups = new List<List<Soldier>>();
        public static bool wasSelected = false;
        public static bool creatGroup = false;
        public static bool writeTextForGroup = false;
        public static Vector4 recOfLastSelection = new Vector4();
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
        public static SpriteFont fontBold;
        public static int Width = 800;
        public static int Height = 600;
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static void initGraphic(Game1 game)
        {
            _graphics = game._graphics;
        }

    }
}
