using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;

namespace dreidengine
{
    public class DamageableObject : RenderableObject
    {
        float maxLife;
        public float MaxLife
        {
            get { return maxLife; }
            set { maxLife = value; }
        }
        float curLife;
        public float CurLife
        {
            get { return curLife; }
            set { curLife = value; }
        }
        bool takesDamage;
        public bool TakesDamage
        {
            get { return takesDamage; }
            set { takesDamage = value; }
        }
        bool alive;
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public DamageableObject(Game game)
            : base(game)
        {
            takesDamage = false;
            this.maxLife = 1;
            this.curLife = 1;
            alive = true;
        }

        public DamageableObject(Game game, float maxLife, float curLife)
            : base(game)
        {
            takesDamage = false;
            this.maxLife = maxLife;
            this.curLife = curLife;
            alive = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (curLife <= 0 && alive)
            {
                Console.WriteLine("died");
                alive = false;
                Game1.GetInstance().World.RemoveBody(this.Body);
                Game1.GetInstance().Components.Remove(this);
            }
        }

    }
}
