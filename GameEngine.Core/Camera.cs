using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public struct Camera
    {
        public Guid Id { get; }

        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }
        public Vector2f Scale { get; set; }
        public float Rotation { get; set; } = 0;

        public Camera() { }

        public Camera(Vector2f position, Vector2f size)
        {
            Position = position;
            Size = size;
            Id = Guid.NewGuid();
        }
    }
}
