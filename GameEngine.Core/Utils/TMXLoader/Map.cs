using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class Map
    {
        public List<TileSet> TileSets;
        public List<Layer> Layers;

        public int TileWidth;
        public int TileHeight;

        public Map(string filePath)
        {
            TileSets = new List<TileSet>();
            Layers = new List<Layer>();

            XDocument xDocument = XDocument.Load(filePath);

            var map = xDocument.Element("map");

            var tileSet = TileSetLoader.Load(map);

            var layers = LayerLoader.Load(map);
        }
    }
}
