using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Particle_Generator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        KeyboardState keyboardState;
        ParticleEngine particleEngine;
        List<Texture2D> particles;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            particles = new List<Texture2D>();
            base.Initialize();
            particleEngine = new ParticleEngine(particles, Vector2.Zero, false, false);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            particles.Add(Content.Load<Texture2D>("circle"));
            particles.Add(Content.Load<Texture2D>("diamond"));
            particles.Add(Content.Load<Texture2D>("star"));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            particleEngine.EmitterLocation = mouseState.Position.ToVector2();
            particleEngine.Update();
            if (mouseState.LeftButton == ButtonState.Pressed)
                particleEngine.Enabled = true;
            else
                particleEngine.Enabled = false;
            if (keyboardState.IsKeyDown(Keys.G) && particleEngine.GravityEnabled == false)
                particleEngine.GravityEnabled = true;
            else if (keyboardState.IsKeyDown(Keys.G) && particleEngine.GravityEnabled == true)
                particleEngine.GravityEnabled = false;
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            particleEngine.Draw(_spriteBatch);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
