using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Square
{
    public static int Size = 20;

    private Vector2 position;
    private Texture2D texture;

    public Rectangle Bounds => new Rectangle((int)position.X, (int)position.Y, Size, Size);

    public Square(GraphicsDevice graphicsDevice, Vector2 position)
    {
        this.position = position;
        texture = CreateTexture(graphicsDevice);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.Red);
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