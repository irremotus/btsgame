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
    class Camera : GameComponent
    {
        private Vector3 position;
        private RenderableObject followObject;
        private float aspectRatio;
        private float nearClip;
        private float farClip;
        private static Matrix _view;
        public static Matrix View
        {
            get { return _view; }
        }
        private Matrix _projection;
        public Matrix Projection
        {
            get { return _projection; }
        }
        private float _followDistance;
        public float FollowDistance
        {
            get { return _followDistance; }
            set { _followDistance = value; }
        }

        public Camera(Game game, RenderableObject followObject, float aspectRatio, float nearClip, float farClip)
            : base(game)
        {
            this.followObject = followObject;
            this.aspectRatio = aspectRatio;
            this.nearClip = nearClip;
            this.farClip = farClip;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector3 campos = new Vector3(0, 0.0f, 0.6f);
            Quaternion camrot = Quaternion.Identity;

            campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(camrot));
            campos += followObject.Body.Position;

            Vector3 camup = Vector3.Up;
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(camrot));

            camrot = Quaternion.Lerp(camrot, followObject.Body.Rotation, 0.1f);

            _view = Matrix.CreateLookAt(campos, followObject.Body.Position, camup);
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
        }
    }
}
