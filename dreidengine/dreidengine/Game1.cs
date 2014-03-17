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

        Model terrainModel;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keys, oldKeys;
<<<<<<< HEAD
        
        public KeyboardState Keysp
        {
            get { return keys; }
        }
        public KeyboardState OldKeysp
        {
            get { return oldKeys; }
        }
 
        boxtest testBox, fallBox, fall2box, fall3box;
=======

        boxtest testBox, fallBox, cambox;
>>>>>>> origin/3rd_person_camera

        bool flag = true;

        float camStepSize = 0.5f;

        SpriteFont font;

        SampleGrid grid;

        HeightmapObject heightmapObj;
        public HeightmapObject HeightMapObj
        {
            get { return heightmapObj; }
        }

        debug d;
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
<<<<<<< HEAD
        {            
=======
        {
            this.IsMouseVisible = false;
            
>>>>>>> origin/3rd_person_camera
            PhysicsSystem world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();

            grid = new SampleGrid();
            grid.GridColor = Color.LimeGreen;
            grid.GridScale = 1.0f;
            grid.GridSize = 32;

<<<<<<< HEAD
            grid.WorldMatrix = Matrix.Identity;

            testBox = new boxtest(this, "box", Vector3.Zero, new Vector3(1, 1, 1), true, new Vector3(0, 0, 0));
            fallBox = new boxtest(this, "box", new Vector3(0, 10, 0), new Vector3(1, 1, 1));
     
            //WTF IS THIS SHIT
            fall2box = new boxtest(this, "box", new Vector3(0, 50, 0));
            fall3box = new boxtest(this, "box", new Vector3(0, 40, 0));
            ///i hate my life so much

            _camera = new Camera(this, testBox, new Vector3(0.0f, 10.0f, 20.0f), 6 / 8f, 0.1f, 10000.0f);

            testBox.Body.Immovable = false;            
            //fallBox.Body.Immovable = false;  

            //Components.Add(a);
            //Components.Add(b);
            Components.Add(testBox);
            Components.Add(fallBox);
            Components.Add(fall2box);
            Components.Add(fall3box);
=======
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
>>>>>>> origin/3rd_person_camera
            Components.Add(_camera);
        }

        
        protected override void Initialize()
        {

            this.IsMouseVisible = false;
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            grid.LoadGraphicsContent(graphics.GraphicsDevice);
            terrainModel = Content.Load<Model>("terrain");
            heightmapObj = new HeightmapObject(this, terrainModel, Vector2.Zero);
            
            heightmapObj.PhysicsBody.Immovable = false;
            this.Components.Add(heightmapObj);
            font = Content.Load<SpriteFont>("SpriteFont1");
            spriteBatch = new SpriteBatch(GraphicsDevice);            
        }

        
        protected override void UnloadContent()
        {
                        
        }

        
        protected override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            grid.ViewMatrix = Camera.View;
            grid.ProjectionMatrix = Camera.Projection;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape))
                this.Exit();
<<<<<<< HEAD


            #region SHITTY MOUSE STUFF THAT IISNT DOING ANYTHING

            if (keys.IsKeyDown(Keys.OemPlus))
                _camera.PositionOffset += new Vector3(0, 0, 1);
            if (keys.IsKeyDown(Keys.OemMinus))
                _camera.PositionOffset += new Vector3(0, 0, -1);
=======
>>>>>>> origin/3rd_person_camera

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

            #endregion


            float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);

            oldKeys = keys;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
<<<<<<< HEAD
            GraphicsDevice.Clear((flag) ? Color.Black : Color.Red);
            //flag = (flag) ? false : true;
            //heightmapObj.Visible = (heightmapObj.Visible) ? false : true;
            grid.Draw();
            spriteBatch.Begin();
       
            spriteBatch.DrawString(font, testBox.Body.Position.ToString(), new Vector2(50, 50), Color.Red); 
            

=======
            //GraphicsDevice.Clear((flag)?Color.Green:Color.Red);
            //flag = (flag) ? false : true;
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, fallBox.Body.Position.ToString(), new Vector2(50, 50), Color.Red);
            Vector3 newpos = _camera.Lookat - _camera.Position;
            newpos.Normalize();
            spriteBatch.DrawString(font, "rotX: " + _camera.RotX.ToString() + "\nrotY: " + _camera.RotY.ToString(), new Vector2(50, 50), Color.Red); 
>>>>>>> origin/3rd_person_camera
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
