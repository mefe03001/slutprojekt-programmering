using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Template
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Läser in pixeltexturen och skapar bollen och paddeln
		Texture2D pixel;
		Rectangle ball = new Rectangle(250, 550, 20, 20);
		Rectangle paddle = new Rectangle(200, 570, 150, 20);

		// Skapar blocken
		Rectangle block = new Rectangle(0, 0, 40, 20);
		List<Vector2> blockPos = new List<Vector2>();
		List<int> blockType = new List<int>();

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


			// Skapar blocken
			for (int j = 0; j < 6; j++)
			{
				for (int i = 0; i < 11; i++)
				{
					blockPos.Add(new Vector2((i * 40) + 200, (j * 20) + 100));
					blockType.Add(j);
				}
			}
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


			// Gör så att bollen kan studsa mot blocken. När bollen har studsat på ett block tas det bort.
			for (int i = 0; i < blockPos.Count; i++)
			{
				Rectangle block = new Rectangle((int)blockPos[i].X, (int)blockPos[i].Y, 40, 20);
				if (block.Intersects(ball))
				{

					y_speed *= -1;
					
					
					if (ball.X + 6 >= blockPos[i].X && ball.X + 6 <= blockPos[i].X + 40)
					{
						y_speed *= -1;
					}
					else
					{
						x_speed *= -1;
					}
					blockPos.RemoveAt(i);
					blockType.RemoveAt(i);
					i--;
					break;
				}
			}


			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.DarkGray);

			spriteBatch.Begin();
			spriteBatch.Draw(pixel, ball, Color.Green);
			spriteBatch.Draw(pixel, paddle, Color.White);

			for (int i = 0; i < blockPos.Count; i++)
			{
				spriteBatch.Draw(pixel, blockPos[i], block, Color.White);
			}


			spriteBatch.End();


			base.Draw(gameTime);
		}
	}
}
