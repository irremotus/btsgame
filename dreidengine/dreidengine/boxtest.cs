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
    class boxtest : RenderableObject
    {
        private bool flagMovable = false;
        private Vector3 moveVector = Vector3.Zero;
        private float amount = 1.0f;
        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        #region Constructors
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

         public boxtest(Game game, string name, Vector3 position, Vector3 scale, bool movable, Vector3 rotation)
             : base(game)
         {
             ModelName = name;
             Position = position;
             Scale = scale;
             flagMovable = movable;
             Rotation = rotation;
             setBody(position);
             this.Skin.callbackFn += new CollisionCallbackFn(handleCollisionDetection);
         }
        #endregion

        public override void Update(GameTime gameTime)
         {
             if (flagMovable)
             {
                 KeyboardState keys = ((Game1)this.Game).Keysp;
                 
                 moveVector = Vector3.Zero;
                 if (keys.IsKeyDown(Keys.W))
                     moveVector += new Vector3(0, 0, -1);
                 if (keys.IsKeyDown(Keys.S))
                     moveVector += new Vector3(0, 0, 1);
                 if (keys.IsKeyDown(Keys.A))
                     moveVector += new Vector3(-1, 0, 0);
                 if (keys.IsKeyDown(Keys.D))
                     moveVector += new Vector3(1, 0, 0);

                 if (keys.IsKeyDown(Keys.Up))
                     amount += 0.1f;
                 if (keys.IsKeyDown(Keys.Down))
                     amount -= 0.1f;

                 addToPosition(moveVector * amount);

                 if (((Game1)this.Game).HeightMapObj.HMI.IsOnHeightmap(Body.Position))
                     Body.Position = new Vector3(Body.Position.X, ((Game1)this.Game).HeightMapObj.HMI.GetHeight(Body.Position) + Scale.Y /2, Body.Position.Z);

                 Body.Velocity = Vector3.Lerp(Body.Velocity, Vector3.Zero, 0.4f);
                 Body.AngularVelocity = Vector3.Lerp(Body.AngularVelocity, Vector3.Zero, 0.1f);
             }
               
             base.Update(gameTime);
         }

        private void addToPosition(Vector3 vectorToAdd)
        {
            //Matrix camRot = Matrix.CreateRotationX(((Game1)(this.Game)).Camera.RotX) * Matrix.CreateRotationY(((Game1)this.Game).Camera.RotY);
            Matrix camRot = ((Game1)this.Game).Camera.Rotation;
            Vector3 rotVector = Vector3.Transform(vectorToAdd, camRot);
            Body.Position += rotVector;
        }

        public static bool handleCollisionDetection(CollisionSkin owner, CollisionSkin collidee)
        {
            // here is handled what happens if your Object collides with another special Object (= OtherObject)
            if (owner.Owner is Body && collidee.Owner is Body)
            {
                // YourObject hits OtherObject ( or vice versa) and Collision is processed
                //owner.Owner.Position -= owner.Owner.Velocity;
                Vector3 newvec = owner.Owner.Position - collidee.Owner.Position;
                collidee.Owner.ApplyBodyImpulse(newvec * 1000);
                //System.Windows.Forms.MessageBox.Show("Test");
                return true;
            }
            //else if (collidee.Equals(GhostObject.Skin))
            //{
            //    // this time you'll be able to walk through Ghost-like object
            //    return false;
            //}
            // all other collisions will be handled by physicengine
            return true;
        }
    }
}