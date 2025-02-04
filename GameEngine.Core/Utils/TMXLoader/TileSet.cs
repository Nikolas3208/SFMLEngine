using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class TileSet
    {
        public int FirstGid;
        public string Source = string.Empty;

        public string Name;
        public int TileWidth;
        public int TileHeight;

        public int TileCount;
        public int Columns;

        public string ImageSource;
        public int ImageWidth;
        public int ImageHeight; 
    }
}
