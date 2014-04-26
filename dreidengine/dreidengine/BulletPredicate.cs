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
    class BulletPredicate : CollisionSkinPredicate1
    {
        public override bool ConsiderSkin(CollisionSkin skin0)
        {
            if (skin0.Owner != null && ((RenderableObject.BodyExternalData)skin0.Owner.ExternalData).RenderableObject.GetType() != typeof(PistolGun))
            {
                Console.WriteLine(((RenderableObject.BodyExternalData)skin0.Owner.ExternalData).RenderableObject.ToString());
                return true;
            }
                
            return false;
        }
    }
}
