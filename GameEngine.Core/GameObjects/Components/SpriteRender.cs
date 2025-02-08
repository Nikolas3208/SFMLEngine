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
        private Color _color = Color.White;
        private bool _flipX = false;
        private bool _flipY = false;

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IGameObject Perent { get => _gameObject; set => _gameObject = value; }
        public string SpriteName { get; set; } = string.Empty;
        public Color Color { get => _color; set { _sprite.Color = value; _color = value; } }

        public bool FlipX
        {
            get => _flipX;
            set 
            { 
                _flipX = value;
                if (value)
                    _sprite.Scale = new Vector2f(-1, _sprite.Scale.Y);
                else
                    _sprite.Scale = new Vector2f(1, _sprite.Scale.Y);
            }
        }
        public bool FlipY
        {
            get => _flipY;
            set
            {
                _flipY = value;
                if (value)
                    _sprite.Scale = new Vector2f(_sprite.Scale.X, -1);
                else
                    _sprite.Scale = new Vector2f(_sprite.Scale.X, 1);
            }
        }

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

        public SpriteRender(Sprite sprite, IGameObject perent)
        {
            _sprite = sprite;

            Perent = perent;
            Id = Guid.NewGuid();
        }

        public SpriteRender(SpriteSheet spriteSheet, IGameObject perent)
        {
            _sprite = spriteSheet.GetSprite();
            _sprite.Origin = new Vector2f(spriteSheet.SubWidth, spriteSheet.SubHeight);

            Perent = perent;
            Id = Guid.NewGuid();
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

        public void UpdateSprite(Sprite? sprite)
        {
            if(sprite != null)
            {
                _sprite = sprite;
                _sprite.Color = _color;
            }
        }

        public Sprite GetSprite() => _sprite;
    }
}
