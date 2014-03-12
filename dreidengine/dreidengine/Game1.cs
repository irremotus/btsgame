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
        BoxActor fallingBox;
        BoxActor immovableBox;
        boxtest testBox;
    

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            InitializePhyics();

            _projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight,
                0.1f,
                1000.0f
                );
        }

        private void InitializePhyics()
        {
            PhysicsSystem world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();

            fallingBox = new BoxActor(this, new Vector3(0, 50, 0), new Vector3(1, 1, 1));
            immovableBox = new BoxActor(this, new Vector3(0, -5, 0), new Vector3(5, 5, 5));
            testBox = new boxtest(this, "box");
            immovableBox.Body.Immovable = true;
            testBox.Body.Immovable = true;
            BoxActor ba = new BoxActor(this, new Vector3(0, 20, 0), new Vector3(2, 2, 2));
            Components.Add(ba);
            Components.Add(testBox);
            Components.Add(fallingBox);
           // Components.Add(immovableBox);
        }

        
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
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

            float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);


            _view = Matrix.CreateLookAt(
                new Vector3(0, 5, 20),
                fallingBox.Body.Position,
                Vector3.Up
                );

            oldKeys = keys;
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
