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
    public class Camera : GameComponent
    {
        private RenderableObject followObject;
        private float aspectRatio;
        private float nearClip;
        private float farClip;
        private Vector3 _position;
        public Vector3 Position
        {
            get { return _position; }
        }
        private Vector3 _lookat;
        public Vector3 Lookat
        {
            get { return _lookat; }
            set { _lookat = value; }
        }
        /*private Vector3 _positionOffset;
        public Vector3 PositionOffset
        {
            get { return _positionOffset; }
            set { _positionOffset = value; }
        }*/
        private Quaternion rotation;
        private Matrix _view;
        public Matrix View
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

        public Camera(Game game, RenderableObject followObject, float followDistance, float aspectRatio)
            : this(game, followObject, followDistance, aspectRatio, 0.1f, 10000.0f)
        {
            // calls Camera constructor with 0.1f and 10000.0f as clipping planes
        }
        
        public Camera(Game game, RenderableObject followObject, float followDistance, float aspectRatio, float nearClip, float farClip)
            : base(game)
        {
            this.followObject = followObject;
            this._followDistance = followDistance;
            this.aspectRatio = aspectRatio;
            this.nearClip = nearClip;
            this.farClip = farClip;

            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 campos = new Vector3(0, 0, 0);

            Vector3 camup = Vector3.Up;
            camup = Vector3.Transform(camup, followObject.Body.Orientation);

            //rotation = Quaternion.Lerp(rotation, objrot, 0.5f);

            Matrix lookatFromObj = Matrix.CreateLookAt(followObject.Body.Position, _lookat, camup);
            Vector3 scale;
            Quaternion rot;
            Vector3 trans;
            lookatFromObj.Decompose(out scale, out rot, out trans);
            rotation = Quaternion.Lerp(rotation, rot, 0.5f);
            Vector3 followVector = new Vector3(0.0f, 0.0f, _followDistance);
            followVector = Vector3.Transform(followVector, rotation);
            campos = followObject.Body.Position + new Vector3(-followVector.X, ((_lookat.Z < 0) ? -followVector.Y : followVector.Y), followVector.Z);
            _view = Matrix.CreateLookAt(campos, _lookat, camup);
            _position = campos;

            base.Update(gameTime);
        }

        public void ChangeLook(Vector3 angles)
        {
            Quaternion newlook = Quaternion.CreateFromYawPitchRoll(angles.X, angles.Y, angles.Z);
            newlook.Normalize();
            _lookat = Vector3.Transform(_lookat, newlook);
        }
    }
}
