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
        Texture2D _waterTex;
        String _waterName;
        VertexPositionTexture[] vertecies;
        //Vector3 pos = new Vector3(-100000, 50, -100000);
        Vector3 pos = new Vector3(0, 0, 20);
        float s = 100000f;
        Effect _waterE;

        public intro(Game game, String waterName)
            : base(game)
        {
            _waterName = waterName;
        }

        protected override void LoadContent()
        {
            _waterTex = Game.Content.Load<Texture2D>(_waterName);
            _waterE = Game.Content.Load<Effect>("waterPaneEffect");

            vertecies = new VertexPositionTexture[6];
            int i = 0;

            vertecies[i++] = new VertexPositionTexture(pos, new Vector2(0, 0));
            vertecies[i++] = new VertexPositionTexture(pos, new Vector2(s, 0));
            vertecies[i++] = new VertexPositionTexture(pos, new Vector2(s, s));

            vertecies[i++] = new VertexPositionTexture(pos, new Vector2(0, 0));
            vertecies[i++] = new VertexPositionTexture(pos, new Vector2(s, s));
            vertecies[i++] = new VertexPositionTexture(pos, new Vector2(0, s));


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _waterE.CurrentTechnique = _waterE.Techniques["TexturedNoShading"];
            _waterE.Parameters["xWorld"].SetValue(Matrix.Identity);
            _waterE.Parameters["xView"].SetValue(((Game1)this.Game).Camera.View);
            _waterE.Parameters["xProjection"].SetValue(((Game1)this.Game).Camera.Projection);
            _waterE.Parameters["xTexture"].SetValue(_waterTex);
            
            foreach (EffectPass pass in _waterE.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(Microsoft.Xna.Framework.Graphics.PrimitiveType.TriangleList, vertecies, 0, 2, VertexPositionTexture.VertexDeclaration);
            }
         
            base.Draw(gameTime);
        }
    }
}
