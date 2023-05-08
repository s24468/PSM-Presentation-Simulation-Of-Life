using System;
using System.Collections.Generic;

namespace NaturalSelection;

using Microsoft.Xna.Framework;

public class Bacteria
{
    public float Hunger { get; set; }
    public Vector2 Position { get; set; }
    public float Size { get; set; }
    public float Speed { get; set; }
    public Color Color { get; set; }
    public Dictionary<string, float> Genes { get; set; } //mnożnik

    public float ReproductionCooldown { get; set; }


    // Dodaj te nowe właściwości
    public float MaxGeneValue { get; }
    public float GeneSum { get; }


    public Vector2 Direction { get; set; }


    // float speed = _random.Next(1, 10) / 20f; 0.05 - 0.5:: 1-100
    // float size = _random.Next(15, 20);10- 20:: 1-100
    // float hunger = _random.Next(5000, 10000); 1-100

    // int minValue = 1;
    // int maxValue = 100;
    // int newMinValue = 10;
    // int newMaxValue = 20;
    // int randomNumber = random.Next(minValue, maxValue + 1);
    // double scaledValue = ((double)randomNumber - minValue) / (maxValue - minValue) * (newMaxValue - newMinValue) + newMinValue;
    //scaledValue = hunger-1
    public Bacteria(Vector2 position, float size, float speed, Color color, Dictionary<string, float> genes,
        float hunger, float reproductionCooldown, float maxGeneValue, float geneSum)
    {
        Position = position;
        Speed = speed / 200;
        Size = (size - 1) / (100 - 1) * (20 - 10) + 10;
        Hunger = (hunger - 1) / (100 - 1) *(10000-5000)+ 5000; // Przykładowa wartość początkowa dla głodu
        Color = color;
        Genes = genes;
        Direction = new Vector2((float)(new Random().NextDouble() * 2 - 1), (float)(new Random().NextDouble() * 2 - 1));
        ReproductionCooldown = reproductionCooldown;

        // Inicjalizuj nowe właściwości
        MaxGeneValue = maxGeneValue;
        GeneSum = geneSum;

        // Przeskaluj geny, aby ich suma była równa GeneSum
        Genes = new Dictionary<string, float>();
        float currentSum = 0;
        foreach (var gene in genes)
        {
            currentSum += gene.Value;
        }

        foreach (var gene in genes)
        {
            Genes[gene.Key] = (gene.Value / currentSum) * GeneSum;
        }
    }

    public void Update(float elapsedTime)
    {
        ReproductionCooldown -= elapsedTime;
    }

    public bool Intersects(Bacteria other)
    {
        var distance = Vector2.Distance(Position, other.Position);
        return distance <= (Size / 2 + other.Size / 2);
    }

    public void UpdateHunger(float elapsedTime)
    {
        Hunger -= elapsedTime * 2.0f; // Wartość 2.0f to przykładowa prędkość spadku głodu
    }

    public bool Intersects(Food food)
    {
        float distance = Vector2.Distance(Position, food.Position);
        return distance <= (Size / 2 + food.Size / 2);
    }

    public void MoveSmoothly(float deltaTime)
    {
        // Aktualizuj kierunek
        var rotationSpeed = 0.2f; // Wartość regulująca szybkość zmiany kierunku
        var angle = (float)(new Random().NextDouble() * 2 * Math.PI * rotationSpeed - Math.PI * rotationSpeed);
        var cos = (float)Math.Cos(angle);
        var sin = (float)Math.Sin(angle);

        Direction = new Vector2(
            Direction.X * cos - Direction.Y * sin,
            Direction.X * sin + Direction.Y * cos
        );

        // Normalizuj wektor kierunku
        Direction.Normalize();

        // // Aktualizuj pozycję
        Position += Direction * Speed * deltaTime;
    }

    public void CheckBounds()
    {
        if (Position.X < 0)
        {
            Position = new Vector2(0, Position.Y);
        }
        else if (Position.X + Size > Game1.ScreenWidth)
        {
            Position = new Vector2(Game1.ScreenWidth - Size, Position.Y);
        }

        if (Position.Y < 0)
        {
            Position = new Vector2(Position.X, 0);
        }
        else if (Position.Y + Size > Game1.ScreenHeight)
        {
            Position = new Vector2(Position.X, Game1.ScreenHeight - Size);
        }
    }
}