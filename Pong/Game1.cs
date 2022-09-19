using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

// ****************             Assignment 1: Pong             **************** \\
/*
*    Mandatory parts of the assignment
*    
* 1. Moving paddles
*      -   As a first step, draw the two paddles on the left and right side of the screen. Next, fill in the Update method to 
*          handle keyboard input: when one of the relevant keys is being pressed, the corresponding paddle should move up 
*          or down. Of course, the Draw method should draw the paddles at their most recent position.
*      -  Make sure that you can easily change the movement speed of the paddles in your C# code, so that you can look for 
*          a speed that leads to fun gameplay. Of course, both paddles should have the same speed.
*      -  Make sure that the paddles can not move outside of the screen.
*      
* 2. A moving and bouncing ball
*      -  The next step is to add a moving ball. Start by drawing the ball sprite in the center of the screen. Write code that 
*          moves the ball in a certain direction, for example diagonally to the right and down. 
*          Next, instead of a fixed direction, write code that gives the ball a random movement direction, 
*          using the System.Random class. Make sure that the direction (so: the angle) is random, but that the speed is 
*          always the same. You could choose between a few pre-defined directions, or calculate a truly random direction.
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
*          
* 3. Storing and showing the number of lives
* 
*    -    Add code so that both players start with 3 lives. Whenever a player fails to bounce the ball back, that player should lose 
*          a life.
*    -    Make sure that the number of lives for both players is drawn on the screen. You can do this by drawing sprites in the top-
*          left and top-right corners, such as in our example screenshots. Of course, the number of sprites that you draw should 
*          match the number of lives that a player has.
*          
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
        Color background;
        Texture2D ballTexture;
        Texture2D Left_Paddle_Texture;
        Texture2D Right_Paddle_Texture;
        Vector2 position_Left;
        Vector2 position_Right;
        Vector2 position_Ball;

        int xPos2_Left = 0;
        int yPos2_Left = 0;

        int xPos2_Right = 0;
        int yPos2_Right = 0;

        int xPos_Ball = 400;
        int yPos_Ball = 250;
        int x_Change = 4;
        int y_Change = 3;

        // waarde dat bijhoudt hoeveel de bal versneld of vertraagd per hit.
        // 1 = geen change, 
        // <1 = vertraging
        // >1 = versnelling
        int speed_modifier = 2;
    
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
            ballTexture = Content.Load<Texture2D>("bal");
            Left_Paddle_Texture = Content.Load<Texture2D>("blauweSpeler");
            Right_Paddle_Texture = Content.Load<Texture2D>("rodeSpeler");
        }

        protected override void Update(GameTime gameTime)
        {
            // stopt de game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            //int redComponent = gameTime.TotalGameTime.Milliseconds / 4;
            //background = new Color(redComponent, 0, redComponent);

            //position = Vector2.Zero; // geeft starting position linksbovenin het scherm
            // x = 600 breed
            // y = 480 = bottom of screen
            // int xPos = 0 + gameTime.TotalGameTime.Milliseconds;
            //xPos2 += 2;

            // Als de S-key ingedrukt is dan wordt de yPos2 met 4 verhoogd (dus gaat de pad naar beneden) Als de W-key wordt ingedrukt dan juist andersom.
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                // Als ook de leftshift wordt gebruikt met S of W dan gaat de pad sneller naar boven of beneden (een speed boost)
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    yPos2_Left += 10;
                else yPos2_Left += 5;
            // Herhaling van regels 83 tot 87 alleen dan gaat de pad de andere richting op.
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    yPos2_Left -= 10;
                else yPos2_Left -= 5;

            //Buttons voor rode speler Up arrow en Down arrow en Right arrow voor Boost

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    yPos2_Right += 10;
                else yPos2_Right += 5;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    yPos2_Right -= 10;
                else yPos2_Right -= 5;

            //bepaal de boundaries van de game map zodat de Pad niet van het scherm af gaat
            // boven = 0 
            // onderin = 470
            //(470 - sprite lengte = positie lengte van de sprite)

            //          ##################################################
            //          ||                                              ||
            //          ||                                              ||
            //          ||                                              ||
            //          ||   ##                 X                  ##   ||      
            //          ||   ##                XXX                 ##   ||
            //          ||   ##                 X                  ##   ||
            //          ||   ##                                    ##   ||
            //          ||                                              ||
            //          ||                                              ||
            //          ||                                              ||
            //          ##################################################
            //
            //      paddle grootte: width 25px
            //                      height 80px
            //



            // Blauwe speler
            if (yPos2_Left >= 390)
            {
                yPos2_Left = 390;
            }
            if (yPos2_Left <= 0)
            {
                yPos2_Left = 0;
            }
            //Rechts (rode speler)
            if (yPos2_Right >= 390)
            {
                yPos2_Right = 390;
            }
            if (yPos2_Right <= 0)
            {
                yPos2_Right = 0;
            }

            int xPos_Left = 100;
            int xPos_Right = 675;
            position_Left = new Vector2(xPos_Left, yPos2_Left);
            position_Right = new Vector2(xPos_Right, yPos2_Right);

            

            // ball bounced terug rechts van het scherm
            if (xPos_Ball>=800-18)
            {
                if (x_Change >= 0)
                {
                    x_Change = x_Change * -1;
                }
            }
            // ball bounced terug links van het scherm
            if (xPos_Ball <= 0)
            {
                if (x_Change <= 0)
                {
                    x_Change = x_Change * -1;
                }
            }

            // boven
            if (yPos_Ball >= 468)
            {
                if (y_Change >= 0)
                {
                    y_Change = y_Change * -1;
                }
            }
            // onder
            if (yPos_Ball <= 0)
            {
                if (y_Change <= 0)
                {
                    y_Change = y_Change * -1;
                }
            }


            //check of de bal de linker speler raakt
            if (xPos_Ball <= xPos_Left+18 && yPos_Ball>=yPos2_Left  && yPos_Ball<=yPos2_Left+80)
            {
                if (x_Change <= 0)
                {
                    x_Change = x_Change * -1 * speed_modifier;
                }
            }
            //check of de bal de rechter speler raakt
            if (xPos_Ball >= xPos_Right-18 && yPos_Ball>=yPos2_Right && yPos_Ball<=yPos2_Right+80)
            {
                if (x_Change >= 0)
                {
                    x_Change = x_Change * -1 * speed_modifier;
                }
            }

            if (x_Change >= 16)
            {
                speed_modifier = 1;
            }

            xPos_Ball = xPos_Ball + x_Change;
            yPos_Ball = yPos_Ball + y_Change;
            

            position_Ball = new Vector2(xPos_Ball, yPos_Ball);
            
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Beige);


            _spriteBatch.Begin();

            

            _spriteBatch.Draw(Left_Paddle_Texture, position_Left, Color.White); // blauwe speler 

            _spriteBatch.Draw(Right_Paddle_Texture, position_Right, Color.White); // rode speler

            _spriteBatch.Draw(ballTexture, position_Ball, Color.White); // de bal


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}