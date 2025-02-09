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
                int firstgid = int.Parse(tileSet.Attribute("firstgid")!.Value);

                if (tileSet.Element("image") != null)
                {
                    var tileset = LoadTSX(tileSet.Element("image")!.Attribute("source")!.Value, firstgid);

                    retTileSets.Add(tileset);
                }
                else
                {
                    var source = tileSet.Attribute("source")!.Value;

                    var tileset = LoadTSX(source, firstgid);

                    retTileSets.Add(tileset);
                }
            }

            return retTileSets;
        }

        private static TileSet LoadTSX(string source, int firstId)
        {
            XDocument xDocument = XDocument.Load(source);

            var tileSet = xDocument.Element("tileset");

            var name = tileSet!.Attribute("name")!.Value;
            int tileWidth = int.Parse(tileSet.Attribute("tilewidth")!.Value);
            int tileHeight = int.Parse(tileSet.Attribute("tileheight")!.Value);

            var image = tileSet.Element("image");

            var sourceImage = image!.Attribute("source")!.Value;
            int width = int.Parse(image.Attribute("width")!.Value);
            int height = int.Parse(image.Attribute("height")!.Value);

            return new TileSet(firstId, name, tileWidth, tileHeight, new TileSetImage(sourceImage, width, height));
        }
    }
}
 