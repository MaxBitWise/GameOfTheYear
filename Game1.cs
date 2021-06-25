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



