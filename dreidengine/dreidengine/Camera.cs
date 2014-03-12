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
        private RenderableObject followObject; /* for RenderableObject */
        //private BoxActor followObject;
        private float aspectRatio;
        private float nearClip;
        private float farClip;
        private Vector3 _position;
        public Vector3 Position
        {
            get { return _position; }
        }
        private Vector3 _positionOffset;
        public Vector3 PositionOffset
        {
            get { return _positionOffset; }
            set { _positionOffset = value; }
        }
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
        private Vector3 _followDistance;
        public Vector3 FollowDistance
        {
            get { return _followDistance; }
            set { _followDistance = value; }
        }

        public Camera(Game game, RenderableObject/* for RenderableObject */  followObject, Vector3 followDistance, float aspectRatio, float nearClip, float farClip)
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
            Vector3 campos = _followDistance;

            campos = Vector3.Transform(campos, Matrix.CreateFromQuaternion(rotation));
            //campos += followObject.Position; /* for RenderableObject */
            campos += followObject.Body.Position;
            //_position += _positionOffset; /* to move the camera */

            Quaternion objrot = Quaternion.CreateFromRotationMatrix(followObject.Body.Orientation);
            
            Vector3 camup = Vector3.Up;
            camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(rotation));

            rotation = Quaternion.Lerp(rotation, objrot, 0.5f);

            _view = Matrix.CreateLookAt(campos, /*followObject.Position*//* for RenderableObject */ followObject.Body.Position + new Vector3(0.0f, 0.0f, 0.0f), camup);
            _position = campos;

            base.Update(gameTime);
        }
    }
}
