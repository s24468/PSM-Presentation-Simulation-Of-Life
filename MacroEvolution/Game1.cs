using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MacroEvolution;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;
    private static readonly int ScreenWidth = 1400;

    private static readonly int ScreenHeight = 800;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        base.Initialize();
    }

// Create a new Monster
    Monster myMonster = new Monster(1.0f, 1.0f, 1.0f, 1.0f);

    Texture2D pixelTexture;

    protected override void LoadContent()
    {
        // Create a 1x1 pixel texture
        pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
    
        spriteBatch.Begin();
    
        // Draw a segment representing the leg of the monster
        Vector2 legStart = new Vector2(0, 0);
        Vector2 legEnd = new Vector2(100, 200); // you can adjust these coordinates as you like
        Vector2 legDirection = legEnd - legStart;
        float legLength = legDirection.Length();
        legDirection.Normalize();
    
        spriteBatch.Draw(pixelTexture, legStart, null, Color.Red, (float)Math.Atan2(legDirection.Y, legDirection.X),
            Vector2.Zero, new Vector2(legLength, myMonster.Speed), SpriteEffects.None, 0f);
    
        spriteBatch.End();
    
        base.Draw(gameTime);
    }


    
}