using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class TileSetLoader
    {
        public static List<TileSet> Load(XElement map)
        {
            var retTileSets = new List<TileSet>();

            var tileSets = map.Elements("tileset");

            foreach (var tileSet in tileSets)
            {
                var firstgid = tileSet.Attribute("firstgid").Value;
                var source = tileSet.Attribute("source").Value;

                var tileset = LoadTSX(source);
                tileset.FirstGid = int.Parse(firstgid);

                retTileSets.Add(tileset);

            }

            return retTileSets;
        }

        private static TileSet LoadTSX(string source)
        {
            XDocument xDocument = XDocument.Load("Assets/Levels/" + source);

            var tileSet = xDocument.Element("tileset");
            var name = tileSet.Attribute("name").Value;
            var tilewidth = tileSet.Attribute("tilewidth").Value;
            var tileheight = tileSet.Attribute("tileheight").Value;
            var tilecount = tileSet.Attribute("tilecount").Value;
            var columns = tileSet.Attribute("columns").Value;

            var image = tileSet.Element("image");
            var sourceImage = image.Attribute("source").Value;
            var width = image.Attribute("width").Value;
            var height = image.Attribute("height").Value;

            return new TileSet { Name = name, Source = source, TileWidth = int.Parse(tilewidth), TileHeight = int.Parse(tileheight), TileCount = int.Parse(tilecount), Columns = int.Parse(columns), ImageSource = sourceImage, ImageWidth = int.Parse(width), ImageHeight = int.Parse(height) };
        }
    }
}
 