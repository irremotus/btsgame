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

namespace dreidengine
{
    class boxtest : RenderableObject
    {
        private bool flagMovable = false;

        public boxtest(Game game, string name) 
            : base(game)
        {
            ModelName = name;
        }
        public boxtest(Game game, string name, Vector3 position)
            : base(game)
        {
            ModelName = name;
            Position = position;
            setBody(position);
        }
        public boxtest(Game game, string name, Vector3 position, Vector3 scale)
            : base(game)
        {
            ModelName = name;
            Position = position;
            Scale = scale;
            setBody(position);
        }

         public boxtest(Game game, string name, Vector3 position, Vector3 scale, bool movable)
            : base(game)
        {
            ModelName = name;
            Position = position;
            Scale = scale;
            flagMovable = movable;
            setBody(position);
        }

         public override void Update(GameTime gameTime)
         {
             if (flagMovable)
             {
                 if (((Game1)this.Game).Keysp.IsKeyDown(Keys.W))
                     Body.Position = new Vector3(Body.Position.X, Body.Position.Y, Body.Position.Z - 1);
                 if (((Game1)this.Game).Keysp.IsKeyDown(Keys.S))
                     Body.Position = new Vector3(Body.Position.X, Body.Position.Y, Body.Position.Z + 1);
                 if (((Game1)this.Game).Keysp.IsKeyDown(Keys.A))
                     Body.Position = new Vector3(Body.Position.X - 1, Body.Position.Y, Body.Position.Z);
                 if (((Game1)this.Game).Keysp.IsKeyDown(Keys.D))
                     Body.Position = new Vector3(Body.Position.X + 1, Body.Position.Y, Body.Position.Z);

                 if (((Game1)this.Game).HeightMapObj.HMI.IsOnHeightmap(Body.Position))
                     Body.Position = new Vector3(Body.Position.X, ((Game1)this.Game).HeightMapObj.HMI.GetHeight(Body.Position), Body.Position.Z);
             }

             base.Update(gameTime);
         }
    }
}