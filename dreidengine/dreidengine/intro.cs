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
        boxtest b;
        Texture2D blue;
        bool drawT = false;

        public intro(Game game, String waterName)
            : base(game)
        {
            b = new boxtest(game, "box", new Vector3(0,500, 0), new Vector3(3000, 10, 3000));
            b.Body.Immovable = true;
            b.Body.Velocity = Vector3.Zero;
            b.DrawOrder = 250;
            //game.Components.Add(b);
        }

        public override void Update(GameTime gameTime)
        {
            if (((Game1)this.Game).Camera.Position.Y < 510)
                drawT = true;
            else
                drawT = false;
            base.Update(gameTime);
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
            sb.Draw(blue, new Microsoft.Xna.Framework.Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White * 0.4f);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
