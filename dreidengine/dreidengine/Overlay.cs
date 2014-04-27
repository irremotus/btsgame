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
    public struct hud
    {
        public Vector2 Position;
        public float Health;
        public int ammo;
    };

    public class Overlay : DrawableGameComponent
    {
        Texture2D overlayAmmo;
        Texture2D overlayHealth;
        Texture2D overlayCrosshair;
        Microsoft.Xna.Framework.Rectangle rectange;
        hud overlay;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Overlay(Game game, GraphicsDevice graphicsDevice):base(game)
        {
            InitHUD();
        }

        private void InitHUD()
        {
            overlay.Health = ((Game1)Game).C1.CurLife;
            overlay.ammo = 999;
            overlay.Position = new Vector2(100, 193);
        }

        public override void Update(GameTime gameTime)
        {
            if (((Game1)Game).C1.CurLife < (int)overlay.Health)
            {
                rectange.Width = (int) (((Game1)Game).C1.CurLife);
            }

            rectange.Height = 22;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game1 game = (Game1)Game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            base.Draw(gameTime);
            spriteBatch.Begin();
            DrawOverlay();
            DrawText();
            spriteBatch.End();
        }

        private void DrawOverlay()
        {
            spriteBatch.Draw(overlayAmmo, new Microsoft.Xna.Framework.Rectangle(0, 0, (int) 100, 50), Color.White);
            spriteBatch.Draw(overlayHealth, rectange, Color.White);
            spriteBatch.Draw(overlayCrosshair, new Microsoft.Xna.Framework.Rectangle(((Game1)Game).Graphics.PreferredBackBufferWidth / 2, ((Game1)Game).Graphics.PreferredBackBufferHeight / 2, 35, 35), Color.White);
        }

        private void DrawText()
        {
            if (((Game1)Game).C1.CurWeapon.GetType().BaseType == typeof(Gun))
                spriteBatch.DrawString(font, "" + ((Gun)((Game1)Game).C1.CurWeapon).CurAmmo, new Vector2(60, 25), Color.Black);
        }

        protected override void LoadContent()
        {
            rectange = new Microsoft.Xna.Framework.Rectangle(0, 0, ((int) overlay.Health), 15);
            overlayAmmo = Game.Content.Load<Texture2D>("overlay");
            overlayHealth = Game.Content.Load<Texture2D>("HealthBar");
            overlayCrosshair = Game.Content.Load<Texture2D>("crosshair");
            font = Game.Content.Load<SpriteFont>("Arial");
            base.LoadContent();
        }


        public GameTime gameTime { get; set; }
    }
}