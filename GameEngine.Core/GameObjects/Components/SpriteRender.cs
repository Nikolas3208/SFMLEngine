using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public class SpriteRender : IComponent
    {

        private string _name = nameof(SpriteRender);
        private IGameObject _gameObject;
        private Sprite _sprite;
        private bool _draw = true;
        private Color _color = Color.White;
        private bool _flipX = false;
        private bool _flipY = false;

        [JsonInclude]
        public Guid Id { get; }
        [JsonInclude]
        public string Name { get => _name; set => _name = value; }
        [JsonInclude]
        public IGameObject Perent { get => _gameObject; set => _gameObject = value; }
        [JsonInclude]
        public string SpriteName { get; set; } = string.Empty;
        [JsonInclude]
        public Color Color { get => _color; set { _sprite.Color = value; _color = value; } }

        [JsonInclude]
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
        [JsonInclude]
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

        public SpriteRender()
        {
            _sprite = new Sprite();

            Id = Guid.NewGuid();
        }

        public SpriteRender(Texture texture)
        {
            _sprite = new Sprite(texture);
            _sprite.Origin = (Vector2f)texture.Size / 2;

            Id = Guid.NewGuid();
        }

        public SpriteRender(Sprite sprite)
        {
            _sprite = sprite;

            Id = Guid.NewGuid();
        }

        public SpriteRender(SpriteSheet spriteSheet)
        {
            _sprite = spriteSheet.GetSprite();
            _sprite.Origin = new Vector2f(spriteSheet.SubWidth, spriteSheet.SubHeight);

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

        public FloatRect? GetFloatRect()
        {
            if (_sprite != null && _sprite.Texture != null)
                return new FloatRect(-_sprite.Origin, new Vector2f(_sprite.Texture.Size.X, _sprite.Texture.Size.Y));

            return null;
        }

        public Vector2f GetSize()
        {
            if (_sprite != null && _sprite.Texture != null)
                return (Vector2f)_sprite.Texture.Size;

            return new Vector2f();
        }

        public Vector2f GetOrigin() => _sprite.Origin;
    }
}
