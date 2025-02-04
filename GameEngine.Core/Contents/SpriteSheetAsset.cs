using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents
{
    public class SpriteSheetAsset : Asset
    {
        private SpriteSheet _spriteSheet;

        public SpriteSheetAsset(AssetType type, string name, string path) : base(type, name, path)
        {
            _spriteSheet = new SpriteSheet();
        }

        public SpriteSheetAsset(AssetType type, Texture texture, string name, string path) : base(type, name, path)
        {
            _spriteSheet = new SpriteSheet(32, 32, false, 0, texture);
        }

        public SpriteSheetAsset(AssetType type, SpriteSheet spriteSheet, string name, string path) : base(type, name, path)
        {
            _spriteSheet = spriteSheet;
        }

        public void SetBorderSize(int borderSize) => _spriteSheet.SetBorderSize(borderSize);

        public int GetBorderSize() => _spriteSheet.GetBorderSize();
        public void SetTileSize(Vector2i size) => _spriteSheet.SetTileSize(size);
        public Vector2i GetTileSize() => _spriteSheet.GetTileSize();
    }
}
