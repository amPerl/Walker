using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Walker.Shared.Map
{
  static class BinaryExtensions
  {
    public static void WriteFixedString(this BinaryWriter writer, string str)
    {
      var strBuffer = Encoding.UTF8.GetBytes(str);
      writer.Write((ushort)strBuffer.Length);
      writer.Write(strBuffer);
    }

    public static string ReadFixedString(this BinaryReader reader)
    {
      var bufferLength = reader.ReadUInt16();
      var strBuffer = reader.ReadBytes(bufferLength);
      return Encoding.UTF8.GetString(strBuffer);
    }
  }

  public class Map
  {
    public List<Tileset> Tilesets;

    public Map()
    {
      Tilesets = new List<Tileset>();
    }

    public static Map Read(BinaryReader reader)
    {
      var map = new Map();

      int tilesetCount = reader.ReadInt32();

      for (int i = 0; i < tilesetCount; i++)
        map.Tilesets.Add(Tileset.Read(reader));

      return map;
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(Tilesets.Count);

      foreach (var tileset in Tilesets)
        tileset.Write(writer);
    }
  }

  public class Tileset
  {
    public string Name, ImagePath;
    public int Margin, Spacing, TileCount, TileWidth, TileHeight, FirstGid;
    public List<TilePropertyEntry> PropertyEntries;

    public Tileset(string name, string imagePath)
    {
      Name = name;
      ImagePath = imagePath;
      PropertyEntries = new List<TilePropertyEntry>();
    }

    public static Tileset Read(BinaryReader reader)
    {
      var name = reader.ReadFixedString();
      var imagePath = reader.ReadFixedString();

      var tileset = new Tileset(name, imagePath)
      {
        Margin = reader.ReadInt32(),
        Spacing = reader.ReadInt32(),
        TileCount = reader.ReadInt32(),
        TileWidth = reader.ReadInt32(),
        TileHeight = reader.ReadInt32(),
        FirstGid = reader.ReadInt32()
      };

      var propertyCount = reader.ReadInt32();

      for (var i = 0; i < propertyCount; i++)
        tileset.PropertyEntries.Add(TilePropertyEntry.Read(reader));

      return tileset;
    }

    public void Write(BinaryWriter writer)
    {
      writer.WriteFixedString(Name.ToLower());
      writer.WriteFixedString(ImagePath.ToLower());
      writer.Write(Margin);
      writer.Write(Spacing);
      writer.Write(TileCount);
      writer.Write(TileWidth);
      writer.Write(TileHeight);
      writer.Write(FirstGid);

      writer.Write(PropertyEntries.Count);

      foreach (var entry in PropertyEntries)
        entry.Write(writer);
    }
  }

  public class TilePropertyEntry
  {
    public int Id;
    public Dictionary<string, string> Properties;

    public TilePropertyEntry()
    {
      Properties = new Dictionary<string, string>();
    }

    public static TilePropertyEntry Read(BinaryReader reader)
    {
      var tile = new TilePropertyEntry
      {
        Id = reader.ReadInt32()
      };

      var propertyCount = reader.ReadInt32();
      for (var i = 0; i < propertyCount; i++)
      {
        var key = reader.ReadFixedString();
        var value = reader.ReadFixedString();
        tile.Properties.Add(key, value);
      }

      return tile;
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(Id);
      writer.Write(Properties.Count);

      foreach (var property in Properties)
      {
        writer.WriteFixedString(property.Key.ToLower());
        writer.WriteFixedString(property.Value.ToLower());
      }
    }
  }
}
