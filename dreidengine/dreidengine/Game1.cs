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
    struct rmStruct
    { 
        public Room r;
        public int id;
        public float rot;
    };
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Random r = new Random();
        static Game1 gameInstance;
        public static Game1 GetInstance()
        {
            return gameInstance;
        }

        List<rmStruct>roomList;

        private Character c1;
        public Character C1
        {
            get { return c1; }
        }

        Room[] room = new Room[8];

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
            roomList = new List<rmStruct>();
            this.IsMouseVisible = true;
            world = new PhysicsSystem();
            world.CollisionSystem = new CollisionSystemSAP();
            world.Gravity = new Vector3(0, -400, 0);
            
            intro introduction = new intro(this, "cloudMap");
            
            c1 = new Character(this, new Vector3(0, 150, 0), Vector3.One);

            PistolGun pistol = new PistolGun(this, new Vector3(19, -15, 10));
            MachineGun machine = new MachineGun(this, new Vector3(20, -15, 20));
            Knife knife = new Knife(this, new Vector3(17, -15, 10));

            
            room[0] = new Room(this, Vector3.Down * 63, Vector3.One, "roomModels/CornerRoom");
            room[1] = new Room(this, new Vector3(50, -63, 0), Vector3.One, "roomModels/GeodesicDome");
            room[2] = new Room(this, new Vector3(0, -63, 50), Vector3.One, "roomModels/GeodesicdomeOneEntry");
            room[3] = new Room(this, new Vector3(100, -63, 0), Vector3.One, "roomModels/GeodesicDomeTwoEntry");
            room[4] = new Room(this, new Vector3(0, -63, 100), Vector3.One, "roomModels/SimpleHallway");
            room[5] = new Room(this, new Vector3(150, -63, 0), Vector3.One, "roomModels/SimpleHallwayDoor");
            room[6] = new Room(this, new Vector3(0, -63, 150), Vector3.One, "roomModels/SimpleWindowedHallway");
            room[7] = new Room(this, new Vector3(200, -63, 0), Vector3.One, "roomModels/SimpleWindowedHallwayDoors");

            //foreach (Room r in room)
            //    Components.Add(r);

            
            _camera = new Camera(this, c1, 10.0f, 6/8f);
            _camera.Lookat = c1.Body.Position;
            _camera.CameraMode = Camera.CameraModes.FIRST_PERSON;

            SkyDome sky = new SkyDome(this, "dome", "white", Vector3.Up * -150, new Vector3(390, 8500, 390));
            introduction.DrawOrder = 500;
            

            /*squid s;

            for (int i = 0; i < 10; i++)
            {
                s = new squid(this, "cone2",new Vector3(r.Next(-500,500), r.Next(0,800), r.Next(-500,500)), Vector3.One, 50, 50);
                Components.Add(s);
            }*/

            
            //Components.Add(introduction);            
            Components.Add(_camera);
            Components.Add(sky);

            Components.Add(pistol);
            Components.Add(machine);
            Components.Add(knife);
            Components.Add(c1);

            //c1.PickUpWeapon(pistol);
            //c1.PickUpWeapon(machine);
            //c1.PickUpWeapon(knife);
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
        }

        
        protected override void UnloadContent()
        {
                        
        }

        int index = 0;
        bool oldState = false;
        Room rtoadd;
        MouseState om;
        float rotaaaaa;

        String[] namesshit = {"roomModels/CornerRoom","roomModels/GeodesicDome","roomModels/GeodesicdomeOneEntry","roomModels/GeodesicDomeTwoEntry","roomModels/SimpleHallway","roomModels/SimpleHallwayDoor","roomModels/SimpleWindowedHallway","roomModels/SimpleWindowedHallwayDoors"};
        protected override void Update(GameTime gameTime)
        {
            float distance;
            CollisionSkin skin;
            Vector3 pos, norm;
            keys = Keyboard.GetState();

            MouseState m = Mouse.GetState();
            

            if (keys.IsKeyDown(Keys.Tab) && oldKeys.IsKeyUp(Keys.Tab))
                index = (index == room.Length - 1) ? 0 : index + 1;
            Console.WriteLine(index.ToString() + " "+namesshit[index]);
            if(m.LeftButton == ButtonState.Pressed)
            {
            //    Camera.camUpdate();
            //    Draw(gameTime);
                RayCollision ray = new RayCollision(World.CollisionSystem);
                GroundPredicate gp = new GroundPredicate();
                if (ray.CastRay(out distance, out skin, out pos, out norm, Camera.Position, Vector3.Normalize((Camera.Lookat - Camera.Position)) * 500f, gp))
                {
                    if (om.LeftButton == ButtonState.Released)
                    {
                        rtoadd = new Room(this, new Vector3(pos.X, HeightMapObj.HMI.GetHeight(pos), pos.Z), new Vector3(1,2,1), namesshit[index]);
                        rotaaaaa = 0 * (float)Math.PI;
                         Components.Add(rtoadd);
                         rmStruct s;
                         s.r = rtoadd;
                         s.id = index;
                         s.rot = rotaaaaa; ;
                        
                        roomList.Add(s);
                    }
                    rtoadd.Body.Position = new Vector3(pos.X, HeightMapObj.HMI.GetHeight(pos), pos.Z);
                    if(keys.IsKeyDown(Keys.R) && oldKeys.IsKeyUp(Keys.R)) 
                        rtoadd.Body.SetOrientation(Matrix.CreateRotationY(rotaaaaa = (rotaaaaa == 2 * (float)Math.PI) ? 0 : rotaaaaa + ((float)Math.PI /2)));
                }
                oldState = true;
            }
            oldState = false;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keys.IsKeyDown(Keys.Escape))
                this.Exit();


            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
            {
                StreamWriter sw = new StreamWriter("map.mpafd");
                foreach (rmStruct r in roomList)
                {
                    sw.WriteLine(r.id.ToString());
                    sw.WriteLine(r.r.Body.Position.X.ToString());
                    sw.WriteLine(r.r.Body.Position.Y.ToString());
                    sw.WriteLine(r.r.Body.Position.Z.ToString());
                    sw.WriteLine(r.rot.ToString());
                }
                System.Windows.Forms.MessageBox.Show("Saved");
                sw.Close();
            }

            float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            
            PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);

            oldKeys = keys;
            om = m;
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
