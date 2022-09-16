using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/* Assignment 1: Pong
 * Mandatory parts of the assignment
 * 1. Moving paddles
 *      -   As a first step, draw the two paddles on the left and right side of the screen. Next, fill in the Update method to 
 *          handle keyboard input: when one of the relevant keys is being pressed, the corresponding paddle should move up 
 *          or down. Of course, the Draw method should draw the paddles at their most recent position.
 *      -  Make sure that you can easily change the movement speed of the paddles in your C# code, so that you can look for 
 *          a speed that leads to fun gameplay. Of course, both paddles should have the same speed.
 *      -  Make sure that the paddles can not move outside of the screen.
 * 2. A moving and bouncing ball
 *      -  The next step is to add a moving ball. Start by drawing the ball sprite in the center of the screen. Write code that 
 *          moves the ball in a certain direction, for example diagonally to the right and down. Next, instead of a fixed direction, 
 *          write code that gives the ball a random movement direction, using the System.Random class. Make sure that the 
 *          direction (so: the angle) is random, but that the speed is always the same. You could choose between a few pre-
 *          defined directions, or calculate a truly random direction.
 *     -   Next up, make sure that the ball can bounce off the top and bottom edges of the screen. When the ball sprite 
 *          reaches one of those edges, its vertical speed should flip.
 *     -   After that, add code to detect if the ball has reached the left or right edge of the screen. If that happens, check 
 *          whether or not the ball touches the paddle on that side of the screen. If the ball touches the paddle, the ball should 
 *          bounce off horizontally. When this happens, the speed of the ball should also increase a bit.
 *    -    To make things more interesting for the players, add code so that the bouncing angle of the ball depends on where
 *          it hits a paddle. If the ball is close to the edge of the paddle, it should bounce off with a more extreme angle than when 
 *          it hits the paddle in the middle. This allows players to apply some strategy, to make the game harder for their opponent.
 *    -    If the ball reaches the left/right side of the screen and does not touch a paddle, then the ball should re-appear in the 
 *          center of the screen. It should start moving in a new random direction, at its relaxed starting speed.
 * 3. Storing and showing the number of lives
 *    -    Add code so that both players start with 3 lives. Whenever a player fails to bounce the ball back, that player should lose 
 *          a life.
 *    -    Make sure that the number of lives for both players is drawn on the screen. You can do this by drawing sprites in the top-
 *          left and top-right corners, such as in our example screenshots. Of course, the number of sprites that you draw should 
 *          match the number of lives that a player has.
 * 4. Game states
 *    -     In total, the game should have three so-called "game states": a welcome screen, a playing state that contains the main 
 *          game, and a "game over state". So far, you've written the "playing state". It's now time to implement the two other states. 
 *          Consider writing an 'enum' that contains all possible game states, and adding a member variable of that type to represent 
 *          the current state. Depending on that value, the Update and Draw methods should do different things.   
 *    -    When the game starts, the game should show a welcoming message. Players cannot do anything yet, except for starting the 
 *          game by pressing the spacebar.  
 *    -    When one of the players has no more lives left, the game should reach the "game over" state. In that state, the paddles and 
 *          balls can no longer move. There should then also be a message on the screen that says which player has won. Players 
 *          should now be able to start a new game by pressing the spacebar.
 */

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}