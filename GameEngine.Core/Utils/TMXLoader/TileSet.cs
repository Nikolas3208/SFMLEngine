using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using GameEngine.Core.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class TileSet
    {
        public int FirstId { get; set; }
        public int LastId { get; set; }
        public string Name { get; set; }
        public int TileWidth {  get; set; }
        public int TileHeight { get; set; }

        public TileSetImage Image { get; set; }

        public SpriteSheet SpriteSheet { get; set; }

        public TileSet(int firstId, string name, int tileWidth, int tileHeight, TileSetImage image)
        {
            FirstId = firstId;
            Name = name;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Image = image;

            LastId = FirstId + ((Image.Width / TileWidth) * (Image.Height / TileHeight)) - 1;

            LoadSpriteSheet(image);
        }

        private void LoadSpriteSheet(TileSetImage image)
        {
            string name = Path.GetFileName(image.FilePath);
            name = name.Remove(name.IndexOf('.'));

            var assets = AssetsMenager.GetAsset<ImageAsset>(name);
            assets!.IsMultiplaySprite = true;

            assets.SpriteSheet!.SetTileSize(new Vector2i(TileWidth, TileHeight));

            SpriteSheet = assets.SpriteSheet;
        }

        public bool ContainsTile(int tileId)
        {
            return (tileId >= FirstId && tileId <= LastId);
        }
    }
}
