using System;
using Microsoft.Xna.Framework;

namespace Walker.WalkerEngine
{
  public abstract class Object
  {
    /// <summary>
    /// The name of the object.
    /// </summary>
    public string name;

    /// <summary>
    /// Returns the name of the game object.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return name;
    }

    /// <summary>
    /// Removes a GameObject or Component.
    /// </summary>
    /// <param name="obj">The object to destroy.</param>
    /// <param name="t">The optional amount of time to delay before destroying the object</param>
    public static void Destroy(Object obj, float t = 0.0f)
    {
      obj.Destroy(t);
    }

    protected abstract void Destroy(float t);

    /// <summary>
    /// Clones the object 'original'.
    /// </summary>
    /// <param name="original">The object to clone.</param>
    /// <returns>A clone of the object 'original'</returns>
    public static Object Instantiate(Object original)
    {
      return Instantiate(original, Vector2.Zero, 0.0f);
    }

    /// <summary>
    /// Clones the object original and returns the clone.
    /// </summary>
    /// <param name="original">The object to clone.</param>
    /// <param name="position">The position of the cloned object.</param>
    /// <param name="rotation">The rotation of the cloned object.</param>
    /// <returns>A clone of the object 'original'</returns>
    public static Object Instantiate(Object original, Vector2 position, float rotation)
    {
      throw new NotImplementedException();
    }
  }
}
