namespace NaturalSelection;

using Microsoft.Xna.Framework;

public class Food
{
    public Vector2 Position { get; set; }
    public float Size { get; set; }
    public Color Color { get; set; }

    public Food(Vector2 position, float size, Color color)
    {
        Position = position;
        Size = size;
        Color = color;
    }
}