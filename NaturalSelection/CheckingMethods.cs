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

                int numberOfNewBacteria = random.Next(1, 6); // Wybierz losową liczbę nowych bakterii od 1 do 5
                for (int k = 0; k < numberOfNewBacteria; k++)
                {
                    
                    // Przekazuj geny rodziców do potomstwa z pewnym stopniem mutacji
                    Dictionary<string, float> childGenes = new Dictionary<string, float>();

                    foreach (var gene in bacteria.Genes)
                    {
                        float mutation = (float)(random.NextDouble() * 0.1 - 0.05); // losowa mutacja -5% do 5%
                        float parentGeneValue = (random.NextDouble() > 0.5) ? bacteria.Genes[gene.Key] : otherBacteria.Genes[gene.Key];
                        childGenes[gene.Key] = MathHelper.Clamp(parentGeneValue + mutation * parentGeneValue, 0, bacteria.MaxGeneValue);
                    }

                    var childBacteria = GeneratingMethods.GetNewBacteriaWithGenes(random.Next(2) == 0 ? newPosition1 : newPosition2, newReproductionCooldown, random, childGenes);
                    bacteriaList.Add(childBacteria);
                }

                bacteria.ReproductionCooldown = newReproductionCooldown;
                otherBacteria.ReproductionCooldown = newReproductionCooldown;
                
            }
        }
    }
}