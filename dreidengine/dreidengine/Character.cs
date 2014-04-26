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
<<<<<<< HEAD

        public Weapon CurWeapon
=======
        Weapon CurWeapon
>>>>>>> a4b0fe46e8647912ec4133a12ff0d61b4af9af85
        {
            get { return curWeapon; }
        }
        KeyboardState ks, kold;
        
        public Character(Game game, Vector3 pos, Vector3 scale)
            : base(game, "box", pos, scale, true)
        {
            weapons = new List<Weapon>();
            curWeapon = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

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

        public int curAmmo(Weapon weapon)
        {
            Gun gun = (Gun) curWeapon;
            return gun.CurAmmo;
        }

    }
}
