using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Graphics
{
    public class Camera
    {
        private View _view;

        private Vector2f _position;
        private Vector2f _size;

        public Camera(View view) 
        {
            _position = view.Center;
            _size = view.Size;

            _view = view;
        }

        public Camera(Vector2f position, Vector2f size)
        {
            _position = position;
            _size = size;

            _view = new View(new FloatRect(position, size));
        }

        public View GetView() => _view;
        public Camera Resize(Vector2f size)
        {
            _size = size;
            _view.Size = size;

            return this;
        }

        public Camera MoveToPosition(Vector2f position)
        {
            _position = position;
            _view.Center = position;

            return this;
        }

        public Camera Move(Vector2f offset)
        {
            _view.Move(offset);

            return this;
        }
    }
}
