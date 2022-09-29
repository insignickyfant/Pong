using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using System.Reflection.Metadata;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

// to use, add left/rightPaddle = new Paddle(Content, left/right); in LoadContent

class Paddle
{
    // Member Variables
    Texture2D leftPaddle, rightPaddle;
    Vector2 leftPaddlePosition, rightPaddlePosition;
    float leftPaddleVelocity, rightPaddleVelocity; // extra: use boost for speed-up
    int paddleWidth, paddleHeight;

    // Paddle Constructor
    public Paddle(ContentManager Content, String player)
    {
        paddleHeight = leftPaddle.Height;
        paddleWidth = leftPaddle.Width;

        if (player == "left")
        {
            leftPaddle = Content.Load<Texture2D>("blauweSpeler");

        }
        else if (player == "right")
        {
            rightPaddle = Content.Load<Texture2D>("rodeSpeler");

        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(leftPaddle, leftPaddlePosition, Color.CornflowerBlue);
        spriteBatch.Draw(rightPaddle, rightPaddlePosition, Color.CornflowerBlue);
    }

    public void Reset()
    {
        // Place paddles centered in left and right edges at normal speed
        // windowheight etc werkt niet
        leftPaddlePosition = new Vector2(0, (300 - paddleHeight) / 2); 
        rightPaddlePosition = new Vector2(480 - paddleWidth, (300 - paddleHeight) / 2);
        leftPaddleVelocity = 10;
        rightPaddleVelocity = 10;
    }
}