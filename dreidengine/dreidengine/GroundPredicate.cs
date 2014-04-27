using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;

namespace dreidengine
{
    class GroundPredicate : CollisionSkinPredicate1
    {
        public override bool ConsiderSkin(CollisionSkin skin0)
        {
            Game1 game = Game1.GetInstance();
            Body ground = game.HeightMapObj.PhysicsBody;
            if (skin0.Owner != null)
                return false;
            else
                return true;
        }
    }
}
