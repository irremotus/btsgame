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
        private Matrix rotation;
        private float rotX;
        public float RotX
        {
            get { return rotX; }
        }
        private float rotY;
        public float RotY
        {
            get { return rotY; }
        }
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
        public enum CameraModes
        {
            FIRST_PERSON, THIRD_PERSON
        }
        private CameraModes _cameraMode;
        public CameraModes CameraMode
        {
            get { return _cameraMode; }
            set { _cameraMode = value; }
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

            rotation = followObject.Body.Orientation;

            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearClip, farClip);
        }

        public override void Update(GameTime gameTime)
        {
            if (_cameraMode == CameraModes.FIRST_PERSON)
            {
                Vector3 camRef = new Vector3(0, 0, -1);
                Vector3 objHeadOffset = new Vector3(0, 5.0f, 0);
                Matrix rotMat = rotation;
                Vector3 headOffset = Vector3.Transform(objHeadOffset, rotMat);
                _position = followObject.Body.Position + headOffset;
                Vector3 transRef = Vector3.Transform(camRef, rotMat);
                _lookat = transRef + _position;
                _view = Matrix.CreateLookAt(_position, _lookat, followObject.Body.Orientation.Up);
            }
            else if (_cameraMode == CameraModes.THIRD_PERSON)
            {
                Vector3 thirdPRef = new Vector3(0, 10.0f, 20.0f);
                Matrix rotMat = rotation;
                Vector3 transRef = Vector3.Transform(thirdPRef, rotMat);
                _position = transRef + followObject.Body.Position;
                _view = Matrix.CreateLookAt(_position, followObject.Body.Position, followObject.Body.Orientation.Up);
            }

            base.Update(gameTime);
        }

        public void ChangeLook(Vector3 angles)
        {
            if (_cameraMode == CameraModes.FIRST_PERSON || _cameraMode == CameraModes.THIRD_PERSON)
            {
                rotX += angles.X;
                rotY += angles.Y;
                //if (rotX >= Math.PI * 15 / 16)
                //    rotX = (float)(Math.PI-Math.PI/16);
                //if (rotX < -Math.PI * 15 / 16)
                //    rotX = (float)(-Math.PI + Math.PI / 16);
                //if (rotY > Math.PI * 15 / 16)
                //    rotY = (float)(Math.PI - Math.PI / 16);
                //if (rotY < -Math.PI * 15 / 16)
                //    rotY = (float)(-Math.PI + Math.PI / 16);
                Matrix newRot = Matrix.CreateRotationX(rotX) * Matrix.CreateRotationY(rotY) * followObject.Body.Orientation;
                rotation = Matrix.Lerp(rotation, newRot, 0.5f);
            }
        }
    }
}
