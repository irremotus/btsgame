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
using System.Windows;

namespace dreidengine
{
    public class spheretest : DrawableGameComponent
    {
        Vector3 Position;
        Vector3 Scale;
        string ModelName;
        Model model;

        private int ID;

        private CollisionSkin _skin;
        public CollisionSkin Skin
        {
            get { return _skin; }
        }

        private Body _body;
        public Body Body
        {
            get { return _body; }
        }

        public spheretest(Game game, string name, Vector3 position, Vector3 scale, int ID)
             : base(game)
         {
             this.ID = ID;
             ModelName = name;
             Position = position;
             Scale = scale;
             setBody(position);
             Game.Components.Add(this);
         }

        public void setBody(Vector3 position)
        {
            _body = new Body();
            _skin = new CollisionSkin(_body);
            _body.CollisionSkin = _skin;

            
            Sphere sphere = new Sphere(Position, Scale.X);
            
            
            //_skin.AddPrimitive(collisionPrimitive, new MaterialProperties(0.8f, 0.8f, 0.7f)); // why 2 primitives?

            _skin.AddPrimitive(sphere, (int)MaterialTable.MaterialID.NotBouncySmooth);

            Vector3 com = SetMass(1.0f);

            _body.MoveTo(Position, Matrix.Identity);
            _skin.ApplyLocalTransform(new JigLibX.Math.Transform(-com, Matrix.Identity));
            _body.EnableBody();
            PhysicsSystem.CurrentPhysicsSystem.CollisionSystem.AddCollisionSkin(_skin);

        }
        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>(ModelName);
            base.LoadContent();
        }
        private Vector3 SetMass(float mass)
        {
            PrimitiveProperties primitiveProperties = new PrimitiveProperties(
                PrimitiveProperties.MassDistributionEnum.Solid,
                PrimitiveProperties.MassTypeEnum.Mass, mass);

            float junk;
            Vector3 com;
            Matrix it;
            Matrix itCoM;

            Skin.GetMassProperties(primitiveProperties, out junk, out com, out it, out itCoM);

            Body.BodyInertia = itCoM;
            Body.Mass = junk;

            return com;
        }

        public override void Update(GameTime gameTime)
        {
            Position.Y = ((Game1)this.Game).HeightMapObj.HMI.GetHeight(Position);
            if(gameTime.TotalGameTime.Seconds >= 5)
                Body.Immovable = true;
            foreach (squid s in ((Game1)this.Game).sql)
            {
                if (s.ID == this.ID && Vector3.Distance(s.Position, this.Position) > this.Scale.X)
                    ((Game1)this.Game).Components.Remove(s);
            }
            

            base.Update(gameTime);
        }

        private Matrix GetWorldMatrix()
        {
            return
                Matrix.CreateScale(Scale) *
                _skin.GetPrimitiveLocal(0).Transform.Orientation *
                _body.Orientation *
                Matrix.CreateTranslation(_body.Position);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1 game = (Game1)Game;
            this.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            this.Game.GraphicsDevice.BlendState = BlendState.Opaque;
            this.Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            this.Game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix worldMatrix = GetWorldMatrix();

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.Alpha = 0.5f;
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
