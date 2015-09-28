using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Walker.WalkerEngine
{
  public abstract class Game : Microsoft.Xna.Framework.Game
  {
    protected GraphicsDeviceManager _graphics;
    protected SpriteBatch _spriteBatch;

    public Game()
    {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      base.Initialize();

      _spriteBatch = new SpriteBatch(GraphicsDevice);
      Engine.Initialise(this, _spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      Engine.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Gray);
      Engine.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}