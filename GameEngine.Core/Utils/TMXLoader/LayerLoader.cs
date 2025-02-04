using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class LayerLoader
    {
        public static List<Layer> Load(XElement map)
        {
            var retLayers = new List<Layer>();

            var layers = map.Elements("layer");

            foreach (XElement layer in layers)
            {
                var Id = layer.Attribute("id").Value;
                var name = layer.Attribute("name").Value;
                var data = layer.Element("data").Value;

                retLayers.Add(new Layer { Id = int.Parse(Id), Name = name, Data = data });
            }

            return retLayers;
        }
    }
}
