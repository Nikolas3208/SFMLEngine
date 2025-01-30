using GameEngine.Core.Graphics;
using GameEngine.Core.Scenes;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core
{
    public class Application : Drawable
    {
        private Window _window;
        private WindowSettings _windowSettings;

        private ScenesStack _scenesStack;

        public Application(WindowSettings settings, ScenesStack scenesStack)
        {
            _windowSettings = settings;
            _window = new Window(this, settings.VideoMode, settings.Title);

            _scenesStack = scenesStack;
        }

        public ScenesStack GetScenesStack() => _scenesStack;

        public void Run()
        {
            _scenesStack.SetCamera(new Camera(_window.GetView()));
            _scenesStack.Start();

            _window.Run();
        }

        public void Update(float deltaTime)
        {
            _scenesStack.Update(deltaTime);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _scenesStack.Draw(target, states);
        }

        public Camera Resize(Vector2u size)
        {
            return _scenesStack.GetCamera().Resize((Vector2f)size);
        }

        public void Close()
        {
            _scenesStack.Close();
        }
    }
}
