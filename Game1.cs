using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private SpriteFont _font;
    private int _windowWidth;
    private int _windowHeight;
    

    private Texture2D _staticImage;
    private Vector2 _autoMovePosition;
    private float _autoSpeed = 80f;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1024;
        _graphics.PreferredBackBufferHeight = 768;
    }

    protected override void Initialize()
    {
        _windowWidth = GraphicsDevice.Viewport.Width;
        _windowHeight = GraphicsDevice.Viewport.Height;
        
        _autoMovePosition = new Vector2(0, 100);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _background = Content.Load<Texture2D>("Background");
        _font = Content.Load<SpriteFont>("Sansfont");
        
        _staticImage = Content.Load<Texture2D>("Idle");
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _autoMovePosition.X += _autoSpeed * deltaTime;
        
        if (_autoMovePosition.X > _windowWidth)
            _autoMovePosition.X = -_staticImage.Width;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _spriteBatch.Draw(
            _background,
            new Rectangle(0, 0, _windowWidth, _windowHeight),
            Color.White
        );

        // Draws static image with automatic movement
        _spriteBatch.Draw(_staticImage, _autoMovePosition, Color.White);

        // Draw text
        _spriteBatch.DrawString(
            _font,
            "Assignment 01 - Animation & Input",
            new Vector2(20, 20),
            Color.White
        );

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}