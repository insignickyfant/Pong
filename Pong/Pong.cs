﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D;
using SharpDX.Direct3D9;
using System;

// ****************             Assignment 1: Pong             **************** \\
/*
*    Mandatory parts of the assignment
*/
/* 1. Moving paddles DONE
*      -   As a first step, draw the two paddles on the left and right side of the screen. Next, fill in the Update method to 
*          handle keyboard input: when one of the relevant keys is being pressed, the corresponding paddle should move up 
*          or down. Of course, the Draw method should draw the paddles at their most recent position.
*      -  Make sure that you can easily change the movement speed of the paddles in your C# code, so that you can look for 
*          a speed that leads to fun gameplay. Of course, both paddles should have the same speed.
*      -  Make sure that the paddles can not move outside of the screen.
*/
/* 2. A moving and bouncing ball TODO
*      -  The next step is to add a moving ball. Start by drawing the ball sprite in the center of the screen. Write code that 
*          moves the ball in a certain direction, for example diagonally to the right and down. 
*          DONE
*          
*      -  Next, instead of a fixed direction, write code that gives the ball a random movement direction, 
*          using the System.Random class. Make sure that the direction (so: the angle) is random, but that the speed is 
*          always the same. You could choose between a few pre-defined directions, or calculate a truly random direction.
*          DONE
*          
*     -   Next up, make sure that the ball can bounce off the top and bottom edges of the screen. When the ball sprite 
*          reaches one of those edges, its vertical speed should flip. 
*          DONE
*          
*     -   After that, add code to detect if the ball has reached the left or right edge of the screen. If that happens, check 
*          whether or not the ball touches the paddle on that side of the screen. If the ball touches the paddle, the ball should 
*          bounce off horizontally. When this happens, the speed of the ball should also increase a bit.  
*          DONE
*          Alternative: 8.4.1 Using Rectangular Bounding Boxes
*               
*    -    To make things more interesting for the players, add code so that the bouncing angle of the ball depends on where
*          it hits a paddle. If the ball is close to the edge of the paddle, it should bounce off with a more extreme angle than when 
*          it hits the paddle in the middle. This allows players to apply some strategy, to make the game harder for their opponent.
*          TODO: bal gaat de paddle in
*          
*    -    If the ball reaches the left/right side of the screen and does not touch a paddle, then the ball should re-appear in the 
*          center of the screen. It should start moving in a new random direction, at its relaxed starting speed. 
*          DONE
*/
/* 3. Storing and showing the number of lives TODO
*    -    Add code so that both players start with 3 lives. Whenever a player fails to bounce the ball back, that player should lose 
*          a life.
*    -    Make sure that the number of lives for both players is drawn on the screen. You can do this by drawing sprites in the top-
*          left and top-right corners, such as in our example screenshots. Of course, the number of sprites that you draw should 
*          match the number of lives that a player has.
*/
/* 4. Game states TODO
*    -     In total, the game should have three so-called "game states": a welcome screen, a playing state that contains the main 
*          game, and a "game over state". So far, you've written the "playing state". It's now time to implement the two other states. 
*          Consider writing an 'enum' that contains all possible game states, and adding a member variable of that type to represent 
*          the current state. Depending on that value, the Update and Draw methods should do different things.   
*    -    When the game starts, the game should show a welcoming message. Players cannot do anything yet, except for starting the 
*          game by pressing the spacebar.  
*    -    When one of the players has no more lives left, the game should reach the "game over" state. In that state, the paddles and 
*          balls can no longer move. There should then also be a message on the screen that says which player has won. Players 
*          should now be able to start a new game by pressing the spacebar.
*   
*/

namespace Pong
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Sprite variables
        // PongBall pongBall;
        // Paddle leftPaddle, rightPaddle;
        Texture2D ballSprite;
        Vector2 ballPosition, ballDirection;
        //float ballSpeed; // or double? --> Draw wants float.. or..?
        int ballWidth, ballHeight;

        Texture2D leftPaddle, rightPaddle;
        Vector2 leftPaddlePosition, rightPaddlePosition;
        int paddleWidth, paddleHeight, leftPaddleVelocity, rightPaddleVelocity;

        // Other variables
        //Texture2D startBackground, gameBackground, endBackground;
        int windowWidth, windowHeight;
        public static Random random = new Random();


        public Pong()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            windowWidth = _graphics.PreferredBackBufferWidth;
            windowHeight = _graphics.PreferredBackBufferHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Set paddles and ball to starting positions
            ResetField();

            // TODO: Starting Screen
            // Start();
            // TODO: Player lives
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Game sprites and helper variables
            //startBackground = Content.Load<Texture2D>(”background1”);
            //gameBackground = Content.Load<Texture2D>(”background2”);
            //endBackground = Content.Load<Texture2D>(”background3”);


            ballSprite = Content.Load<Texture2D>("pongBall");
            ballHeight = ballSprite.Height;
            ballWidth = ballSprite.Width;

            leftPaddle = Content.Load<Texture2D>("blauweSpeler");
            rightPaddle = Content.Load<Texture2D>("rodeSpeler");
            paddleHeight = leftPaddle.Height;
            paddleWidth = leftPaddle.Width;

            // Alternatively, when using classes:
            // pongBall = new PongBall(Content); to add as class
            // leftPaddle = new Paddle(Content, "left");            
            // rightPaddle = new Paddle(Content, "right");
        }

        protected override void Update(GameTime gameTime)
        {
            // Ball and Paddle movement
            BallMovement(gameTime);
            PaddleMovement();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // background color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw sprites (Draws over previous content, so start with background)
            _spriteBatch.Begin();
            //_spriteBatch.Draw(background, Vector2.Zero, Color.White);
            _spriteBatch.Draw(leftPaddle, leftPaddlePosition, Color.CornflowerBlue);
            _spriteBatch.Draw(rightPaddle, rightPaddlePosition, Color.CornflowerBlue);
            _spriteBatch.Draw(ballSprite, ballPosition, Color.CornflowerBlue);
            //leftPaddle.Draw(gameTime, _spriteBatch);
            //rightPaddle.Draw(gameTime, _spriteBatch); maybe just variable for both paddles?
            //pongBall.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ResetField()
        {
            // Place ball in center of the screen (from center of the ball)
            ballPosition = new Vector2(windowWidth - ballWidth, windowHeight - ballHeight) / 2;

            // TODO: exclude middle of the range (dat hij niet recht omhoog/naar beneden gaat)
            // TODO: hij gaat soms nog steeds veels te langzaam, dus minimum snelheid nodig
            // Random Direction
            float min = -250f;
            float max = 250f;
            //float inclNegativeRange = (max - min) + min; --> waarom werkt dit niet??
            ballDirection = new Vector2(random.NextSingle() * (max - min) + min,
                                        random.NextSingle() * (max - min) + min);


            // Place paddles centered in left and right edges at normal speed
            leftPaddlePosition = new Vector2(0, (windowHeight - paddleHeight) / 2);
            rightPaddlePosition = new Vector2(windowWidth - paddleWidth, (windowHeight - paddleHeight) / 2);
            leftPaddleVelocity = 10;
            rightPaddleVelocity = 10;
            

            // Alternatively, when using classes:
            // leftPaddlePosition.Reset();
            // righttPaddlePosition.Reset();
            // pongBall.Reset();

        }


        public void BallMovement(GameTime gameTime)
        {
            ballPosition += ballDirection * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Boundaries and Paddle Collision
            // RIGHT
            if (ballPosition.X + ballSprite.Width > windowWidth)
            {
                // Check if and where rightPaddle is touched 
                if (ballPosition.X + ballSprite.Width >= rightPaddlePosition.X - paddleWidth &&
                    ballPosition.Y >= rightPaddlePosition.Y && ballPosition.Y <= rightPaddlePosition.Y + paddleHeight)
                {
                    // return and speed up ball 
                    ballPosition.X = windowWidth - ballWidth - paddleWidth;
                    ballDirection.X *= -1.2f;
                    
                    // AND appropriately angled ballDirection
                    // AngledBounce("right");
                    //Vector2 rightMiddle = new Vector2(rightPaddlePosition.X, rightPaddlePosition.Y + paddleHeight / 2);
                    //float delta = ballPosition.Y - rightMiddle.Y;
                    //float d = paddleHeight / 2;

                    //ballDirection = Vector2.Normalize(delta / d * -Vector2.UnitY + (1 - delta / d) * Vector2.UnitX);

                }
                // if no --> reset ball
                else
                {
                    ResetField();
                }
            }
            // LEFT
            else if (ballPosition.X < 0)
            {
                // TODO: add check if leftPaddle is touched and WHERE
                if (ballPosition.X <= paddleWidth &&
                    ballPosition.Y >= leftPaddlePosition.Y && ballPosition.Y <= leftPaddlePosition.Y + paddleHeight)
                {
                    // if yes--> return and speed up ball 
                    ballPosition.X = paddleWidth;
                    ballDirection.X *= -1.2f;
                    // AND appropriately angled ballDirection
                    //ballDirection = ?;
                }
                // if no --> reset ball
                else
                {
                    ResetField();
                }
            }
            // TOP
            if (ballPosition.Y < 0)
            {
                ballPosition.Y = 0;
                if (ballDirection.Y <= 0)
                {
                    ballDirection.Y *= -1;
                }
            }
            // BOTTOM
            else if (ballPosition.Y + ballHeight >= windowHeight)
            {
                ballPosition.Y = windowHeight - ballHeight;
                if (ballDirection.Y >= 0)
                {
                    ballDirection.Y *= -1;
                }
            }
        }

        private void PaddleMovement()
        {
            // Take user input from keyboard and moves corresponding paddle
            KeyboardInput(leftPaddleVelocity, rightPaddleVelocity);

            // Paddle Boundaries
            // TOP
            if (leftPaddlePosition.Y < 0)
            {
                leftPaddlePosition.Y = 0;
            }
            // BOTTOM
            else if (leftPaddlePosition.Y > windowHeight - paddleHeight)
            {
                leftPaddlePosition.Y = windowHeight - paddleHeight;
            }
            // TOP
            if (rightPaddlePosition.Y < 0)
            {
                rightPaddlePosition.Y = 0;
            }
            // BOTTOM
            else if (rightPaddlePosition.Y > windowHeight - paddleHeight)
            {
                rightPaddlePosition.Y = windowHeight - paddleHeight;
            }
        }

        public void KeyboardInput(int lPV, int rPV)
        {
            // Start and Exit
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // Start();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Paddle movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                leftPaddlePosition.Y -= lPV;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                leftPaddlePosition.Y += lPV;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                rightPaddlePosition.Y -= rPV;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                rightPaddlePosition.Y += rPV;
            }
        }
    }
}
