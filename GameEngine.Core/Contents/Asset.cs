using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents
{
    public enum AssetType
    {
        Texture,
        SpriteSheet,
        Sound
    }
    public class Asset
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public AssetType Type { get; set; }

        public Asset(AssetType type, string name, string path)
        {
            Type = type;
            Name = name;
            Path = path;
        }
    }
}
