using System;

namespace NaturalSelection;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<Bacteria> _bacteriaList;
    private Texture2D _bacteriaTexture;

    private List<Food> _foodList = new();
    private Texture2D _foodTexture;

    private float _timeSinceLastBacteriaAdded;
    private float _timeSinceLastFoodAdded;

    private const float MinTimeBetweenBacteria = 1000f; // minimalny czas od ostatniego dodania bakterii w milisekundach

    public static readonly int ScreenWidth = 1400;
    public static readonly int ScreenHeight = 800;

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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Utwórz teksturę dla bakterii
        _bacteriaTexture = new Texture2D(GraphicsDevice, 1, 1);
        _bacteriaTexture.SetData(new[] { Color.White });

        // Utwórz teksturę dla jedzenia
        _foodTexture = new Texture2D(GraphicsDevice, 1, 1);
        _foodTexture.SetData(new[] { Color.White });
    }


    protected override void Update(GameTime gameTime)
    {
        _timeSinceLastBacteriaAdded += (float)gameTime.ElapsedGameTime.Milliseconds * 4;
        _timeSinceLastFoodAdded += (float)gameTime.ElapsedGameTime.Milliseconds / 2;
        var elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds;
        var _random = new Random();


        for (var i = _bacteriaList.Count - 1; i >= 0; i--)
        {
            var bacteria = _bacteriaList[i];
            bacteria.MoveSmoothly(elapsedTime);
            bacteria.Update(elapsedTime);

            bacteria.CheckBounds();
            bacteria.UpdateHunger(elapsedTime);


            // Sprawdzanie kolizji z jedzeniem
            CheckingMethods.CheckCollisionsBacteriasWithFood(bacteria, _foodList);
            // Sprawdzanie kolizji z innymi bakteriami
            CheckingMethods.CheckCollisionsBacteriasWithOtherBacterias(i, bacteria, _bacteriaList, _random);

            if (bacteria.Hunger <= 0)
            {
                _bacteriaList.RemoveAt(i);
            }
        }


        GeneratingMethods.RandomlySpawnFood(ref _timeSinceLastFoodAdded, MinTimeBetweenBacteria, _foodList,
            _random);
        GeneratingMethods.RandomlyAddBacteria(ref _timeSinceLastBacteriaAdded, MinTimeBetweenBacteria, _graphics,
            _bacteriaList, _random);
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
}