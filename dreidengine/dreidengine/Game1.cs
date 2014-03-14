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
 
        boxtest testBox, fallBox, fall2box, fall3box;

        bool flag = true;

        SpriteFont font;

        SampleGrid grid;

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
        {
<<<<<<< HEAD
           
=======
            //this.IsMouseVisible = true;
>>>>>>> origin/Testing-issues
            
            PhysicsSystem world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();

<<<<<<< HEAD

            grid = new SampleGrid();
            grid.GridColor = Color.LimeGreen;
            grid.GridScale = 1.0f;
            grid.GridSize = 32;

            grid.WorldMatrix = Matrix.Identity;


            fallingBox = new BoxActor(this, new Vector3(0, 50, 0f), new Vector3(1, 1, 1));
            immovableBox = new BoxActor(this, new Vector3(0, -5, 0), new Vector3(5, 5, 5));
=======
>>>>>>> origin/Testing-issues

            testBox = new boxtest(this, "box", Vector3.Zero, new Vector3(10,5,10));
            fallBox = new boxtest(this, "box", new Vector3(0, 150, 0));
            //fall2box = new boxtest(this, "box", new Vector3(0, 50, 0));
            //fall3box = new boxtest(this, "box", new Vector3(0, 40, 0));
            ///i hate my life so much
            _camera = new Camera(this, testBox, new Vector3(20.0f, 20.0f, 20.0f), 6/8f, 0.1f, 10000.0f);

<<<<<<< HEAD
            testBox = new boxtest(this, "box");
            
            immovableBox.Body.Immovable = true;
            testBox.Body.Immovable = true;

            BoxActor ba = new BoxActor(this, new Vector3(0, 20, 0), new Vector3(2, 2, 2));
            


            _camera = new Camera(this, testBox, new Vector3(0.0f, 0.0f, 50.0f), 6 / 8f, 0.1f, 10000.0f);

//#if DEBUG
            d = new debug(this);
            Components.Add(d);
//#endif
            Components.Add(ba);
=======
            testBox.Body.Immovable = true;
            fallBox.Body.Immovable = false;  
            
>>>>>>> origin/Testing-issues
            Components.Add(testBox);
            Components.Add(fallBox);
            //Components.Add(fall2box);
            //Components.Add(fall3box);
            Components.Add(_camera);
<<<<<<< HEAD
            Components.Add(immovableBox);
=======
        
>>>>>>> origin/Testing-issues
        }

        
        protected override void Initialize()
        {

            this.IsMouseVisible = false;
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            grid.LoadGraphicsContent(graphics.GraphicsDevice);
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape))
                this.Exit();




            if (keys.IsKeyDown(Keys.OemPlus))
                _camera.PositionOffset += new Vector3(0, 0, 1);
            if (keys.IsKeyDown(Keys.OemMinus))
                _camera.PositionOffset += new Vector3(0, 0, -1);

            MouseState mouse = Mouse.GetState();
            int x = graphics.PreferredBackBufferWidth / 2;
            int y = graphics.PreferredBackBufferHeight / 2;
            if (mouse.X - x < -2)
            {
                _camera.PositionOffset += Vector3.Left;
                Mouse.SetPosition(x, mouse.Y);
            }
            if (mouse.X - x > 2)
            {
                _camera.PositionOffset += Vector3.Right;
                Mouse.SetPosition(x, mouse.Y);
            }
            if (mouse.Y - y < -2)
            {
                _camera.PositionOffset += Vector3.Up;
                Mouse.SetPosition(mouse.X, y);
            }
            if (mouse.Y - y > 2)
            {
                _camera.PositionOffset += Vector3.Down;
                Mouse.SetPosition(mouse.X, y);
            }

            float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);

            oldKeys = keys;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
<<<<<<< HEAD
            GraphicsDevice.Clear(Color.CornflowerBlue);
            grid.Draw();
            spriteBatch.Begin();
            spriteBatch.DrawString(font, d.j.ToString(), new Vector2(50, 50), Color.Red); 
=======
            GraphicsDevice.Clear((flag)?Color.Green:Color.Red);
            //flag = (flag) ? false : true;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, fallBox.Body.Position.ToString(), new Vector2(50, 50), Color.Red); 
>>>>>>> origin/Testing-issues
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
