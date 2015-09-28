using Microsoft.Xna.Framework.Graphics;

namespace Walker.WalkerEngine
{
  public abstract class Renderer : Component
  {
    public bool isSmooth { get; set; } = true;

    protected Renderer()
    {
      Engine.rendererManager.AddItem(this);
    }

    public abstract void Render();

    protected override void Destroy(float t)
    {
      Engine.rendererManager.RemoveItem(this);
      base.Destroy(t);
    }
  }
}
