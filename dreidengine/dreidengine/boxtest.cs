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
    class boxtest : DamageableObject
    {
        private bool flagMovable = false;
        private Vector3 moveVector = Vector3.Zero;
        private Vector3 oldPosition;
        private float amount = 50.0f;
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
            : base(game, 100, 100)
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

                 addToVelocity(moveVector * amount);

                 if (((Game1)this.Game).HeightMapObj.HMI.IsOnHeightmap(Body.Position) && ((Game1)this.Game).HeightMapObj.HMI.GetHeight(Body.Position) > Body.Position.Y)
                     Body.Position = new Vector3(Body.Position.X, ((Game1)this.Game).HeightMapObj.HMI.GetHeight(Body.Position) + Scale.Y /2, Body.Position.Z);


                 //Body.Velocity = Vector3.Lerp(Body.Velocity, Vector3.Zero, 0.4f);
                 Body.Velocity = new Vector3(Vector3.Lerp(Body.Velocity, Vector3.Zero, 0.4f).X, Body.Velocity.Y, Vector3.Lerp(Body.Velocity, Vector3.Zero, 0.4f).Z);
                 //Body.Velocity = new Vector3(0, Body.Velocity.Y, 0);
                 Body.AngularVelocity = Vector3.Lerp(Body.AngularVelocity, Vector3.Zero, 0.1f);
             }

             if (!((Game1)this.Game).HeightMapObj.HMI.IsOnHeightmap(Body.Position))
                 Body.Position = oldPosition;

             oldPosition = Body.Position;
              
             base.Update(gameTime);
         }

        private void addToVelocity(Vector3 vectorToAdd)
        {
            Matrix camRot = ((Game1)this.Game).Camera.Rotation;
            Vector3 rotVector = Vector3.Transform(vectorToAdd, camRot);
            //Body.Velocity += rotVector;
            Body.Velocity = new Vector3(rotVector.X, Body.Velocity.Y, rotVector.Z);
        }
    }
}