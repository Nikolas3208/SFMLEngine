using GameEngine.Core.GameObjects;
using GameEngine.Core.Graphics;
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

        public Cube(SceneBase perent) : base(perent)
        {
        }

        public override void Start()
        {
            _spriteSheet = new SpriteSheet(6, 8, true, 1, Perent.AssetsMenager.GetTexture("Warrior_Blue"));

            _rectangleShape = new RectangleShape(new Vector2f(192, 192));
            _rectangleShape.Texture = _spriteSheet.GetTexture();
            _rectangleShape.TextureRect = _spriteSheet.GetTextureRect(0, 0);
        }

        public override void Update(float deltaTime)
        {

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            _rectangleShape.Draw(target, states);
        }
    }
}
