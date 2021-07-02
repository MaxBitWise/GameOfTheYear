using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameTrench
{
    public class ArtilleryStrike
    {
        Vector2 Center;
        public ArtilleryStrike(Vector2 Vin)
        {
            Center = Vin;
           
        }
        Random rand = new Random();
        private bool started = false;
        private int counter = 300;
        private int smallcounter = 50;
        private int BlastsCounter = 0;
        private int StrikesCount = Globals.StrikesCount;
        private int StrikeSize = Globals.StrikesSize;
        Vector3[] Blasts = new Vector3[Globals.StrikesCount];

        public void UpdateArtilleryStrike()
        {
            if (!started) WaitToStart();
            if (started)
            {

                WaitAndBoom();

            }
            UpdateBlasts();
            ClearBlasts();
        }

        
        void WaitToStart()
        {
            if (counter > 0) counter--;
            else started = true;
        }

        void WaitAndBoom()
        {
            if (smallcounter > 0) smallcounter--;
            else
            {
                smallcounter = 25;
                Blast();
            }
        }

        void Blast()
        {
            if (BlastsCounter < StrikesCount)
            {
                Vector3 Spreaded = SpreadBlast();
                Blasts[BlastsCounter] = Spreaded;
                BlastsCounter++;
            }
        }
        Vector3 SpreadBlast()
        {
            int dX = (int)(Center.X + rand.Next(20 * 10) - 20 * 5);
            int dY = (int)(Center.Y + rand.Next(20 * 10) - 20 * 5);
            // int dX = (int)(Center.X + rand.Next(Globals.StrikesSize*10) - Globals.StrikesSize * 5);
            // int dY = (int)(Center.Y + rand.Next(Globals.StrikesSize * 10) - Globals.StrikesSize * 5);
            return new Vector3(dX, dY, 0);
        }
        void UpdateBlasts()
        {
            for(int i = 0; i < BlastsCounter; i++)
            {
                if(Blasts[i].Z >=0) Blasts[i].Z++;
            }
        }

        void ClearBlasts()
        {
            for (int i = 0; i < BlastsCounter; i++)
            {
                if (Blasts[i].Z >= 180) Blasts[i].Z = -1;
            }
        }

        public void DrawBlasts(GraphicsDevice device)
        {
            for(int i = 0; i < BlastsCounter; i++)
            {
                if (Blasts[i].Z != -1)
              Globals._spriteBatch.Draw(Globals.BlastTex, new Rectangle(Resolution.ScaledPoint(new Point((int)Blasts[i].X, (int)Blasts[i].Y)), Resolution.ScaledPoint(new Point(StrikeSize))), Color.White);
            }
        }

    }
}
