using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dreidengine
{
    class debug : DrawableGameComponent
    {
 
        private BasicEffect basicEffect;
        private Vector3 startPointx = new Vector3(-10000f, 0, 0);
        private Vector3 endPointx = new Vector3(10000f, 0, 0);

        private Vector3 startPointy = new Vector3(0, -10000f, 0);
        private Vector3 endPointy = new Vector3(0, 10000f, 0);

        private Vector3 startPointz = new Vector3(0, 0, -10000f);
        private Vector3 endPointz = new Vector3(0, 0, 10000f);
    
        public int j = 0;

        public debug(Game game)
            : base(game)
        {

        }

        protected override void LoadContent()
        {
            Game1 game = (Game1)Game;
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.World = Matrix.Identity;
            basicEffect.View = game.Camera.View;
            basicEffect.Projection = game.Camera.Projection;
            basicEffect.VertexColorEnabled = true;
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            //basicEffect.TextureEnabled = false;
            //basicEffect.LightingEnabled = false;
            VertexPositionColor[] lines;
            lines = new VertexPositionColor[6];

            lines[0] = new VertexPositionColor(new Vector3(-10, 0, 0), Color.Red);

            lines[1] = new VertexPositionColor(new Vector3(10,0,0), Color.Red);

            lines[2] = new VertexPositionColor(new Vector3(0, -10, 0), Color.Green);

            lines[3] = new VertexPositionColor(new Vector3(0,10,0), Color.Green);

            lines[4] = new VertexPositionColor(new Vector3(0, 0, -10), Color.Blue);

            lines[5] = new VertexPositionColor(new Vector3(0,0,10), Color.Blue);


 /*
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lines, 0, 3);
                
            }
   */
            basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, lines, 0, 3);
            
            
            j++;
            base.Draw(gameTime);
        }
    }
}
