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
        Vector3 oldPos;
        bool oldCol = false;
        RayCollision rayColl;

        float te;


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

            te += gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerMillisecond;
            if (te > 100)
            {
                te = 0;
                CheckCollisions(oldPos, oldCol);
            }
            oldPos = Body.Position;
        }

        public void CheckCollisions(Vector3 oldPos, bool oldCol)
        {
                float dist;
                CollisionSkin skin;
                Vector3 pos;
                Vector3 normal;
                float r;
                CollisionSkinPredicate1 pred = new CollisionPredicate();

                bool hitObj = false;

                Vector3 delta = Body.Velocity;// ((Game1)Game).Camera.Rotation.Forward;
                //delta.Y = 0.000001f;
                delta.Normalize();
                delta *= 5000.0f;
                Vector3 test = Body.Position + delta / 5000f;
                if (float.IsNaN(test.X))
                    return;
                hitObj = rayColl.CastRay(out dist, out skin, out pos, out normal, Body.Position + delta / 5000.0f, delta, pred);

                if (hitObj && skin.GetMaterialProperties(0).Equals((Object)new MaterialProperties(0.8f, 0.7f, 0.6f)))
                {
                    Console.WriteLine(dist.ToString() + ", " + Vector3.Distance(pos, Body.Position).ToString());
                    while ((r = Vector3.Distance(pos, Body.Position)) < 10.0f)
                    {
                        Console.WriteLine("I GAOOHWDWAD IM NORMAL"+normal.ToString());
                        //Body.ApplyBodyImpulse(500000f*normal);
                        //Body.Velocity = normal * 500000 * new Vector3(1, 0, 1);
                        Body.Velocity = -Body.Velocity;
                        Body.UpdateVelocity(50);
                        //Body.Position = Body.Position + (normal * 5000f);
                        oldCol = true;
                        return;
                        //Body.Velocity = normal * 500;
                        //Body.Velocity += normal * (1 / r) * (1 / r);
                        
                    }
                }
                oldCol = false;
          
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
