using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using SFML.System;
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
        public List<MapLayer> MapLayers;

        public int TileWidth;
        public int TileHeight;

        public Map(List<TileSet> tileSets, List<MapLayer> mapLayers)
        {
            TileSets = tileSets;
            MapLayers = mapLayers;
        }
    }
}
