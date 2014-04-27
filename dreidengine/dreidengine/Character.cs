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
    public class Character : boxtest
    {
        List<Weapon> weapons;
        Weapon curWeapon;

        RayCollision rayColl;


        public Weapon CurWeapon
        {
            get { return curWeapon; }
        }
        KeyboardState ks, kold;
        
        public Character(Game game, Vector3 pos, Vector3 scale)
            : base(game, "box", pos, scale, true)
        {
            weapons = new List<Weapon>();
            curWeapon = null;
            rayColl = new RayCollision(((Game1)game).World.CollisionSystem); ;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateWeapons();


        }

        public void CheckCollisions()
        {
            float dist;
            CollisionSkin skin;
            Vector3 pos;
            Vector3 normal;

            CollisionSkinPredicate1 pred = new CollisionPredicate();

            bool hitObj = false;

            Vector3 delta = Body.Velocity;
            delta.Normalize();
            delta *= 5.0f;
            hitObj = rayColl.CastRay(out dist, out skin, out pos, out normal, Body.Position, delta, pred);

            if (hitObj)
            {
                Console.WriteLine("knifed " + ((RenderableObject.BodyExternalData)skin.Owner.ExternalData).RenderableObject.ToString());
                Console.WriteLine(dist.ToString() + " away from " + skin.ToString());
            }
        }

        public void UpdateWeapons()
        {
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Tab))
            {
                if (!kold.IsKeyDown(Keys.Tab))
                {
                    curWeapon.Deactivate();
                    weapons.Remove(curWeapon);
                    weapons.Add(curWeapon);
                    curWeapon = weapons.First();
                    curWeapon.Activate();
                    Console.WriteLine("CW: " + curWeapon.ToString());
                }
            }
            kold = ks;
        }

        public void PickUpWeapon(Weapon weapon)
        {
            if (curWeapon != null)
            {
                curWeapon.Deactivate();
            }
            curWeapon = weapon;
            weapons.Add(curWeapon);
            //Console.WriteLine("Weapons:");
            //foreach (RenderableObject w in Game.Components)
            //{
            //    Console.WriteLine(w.ToString());
            //}
            //Console.WriteLine("\n");
        }

        /*public int curAmmo()
        {
            Gun gun = CurWeapon.
            return ((Gun)gun).CurAmmo;
        }*/

    }
}
