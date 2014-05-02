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
    class Room : DrawableGameComponent
    {
        protected Vector3 position;
        protected Vector3 scale;

        protected Texture2D collisionTexture;

        protected Model model;

        protected Body body;
        public Body Body
        {
            get { return body; }

        }

        protected CollisionSkin skin;
        public CollisionSkin Skin
        {
            get { return skin; }
        }

        protected TriangleMesh triangleMesh;

        string modelName;

        public Room(Microsoft.Xna.Framework.Game game, Vector3 position, Vector3 scale, string name)
            : base(game)
        {
            this.position = position;
            this.scale = scale;
            modelName = name;
        }

        protected override void LoadContent()
        {
            
            model = Game.Content.Load<Model>(modelName);
            
            body = new Body();
            skin = new CollisionSkin(body);

            body.CollisionSkin = skin;

            triangleMesh = new TriangleMesh();

            List<Vector3> vertexList = new List<Vector3>();
            List<TriangleVertexIndices> indexList = new List<TriangleVertexIndices>();

            ExtractModelData(vertexList, indexList, model);

            triangleMesh.CreateMesh(vertexList, indexList, 4, 1.0f);

            skin.AddPrimitive(triangleMesh, new MaterialProperties(0.8f, 0.7f, 0.6f));

            Vector3 com = setMass(10.0f);

            body.MoveTo(position, Matrix.Identity);

            skin.ApplyLocalTransform(new JigLibX.Math.Transform(-com, Matrix.Identity));
            body.Immovable = true;
            body.EnableBody();
        }

        private Matrix getWorldMatrix()
        {
            return Matrix.CreateScale(scale) * skin.GetPrimitiveLocal(0).Transform.Orientation * body.Orientation * Matrix.CreateTranslation(body.Position);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1 game = (Game1)Game;

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix worldMatrix = getWorldMatrix();
            RasterizerState r = new RasterizerState();
            r.CullMode = CullMode.None;
            ((Game1)this.Game).GraphicsDevice.RasterizerState = r;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.Texture = collisionTexture;      // Assign the texture 
                    effect.TextureEnabled = false;           // Enable drawing the texture 
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = transforms[mesh.ParentBone.Index]
                                 * worldMatrix;
                    effect.View = ((Game1)this.Game).Camera.View;
                    effect.Projection = ((Game1)this.Game).Camera.Projection;
                }
                mesh.Draw();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected Vector3 setMass(float mass)
        {
            PrimitiveProperties primitiveProperties = new PrimitiveProperties(PrimitiveProperties.MassDistributionEnum.Solid,
                                                                              PrimitiveProperties.MassTypeEnum.Mass,
                                                                              mass);

            float junk;
            Vector3 com;
            Matrix it;
            Matrix itCoM;

            Skin.GetMassProperties(primitiveProperties, out junk, out com, out it, out itCoM);

            Body.BodyInertia = itCoM;
            Body.Mass = junk;

            return com;
        }

        protected void ExtractModelData(List<Vector3> vertices, List<TriangleVertexIndices> indices, Model model)
        {
            Matrix[] bones_ = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bones_);
            foreach (ModelMesh mm in model.Meshes)
            {
                Matrix xform = bones_[mm.ParentBone.Index];
                foreach (ModelMeshPart mmp in mm.MeshParts)
                {
                    int offset = vertices.Count;
                    Vector3[] a = new Vector3[mmp.NumVertices];

                    int stride = mmp.VertexBuffer.VertexDeclaration.VertexStride;
                    int nV = mmp.NumVertices;

                    mmp.VertexBuffer.GetData<Vector3>(mmp.VertexOffset * stride,
                        a, 0, mmp.NumVertices, stride);
                    for (int i = 0; i != a.Length; ++i)
                        Vector3.Transform(ref a[i], ref xform, out a[i]);
                    vertices.AddRange(a);

                    if (mmp.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
                        throw new Exception(
                            String.Format("Model uses 32-bit indices, which are not supported."));
                    short[] s = new short[mmp.PrimitiveCount * 3];
                    mmp.IndexBuffer.GetData<short>(mmp.StartIndex * 2, s, 0, mmp.PrimitiveCount * 3);
                    JigLibX.Geometry.TriangleVertexIndices[] tvi = new JigLibX.Geometry.TriangleVertexIndices[mmp.PrimitiveCount];
                    for (int i = 0; i != tvi.Length; ++i)
                    {
                        tvi[i].I0 = s[i * 3 + 2] + offset;
                        tvi[i].I1 = s[i * 3 + 1] + offset;
                        tvi[i].I2 = s[i * 3 + 0] + offset;
                    }
                    indices.AddRange(tvi);
                }
            }
        }
    }
}