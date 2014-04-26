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
        bool automatic;

        RayCollision rayColl;

        ButtonState lastLeftState;
        
        
        public Gun(Game game, string name, Vector3 pos, float fireDelta, float reloadDelta, float damage, float range, bool automatic, int maxAmmo, int magSize, int curAmmo, int magCur)
            : base(game, name, pos, Vector3.One, false)
        {
            rayColl = new RayCollision(((Game1)game).World.CollisionSystem);

            this.fireDelta = fireDelta;
            this.reloadDelta = reloadDelta;
            this.damage = damage;
            this.range = range;
            this.automatic = automatic;
            this.maxAmmo = maxAmmo;
            this.magSize = magSize;
            this.curAmmo = curAmmo;
            this.magCur = magCur;

            Body.DisableBody();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float elapsedTime = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerMillisecond;

            lastFireDelta += elapsedTime;

            MouseState mouse = Mouse.GetState();

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


            if (mouse.LeftButton == ButtonState.Pressed)
                if ((automatic || lastLeftState == ButtonState.Released) && CanFire())
                    Fire();

            lastLeftState = mouse.LeftButton;

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
            magCur--;
            curAmmo--;

            Console.WriteLine(magCur.ToString() + "/" + curAmmo.ToString());

            float dist;
            CollisionSkin skin;
            Vector3 pos;
            Vector3 normal;

            CollisionSkinPredicate1 pred = new BulletPredicate();

            bool hitObj = false;
            hitObj = rayColl.CastRay(out dist, out skin, out pos, out normal, Body.Position, Body.Orientation.Forward * range, pred);

            if (hitObj)
            {
                Console.WriteLine("hit");
                //RenderableObject obj = ((RenderableObject.BodyExternalData)skin.Owner.ExternalData).RenderableObject;
                //if (obj.TakesDamage)
                //    obj.CurLife -= damage;
            }
        }

        void ReloadMag()
        {
            if (curAmmo > 0)
            {
                Console.WriteLine("reloading");
                reloading = true;
                startReloadDelta = 0;
                if (curAmmo > magSize)
                    magCur = magSize;
                else
                    magCur = curAmmo;
                curAmmo -= magCur;
            }
        }

    }
}
