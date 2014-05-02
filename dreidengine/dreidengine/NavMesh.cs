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
    public class NavMesh : DrawableGameComponent
    {
        Model navMeshModel;
        private string navMeshName;
        private HeightMapInfo heihgtMapInfo;
        public HeightMapInfo HMI { get { return heihgtMapInfo; } }

        public NavMesh(Game game, string name)
            : base(game)
        {
            navMeshName = name;
        }

        protected override void LoadContent()
        {
            navMeshModel = Game.Content.Load<Model>(navMeshName);
            heihgtMapInfo = navMeshModel.Tag as HeightMapInfo;
            base.LoadContent();
        }
        private Matrix GetWorldMatrix()
        {
            return
                Matrix.CreateScale(Vector3.One) *
                Matrix.CreateTranslation(Vector3.Zero);
        }
        public override void Draw(GameTime gameTime)
        {
            Game1 game = (Game1)Game;
            this.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            this.Game.GraphicsDevice.BlendState = BlendState.Opaque;
            this.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            this.Game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;


            Matrix[] transforms = new Matrix[navMeshModel.Bones.Count];
            navMeshModel.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix worldMatrix = GetWorldMatrix();

            foreach (ModelMesh mesh in navMeshModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = transforms[mesh.ParentBone.Index] * worldMatrix;
                    effect.View = game.Camera.View;
                    effect.Projection = game.Camera.Projection;
                }
                mesh.Draw();
            }

            base.Draw(gameTime);
        }
    }
}
