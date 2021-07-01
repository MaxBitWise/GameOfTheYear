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

        public static Texture2D InterfaceBackgroundTex;
        public static Texture2D Select25Tex;
        public static Texture2D Select25SelectedTex;
        public static Texture2D Select50Tex;
        public static Texture2D Select50SelectedTex;
        public static Texture2D Select75Tex;
        public static Texture2D Select75SelectedTex;
        public static Texture2D Select100Tex;
        public static Texture2D Select100SelectedTex;
        public static Texture2D MachinegunIconTex;
        public static Texture2D MachinegunIconSelectedTex;
        public static Texture2D BunkerIconTex;
        public static Texture2D BunkerIconSelectedTex;
        public static Texture2D ArtilleryStrikeIconTex;
        public static Texture2D ArtilleryStrikeIconSelectedTex;
        public static Texture2D MenuIconTex;
        public static Texture2D MenuIconSelectedTex;
        public static Texture2D TrenchUpIconTex;
        public static Texture2D TrenchUpIconSelectedTex;


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
