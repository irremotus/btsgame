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
    class RayCollision
    {
        CollisionSystem collSys;

        public RayCollision(CollisionSystem collSys)
        {
            this.collSys = collSys;
        }
        
        public bool CastRay(out float dist, out CollisionSkin skin, out Vector3 pos, out Vector3 normal, Vector3 start, Vector3 delta, CollisionSkinPredicate1 pred)
        {

            Segment seg = new Segment(start, delta);

            collSys.SegmentIntersect(out dist, out skin, out pos, out normal, seg, pred);

            if (skin != null)
                return true;
            return false;
        }

    }
}
