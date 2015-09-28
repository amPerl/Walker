using Microsoft.Xna.Framework.Graphics;

namespace Walker.WalkerEngine
{
  public class RendererManager : GenericManager<Renderer>
  {
    public void Render()
    {
      if (_items.Count == 0) return;

      _items.Sort((a, b) => a.isSmooth.CompareTo(b.isSmooth));

      bool smoothing = _items[0].isSmooth;
      Engine.spriteBatch.Begin(SpriteSortMode.Deferred, null, smoothing ? SamplerState.LinearClamp : SamplerState.PointClamp);

      foreach (var renderer in _items)
      {
        if (renderer.isSmooth != smoothing)
        {
          smoothing = renderer.isSmooth;
          Engine.spriteBatch.End();
          Engine.spriteBatch.Begin(SpriteSortMode.Deferred, null, smoothing ? SamplerState.LinearClamp : SamplerState.PointClamp);
        }

        renderer.Render();
      }

      Engine.spriteBatch.End();
    }
  }
}
