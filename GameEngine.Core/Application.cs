using GameEngine.Core.Graphics;
using GameEngine.Core.Layers;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public class Application : Drawable
    {
        private Window _window;
        private WindowSettings _windowSettings;

        private LayerStack _layerStack;

        public Application(WindowSettings settings, LayerStack layerStack)
        {
            _windowSettings = settings;
            _window = new Window(this, settings.VideoMode, settings.Title);

            _layerStack = layerStack;
        }

        public LayerStack GetLayerStack() => _layerStack;

        public void Run()
        {
            _layerStack.SetCamera(new Camera(_window.GetView()));
            _layerStack.Start();

            _window.Run();
        }

        public void Update(float deltaTime)
        {
            _layerStack.Update(deltaTime);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _layerStack.Draw(target, states);
        }

        public Camera Resize(Vector2u size)
        {
            return _layerStack.Resize(size);
        }

        public void Close()
        {
            _layerStack.Close();
        }
    }
}
