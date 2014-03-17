using System;
using System.Collections.Generic;
using System.Linq;
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

namespace dreidengine
{
  
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keys, oldKeys;

        boxtest testBox, fallBox, cambox;

        bool flag = true;

        float camStepSize = 0.5f;

        SpriteFont font;
        /*
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
        */

        private Camera _camera;
        public Camera Camera
        {
            get { return _camera; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            InitializePhyics();

            /*_projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight,
                0.1f,
                1000.0f
                );*/
        }

        private void InitializePhyics()
        {
            this.IsMouseVisible = false;
            
            PhysicsSystem world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();


            testBox = new boxtest(this, "box", new Vector3(0, 0, -20));
            fallBox = new boxtest(this, "cone", new Vector3(0, 50, -20));
            cambox = new boxtest(this, "cone2", new Vector3(0, 0, 20));
            List<boxtest> boxes = new List<boxtest>();
            int i = 0;
            for (i = 0; i < 10; i++)
            {
                boxes.Add(new boxtest(this, "box", new Vector3(20, 0, i * 10)));
            }
            foreach (boxtest box in boxes)
            {
                box.Body.Immovable = true;
                Components.Add(box);
            }

            _camera = new Camera(this, cambox, 10.0f, 6/8f);
            _camera.Lookat = fallBox.Body.Position;
            _camera.CameraMode = Camera.CameraModes.THIRD_PERSON;

            testBox.Body.Immovable = true;
            fallBox.Body.Immovable = false;
            cambox.Body.Immovable = true;
            
            Components.Add(testBox);
            Components.Add(fallBox);
            Components.Add(cambox);
            Components.Add(_camera);
        
        }

        
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            spriteBatch = new SpriteBatch(GraphicsDevice);            
        }

        
        protected override void UnloadContent()
        {
                        
        }

        
        protected override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keys.IsKeyDown(Keys.F))
                _camera.CameraMode = dreidengine.Camera.CameraModes.FIRST_PERSON;
            if (keys.IsKeyDown(Keys.T))
                _camera.CameraMode = dreidengine.Camera.CameraModes.THIRD_PERSON;
            if (keys.IsKeyDown(Keys.OemMinus))
                camStepSize -= 0.1f;
            if (keys.IsKeyDown(Keys.OemPlus))
                camStepSize += 0.1f;
            if (camStepSize <= 0)
                camStepSize = 0.1f;
            
            MouseState mouse = Mouse.GetState();
            int x = graphics.PreferredBackBufferWidth / 2;
            int y = graphics.PreferredBackBufferHeight / 2;
            if (mouse.X - x < -2 || keys.IsKeyDown(Keys.Left))
            {
                _camera.ChangeLook(new Vector3(0, MathHelper.ToRadians(camStepSize), 0));
                Mouse.SetPosition(x, mouse.Y);
            }
            if (mouse.X - x > 2 || keys.IsKeyDown(Keys.Right))
            {
                _camera.ChangeLook(new Vector3(0, -MathHelper.ToRadians(camStepSize), 0));
                Mouse.SetPosition(x, mouse.Y);
            }
            if (mouse.Y - y < -2 || keys.IsKeyDown(Keys.Up))
            {
                _camera.ChangeLook(new Vector3(MathHelper.ToRadians(camStepSize), 0, 0));
                Mouse.SetPosition(mouse.X, y);
            }
            if (mouse.Y - y > 2 || keys.IsKeyDown(Keys.Down))
            {
                _camera.ChangeLook(new Vector3(-MathHelper.ToRadians(camStepSize), 0, 0));
                Mouse.SetPosition(mouse.X, y);
            }

            float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);

            oldKeys = keys;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear((flag)?Color.Green:Color.Red);
            //flag = (flag) ? false : true;
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, fallBox.Body.Position.ToString(), new Vector2(50, 50), Color.Red);
            Vector3 newpos = _camera.Lookat - _camera.Position;
            newpos.Normalize();
            spriteBatch.DrawString(font, "rotX: " + _camera.RotX.ToString() + "\nrotY: " + _camera.RotY.ToString(), new Vector2(50, 50), Color.Red); 
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
