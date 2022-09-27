using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

// to use, add pongBall = new PongBall(Content); in LoadContent

class PongBall
{
    // Member Variables
    Texture2D ballSprite;
    Vector2 ballPosition, ballDirection, ballOrigin;
    //float ballSpeed; toevoegen aan alternatieve Vector2?
    int ballWidth, ballHeight;

    public static Random random = new Random();

    // PongBall Constructor that initialises all related variables
    public PongBall(ContentManager Content)
    {
        ballSprite = Content.Load<Texture2D>("pongBall");

        ballHeight = ballSprite.Height;
        ballWidth = ballSprite.Width;
        ballOrigin = new Vector2(ballHeight, ballWidth) / 2;
        ballPosition = new Vector2(480, 300) / 2 - ballOrigin; // get windowwidth and height from graphics

        // Minimum speed
        float min = -250f;
        float max = 250f;
        // TODO: exclude middle of the range (dat hij niet recht omhoog/naar beneden gaat)
        ballDirection = new Vector2(random.NextSingle() * (max - min) + min,
                                   random.NextSingle() * (max - min) + min);
    }
}
