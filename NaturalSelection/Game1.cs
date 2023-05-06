using System;

namespace NaturalSelection;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<Bacteria> _bacteriaList;
    private Texture2D _bacteriaTexture;

    private List<Food> _foodList = new List<Food>();
    private Texture2D _foodTexture;

    private float _timeSinceLastBacteriaAdded = 0f;
    private float _timeSinceLastFoodAdded = 0f;

    private const float
        _minTimeBetweenBacteria = 1000f; // minimalny czas od ostatniego dodania bakterii w milisekundach

    public static int ScreenWidth = 1400;
    public static int ScreenHeight = 800;

    public Game1() //1920x1080
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _bacteriaList = new List<Bacteria>();

        // // Dodaj przykładowe bakterie
        // _bacteriaList.Add(new Bacteria(new Vector2(100, 100), 10, 0.5f, Color.Red,
        //     new Dictionary<string, float> { { "speed", 3000f }, { "size", 50f } }));
        // _bacteriaList.Add(new Bacteria(new Vector2(200, 200), 15, 0.5f, Color.Blue,
        //     new Dictionary<string, float> { { "speed", 4000f }, { "size", 50f } }));
        //
        //
        //

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Utwórz teksturę dla bakterii
        _bacteriaTexture = new Texture2D(GraphicsDevice, 1, 1);
        _bacteriaTexture.SetData(new[] { Color.White });


        _foodTexture = new Texture2D(GraphicsDevice, 1, 1);
        _foodTexture.SetData(new[] { Color.White });
        SpawnFood(50);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _timeSinceLastBacteriaAdded += (float)gameTime.ElapsedGameTime.Milliseconds;
        _timeSinceLastFoodAdded += (float)gameTime.ElapsedGameTime.Milliseconds/2;
        var elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds;

        for (int i = _bacteriaList.Count - 1; i >= 0; i--)
        {
            Bacteria bacteria = _bacteriaList[i];
            bacteria.MoveSmoothly(elapsedTime);
            bacteria.CheckBounds();
            bacteria.UpdateHunger(elapsedTime);
            // Sprawdzanie kolizji z jedzeniem
            for (int j = _foodList.Count - 1; j >= 0; j--)
            {
                Food food = _foodList[j];
                if (bacteria.Intersects(food))
                {
                    _foodList.RemoveAt(j); // Usuwanie jedzenia z listy
                    bacteria.Hunger += 50; // Zwiększenie wartości głodu bakterii (dostosuj wartość według potrzeb)
                }
            }

            if (bacteria.Hunger <= 0)
            {
                _bacteriaList.RemoveAt(i);
            }
        }

        var _random = new Random();
        if (_timeSinceLastBacteriaAdded >= _minTimeBetweenBacteria)
        {
            Vector2 vector2 = new Vector2(_random.Next(0, _graphics.PreferredBackBufferWidth),
                _random.Next(0, _graphics.PreferredBackBufferHeight));

            float size = _random.Next(15, 20);
            var color = new Color(_random.Next(0, 256), _random.Next(0, 256), _random.Next(0, 256));
            float speed = _random.Next(1, 10) / 20f;
            float hunger = _random.Next(5000, 10000);

            _bacteriaList.Add(new Bacteria(vector2, size, speed, color,
                new Dictionary<string, float> { { "speed", 4000f }, { "size", 50f } }, hunger));
            _timeSinceLastBacteriaAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
        }

        if (_timeSinceLastFoodAdded >= _minTimeBetweenBacteria)
        {
            int foodCount = _random.Next(5, 20);
            SpawnFood(foodCount);
            _timeSinceLastFoodAdded = 0f; // zresetuj czas od ostatniego dodania bakterii

        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach (Bacteria bacteria in _bacteriaList)
        {
            _spriteBatch.Draw(_bacteriaTexture,
                new Rectangle((int)bacteria.Position.X, (int)bacteria.Position.Y, (int)bacteria.Size,
                    (int)bacteria.Size), bacteria.Color);
        }

        foreach (Food food in _foodList)
        {
            _spriteBatch.Draw(_foodTexture,
                new Rectangle((int)food.Position.X, (int)food.Position.Y, (int)food.Size, (int)food.Size), food.Color);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void SpawnFood(int count)
    {
        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            Vector2 position = new Vector2(random.Next(Game1.ScreenWidth), random.Next(Game1.ScreenHeight));
            _foodList.Add(new Food(position, 10f, Color.Black));
        }
    }
}