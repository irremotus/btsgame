using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;

namespace dreidengine
{
    class boxtest : RenderableObject
    {
        public boxtest(Game game, string name) 
            : base(game)
        {
            ModelName = name;
        }
    }
}
