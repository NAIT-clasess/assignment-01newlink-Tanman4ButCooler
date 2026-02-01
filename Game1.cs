using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // TODO: Add your game content here
    private Texture2D _background;
    private SpriteFont _font;
    private int _windowWidth;
    private int _windowHeight;
    private Texture2D _staticImage;
    private Vector2 _autoMovePosition;
    private float _autoSpeed = 80f;
    
    private SimpleAnimation _playerWalk;
    private SimpleAnimation _playerIdle;
    private Vector2 _playerPosition;
    private float _playerSpeed = 200f;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        // TODO: Set window size
        _graphics.PreferredBackBufferWidth = 1024;
        _graphics.PreferredBackBufferHeight = 768;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _windowWidth = GraphicsDevice.Viewport.Width;
        _windowHeight = GraphicsDevice.Viewport.Height;
        
        _autoMovePosition = new Vector2(0, 100);
        _playerPosition = new Vector2(100, _windowHeight - 200);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _background = Content.Load<Texture2D>("Background");
        _font = Content.Load<SpriteFont>("Sansfont");
        _staticImage = Content.Load<Texture2D>("Idle");
        
        _playerWalk = new SimpleAnimation(
            Content.Load<Texture2D>("Player"),
            405 / 6,    
            1820 / 6,  
            6,          
            6f          
        );

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
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Keyboard input for player movement
        KeyboardState kb = Keyboard.GetState();
        Vector2 movement = Vector2.Zero;
        bool isMoving = false;

        if (kb.IsKeyDown(Keys.A))
        {
            movement.X -= 1;
            isMoving = true;
        }
        if (kb.IsKeyDown(Keys.D))
        {
            movement.X += 1;
            isMoving = true;
        }
        if (kb.IsKeyDown(Keys.W))
        {
            movement.Y -= 1;
            isMoving = true;
        }
        if (kb.IsKeyDown(Keys.S))
        {
            movement.Y += 1;
            isMoving = true;
        }

        if (movement != Vector2.Zero)
            movement.Normalize();

        _playerPosition += movement * _playerSpeed * deltaTime;

        // Keep player within screen bounds
        _playerPosition.X = MathHelper.Clamp(
            _playerPosition.X,
            0,
            _windowWidth - 64
        );

        _playerPosition.Y = MathHelper.Clamp(
            _playerPosition.Y,
            0,
            _windowHeight - 128
        );

        if (isMoving)
            _playerWalk.Update(gameTime);
        else
            _playerIdle.Update(gameTime);

        _autoMovePosition.X += _autoSpeed * deltaTime;
        if (_autoMovePosition.X > _windowWidth)
            _autoMovePosition.X = -_staticImage.Width;

        base.Update(gameTime); 
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        _spriteBatch.Draw(
            _background,
            new Rectangle(0, 0, _windowWidth, _windowHeight),
            Color.White
        );

        _spriteBatch.Draw(_staticImage, _autoMovePosition, Color.White);

        KeyboardState kb = Keyboard.GetState();
        if (kb.GetPressedKeys().Length > 0 && 
            (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.D) || 
             kb.IsKeyDown(Keys.W) || kb.IsKeyDown(Keys.S)))
        {
            _playerWalk.Draw(_spriteBatch, _playerPosition, SpriteEffects.None);
        }
        else
        {
            _playerIdle.Draw(_spriteBatch, _playerPosition, SpriteEffects.None);
        }

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