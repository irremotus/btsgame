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
    class enemy : boxtest
    {
        public enemy(Game game, String modelName, Vector3 pos, Vector3 scale, int maxHP, int minHP)
            : base(game, modelName, pos, scale, false, maxHP, minHP)
        {

        }
    }
}
