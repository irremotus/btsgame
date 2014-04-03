using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace dreidengine
{
    class SkyDome : DrawableGameComponent
    {
        string name;
        float scale;
        Vector3 position;
        private Model skyDome;
        private Texture2D cloudMap;

        public SkyDome(Game1 game, string name, float scale)
            : base(game)
        {
            this.name = name;
            this.scale = scale;
            position = Vector3.Zero;
        }
        protected override void LoadContent()
        {
            skyDome = Game.Content.Load<Model>(name);
            cloudMap = Game.Content.Load<Texture2D>("cloudMap");
            base.LoadContent();
        }
        private void DrawSkyDome()
        {
            Matrix[] modelTransforms = new Matrix[skyDome.Bones.Count];
            skyDome.CopyAbsoluteBoneTransformsTo(modelTransforms);

            Matrix wMatrix = Matrix.CreateTranslation(0, -0.3f, 0) * Matrix.CreateScale(100) * Matrix.CreateTranslation(((Game1)this.Game).Camera.Position);
            foreach (ModelMesh mesh in skyDome.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    Matrix worldMatrix = modelTransforms[mesh.ParentBone.Index] * wMatrix;
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = worldMatrix;
                    effect.View = ((Game1)this.Game).Camera.View;
                    effect.Projection = ((Game1)this.Game).Camera.Projection;
                }
                mesh.Draw();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            DrawSkyDome();
            base.Draw(gameTime);
        }
    }
}
