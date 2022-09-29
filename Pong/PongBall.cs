using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

// to use, add pongBall = new PongBall(Content); in LoadContent

class PongBall
{
    // Member Variables
    Texture2D sprite;
    Vector2 position, ballDirection, ballOrigin;
    //float speed; toevoegen aan alternatieve Vector2?

    public static Random random = new Random();

    // PongBall Constructor that initialises all related variables
    public PongBall(ContentManager Content)
    {
        sprite = Content.Load<Texture2D>("pongBall");
        ballOrigin = new Vector2(sprite.Height, sprite.Width) / 2;

        // Minimum speed
        float min = -250f;
        float max = 250f;
        // TODO: exclude middle of the range (dat hij niet recht omhoog/naar beneden gaat)
        ballDirection = new Vector2(random.NextSingle() * (max - min) + min,
                                   random.NextSingle() * (max - min) + min);

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, Color.CornflowerBlue);

    }

    public void Reset()
    {
        // Place ball in center of the screen (from center of the ball)
        position = Pong.ScreenSize / 2 - ballOrigin; 
        

        // TODO: exclude middle of the range (dat hij niet recht omhoog/naar beneden gaat)
        // TODO: hij gaat soms nog steeds veels te langzaam, dus minimum snelheid nodig
        // Random Direction
        float min = -250f;
        float max = 250f;
        //float inclNegativeRange = (max - min) + min; --> waarom werkt dit niet??
        ballDirection = new Vector2(random.NextSingle() * (max - min) + min,
                                    random.NextSingle() * (max - min) + min);

    }
}
