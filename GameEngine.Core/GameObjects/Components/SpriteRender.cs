using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public class SpriteRender : IComponent
    {
        private Guid _id;
        private string _name = nameof(SpriteRender);
        private IGameObject _gameObject;
        private Sprite _sprite;
        private bool _draw = true;

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IGameObject Perent { get => _gameObject; set => _gameObject = value; }
        public string TextureName { get; set; }

        public SpriteRender(IGameObject perent)
        {
            _sprite = new Sprite();

            Perent = perent;
            Id = Guid.NewGuid();
        }

        public SpriteRender(Texture texture, IGameObject perent)
        {
            _sprite = new Sprite(texture);
            _sprite.Origin = (Vector2f)texture.Size / 2;

            Perent = perent;
            Id = Guid.NewGuid();
        }

        public SpriteRender(SpriteSheet spriteSheet, IGameObject perent)
        {
            _sprite = new Sprite(spriteSheet.GetTexture(), spriteSheet.GetRectangle());
            _sprite.Origin = new Vector2f(spriteSheet.SubWidth, spriteSheet.SubHeight);

            Perent = perent;
            Id = Guid.NewGuid();
        }

        public Texture GetTexture() => _sprite.Texture;
        public void UpdateTexture(Texture texture, string name)
        {
            TextureName = name;
            _sprite = new Sprite(texture);
            _sprite.Origin = (Vector2f)texture.Size / 2;

            if(Perent.GetComponent<AnimRender>() != null)
            {
                Perent.GetComponent<AnimRender>().UpdateTexture(TextureName);
            }
        }
        public IntRect GetTextureRect() => _sprite.TextureRect;
        public void UpdateTextureRect(IntRect textureRect)
        {
            _sprite.TextureRect = textureRect;
        }
        
        public void Start()
        {
            
        }

        public void Update(Time deltaTime)
        {
            if (Perent.GetComponent<AnimRender>() != null)
                _draw = false;
            else
                _draw = true;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_draw)
                _sprite.Draw(target, states);
        }
    }
}
