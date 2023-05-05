using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LifeSimulation;

public class Square
{
    public static int Size = 20;

    private readonly Vector2 _position;
    private readonly Texture2D _texture;

    public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, Size, Size);

    public Square(GraphicsDevice graphicsDevice, Vector2 position)
    {
        this._position = position;
        _texture = CreateTexture(graphicsDevice);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.Red);
    }

    private Texture2D CreateTexture(GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new Texture2D(graphicsDevice, Size, Size);
        Color[] data = new Color[Size * Size];
        for (int i = 0; i < data.Length; i++)
            data[i] = Color.Red;
        texture.SetData(data);
        return texture;
    }
}