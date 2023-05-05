using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LifeSimulation;

public class Dot
{
    private const int Size = 10;

    private Vector2 _position;
    private Vector2 _velocity = new(2, 2);
    private readonly Texture2D _texture;
    private readonly GraphicsDevice _graphicsDevice;

    public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, Size, Size);

    public Dot(GraphicsDevice graphicsDevice)
    {
        this._graphicsDevice = graphicsDevice;
        _position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        _texture = CreateTexture(graphicsDevice);
    }

    public void Update(GameTime gameTime)
    {
        _position += _velocity;

        if (_position.X < 0 || _position.X > _graphicsDevice.Viewport.Width - Size)
            _velocity.X *= -1;
        if (_position.Y < 0 || _position.Y > _graphicsDevice.Viewport.Height - Size)
            _velocity.Y *= -1;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    private static Texture2D CreateTexture(GraphicsDevice graphicsDevice)
    {
        var texture = new Texture2D(graphicsDevice, Size, Size);
        var data = new Color[Size * Size];
        for (var i = 0; i < data.Length; i++)
            data[i] = Color.Black;
        texture.SetData(data);
        return texture;
    }
}