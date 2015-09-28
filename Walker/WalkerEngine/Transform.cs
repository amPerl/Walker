using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Walker.WalkerEngine
{
  public class Transform : Component
  {
    List<Transform> _children = new List<Transform>();

    /// <summary>
    /// The number of children the transform has.
    /// </summary>
    public int childCount { get { return _children.Count; } }

    Transform _parent;

    /// <summary>
    /// The parent of the transform.
    /// </summary>
    public Transform parent
    {
      get { return _parent; }
      set
      {
        _parent = value;
        
        if (_parent != null)
        {
          localPosition = position - _parent.position;
          localRotation = rotation - _parent.rotation;
          localScale = scale - _parent.scale;
        }
      }
    }

    /// <summary>
    /// Return the topmost transform in the hierarchy.
    /// </summary>
    public Transform root
    {
      get
      {
        var t = this;
        while (t.parent != null) t = t.parent;
        return t;
      }
    }

    /// <summary>
    /// Position of the transform relative to the parent transform.
    /// </summary>
    public Vector2 localPosition { get; set; } = Vector2.Zero;

    /// <summary>
    /// Depth of the transform relative to the parent transform.
    /// </summary>
    public float localDepth { get; set; } = 0.0f;

    /// <summary>
    /// The rotation of the transform relative to the parent transform's rotation.
    /// </summary>
    public float localRotation { get; set; } = 0.0f;

    /// <summary>
    /// The scale of the transform relative to the parent.
    /// </summary>
    public Vector2 localScale { get; set; } = Vector2.One;

    /// <summary>
    /// The position of the transform in world space.
    /// </summary>
    public Vector2 position
    {
      get
      {
        if (_parent == null) return localPosition;
        else return localPosition + _parent.position;
      }
      set
      {
        if (_parent == null) localPosition = value;
        else localPosition = value - _parent.position;
      }
    }

    /// <summary>
    /// The depth of the transform in world space.
    /// </summary>
    public float depth
    {
      get
      {
        if (_parent == null) return localDepth;
        else return localDepth + _parent.depth;
      }
      set
      {
        if (_parent == null) localDepth = value;
        else localDepth = value - _parent.depth;
      }
    }

    /// <summary>
    /// The rotation of the transform in world space.
    /// </summary>
    public float rotation
    {
      get
      {
        if (_parent == null) return localRotation;
        else return localRotation + _parent.rotation;
      }
      set
      {
        if (_parent == null) localRotation = value;
        else localRotation = value - _parent.rotation;
      }
    }

    /// <summary>
    /// The scale of the transform in world space.
    /// </summary>
    public Vector2 scale
    {
      get
      {
        if (_parent == null) return localScale;
        else return localScale + _parent.scale;
      }
      set
      {
        if (_parent == null) localScale = value;
        else localScale = value - _parent.scale;
      }
    }
    
    /// <summary>
    /// The up direction of the transform in world space.
    /// </summary>
    public Vector2 up
    {
      get
      {
        var v = right;
        return new Vector2(-v.Y, v.X);
      }
    }

    /// <summary>
    /// The right direction of the transform in world space.
    /// </summary>
    public Vector2 right
    {
      get
      {
        return new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
      }
    }

    /// <summary>
    /// Returns a transform child by index.
    /// </summary>
    /// <param name="index">Index of the child transform to return. Must be smaller than Transform.childCount.</param>
    /// <returns>Transform child by index.</returns>
    public Transform GetChild(int index)
    {
      if (index < 0 || index > childCount) throw new IndexOutOfRangeException();
      return _children[index];
    }

    /// <summary>
    /// Rotates the transform so the forward vector points at target's current position.
    /// </summary>
    /// <param name="target">Object to point towards.</param>
    public void LookAt(Transform target)
    {
      var diff = target.position - position;
      rotation = (float)Math.Atan2(diff.Y, diff.X);
    }
  }
}