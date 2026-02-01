using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Assignment_01;

public class SimpleAnimation
{
    private readonly Texture2D _texture;
    private readonly List<Rectangle> _frames;
    private readonly float _timePerFrame;

    private float _timer;
    private int _frameIndex;

    public bool Looping { get; set; } = true;
    public bool Paused { get; set; } = false;

    public SimpleAnimation(Texture2D texture, int frameWidth, int frameHeight, int frameCount, float framesPerSecond)
    {
        _texture = texture;
        _frames = new List<Rectangle>();

        for (int i = 0; i < frameCount; i++)
        {
            _frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
        }

        _timePerFrame = 1f / framesPerSecond;
    }

    public void Update(GameTime gameTime)
    {
        if (Paused)
            return;

        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= _timePerFrame)
        {
            _timer -= _timePerFrame;
            _frameIndex++;

            if (_frameIndex >= _frames.Count)
            {
                if (Looping)
                    _frameIndex = 0;
                else
                    _frameIndex = _frames.Count - 1;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects)
    {
        spriteBatch.Draw(
            _texture,
            position,
            _frames[_frameIndex],
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            effects,
            0f
        );
    }
}