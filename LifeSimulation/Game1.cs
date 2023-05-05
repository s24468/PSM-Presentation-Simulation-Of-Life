using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LifeSimulation
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dot _dot;
        private List<Square> _squares;
        private Random _random;

        private double SquareSpawnTimer { get; set; }
        private const double SquareSpawnInterval = 2000;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _dot = new Dot(_graphics.GraphicsDevice);
            _squares = new List<Square>();
            _random = new Random();
            SquareSpawnTimer = 0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _dot.Update(gameTime);

            foreach (var square in _squares)
            {
                square.Update(gameTime);
            }

            _squares.RemoveAll(square => _dot.Bounds.Intersects(square.Bounds));

            SquareSpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (SquareSpawnTimer > SquareSpawnInterval)
            {
                SpawnSquare();
                SquareSpawnTimer = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _dot.Draw(_spriteBatch);
            foreach (var square in _squares)
            {
                square.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SpawnSquare()
        {
            int x = _random.Next(0, _graphics.PreferredBackBufferWidth - Square.Size);
            int y = _random.Next(0, _graphics.PreferredBackBufferHeight - Square.Size);

            _squares.Add(new Square(_graphics.GraphicsDevice, new Vector2(x, y)));
        }
    }
}