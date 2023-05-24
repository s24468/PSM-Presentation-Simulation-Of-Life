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

    // This would be a field in your Game class
    Texture2D monsterTexture;

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
        // TODO: Add your initialization logic here

        base.Initialize();
    }

// These would be fields in your Game class
    Texture2D monsterLegTexture;
    Texture2D monsterEyeTexture;
    Monster myMonster = new Monster(100.0f, 100.0f, 100.0f, 100.0f, "C:\\Users\\Jarek\\RiderProjects\\LifeSimulation\\MacroEvolution\\Content\\myMonsterLeg.png", "C:\\Users\\Jarek\\RiderProjects\\LifeSimulation\\MacroEvolution\\Content\\myMonsterEye.png");

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // Create a new Monster

        // Create a new Monster
        
        // Load the Monster image
        // monsterTexture = Content.Load<Texture2D>(myMonster.ImagePath);
        // Load the Monster images
        // monsterLegTexture = Content.Load<Texture2D>(myMonster.LegImagePath);
        // monsterEyeTexture = Content.Load<Texture2D>(myMonster.EyeImagePath);
        // Texture2D texture;
        // using var stream = new FileStream(myMonster.LegImagePath, FileMode.Open);
        // monsterLegTexture = Texture2D.FromStream(GraphicsDevice, stream);
        // Load the Monster images
        using (var stream = new FileStream(myMonster.LegImagePath, FileMode.Open))
        {
            monsterLegTexture = Texture2D.FromStream(GraphicsDevice, stream);
        }

        using (var stream = new FileStream(myMonster.EyeImagePath, FileMode.Open))
        {
            monsterEyeTexture = Texture2D.FromStream(GraphicsDevice, stream);
        }
        
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // Draw the Monster parts
        spriteBatch.Begin();

        // Adjust the size and position of the parts as necessary
        // For example, you could make the legs larger if the monster's Speed attribute increases
        
        
        Vector2 legPosition = new Vector2(0, 0);
        Vector2 legSize = new Vector2(myMonster.Speed, myMonster.Speed);
        spriteBatch.Draw(monsterLegTexture, new Rectangle(legPosition.ToPoint(), legSize.ToPoint()), Color.White);

        // spriteBatch.Draw(monsterTexture, new Vector2(0, 0), Color.White);
       
        spriteBatch.End();

        base.Draw(gameTime);
    }
}