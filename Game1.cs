using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameTrench
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Globals.initGraphic(this);
            Globals._graphics.IsFullScreen = false;
            Globals._graphics.PreferredBackBufferHeight = Globals.Height;
            Globals._graphics.PreferredBackBufferWidth = Globals.Width;
            Engine.createTrenches();
            Content.RootDirectory = "Content";
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
            Globals._spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.texture = Content.Load<Texture2D>("soldier");
            Globals.animtexture = Content.Load<Texture2D>("soldieranimation");
            Globals.bigtex = Content.Load<Texture2D>("soldierBIG");
            Globals.trenchtexleft = Content.Load<Texture2D>("trench");
            Globals.font = Content.Load<SpriteFont>("font");
            Globals.gothic = Content.Load<SpriteFont>("gothic");
            Globals.fontBold = Content.Load<SpriteFont>("fontBold");
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
            Globals.TrenchUpIconSelectedTex = Content.Load<Texture2D>("TrenchUpIconSelected");
            Globals.BulletTex = Content.Load<Texture2D>("Bullet");
            Globals.MGFieldTex = Content.Load<Texture2D>("MGFieldTex");
            Globals.BunkerFieldTex = Content.Load<Texture2D>("BunkerFieldTex");
            Globals.BlastTex = Content.Load<Texture2D>("blast");
            InterfaceState.InitButtons();
        }


        
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Escape) == true) this.Exit();

            Engine.UpdateEngine(GraphicsDevice);
            Resolution.Update(_graphics);
            base.Update(gameTime);
        }

        void drawBackground()
        {
            GraphicsDevice.Clear(new Color(52,40,23));
            Globals._spriteBatch.Draw(Globals.trenchtexleft, new Rectangle((int)(50 * Resolution.DetermineDrawScaling().X), (int)(0 * Resolution.DetermineDrawScaling().Y),
                (int)(80 * Resolution.DetermineDrawScaling().X), (int)(1080 * Resolution.DetermineDrawScaling().Y)), null, Color.White, (float)Math.PI,
                new Vector2(Globals.trenchtexleft.Width, Globals.trenchtexleft.Height), SpriteEffects.FlipHorizontally, 0F);
            Globals._spriteBatch.Draw(Globals.trenchtexleft, new Rectangle((int)(1790 * Resolution.DetermineDrawScaling().X), (int)(0 * Resolution.DetermineDrawScaling().Y),
                (int)(80 * Resolution.DetermineDrawScaling().X), (int)(1080 * Resolution.DetermineDrawScaling().Y)), null, Color.White, (float)Math.PI,
                new Vector2(Globals.trenchtexleft.Width, Globals.trenchtexleft.Height ), SpriteEffects.None,0F);
        }
        protected override void Draw(GameTime gameTime)
        {
           
            Globals._spriteBatch.Begin();
            drawBackground();
            Engine.Draw(GraphicsDevice);
    //        double fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
    //        Globals._spriteBatch.DrawString(Globals.font, fps.ToString(), new Vector2(300, 20), Color.White);
    //        Globals._spriteBatch.DrawString(Globals.font, (Globals.humanunits.Count*2).ToString(), new Vector2(300, 10), Color.White);

            Globals._spriteBatch.End();
            
            base.Draw(gameTime);

        }
    }
}



