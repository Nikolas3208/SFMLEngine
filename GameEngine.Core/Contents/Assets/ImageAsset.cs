using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents.Assets
{
    public class ImageAsset : Asset
    {
        private Texture? _texture;
        private Sprite? _sprite;
        private SpriteSheet? _spriteSheet;

        private bool _isSprite = false;
        private bool _isMultiplaySprite = false;
        private bool _isSmooth = false;

        public Texture? Texture { get { return _texture; } }
        public Sprite? Sprite { get { return _sprite; } }
        public SpriteSheet? SpriteSheet { get { return _spriteSheet; } }

        public bool IsSprite
        {
            get => _isSprite;
            set
            {
                _isSprite = value;
                if (value)
                {
                    Type = AssetType.Sprite;
                    _sprite = new Sprite(_texture);
                    _sprite.Origin = (Vector2f)_texture!.Size / 2;
                }
                else
                {
                    _sprite = null;
                }
            }
        }
        public bool IsMultiplaySprite
        {
            get => _isMultiplaySprite;
            set
            {
                _isMultiplaySprite = value;

                if(value)
                {
                    _isSprite = true;
                    if(_sprite == null)
                        _sprite = new Sprite(_texture);
                    _spriteSheet = new SpriteSheet(_sprite, _isSmooth);
                    _spriteSheet.Name = Name;
                }
                else
                {
                    _sprite = null;
                    _spriteSheet = null;
                }
            }
        }
        public bool IsSmooth
        {
            get => _isSmooth;
            set { _isSmooth = value; _texture!.Smooth = value; }
        }

        public ImageAsset(Texture texture, string name)
        {
            _texture = texture;
            Name = name;
            Type = AssetType.Image;
        }

        public void SetTileSize(Vector2i size)
        {
            if(IsMultiplaySprite)
            {
                SpriteSheet!.SetTileSize(size);
            }
        }
    }
}