namespace Walker.WalkerEngine
{
  public abstract class Behaviour : Component
  {
    protected Behaviour()
    {
      Engine.behaviourManager.AddItem(this);
    }

    public abstract void Update();

    protected override void Destroy(float t)
    {
      Engine.behaviourManager.RemoveItem(this);
      base.Destroy(t);
    }
  }
}
