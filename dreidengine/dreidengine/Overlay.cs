using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JigLibX.Physics;
using JigLibX.Geometry;
using JigLibX.Collision;
 
namespace dreidengine
{
    public struct HUD
    {
        public Vector2 Position;
        //public Vector2 Scale;
        public float Health;
        public int ammo;
    };

    public class Overlay : DrawableGameComponent
    {
        Texture2D overlayTexture;
        HUD overlay;
        SpriteBatch spriteBatch;

        public Overlay(Game game, GraphicsDevice graphicsDevice, int width, int height):base(game)
        {

        }

        private void InitHUD()
        {
            overlay.Health = 100;
            overlay.ammo = 10;
            overlay.Position = new Vector2(100, 193);
            //overlay.Scale = new Vector2(10, 20);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1 game = (Game1)Game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            
            base.Draw(gameTime);
            spriteBatch.Begin();
            DrawOverlay();
            spriteBatch.End();
        }

        private void DrawOverlay()
        {
            spriteBatch.Draw(overlayTexture, new Microsoft.Xna.Framework.Rectangle(0, 20, 100, 100), Color.White);
        }


        protected override void LoadContent()
        {
            overlayTexture = Game.Content.Load<Texture2D>("overlay");
            base.LoadContent();
        }

    }
}