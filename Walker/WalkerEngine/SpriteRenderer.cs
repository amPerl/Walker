using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Walker.WalkerEngine
{
  class SpriteRenderer : Renderer
  {
    Vector2 _origin;
    Texture2D _texture;

    public Texture2D texture
    {
      get { return _texture; }
      set
      {
        _texture = value;
        _origin = _texture.Bounds.Center.ToVector2();
      }
    }

    public Color tintColour { get; set; } = Color.White;
    
    public override void Render()
    {
      Engine.spriteBatch.Draw(texture, transform.position, null, tintColour, transform.rotation, _origin, transform.scale, SpriteEffects.None, transform.depth);
    }
  }
}
