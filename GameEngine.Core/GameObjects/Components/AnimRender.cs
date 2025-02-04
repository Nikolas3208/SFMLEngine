using GameEngine.Core.Contents;
using GameEngine.Core.Graphics;
using GameEngine.Core.Graphics.Animations;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public class AnimRender : IComponent
    {
        private SpriteSheet _spriteSheet;
        private AnimSprite _animSprite;
        private string currentAnim = string.Empty;

        public Guid Id { get; set; }
        public string Name { get; set; } = nameof(AnimRender);
        public IGameObject Perent { get; set; }

        public AnimRender(IGameObject perent)
        {
            Id = Guid.NewGuid();
            Perent = perent;
        }

        public void Start()
        {
            if (Perent.GetComponent<SpriteRender>() != null && Perent.GetComponent<SpriteRender>().GetTexture() != null)
            {
                var asset = AssetsMenager.GetAsset<SpriteAsset>(Perent.GetComponent<SpriteRender>().TextureName);
                if (asset != null)
                {
                    if (asset.SpriteSheet != null)
                    {
                        _spriteSheet = asset.SpriteSheet;
                        _animSprite = new AnimSprite(_spriteSheet);
                        _animSprite.Origin = (Vector2f)_spriteSheet.GetTileSize() / 2;
                    }
                    else
                    {
                        _spriteSheet = new SpriteSheet(asset.GetTexture(), asset.IsSmooth);
                        _animSprite = new AnimSprite(_spriteSheet);
                        _animSprite.Origin = (Vector2f)_spriteSheet.GetTileSize() / 2;
                    }
                }
            }
        }

        public void Update(Time deltaTime)
        {
            if (_animSprite != null)
                _animSprite.Play(currentAnim);
        }

        public void Draw(RenderTarget target, RenderStates state)
        {
            if (_animSprite != null)
                _animSprite.Draw(target, state);
        }

        public void SetAnimPlay(string name) => currentAnim = name;
        public string GetAnimPlay() => currentAnim;

        public void SetTileSize(Vector2i size) => _spriteSheet.SetTileSize(size);
        public Vector2i GetTileSize() => _spriteSheet.GetTileSize();

        public void SetBorderSize(int borderSize) => _spriteSheet.SetBorderSize(borderSize);
        public int GetBorderSize() => _spriteSheet.GetBorderSize();

        public List<string> GetAnimationsName() => _animSprite != null ? _animSprite.GetAnimationsName() : new List<string>();

        public List<Animation> GetAnimations() => _animSprite != null ? _animSprite.GetAnimations() : new List<Animation>();

        public void AddAnimation(string name, Animation animation)
        {
            if (_animSprite != null)
                _animSprite.AddAnimation(name, animation);
        }

        public void UpdateTexture(string textureName)
        {
            var asset = AssetsMenager.GetAsset<SpriteAsset>(textureName);
            if (asset.SpriteSheet != null && _animSprite != null)
            {
                _spriteSheet = asset.SpriteSheet;
                _animSprite.UpdateSpriteSheet(_spriteSheet);
            }
            else if(asset.SpriteSheet == null && _animSprite != null)
            {
                _spriteSheet = new SpriteSheet(asset.GetTexture(), asset.IsSmooth);
                _animSprite.UpdateSpriteSheet(_spriteSheet);
            }
        }
    }
}
