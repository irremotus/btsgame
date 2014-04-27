using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
        Random r = new Random();
        static Game1 gameInstance;
        public static Game1 GetInstance()
        {
            return gameInstance;
        }

        private Character c1;
        public Character C1
        {
            get { return c1; }
        }

       

        PhysicsSystem world;
        public PhysicsSystem World
        {
            get { return world; }
        }

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

            gameInstance = this;

            InitializePhyics();
        }

        private void InitializePhyics()
        {
            this.IsMouseVisible = false;
            world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();
            world.Gravity = new Vector3(0, -400, 0);
            
            intro introduction = new intro(this, "cloudMap");
            
            c1 = new Character(this, new Vector3(0, 650, 0), Vector3.One);

            PistolGun pistol = new PistolGun(this, new Vector3(19, -15, 10));
            MachineGun machine = new MachineGun(this, new Vector3(20, -15, 20));
            Knife knife = new Knife(this, new Vector3(17, -15, 10));

            

            boxtest b1 = new boxtest(this, "box", new Vector3(20, -15, 10), new Vector3(1, 1, 1), false);
            b1.TakesDamage = true;
            b1.CurLife = 100;            

            _camera = new Camera(this, c1, 10.0f, 6/8f);
            _camera.Lookat = c1.Body.Position;
            _camera.CameraMode = Camera.CameraModes.FIRST_PERSON;

            SkyDome sky = new SkyDome(this, "dome", "white", Vector3.Up * -150, new Vector3(390, 8500, 390));
            introduction.DrawOrder = 500;
            

            //squid s;
            spheretest st;
            for (int i = 1; i < 9; i++)
            {
                Vector3 rand = new Vector3(r.Next(-500, 500), 0, r.Next(-500, 500));
               // s = new squid(this, "squiddles",new Vector3(r.Next(-500,500), r.Next(0,800), r.Next(-500,500)), Vector2.One, new Vector2(4,1), 50);
                st = new spheretest(this, "ballz/s"+i.ToString(), new Vector3(rand.X, 0, rand.Z), Vector3.One*2, Color.White);
            }

            Components.Add(introduction);            
            Components.Add(_camera);
            Components.Add(sky);

            Components.Add(pistol);
            Components.Add(machine);
            Components.Add(knife);
            Components.Add(b1);
            Components.Add(c1);

            c1.PickUpWeapon(pistol);
            c1.PickUpWeapon(machine);
            c1.PickUpWeapon(knife);
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
            Components.Add(heightmapObj);
            
            font = Content.Load<SpriteFont>("SpriteFont1");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            RasterizerState r = new RasterizerState();
            r.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = r;
            string[] names = { "roomModel/CornerRoom.fbx", "roomModel/GeodesicDome", "roomModel/GeodesicDomeOneEntry", "roomModel/GeodesicDomeTwoEntry", "roomModel/SimpleHallway.fbx", "roomModel/SimpleHallwayDoor", "roomModel/SimpleHallwayDoor", "roomModel/SimpleWindowedHallwayDoors" };
            try
            {
                StreamReader sr = new StreamReader("map.mpafd");

                while (true)
                {
                    string l = sr.ReadLine();
                    if (l == null)
                        break;
                    int i = Convert.ToInt32(l);
                    Room rambo = new Room(this, new Vector3((float)Convert.ToDouble(sr.ReadLine()), (float)Convert.ToDouble(sr.ReadLine()), (float)Convert.ToDouble(sr.ReadLine())), new Vector3(1, 2, 1), names[i]);
                    rambo.Body.Orientation = Matrix.CreateRotationY((float)Convert.ToDouble(sr.ReadLine()));

                    Components.Add(rambo);
                }
            }
            catch { }

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
