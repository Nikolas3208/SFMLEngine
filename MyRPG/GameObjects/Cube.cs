using GameEngine.Core.GameObjects;
using GameEngine.Core.Graphics;
using GameEngine.Core.Graphics.Animations;
using GameEngine.Core.Scenes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRPG.GameObjects
{
    public class Cube : GameObject
    {
        private RectangleShape _rectangleShape;
        private SpriteSheet _spriteSheet;
        private AnimSprite anim;

        public Cube(SceneBase perent) : base(perent)
        {
        }

        public override void Start()
        {
            _spriteSheet = new SpriteSheet(192, 192, false, 1, Perent.Perent.AssetsMenager.GetTexture("Torch_Blue"));
            anim = new AnimSprite(_spriteSheet);
            anim.AddAnimation("idel", new Animation(
                new AnimationFrame(0, 2, 2.8f),
                new AnimationFrame(1, 2, 2.8f),
                new AnimationFrame(2, 2, 2.8f),
                new AnimationFrame(3, 2, 2.8f),
                new AnimationFrame(4, 2, 2.8f),
                new AnimationFrame(5, 2, 2.8f)));

            _rectangleShape = new RectangleShape(new Vector2f(192, 192));
            _rectangleShape.Texture = _spriteSheet.GetTexture();
            _rectangleShape.TextureRect = _spriteSheet.GetTextureRect(0, 0);
        }

        public override void Update(float deltaTime)
        {
            anim.Play("idel");
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            anim.Draw(target, states);
        }
    }
}
