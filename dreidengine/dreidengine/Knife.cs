﻿using System;
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
    class Knife : Weapon
    {
        public Knife(Game game, Vector3 pos)
            : base(game, "Knife", pos, new Vector3(MathHelper.PiOver2 + 0.9f, MathHelper.PiOver2 - 0.8f, MathHelper.PiOver4 + 1.3f), 500, 20, 10, false)
        {
            carryPos = new Vector3(-0.3f, 0.0f, -1.5f);
        }

        protected override bool CanFire()
        {
            return true;
        }

        protected override void Fire()
        {
            float dist;
            CollisionSkin skin;
            Vector3 pos;
            Vector3 normal;

            CollisionSkinPredicate1 pred = new BulletPredicate();

            bool hitObj = false;
            hitObj = rayColl.CastRay(out dist, out skin, out pos, out normal, Body.Position, Body.Orientation.Right * range, pred);

            if (hitObj)
            {
                Console.WriteLine("knifed " + ((RenderableObject.BodyExternalData)skin.Owner.ExternalData).RenderableObject.ToString());
                DamageableObject obj = (DamageableObject)((RenderableObject.BodyExternalData)skin.Owner.ExternalData).RenderableObject;
                if (obj.TakesDamage)
                    obj.CurLife -= damage;
                Console.WriteLine(obj.CurLife.ToString());
            }
        }

    }
}
