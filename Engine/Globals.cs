using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameTrench
{
    public enum GroupStates
    {
        Order = 0,
        MoveAttack = 1,
        Lie = 2,
        MoveTrench = 3,
        Stand = 4
    }
    public class Tuple<T, U, W>
    {
        public Tuple()
        {
        }

        public Tuple(T first, U second, W third)
        {
            this.First = first;
            this.Second = second;
            this.Third = third;
        }

        public T First { get; set; }
        public U Second { get; set; }
        public W Third { get; set; }
    };
    static class Globals
    {
        public static MouseAdapted lastMouseState;
        public static List<Soldier> humanunits = new List<Soldier>();
        public static int humanunitsCount = 0;
        public static List<Soldier> aiunits = new List<Soldier>();
        public static int aiunitsCount = 0;
        public static List<Vector2> testArray = new List<Vector2>();
        public static List<Vector3> trenchArrHum = new List<Vector3>();
        public static List<Vector3> trenchArrAi = new List<Vector3>();
        public static List<Tuple<GroupStates, List<Soldier>, bool>> groups = new List<Tuple<GroupStates, List<Soldier>, bool>>();
        public static bool wasSelected = false;
        public static bool creatGroup = false;
        public static bool writeTextForGroup = false;
        public static Vector4 recOfLastSelection = new Vector4();
        public static Texture2D texture;
        public static Texture2D animtexture;
        public static Texture2D bigtex;
        public static Texture2D trenchtexleft;

        #region Textures
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
        #endregion

        #region UnitsStats
        public static int Soldier = 400;
        public static int SoldierAccuracy = 50;
        public static int SoldierDmg = 50;
        public static int SoldierRange = 200;
        public static int SoldierFireRate = 1;
        public static int SoldierCost = 50;

        public static int MGHP = 400;
        public static int MGAccuracy = 5;
        public static int MGDmg = 50;
        public static int MGRange = 300;
        public static int MGFireRate = 10;
        public static int MGCost = 50;

        public static int BunkerHP = 2000;
        public static int BunkerAccuracy = 40;
        public static int BunkerDmg = 70;
        public static int BunkerRange = 500;
        public static int BunkerFireRate = 2;
        public static int BunkerCost = 200;

        #endregion

        public static double timer = 0;
        public static double distance = 0;
        public static Random rand = new Random();
        public static int currentFrame = 0;
        public static  Stopwatch stopWatch = new Stopwatch();
        public static SpriteFont font;
        public static SpriteFont fontBold;
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
