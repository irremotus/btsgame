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
    abstract class Weapon : boxtest
    {
        protected float fireDelta;
        protected float damage;
        protected float range;
        protected bool automatic;
        protected float lastFireDelta;

        protected bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; Console.WriteLine(this.GetType().ToString() + " is " + active.ToString()); }
        }

        protected RayCollision rayColl;

        protected ButtonState lastLeftState;


        public Weapon(Game game, string name, Vector3 pos, float fireDelta, float damage, float range, bool automatic)
            : base(game, name, pos, Vector3.One, false)
        {
            rayColl = new RayCollision(((Game1)game).World.CollisionSystem);

            active = false;
            
            this.fireDelta = fireDelta;
            this.damage = damage;
            this.range = range;
            this.automatic = automatic;
            lastFireDelta = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (active)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerMillisecond;

                lastFireDelta += elapsedTime;

                MouseState mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed)
                    if ((automatic || lastLeftState == ButtonState.Released) && CanFire())
                        Fire();

                lastLeftState = mouse.LeftButton;
            }
        }

        protected abstract bool CanFire();

        protected abstract void Fire();

    }
}
