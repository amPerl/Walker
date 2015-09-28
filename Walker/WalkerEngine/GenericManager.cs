using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walker.WalkerEngine
{
  public abstract class GenericManager<T>
  {
    protected List<T> _items = new List<T>();

    public void AddItem(T item)
    {
      _items.Add(item);
    }

    public void RemoveItem(T item)
    {
      _items.Remove(item);
    }
  }
}
