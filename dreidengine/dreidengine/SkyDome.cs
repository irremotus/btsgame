using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace dreidengine
{
    class SkyDome : RenderableObject
    {
        public SkyDome(Game1 game, string name, float scale)
            : base(game)
        {
            IsSky = true;
            ModelName = name;
            Position = new Vector3(0,-5000,0);
            Scale = Vector3.One * scale;
            Rotation = Vector3.Zero;
        }
    }
}
