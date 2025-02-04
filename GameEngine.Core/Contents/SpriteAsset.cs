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
    public class SpriteAsset : Asset
    {
        private Sprite? _sprite;
        private SpriteSheet? _spriteSheet;

        private bool _isMultiplaySprite = false;
        private bool _isSmooth = false;

        public Sprite? Sprite {  get { return _sprite; } }
        public SpriteSheet? SpriteSheet { get { return _spriteSheet; } }

        public bool IsMultiplaySprite
        {
            get => _isMultiplaySprite;
            set 
            {
                _isMultiplaySprite = value;
                if(value && _sprite != null)
                {
                    _spriteSheet = new SpriteSheet(_sprite.Texture, IsSmooth);
                }
                else if(!value && _spriteSheet != null)
                {
                    _sprite = new Sprite(_spriteSheet.GetTexture());
                    _sprite.Origin = (Vector2f)_sprite.Texture.Size / 2;
                }
            }
        }
        public bool IsSmooth { get => _isSmooth; set => _isSmooth = value; }

        public SpriteAsset(Texture texture, bool multiSprite = false, bool isSmooth = false)
        {
            IsMultiplaySprite = multiSprite;
            IsSmooth = isSmooth;

            if (!multiSprite)
            {
                texture.Smooth = isSmooth;
                _sprite = new Sprite(texture);
                _sprite.Origin = (Vector2f)texture.Size / 2;
            }
            else
            {
                _spriteSheet = new SpriteSheet(texture, isSmooth);
            }
        }

        public SpriteAsset(Sprite sprite)
        {
            _sprite = sprite;
        }

        public SpriteAsset(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
        }

        public Texture? GetTexture()
        {
            if (IsMultiplaySprite && _spriteSheet != null)
                return _spriteSheet.GetTexture();
            else if (!IsMultiplaySprite && _sprite != null)
                return _sprite.Texture;

            return null;
        }
    }
}