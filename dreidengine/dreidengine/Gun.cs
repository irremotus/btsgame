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
    class Gun : boxtest
    {
        float fireDelta;
        float damage;
        float range;
        int maxAmmo;
        int curAmmo;
        int magSize;
        int magCur;
        float reloadDelta;
        bool reloading;
        float startReloadDelta;
        float lastFireDelta;

        RayCollision rayColl;
        
        
        public Gun(Game game, string name, Vector3 pos)
            : base(game, name, pos, Vector3.One, false)
        {
            rayColl = new RayCollision(((Game1)game).World.CollisionSystem);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float elapsedTime = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerMillisecond;

            lastFireDelta += elapsedTime;

            KeyboardState keys = ((Game1)this.Game).Keysp;


            // reload stuff
            if (magCur == 0)
                ReloadMag();
            if (reloading)
            {
                if (startReloadDelta < reloadDelta)
                    startReloadDelta += elapsedTime;
                else
                    reloading = false;
            }


            if (keys.IsKeyDown(Keys.Space))
                if (CanFire())
                    Fire();

        }

        bool CanFire()
        {
            if (lastFireDelta > fireDelta && magCur > 0 && !reloading)
                return true;
            return false;
        }

        void Fire()
        {
            lastFireDelta = 0;

            float dist;
            CollisionSkin skin;
            Vector3 pos;
            Vector3 normal;

            CollisionSkinPredicate1 pred = new BulletPredicate();

            bool hitObj = false;
            hitObj = rayColl.CastRay(out dist, out skin, out pos, out normal, Body.Position, Body.Orientation.Forward * range, pred);

            if (hitObj)
            {
                
            }
        }

        void ReloadMag()
        {
            reloading = true;
            if (curAmmo > magSize)
                magCur = magSize;
            else
                magCur = curAmmo;
            curAmmo -= magCur;
        }

    }
}
