using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment_01;

public class Game1 : Game
{
    // Graphics device manager for handling graphics settings
    private GraphicsDeviceManager _graphics;
    // SpriteBatch for drawing textures
    private SpriteBatch _spriteBatch;

    // Background texture
    private Texture2D _background;
    // Static image texture
    private Texture2D _staticImage;

    // Player animations
    private SimpleAnimation _playerWalk;
    private SimpleAnimation _playerIdle;

    // Font
    private SpriteFont _font;

    // Positions
    private Vector2 _playerPosition;
    private Vector2 _autoMovePosition;

    // Movement speeds
    private float _playerSpeed = 200f;
    private float _autoSpeed = 80f;

    // Window size
    private int _windowWidth;
    private int _windowHeight;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        // Set a reasonable window size (not background image size)
        _graphics.PreferredBackBufferWidth = 1024;  // Fixed width
        _graphics.PreferredBackBufferHeight = 768;  // Fixed height
    }

    protected override void Initialize()
    {
        _windowWidth = GraphicsDevice.Viewport.Width;
        _windowHeight = GraphicsDevice.Viewport.Height;

        _playerPosition = new Vector2(100, _windowHeight - 200);
        _autoMovePosition = new Vector2(0, 100);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load content
        _background = Content.Load<Texture2D>("Background");
        _staticImage = Content.Load<Texture2D>("Idle");
        _font = Content.Load<SpriteFont>("Sansfont");

        // Player walk animation
        _playerWalk = new SimpleAnimation(
            Content.Load<Texture2D>("Player"),
            405 / 6,  // frameWidth
            1820 / 6, // frameHeight
            6,        // frameCount
            6f        // fFPs
        );

        // Player idle animation
        Texture2D idleTexture = Content.Load<Texture2D>("Idle");
        _playerIdle = new SimpleAnimation(
            idleTexture,
            idleTexture.Width / 2,  
            idleTexture.Height,     
            2,                      
            2f                      
        );
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kb = Keyboard.GetState();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _playerWalk.Update(gameTime);
        _playerIdle.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();


        // Draw text
        _spriteBatch.DrawString(
            _font,
            "Assignment 01 - Animation & Input Ben 10 hide as a alien",
            new Vector2(20, 20),
            Color.White
        );

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}