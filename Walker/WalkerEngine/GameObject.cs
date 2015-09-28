using System;
using System.Collections.Generic;

namespace Walker.WalkerEngine
{
  public class GameObject : Object
  {
    /// <summary>
    /// The layer the game object is in. A layer is in the range [0...31].
    /// </summary>
    public int layer { get; set; }

    /// <summary>
    /// The tag of the game object.
    /// </summary>
    public string tag { get; set; }

    /// <summary>
    /// The Transform attached to the game object. (null if there is none attached).
    /// </summary>
    public Transform transform { get; set; }

    Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

    bool _isInitialised = false;

    public GameObject(string name)
    {
      this.name = name;
      transform = AddComponent<Transform>();
    }

    /// <summary>
    /// Adds a component of a given type to the game object.
    /// </summary>
    /// <typeparam name="T">The type of component to add.</typeparam>
    /// <returns>The component added to the game object.</returns>
    public T AddComponent<T>() where T : Component, new()
    {
      var component = Activator.CreateInstance<T>();
      AddComponent(typeof(T), component);
      return component;
    }

    /// <summary>
    /// Adds a component class of type componentType to the game object.
    /// </summary>
    /// <param name="componentType">The type of the component to add.</param>
    /// <returns>The component added to the game object.</returns>
    public Component AddComponent(Type componentType)
    {
      var component = (Component)Activator.CreateInstance(componentType);
      AddComponent(componentType, component);
      return component;
    }

    void AddComponent(Type componentType, Component component)
    {
      component.gameObject = this;
      component.name = name;
      _components.Add(componentType, component);
      if(_isInitialised) component.Initialise();
    }

    /// <summary>
    /// Gets a component from the game object.
    /// </summary>
    /// <typeparam name="T">The type of component to get.</typeparam>
    /// <returns>The component from the game object.</returns>
    public T GetComponent<T>() where T : Component
    {
      var c = _components[typeof(T)];
      return (T)c;
    }

    /// <summary>
    /// Initialises all added components. Components added after calling this function will be initialised as soon as they are added to the object.
    /// </summary>
    public void Initialise()
    {
      if (_isInitialised) return;
      foreach (var component in _components) component.Value.Initialise();
      _isInitialised = true;
    }

    protected override void Destroy(float t)
    {
      throw new NotImplementedException();
    }
  }
}
