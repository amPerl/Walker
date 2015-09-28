using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Walker.WalkerEngine;

namespace Walker.WalkerGame
{
  class GridControls : Behaviour
  {
    GridTransform _gridTransform;

    public override void Initialise()
    {
      _gridTransform = GetComponent<GridTransform>();
    }

    int GetKey(KeyboardState keyState, Keys key)
    {
      return keyState.IsKeyDown(key) ? 1 : 0;
    }

    public override void Update()
    {
      // Wait for any existing motion to be finished.
      if (!_gridTransform.IsAtTarget) return;

      var keyState = Keyboard.GetState();
      Point moveInputX = new Point
      {
        X = GetKey(keyState, Keys.Right) - GetKey(keyState, Keys.Left),
        Y = 0
      };

      Point moveInputY = new Point
      {
        X = 0,
        Y = GetKey(keyState, Keys.Down) - GetKey(keyState, Keys.Up)
      };

      // Move in two phases to form a zig-zag motion and prevent diagonal movement.
      if (moveInputX.X != 0) _gridTransform.Move(moveInputX);
      if (moveInputY.Y != 0) _gridTransform.Move(moveInputY);
    }
  }
}
