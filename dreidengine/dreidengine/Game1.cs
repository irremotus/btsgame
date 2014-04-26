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
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }
        SpriteBatch spriteBatch;

        KeyboardState keys, oldKeys;
        
        public KeyboardState Keysp
        {
            get { return keys; }
        }
        public KeyboardState OldKeysp
        {
            get { return oldKeys; }
        }

        boxtest testBox;

        SpriteFont font;

        HeightmapObject heightmapObj;
        public HeightmapObject HeightMapObj
        {
            get { return heightmapObj; }
        }

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
        }

        private void InitializePhyics()
        {
            intro introduction = new intro(this, "cloudMap");
           
            this.IsMouseVisible = false;

            PhysicsSystem world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();

            world.Gravity = new Vector3(0, -400, 0);

            testBox = new boxtest(this, "box", new Vector3(0, 650,0), new Vector3(1, 1, 1), true, new Vector3(0, 0, 0));          

            //SkyDome sky = new SkyDome(this, "dome", "cloudMap", Vector3.Up * 370 ,Vector3.One * 500f);
            SkyDome sky2 = new SkyDome(this, "dome", "white", Vector3.Up * -150, new Vector3(390, 8500, 390));

            _camera = new Camera(this, testBox, 10.0f, 6/8f);
            _camera.Lookat = testBox.Body.Position;
            _camera.CameraMode = Camera.CameraModes.FIRST_PERSON;
            introduction.DrawOrder = 500;


            Components.Add(introduction);            
            Components.Add(testBox);
            Components.Add(_camera);
            //Components.Add(sky);
            Components.Add(sky2);
        }

        
        protected override void Initialize()
        {
            this.IsMouseVisible = false;
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            terrainModel = Content.Load<Model>("terrain");
            heightmapObj = new HeightmapObject(this, terrainModel, Vector2.Zero);
            
            heightmapObj.PhysicsBody.Immovable = true;
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape))
                this.Exit();

            float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);

            oldKeys = keys;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
