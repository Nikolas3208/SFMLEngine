using GameEngine.Core.Graphics.Animations;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Utils.TMXLoader
{
    public class Tile : Transformable, Drawable
    {
        private bool _isAnimated = false;
        private AnimSprite animSprite;

        public int Id { get; set; }

        public bool IsAnimated
        {
            get => _isAnimated;
            set
            {
                _isAnimated = value;
            }
        }

        public Sprite Sprite { get; set; }

        public Tile(int id, Sprite sprite)
        {
            Id = id;
            Sprite = sprite;
        }

        public Tile(int id, Sprite sprite, AnimSprite animSprite) : this(id, sprite)
        {
            this.animSprite = animSprite;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if(animSprite == null)
                target.Draw(Sprite, states);
            else
            {
                animSprite.Play("tileSet");
                animSprite.Draw(target, states);
            }
        }
    }
}
