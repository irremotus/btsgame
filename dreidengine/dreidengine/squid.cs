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
    class squid : enemy
    {
        Random r = new Random();
        int prevCol = 0;
        public squid(Game game, String modelName, Vector3 pos, Vector3 scale, int maxHP, int minHP)
            : base(game, modelName, pos, scale, maxHP, minHP)
        {
            TakesDamage = true;
        }
        public override void Update(GameTime gameTime)
        {
            Vector3 dir = (((Game1)this.Game).Camera.Position - Body.Position);
            dir.Normalize();
            dir.X = (r.Next() > 0.7) ? dir.X + (r.Next(-1, 1) / 2000) : dir.X;
            dir.Y = (r.Next() > 0.7) ? dir.Y + (r.Next(-1, 1) / 2000) : dir.Y;
            dir.Z = (r.Next() > 0.7) ? dir.Z + (r.Next(-1, 1) / 2000) : dir.Z;
            Body.Velocity = dir * 20f;
            Body.AngularVelocity = Vector3.Lerp(Body.AngularVelocity, Vector3.Zero, 0.1f);
            
            Matrix m = Matrix.CreateFromAxisAngle(dir, MathHelper.Pi / 16);
            Body.ApplyBodyAngImpulse(new Vector3(0f, 0f, 0.1f));
            if (Vector3.Distance(Body.Position, ((Game1)this.Game).Camera.Position) < 10)
            {
                if (prevCol == 0)
                {
                    ((Game1)this.Game).C1.CurLife -= 20;
                    prevCol++;
                }
                else
                {
                    prevCol++;
                    if (prevCol == 300)
                        prevCol = 0;
                }
            }
            base.Update(gameTime);
        }
    }
}
