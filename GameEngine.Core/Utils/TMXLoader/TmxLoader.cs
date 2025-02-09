using GameEngine.Core.Contents.Assets;
using GameEngine.Core.Contents;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class TmxLoader
    {
        public static Map Load(string filePath)
        {
            XDocument xDocument = XDocument.Load(filePath);

            var map = xDocument.Element("map");

            var tileSets = TileSetLoader.Load(map!);

            var layers = MapLayerLoader.Load(map!);

            return new Map(tileSets, layers);
        }
    }
}
