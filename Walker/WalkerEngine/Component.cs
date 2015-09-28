using System;

namespace Walker.WalkerEngine
{
  public abstract class Component : Object
  {
    /// <summary>
    /// 	The game object this component is attached to. A component is always attached to a game object.
    /// </summary>
    public GameObject gameObject;

    /// <summary>
    /// The tag of this game object.
    /// </summary>
    public string Tag { get { return gameObject.tag; } }

    /// <summary>
    /// The Transform attached to this GameObject (null if there is none attached).
    /// </summary>
    public Transform transform { get { return gameObject.transform; } }

    /// <summary>
    /// Gets a component from the game object.
    /// </summary>
    /// <typeparam name="T">The type of component to get.</typeparam>
    /// <returns>The component from the game object.</returns>
    public T GetComponent<T>() where T : Component
    {
      return gameObject.GetComponent<T>();
    }

    protected override void Destroy(float t)
    {
      throw new NotImplementedException();
    }

    public virtual void Initialise() { }
  }
}
