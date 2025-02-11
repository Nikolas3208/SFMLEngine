using GameEngine.Core.Contents.Assets;
using GameEngine.Core.Contents;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Core.Utils.TMXLoader.Loaders
{
    public class TmxLoader
    {
        public static Map Load(string filePath)
        {
            XDocument xDocument = XDocument.Load(filePath);

            var map = xDocument.Element("map");

            var tileSets = TileSetLoader.Load(map!);

            var layers = MapLayerLoader.Load(map!);

            var mapObjects = MapObjectGroupsLoader.Load(map!);

            var Map = new Map(tileSets, layers, mapObjects);
            Map.Width = int.Parse(map.Attribute("width")!.Value);
            Map.Height = int.Parse(map.Attribute("height")!.Value);
            Map.TileWidth = int.Parse(map.Attribute("tilewidth")!.Value);
            Map.TileHeight = int.Parse(map.Attribute("tileheight")!.Value);

            return Map;
        }
    }
}
