using System;
using Microsoft.Xna.Framework;
using Walker.WalkerEngine;

namespace Walker.WalkerGame
{
  class GridTransform : Behaviour
  {
    /// <summary>
    /// The 2-dimensional size of a single grid cell.
    /// </summary>
    public Vector2 gridCellSize { get; set; } = new Vector2(32.0f, 32.0f);

    float _invMovementSpeed = 1.0f;

    /// <summary>
    /// How fast the character moves the distance of one grid cell in seconds.
    /// </summary>
    public float movementDuration
    {
      get { return 1.0f / _invMovementSpeed; }
      set { _invMovementSpeed = 1.0f / value; }
    }

    Point _currentPos, _targetPos;

    float _moveTimer = 1.0f;
    float _moveDistanceModifier = 1.0f;

    public override void Initialise()
    {
      _targetPos = _currentPos = (transform.position / gridCellSize).ToPoint();
      transform.position = _targetPos.ToVector2() * gridCellSize;
    }

    void StartMove()
    {
      var diff = (_targetPos - _currentPos).ToVector2();
      _moveDistanceModifier = 1.0f / diff.Length();
      _moveTimer = 0.0f;
    }

    void FinishMove()
    {
      _moveTimer = 1.0f;
      _currentPos = _targetPos;
    }

    public bool IsAtTarget { get { return _currentPos == _targetPos; } }

    void UpdateGridPosition(Point position, bool warp)
    {
      _targetPos = position;
      if (warp)
      {
        FinishMove();
        transform.position = _targetPos.ToVector2() * gridCellSize;
      }
      else
      {
        StartMove();
      }
    }

    public void Move(Point movement, bool warp = false)
    {
      UpdateGridPosition(_targetPos + movement, warp);
    }

    public void SetGridPosition(Point gridPosition, bool warp = false)
    {
      UpdateGridPosition(gridPosition, warp);
    }

    public override void Update()
    {
      if (_moveTimer < 1.0f)
      {
        _moveTimer += (float)Engine.gameTime.ElapsedGameTime.TotalSeconds * _moveDistanceModifier * _invMovementSpeed;

        if (_moveTimer >= 1.0f)
        {
          transform.position = _targetPos.ToVector2() * gridCellSize;
          FinishMove();
        }
        else
        {
          transform.position = Vector2.Lerp(_currentPos.ToVector2() * gridCellSize, _targetPos.ToVector2() * gridCellSize, _moveTimer);
        }
      }
    }
  }
}
