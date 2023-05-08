using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NaturalSelection;

public static class GeneratingMethods
{
    const float newReproductionCooldown = 500.0f; // Ustaw okres ochronny po reprodukcji
    const float maxGeneValue = 200;
    const float geneSum = 300;
    

    public static Bacteria GetNewBacteria(Vector2 newPosition1, float newReproductionCooldown, Random _random)
    {
        
        float speed = _random.Next(1, 100);
        float hunger = _random.Next(1, 100);
        float size = _random.Next(1, 100);

        Dictionary<string, float> genes =
            new Dictionary<string, float> { { "speed", speed }, { "size", size }, { "hunger", hunger } };
        int red = genes["speed"] > maxGeneValue / 2 ? 255 : 128;
        int green = genes["hunger"] > maxGeneValue / 2 ? 255 : 128;
        int blue = genes["size"] > maxGeneValue / 2 ? 255 : 128;
        var color = new Color(red, green, blue);


        return new Bacteria(newPosition1, size, speed, color, genes, hunger, newReproductionCooldown,  maxGeneValue, geneSum );
    }
    public static Bacteria GetNewBacteriaWithGenes(Vector2 newPosition, float newReproductionCooldown, Random _random, Dictionary<string, float> genes)
    {
       
       
        int red = genes["speed"] > maxGeneValue / 2 ? 255 : 128;
        int green = genes["hunger"] > maxGeneValue / 2 ? 255 : 128;
        int blue = genes["size"] > maxGeneValue / 2 ? 255 : 128;
        var color = new Color(red, green, blue);
        float speed = genes["speed"]; 
        float hunger = genes["hunger"];
        float size = genes["size"];

        return new Bacteria(newPosition, size, speed, color, genes, hunger, newReproductionCooldown, maxGeneValue, geneSum);
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