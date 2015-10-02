using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TiledSharp;
using Walker.Shared.Map;

namespace tmxc
{
    class Program
    {
        static TmxMap _map;

        static void Main(string[] args)
        {
            if (args.Length < 2)
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

            Map walkerMap = new Map();
            foreach (var tileset in _map.Tilesets)
            {
                Tileset walkerTileset = new Tileset(tileset.Name, tileset.Image.Source)
                {
                    Margin = tileset.Margin,
                    Spacing = tileset.Spacing,
                    TileCount = (int)tileset.TileCount,
                    TileWidth = tileset.TileWidth,
                    TileHeight = tileset.TileHeight,
                    FirstGid = tileset.FirstGid
                };

                foreach (var tile in tileset.Tiles.Where(tile => tile.Properties.Count > 0))
                {
                    TilePropertyEntry entry = new TilePropertyEntry { Id = tile.Id };

                    foreach (var kvp in tile.Properties)
                        entry.Properties.Add(kvp.Key, kvp.Value);

                    walkerTileset.PropertyEntries.Add(entry);
                }

                walkerMap.Tilesets.Add(walkerTileset);
            }

            var dest = args[1] + ".map";
            using (var fs = new FileStream(dest, FileMode.Create))
            using (var writer = new BinaryWriter(fs))
            {
                walkerMap.Write(writer);
            }
        }
    }
}
