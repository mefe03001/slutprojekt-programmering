using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D flappy;

		Vector2 flappyPos = new Vector2(100, 300);
		Vector2 gravitation;
		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 480;
			graphics.PreferredBackBufferHeight = 854;
			Content.RootDirectory = "Content";
		}
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			flappy = Content.Load<Texture2D>("flappy");
		}
		protected override void UnloadContent()
		{

		}
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			flappyPos += gravitation;
			gravitation.Y += 0.25f;

			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				gravitation.Y = -5f;
			}

			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			spriteBatch.Draw(flappy, flappyPos, Color.White);
			spriteBatch.End();


			base.Draw(gameTime);
		}
	}
}
