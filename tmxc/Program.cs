using System;
using TiledSharp;

namespace tmxc
{
  class Program
  {
    static void Main(string[] args)
    {
      var map = new TmxMap(args[0]);

      Console.WriteLine($"Loaded map {args[0]}");
      Console.WriteLine($"Version: {map.Version}");
      Console.WriteLine($"Background colour: {map.BackgroundColor.R}, {map.BackgroundColor.G}, {map.BackgroundColor.B}");
      Console.WriteLine($"Size: {map.Width}, {map.Height}");
      Console.WriteLine($"Tile size: {map.TileWidth}, {map.TileHeight}");
      Console.WriteLine();

      foreach(var tileset in map.Tilesets)
      {
        Console.WriteLine($"Tileset: {tileset.Name}");
        Console.WriteLine($"\tPath: {tileset.Image.Source}");
        Console.WriteLine();
      }

      foreach(var layer in map.Layers)
      {
        Console.WriteLine($"Layer: {layer.Name}");
        Console.WriteLine();
      }
    }
  }
}
