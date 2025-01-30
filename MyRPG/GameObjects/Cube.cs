using GameEngine.Core.GameObjects;
using SFML.Graphics;
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
        public override void Start()
        {
            _rectangleShape = new RectangleShape(new SFML.System.Vector2f(10, 10));
            _rectangleShape.Position = new SFML.System.Vector2f(5, 0);
            _rectangleShape.FillColor = Color.White;
        }

        public override void Update(float deltaTime)
        {
            _rectangleShape.Position = new SFML.System.Vector2f(5, _rectangleShape.Position.Y + 0.004f);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            _rectangleShape.Draw(target, states);
        }
    }
}
