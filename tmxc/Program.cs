using System;
using System.IO;
using TiledSharp;

namespace tmxc
{
  class Program
  {
    static TmxMap _map;

    static void Main(string[] args)
    {
      if(args.Length < 2)
      {
        Console.WriteLine("Usage: tmxc source dest");
        return;
      }

      try
      {
        _map = new TmxMap(args[0]);
      }
      catch
      {
        Console.WriteLine($"Failed to open map file '{args[0]}'");
        return;
      }

      var dest = args[1] + ".map";
      using (var fs = new FileStream(dest, FileMode.Create))
      {
        using (var writer = new BinaryWriter(fs))
        {
          Console.WriteLine($"Writing {_map.Tilesets.Count} tilesets...");
          writer.Write(_map.Tilesets.Count);  // int

          foreach(var tileset in _map.Tilesets)
          {
            writer.Write(tileset.Name.ToLower());         // string
            writer.Write(tileset.Image.Source.ToLower()); // string
            writer.Write(tileset.Margin);                 // int
            writer.Write(tileset.Spacing);                // int
            writer.Write((int)tileset.TileCount);         // int
            writer.Write(tileset.TileWidth);              // int
            writer.Write(tileset.TileHeight);             // int
            writer.Write(tileset.FirstGid);               // int

            // Look for properties in the tiles.
            var tilePropertyCount = 0;
            foreach(var tile in tileset.Tiles)
            {
              if (tile.Properties.Count > 0) tilePropertyCount++;
            }

            Console.WriteLine($"Tileset {tileset.Name} has {tilePropertyCount} properties.");
            writer.Write(tilePropertyCount);      // int
            foreach(var tile in tileset.Tiles)
            {
              if (tile.Properties.Count == 0) continue;

              Console.WriteLine($"Writing {tile.Properties.Count} properties for tile {tile.Id} in tileset {tileset.Name}.");
              writer.Write(tile.Id);                // int
              writer.Write(tile.Properties.Count);  // int
              foreach(var property in tile.Properties)
              {
                writer.Write(property.Key.ToLower());   // string
                writer.Write(property.Value.ToLower()); // string
              }
            }
          }
        }
      }
    }
  }
}
