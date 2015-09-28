using System;
using System.Collections.Generic;

namespace Walker.WalkerEngine
{
  public class BehaviourManager : GenericManager<Behaviour>
  {
    public void Update()
    {
      foreach (var behaviour in _items) behaviour.Update();
    }
  }
}
