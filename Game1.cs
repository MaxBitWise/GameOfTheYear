using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Performance_Test
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        static SpriteBatch _spriteBatch;
        List<Unit> humanunits = new List<Unit>();
        List<Unit> aiunits = new List<Unit>();
        List<Vector2> testArray = new List<Vector2>();
        Texture2D texture;
        Texture2D animtexture;
        Texture2D bigtex;
        Texture2D trenchtexleft;
        int counter = 0;
        double timer = 0;
        double distance = 0;
        Random rand = new Random();
        int currentFrame = 0;
        static Stopwatch stopWatch = new Stopwatch();
        SpriteFont font;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
           IsFixedTimeStep = true;  //Force the game to update at fixed time intervals
           TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0f);
            

            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
<<<<<<< Updated upstream
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("soldier");
            animtexture = Content.Load<Texture2D>("soldieranimation");
            bigtex = Content.Load<Texture2D>("soldierBIG");
            trenchtexleft = Content.Load<Texture2D>("trench");
            font = Content.Load<SpriteFont>("font");
            // TODO: use this.Content to load your game content here
        }
        void spawnsoldier()
        {
            for (int i = 0; i < 300; i++)
            {
                humanunits.Add(new Soldier(true));
                aiunits.Add(new Soldier(false));
            }
=======
            Globals._spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.texture = Content.Load<Texture2D>("soldier");
            Globals.animtexture = Content.Load<Texture2D>("soldieranimation");
            Globals.bigtex = Content.Load<Texture2D>("soldierBIG");
            Globals.trenchtexleft = Content.Load<Texture2D>("trench");
            Globals.Select25Tex = Content.Load<Texture2D>("Select25Tex");
            Globals.Select25SelectedTex = Content.Load<Texture2D>("Select25SelectedTex");
            Globals.Select50Tex = Content.Load<Texture2D>("Select50Tex");
            Globals.Select50SelectedTex = Content.Load<Texture2D>("Select50SelectedTex");
            Globals.Select75Tex = Content.Load<Texture2D>("Select75Tex");
            Globals.Select75SelectedTex = Content.Load<Texture2D>("Select75SelectedTex");
            Globals.Select100Tex = Content.Load<Texture2D>("Select100Tex");
            Globals.Select100SelectedTex = Content.Load<Texture2D>("Select100SelectedTex");
            Globals.font = Content.Load<SpriteFont>("font");
            Globals.MachinegunIconTex = Content.Load<Texture2D>("MachinegunIcon");
            Globals.MachinegunIconSelectedTex = Content.Load<Texture2D>("MachinegunIconSelected");
            Globals.BunkerIconTex = Content.Load<Texture2D>("BunkerIcon");
            Globals.BunkerIconSelectedTex = Content.Load<Texture2D>("BunkerIconSelected");
            Globals.InterfaceBackgroundTex = Content.Load<Texture2D>("InterfaceBackgroundTex");
            Globals.ArtilleryStrikeIconTex = Content.Load<Texture2D>("ArtilleryStrikeIcon");
            Globals.ArtilleryStrikeIconSelectedTex = Content.Load<Texture2D>("ArtilleryStrikeIconSelected");
            Globals.MenuIconTex = Content.Load<Texture2D>("MenuTex");
            Globals.MenuIconSelectedTex = Content.Load<Texture2D>("MenuTexSelected");
            Globals.TrenchUpIconTex = Content.Load<Texture2D>("TrenchUpIcon");
            Globals.TrenchUpIconSelectedTex = Content.Load<Texture2D>("TrenchUpIconSelected"); ;
            InterfaceState.InitButtons(); 

>>>>>>> Stashed changes
        }

        void updateSoldiers()
        {
            foreach (Soldier sold in humanunits) sold.UpdateSoldier();
            foreach (Soldier sold in aiunits) sold.UpdateSoldier();
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Escape) == true) this.Exit();
            if(humanunits.Count <= 100000) spawnsoldier();
            updateSoldiers();




            if (counter >= 10) { currentFrame += 1; currentFrame %= 4; counter = 0; }
            counter++;
            base.Update(gameTime);


            /* stopWatch.Start();
             double ts = stopWatch.Elapsed.TotalMilliseconds;
             stopWatch.Stop();*/
        }

        void drawBackground()
        {
            GraphicsDevice.Clear(Color.Bisque);
            _spriteBatch.Draw(trenchtexleft, new Vector2(50,0), Color.White);
            _spriteBatch.Draw(trenchtexleft, new Rectangle(1790,0,80,1080), null, Color.White, (float)Math.PI,new Vector2(trenchtexleft.Width,trenchtexleft.Height ), SpriteEffects.None,0F);
        }
        protected override void Draw(GameTime gameTime)
        {
            
            _spriteBatch.Begin();
            drawBackground();
            double fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            _spriteBatch.DrawString(font, fps.ToString(), new Vector2(300, 20), Color.White);
            _spriteBatch.DrawString(font, (humanunits.Count*2).ToString(), new Vector2(300, 10), Color.White);
            foreach (Soldier sold in humanunits)
                _spriteBatch.Draw(texture, sold.position, Color.White);
            foreach (Soldier sold in aiunits)
                _spriteBatch.Draw(texture, sold.position, Color.White);
            _spriteBatch.End();
            
            base.Draw(gameTime);

           

        }
    }
}



