using System;

namespace Walker
{
#if WINDOWS || LINUX
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var game = new WalkerGame.WalkerGame())
      {
        game.Run();
      }
    }
  }
#endif
}
