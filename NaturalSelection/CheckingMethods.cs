using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NaturalSelection;

public static class CheckingMethods
{
    const float newReproductionCooldown = 1500.0f; // Ustaw okres ochronny po reprodukcji

    public static void CheckCollisionsBacteriasWithFood(Bacteria bacteria, List<Food> foodList)
    {
        for (var j = foodList.Count - 1; j >= 0; j--)
        {
            if (bacteria.Intersects(foodList[j]))
            {
                foodList.RemoveAt(j); // Usuwanie jedzenia z listy
                bacteria.Hunger += 200; // Zwiększenie wartości głodu bakterii (dostosuj wartość według potrzeb)
            }
        }
    }

    public static void CheckCollisionsBacteriasWithOtherBacterias(int i, Bacteria bacteria, List<Bacteria> bacteriaList,
        Random random)
    {
        for (int j = i - 1; j >= 0; j--)
        {
            var otherBacteria = bacteriaList[j];
            //dodaj dwie nowe bakterie
            if (bacteria.Intersects(otherBacteria) && bacteria.ReproductionCooldown <= 0 &&
                otherBacteria.ReproductionCooldown <= 0)
            {
                var newPosition1 = bacteria.Position + new Vector2(bacteria.Size / 2 + 1, 0);
                var newPosition2 = bacteria.Position - new Vector2(bacteria.Size / 2 + 1, 0);

                bacteriaList.Add(
                    GeneratingMethods.GetNewBacteria(newPosition1, newReproductionCooldown, random));

                bacteriaList.Add(
                    GeneratingMethods.GetNewBacteria(newPosition2, newReproductionCooldown, random));

                bacteria.ReproductionCooldown = newReproductionCooldown;
                otherBacteria.ReproductionCooldown = newReproductionCooldown;
            }
        }
    }
}