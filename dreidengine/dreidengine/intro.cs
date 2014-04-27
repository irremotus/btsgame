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
    class intro : DrawableGameComponent
    {
        Texture2D blue;

        public intro(Game game, String waterName)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            blue = Game.Content.Load<Texture2D>("blue");
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = new SpriteBatch(Game.GraphicsDevice);
            sb.Begin();
            //sb.Draw(blue, new Microsoft.Xna.Framework.Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * 0.4f);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
