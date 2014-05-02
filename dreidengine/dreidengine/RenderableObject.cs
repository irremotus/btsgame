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
    public class RenderableObject : DrawableGameComponent
    {
        public class BodyExternalData
        {
            RenderableObject renderableObject;
            public RenderableObject RenderableObject
            {
                set { renderableObject = value; }
                get { return renderableObject; }
            }
        }

        private Vector3 position = Vector3.Zero;
        private Vector3 scale = Vector3.One;
        private Model model;
        private Vector3 rotation; //rotations radians stored in respective vector values
        
        private Matrix rotMatrix;
        public Matrix ROTmatrix { get { return rotMatrix; } set { rotMatrix = value; } }
        private Box collisionPrimitive;

        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; updatePosition(); }
        }

        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private string _modelName;
        public string ModelName
        {
            set { _modelName = value; }
        }
        

        private Body _body;
        public Body Body
        {
            get { return _body; }
        }

        private CollisionSkin _skin;
        public CollisionSkin Skin
        {
            get { return _skin; }
        }

        public RenderableObject(Game game) 
            : base(game)
        {
            setBody(position);  
        }

        public void setBody(Vector3 position)
        {
            _body = new Body();
            _skin = new CollisionSkin(_body);
            _body.CollisionSkin = _skin;

            _body.ExternalData = new BodyExternalData();
            ((BodyExternalData)_body.ExternalData).RenderableObject = this;

            rotMatrix = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
            //Box box = new Box(position, rotMatrix, scale);
            Box box = new Box(position, Matrix.Identity, scale); // rotation is relative to body
 
            //collisionPrimitive = new Box(position, rotMatrix, scale);
    
            //_skin.AddPrimit        ive(collisionPrimitive, new MaterialProperties(0.8f, 0.8f, 0.7f)); // why 2 primitives?

            _skin.AddPrimitive(box, (int)MaterialTable.MaterialID.NotBouncySmooth);

            Vector3 com = SetMass(1.0f);

            _body.MoveTo(position, rotMatrix);
            _skin.ApplyLocalTransform(new JigLibX.Math.Transform(-com, Matrix.Identity));
            _body.EnableBody();             
        }



        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>(_modelName);
        }

        void updatePosition()
        {
            _skin.ApplyLocalTransform(new JigLibX.Math.Transform(position, rotMatrix));
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
        private Matrix GetWorldMatrix()
        {
            return
                Matrix.CreateScale(scale) *
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
