using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Dot
{
    public static int Size = 10;

    private Vector2 position;
    private Vector2 velocity;
    private Texture2D texture;
    private GraphicsDevice graphicsDevice;

    public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, Size, Size);

    public Dot(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
        position = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
        velocity = new Vector2(2, 2);
        texture = CreateTexture(graphicsDevice);
    }

    public void Update(GameTime gameTime)
    {
        position += velocity;

        if (position.X < 0 || position.X > graphicsDevice.Viewport.Width - Size)
            velocity.X *= -1;
        if (position.Y < 0 || position.Y > graphicsDevice.Viewport.Height - Size)
            velocity.Y *= -1;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }

    private Texture2D CreateTexture(GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new Texture2D(graphicsDevice, Size, Size);
        Color[] data = new Color[Size * Size];
        for (int i = 0; i < data.Length; i++)
            data[i] = Color.Black;
        texture.SetData(data);
        return texture;
    }
}