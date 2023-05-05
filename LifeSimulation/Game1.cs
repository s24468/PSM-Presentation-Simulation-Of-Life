using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DotAndSquares
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dot dot;
        private List<Square> squares;
        private Random random;

        private double squareSpawnTimer;
        private double squareSpawnInterval = 1000;
        private ChartWindow _chartWindow;
        private int _eatenItemsCount;
        private double _elapsedTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // Zaktualizujemy metodę Initialize:
        protected override void Initialize()
        {
            base.Initialize();

            dot = new Dot(_graphics.GraphicsDevice);
            squares = new List<Square>();
            random = new Random();
            squareSpawnTimer = 0;

            // Inicjalizujemy okno z wykresem
            _chartWindow = new ChartWindow();
            _chartWindow.Show();

            _eatenItemsCount = 0;
            _elapsedTime = 0;
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            dot.Update(gameTime);

            foreach (var square in squares)
            {
                square.Update(gameTime);
            }

            int initialCount = squares.Count;
            squares.RemoveAll(square => dot.Bounds.Intersects(square.Bounds));
            int removedCount = initialCount - squares.Count;

            if (removedCount > 0)
            {
                _chartWindow.AddDataPoint(removedCount, gameTime.TotalGameTime.TotalSeconds);
            }

            squareSpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (squareSpawnTimer > squareSpawnInterval)
            {
                SpawnSquare();
                squareSpawnTimer = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            dot.Draw(_spriteBatch);
            foreach (var square in squares)
            {
                square.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SpawnSquare()
        {
            int x = random.Next(0, _graphics.PreferredBackBufferWidth - Square.Size);
            int y = random.Next(0, _graphics.PreferredBackBufferHeight - Square.Size);

            squares.Add(new Square(_graphics.GraphicsDevice, new Vector2(x, y)));
        }
    }
}