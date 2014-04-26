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

        protected RayCollision rayColl;

        protected ButtonState lastLeftState;


        public Weapon(Game game, string name, Vector3 pos, Vector3 rot, float fireDelta, float damage, float range, bool automatic)
            : base(game, name, pos, Vector3.One, false, rot)
        {
            rayColl = new RayCollision(((Game1)game).World.CollisionSystem);

            Deactivate();
            
            this.fireDelta = fireDelta;
            this.damage = damage;
            this.range = range;
            this.automatic = automatic;
            lastFireDelta = 0;

            Body.Immovable = true;
            Body.DisableBody();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float elapsedTime = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerMillisecond;

            lastFireDelta += elapsedTime;

            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if ((automatic || lastLeftState == ButtonState.Released) && lastFireDelta > fireDelta && CanFire())
                {
                    lastFireDelta = 0;
                    Fire();
                }
            }

            lastLeftState = mouse.LeftButton;
        }

        public void Activate()
        {
            Enabled = true;
            Game.Components.Add(this);
        }

        public void Deactivate()
        {
            Enabled = false;
            Game.Components.Remove(this);
        }

        protected abstract bool CanFire();

        protected abstract void Fire();

    }
}
