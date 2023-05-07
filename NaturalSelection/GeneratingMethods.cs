using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NaturalSelection;

public static class GeneratingMethods
{
    const float newReproductionCooldown = 500.0f; // Ustaw okres ochronny po reprodukcji

    public static Bacteria GetNewBacteria(Vector2 newPosition1, float newReproductionCooldown, Random _random)
    {
        
        Dictionary<string, float> dictionary =
            new Dictionary<string, float> { { "speed", 4000f }, { "size", 50f } };

        var color = new Color(_random.Next(0, 256), _random.Next(0, 256), 0);
        float speed = _random.Next(1, 10) / 20f;
        float hunger = _random.Next(5000, 10000);
        float size = _random.Next(15, 20);

        return new Bacteria(newPosition1, size, speed, color, dictionary, hunger, newReproductionCooldown);
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
            var vector2 = new Vector2(random.Next(0, graphics.PreferredBackBufferWidth),
                random.Next(0, graphics.PreferredBackBufferHeight));

            bacteriaList.Add(GetNewBacteria(vector2, newReproductionCooldown, random));

            timeSinceLastBacteriaAdded = 0f; // zresetuj czas od ostatniego dodania bakterii
        }
    }
}