using System;
using System.Collections.Generic;

namespace NaturalSelection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;

public class Bacteria
{
    public float Hunger { get; set; }
    public Vector2 Position { get; set; }
    public float Size { get; set; }
    public float Speed { get; set; }
    public Color Color { get; set; }
    public Dictionary<string, float> Genes { get; set; }

    public Vector2 Direction { get; set; }
    
    public Bacteria(Vector2 position, float size, float speed, Color color, Dictionary<string, float> genes, float hunger)
    {
        Position = position;
        Size = size;
        Speed = speed;
        Color = color;
        Genes = genes;
        Direction = new Vector2((float)(new Random().NextDouble() * 2 - 1), (float)(new Random().NextDouble() * 2 - 1));
        Hunger = hunger; // Przykładowa wartość początkowa dla głodu
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
        float rotationSpeed = 0.2f; // Wartość regulująca szybkość zmiany kierunku
        float angle = (float)(new Random().NextDouble() * 2 * Math.PI * rotationSpeed - Math.PI * rotationSpeed);
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);

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