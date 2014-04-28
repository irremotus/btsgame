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
    public class squid : BillBoarding
    {
        Random r = new Random();
        int prevCol = 0;
        public int ID;
        public squid(Game game, String TexName, Vector3 pos, Vector2 size, Vector2 xy, float time, int ID)
            :base(game, TexName, pos, size, xy, time)
        {
            this.ID = ID;
            BillBoarding billy = new BillBoarding(game, TexName, pos, size, xy, time);
            Game.Components.Add(this);
        }
        public override void Update(GameTime gameTime)
        {
            createBillBoardVerticies();
            Vector3 dir = (((Game1)this.Game).Camera.Position - Position);
            dir.Normalize();
            dir.X = (r.Next() > 0.7) ? dir.X + (r.Next(-1, 1) / 2000) : dir.X;
            dir.Y = (r.Next() > 0.7) ? dir.Y + (r.Next(-1, 1) / 2000) : dir.Y;
            dir.Z = (r.Next() > 0.7) ? dir.Z + (r.Next(-1, 1) / 2000) : dir.Z;


            Position += dir/1000;
            
            if (Vector3.Distance(Position, ((Game1)this.Game).Camera.Position) < 10)
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
