using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;
using System.Windows;

namespace dreidengine
{
    class Character : boxtest
    {
        public Character(Game game, Vector3 pos, Vector3 scale)
            : base(game, "box", pos, scale, true)
        {            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);      
        }

    }
}
