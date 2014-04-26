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
    class Gun : Weapon
    {
        int maxAmmo;
        int curAmmo;
        int magSize;
        int magCur;
        float reloadDelta;
        bool reloading;
        float startReloadDelta;
        

        public Gun(Game game, string name, Vector3 pos, Vector3 rot, float fireDelta, float reloadDelta, float damage, float range, bool automatic, int maxAmmo, int magSize, int curAmmo, int magCur)
            : base(game, name, pos, rot, fireDelta, damage, range, automatic)
        {
            this.reloadDelta = reloadDelta;
            this.maxAmmo = maxAmmo;
            this.magSize = magSize;
            this.curAmmo = curAmmo;
            this.magCur = magCur;

            //Body.DisableBody();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float elapsedTime = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerMillisecond;


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

        }

        protected override bool CanFire()
        {
            if (magCur > 0 && !reloading)
                return true;
            return false;
        }

        protected override void Fire()
        {
            magCur--;
            curAmmo--;

            Console.WriteLine(magCur.ToString() + "/" + curAmmo.ToString());

            float dist;
            CollisionSkin skin;
            Vector3 pos;
            Vector3 normal;

            CollisionSkinPredicate1 pred = new BulletPredicate();

            bool hitObj = false;
            hitObj = rayColl.CastRay(out dist, out skin, out pos, out normal, Body.Position, Body.Orientation.Right * range, pred);

            if (hitObj)
            {
                Console.WriteLine("hit " + ((RenderableObject.BodyExternalData)skin.Owner.ExternalData).RenderableObject.ToString());
                DamageableObject obj = (DamageableObject)((RenderableObject.BodyExternalData)skin.Owner.ExternalData).RenderableObject;
                if (obj.TakesDamage)
                    obj.CurLife -= damage;
                Console.WriteLine(obj.CurLife.ToString());
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
