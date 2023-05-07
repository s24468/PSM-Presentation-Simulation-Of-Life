using System;
using System.Diagnostics.CodeAnalysis;

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


        _foodTexture = new Texture2D(GraphicsDevice, 1, 1);
        _foodTexture.SetData(new[] { Color.White });
       
        // RandomlySpawnFood(_random);
        // bacteriaList.Add(Game1.GetNewBacteria(newPosition2, newReproductionCooldown, random));
        
    }


    protected override void Update(GameTime gameTime)
    {
        _timeSinceLastBacteriaAdded += (float)gameTime.ElapsedGameTime.Milliseconds * 4;
        _timeSinceLastFoodAdded += (float)gameTime.ElapsedGameTime.Milliseconds / 2;
        var elapsedTime = (float)gameTime.ElapsedGameTime.Milliseconds;
        var _random = new Random();


        for (int i = _bacteriaList.Count - 1; i >= 0; i--)
        {
            var bacteria = _bacteriaList[i];
            bacteria.MoveSmoothly(elapsedTime);
            bacteria.Update(elapsedTime);

            bacteria.CheckBounds();
            bacteria.UpdateHunger(elapsedTime);


            // Sprawdzanie kolizji z jedzeniem
            // CheckCollisionsBacteriasWithFood(bacteria);
            CheckingMethods.CheckCollisionsBacteriasWithFood(bacteria, _foodList);
            // Sprawdzanie kolizji z innymi bakteriami
            // CheckCollisionsBacteriasWithOtherBacterias(i, bacteria, _random);
            CheckingMethods.CheckCollisionsBacteriasWithOtherBacterias(i, bacteria, _bacteriaList, _random);

            if (bacteria.Hunger <= 0)
            {
                _bacteriaList.RemoveAt(i);
            }
        }


        // RandomlyAddBacteria(_random);
        // RandomlySpawnFood(_random);
        GeneratingMethods.RandomlySpawnFood(ref _timeSinceLastFoodAdded, _minTimeBetweenBacteria, _foodList,
            _random);
        GeneratingMethods.RandomlyAddBacteria(ref _timeSinceLastBacteriaAdded, _minTimeBetweenBacteria, _graphics,
            _bacteriaList, _random);
        base.Update(gameTime);
    }

    // private void RandomlySpawnFood(Random _random)
    // {
    //     if (_timeSinceLastFoodAdded >= _minTimeBetweenBacteria)
    //     {
    //         var foodCount = _random.Next(30, 40);
    //
    //
    //         for (int i = 0; i < foodCount; i++)
    //         {
    //             var position = new Vector2(_random.Next(Game1.ScreenWidth), _random.Next(Game1.ScreenHeight));
    //             _foodList.Add(new Food(position, 10f, Color.Black));
    //         }
    //
    //         _timeSinceLastFoodAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
    //     }
    // }

    // private void RandomlyAddBacteria(Random _random)
    // {
    //     if (_timeSinceLastBacteriaAdded >= _minTimeBetweenBacteria)
    //     {
    //         Vector2 vector2 = new Vector2(_random.Next(0, _graphics.PreferredBackBufferWidth),
    //             _random.Next(0, _graphics.PreferredBackBufferHeight));
    //
    //         float newReproductionCooldown = 5.0f; // Ustaw okres ochronny po reprodukcji
    //
    //         // _bacteriaList.Add(GetNewBacteria(vector2, newReproductionCooldown, _random));
    //         _bacteriaList.Add(GeneratingMethods.GetNewBacteria(vector2, newReproductionCooldown, _random));
    //         _timeSinceLastBacteriaAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
    //     }
    // }

    // private void CheckCollisionsBacteriasWithFood(Bacteria bacteria)
    // {
    //     for (int j = _foodList.Count - 1; j >= 0; j--)
    //     {
    //         Food food = _foodList[j];
    //         if (bacteria.Intersects(food))
    //         {
    //             _foodList.RemoveAt(j); // Usuwanie jedzenia z listy
    //             bacteria.Hunger += 50; // Zwiększenie wartości głodu bakterii (dostosuj wartość według potrzeb)
    //         }
    //     }
    // }

    // private void CheckCollisionsBacteriasWithOtherBacterias(int i, Bacteria bacteria, Random _random)
    // {
    //     for (int j = i - 1; j >= 0; j--)
    //     {
    //         var otherBacteria = _bacteriaList[j];
    //         if (bacteria.Intersects(otherBacteria) && bacteria.ReproductionCooldown <= 0 &&
    //             otherBacteria.ReproductionCooldown <= 0)
    //         {
    //             var newPosition1 = bacteria.Position + new Vector2(bacteria.Size / 2 + 1, 0);
    //             var newPosition2 = bacteria.Position - new Vector2(bacteria.Size / 2 + 1, 0);
    //             const float newReproductionCooldown = 1500.0f; // Ustaw okres ochronny po reprodukcji
    //
    //             for (int k = 0; k < 2; k++)
    //             {
    //                 if (k == 0)
    //                 {
    //                     _bacteriaList.Add(GetNewBacteria(newPosition1, newReproductionCooldown, _random));
    //                 }
    //                 else
    //                 {
    //                     _bacteriaList.Add(GetNewBacteria(newPosition2, newReproductionCooldown, _random));
    //                 }
    //             }
    //
    //             bacteria.ReproductionCooldown = newReproductionCooldown;
    //             otherBacteria.ReproductionCooldown = newReproductionCooldown;
    //             break; // Przerwij wewnętrzną pętlę, aby uniknąć nadmiernego rozmnażania
    //         }
    //     }
    // }

    // private static Bacteria GetNewBacteria(Vector2 newPosition1, float newReproductionCooldown, Random _random)
    // {
    //     float size = _random.Next(15, 20);
    //     var color = new Color(_random.Next(0, 256), _random.Next(0, 256), _random.Next(0, 256));
    //     float speed = _random.Next(1, 10) / 20f;
    //     float hunger = _random.Next(5000, 10000);
    //     return new Bacteria(newPosition1, size, speed, color,
    //         new Dictionary<string, float> { { "speed", 4000f }, { "size", 50f } }, hunger,
    //         newReproductionCooldown);
    // }

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

public static class CheckingMethods
{
    public static void CheckCollisionsBacteriasWithFood(Bacteria bacteria, List<Food> foodList)
    {
        for (int j = foodList.Count - 1; j >= 0; j--)
        {
            Food food = foodList[j];
            if (bacteria.Intersects(food))
            {
                foodList.RemoveAt(j); // Usuwanie jedzenia z listy
                bacteria.Hunger += 50; // Zwiększenie wartości głodu bakterii (dostosuj wartość według potrzeb)
            }
        }
    }

    public static void CheckCollisionsBacteriasWithOtherBacterias(int i, Bacteria bacteria, List<Bacteria> bacteriaList,
        Random random)
    {
        for (int j = i - 1; j >= 0; j--)
        {
            var otherBacteria = bacteriaList[j];
            if (bacteria.Intersects(otherBacteria) && bacteria.ReproductionCooldown <= 0 &&
                otherBacteria.ReproductionCooldown <= 0)
            {
                var newPosition1 = bacteria.Position + new Vector2(bacteria.Size / 2 + 1, 0);
                var newPosition2 = bacteria.Position - new Vector2(bacteria.Size / 2 + 1, 0);
                const float newReproductionCooldown = 1500.0f; // Ustaw okres ochronny po reprodukcji

                for (int k = 0; k < 2; k++)
                {
                    if (k == 0)
                    {
                        // bacteriaList.Add(Game1.GetNewBacteria(newPosition1, newReproductionCooldown, random));
                        bacteriaList.Add(
                            GeneratingMethods.GetNewBacteria(newPosition1, newReproductionCooldown, random));
                    }
                    else
                    {
                        // bacteriaList.Add(Game1.GetNewBacteria(newPosition2, newReproductionCooldown, random));
                        bacteriaList.Add(
                            GeneratingMethods.GetNewBacteria(newPosition1, newReproductionCooldown, random));
                    }

                    bacteria.ReproductionCooldown = newReproductionCooldown;
                    otherBacteria.ReproductionCooldown = newReproductionCooldown;
                    break; // Przerwij wewnętrzną pętlę, aby uniknąć nadmiernego rozmnażania
                }
            }
        }
    }
}

public static class GeneratingMethods
{
    public static Bacteria GetNewBacteria(Vector2 newPosition1, float newReproductionCooldown, Random _random)
    {
        float size = _random.Next(15, 20);
        var color = new Color(_random.Next(0, 256), _random.Next(0, 256), _random.Next(0, 256));
        float speed = _random.Next(1, 10) / 20f;
        float hunger = _random.Next(5000, 10000);
        return new Bacteria(newPosition1, size, speed, color,
            new Dictionary<string, float> { { "speed", 4000f }, { "size", 50f } }, hunger,
            newReproductionCooldown);
    }

    public static void RandomlySpawnFood(ref float timeSinceLastFoodAdded, float minTimeBetweenBacteria,
        List<Food> foodList, Random random)
    {
        if (timeSinceLastFoodAdded >= minTimeBetweenBacteria)
        {
            var foodCount = random.Next(30, 40);

            for (int i = 0; i < foodCount; i++)
            {
                var position = new Vector2(random.Next(Game1.ScreenWidth), random.Next(Game1.ScreenHeight));
                foodList.Add(new Food(position, 10f, Color.Black));
            }

            timeSinceLastFoodAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
        }
    }

    public static void RandomlyAddBacteria(ref float timeSinceLastBacteriaAdded, float minTimeBetweenBacteria,
        GraphicsDeviceManager graphics, List<Bacteria> bacteriaList, Random random)
    {
        if (timeSinceLastBacteriaAdded >= minTimeBetweenBacteria)
        {
            Vector2 vector2 = new Vector2(random.Next(0, graphics.PreferredBackBufferWidth),
                random.Next(0, graphics.PreferredBackBufferHeight));

            float newReproductionCooldown = 5.0f; // Ustaw okres ochronny po reprodukcji
            bacteriaList.Add(GetNewBacteria(vector2, newReproductionCooldown, random));

            // bacteriaList.Add(Game1.GetNewBacteria(vector2, newReproductionCooldown, random));
            timeSinceLastBacteriaAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
        }
    }
}
// public class GeneratingMethods
// {
//     public float RandomlySpawnFood(Random _random, float _timeSinceLastFoodAdded, float _minTimeBetweenBacteria,
//         List<Food> _foodList)
//     {
//         if (_timeSinceLastFoodAdded >= _minTimeBetweenBacteria)
//         {
//             var foodCount = _random.Next(30, 40);
//             
//             for (int i = 0; i < foodCount; i++)
//             {
//                 var position = new Vector2(_random.Next(Game1.ScreenWidth), _random.Next(Game1.ScreenHeight));
//                 _foodList.Add(new Food(position, 10f, Color.Black));
//             }
//
//             _timeSinceLastFoodAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
//         }
//         return 0f;
//     }
// }
//
// public class CheckingMethods
// {
//     private Random _random = new Random();
//     private int i;
//     private Bacteria _bacteria;
//     private Vector2 position;
// }