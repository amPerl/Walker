using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Walker.WalkerEngine
{
  public static class Engine
  {
    public static BehaviourManager behaviourManager { get; private set; }
    public static RendererManager rendererManager { get; private set; }

    static bool _running = false;
    static Game _game;
    
    public static GameTime gameTime { get; private set; }

    public static SpriteBatch spriteBatch { get; private set; }

    static Engine()
    {
      behaviourManager = new BehaviourManager();
      rendererManager = new RendererManager();
    }

    public static void Initialise(Game game, SpriteBatch spriteBatch)
    {
      _running = true;
      _game = game;
      Engine.spriteBatch = spriteBatch;
    }

    public static void Update(GameTime gameTime)
    {
      if (!_running) return;
      Engine.gameTime = gameTime;

      behaviourManager.Update();
    }

    public static void Draw(GameTime gameTime)
    {
      if (!_running) return;
      Engine.gameTime = gameTime;
      
      rendererManager.Render();
    }
  }
}
