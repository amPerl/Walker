using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Walker.WalkerEngine;

namespace Walker.WalkerGame
{
  public class WalkerGame : WalkerEngine.Game
  {
    protected override void Initialize()
    {
      base.Initialize();

      var player = new GameObject("Player");
      player.transform.position = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
      player.transform.scale = new Vector2(2.0f);

      var sr = player.AddComponent<SpriteRenderer>();
      sr.texture = Content.Load<Texture2D>("man");
      sr.isSmooth = false;

      var gridTransform = player.AddComponent<GridTransform>();
      gridTransform.movementDuration = 0.2f;

      player.AddComponent<GridControls>();

      player.Initialise();
    }
  }
}
