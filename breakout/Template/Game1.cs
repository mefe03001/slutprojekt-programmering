using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Läser in pixeltexturen och lägger till bollen och paddeln
		Texture2D pixel;
		Rectangle ball = new Rectangle(100, 100, 20, 20);
		Rectangle paddle = new Rectangle(200, 570, 150, 20);

		// Bollens starthastighet
		int x_speed = 4;
		int y_speed = 4;


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			// Upplösning
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			pixel = Content.Load<Texture2D>("pixel");


			
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			// För styrning med paddeln med A, D och med höger och vänster piltangent.
			if (Keyboard.GetState().IsKeyDown(Keys.A))
				paddle.X -= 5;
			if (Keyboard.GetState().IsKeyDown(Keys.D))
				paddle.X += 5;
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
				paddle.X -= 5;
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
				paddle.X += 5;


            // Gör så att paddeln inte kan åka utanför skärmen.
			if (paddle.X < 0)
				paddle.X = 0;
            if (paddle.X > Window.ClientBounds.Width - paddle.Width)
                paddle.X = Window.ClientBounds.Width - paddle.Width;


            // Gör så att bollen rör på sig
            ball.X += x_speed;
			ball.Y += y_speed;


            // Gör så att bollen byter håll är den går ihop med paddeln
			if (ball.Intersects(paddle))
			{
				y_speed *= -1;
			}


            // Gör så att bollen studsar mot sidorna och toppen
            if (ball.X < 0 || ball.X > Window.ClientBounds.Width - ball.Width)
                x_speed *= -1;
            if (ball.Y < 0)
                y_speed *= -1;

            // Avslutar spelet om bollen når nedre kant
            if (ball.Y > Window.ClientBounds.Height - ball.Height)
                Exit();



            base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.DarkGray);

			spriteBatch.Begin();
			spriteBatch.Draw(pixel, ball, Color.Green);
			spriteBatch.Draw(pixel, paddle, Color.White);
			spriteBatch.End();


			base.Draw(gameTime);
		}
	}
}
