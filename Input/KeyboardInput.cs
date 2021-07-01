using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GameTrench
{
    static class KeyboardInput
    {
        public static void checkCreationGroup()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Y) == true)
            {
                Globals.creatGroup = true;
            }else if (keystate.IsKeyDown(Keys.N) == true)
            {
                Globals.creatGroup = false;
                Globals.wasSelected = false;
            }
        }
    }
}
