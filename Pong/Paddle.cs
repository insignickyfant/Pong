using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

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
}