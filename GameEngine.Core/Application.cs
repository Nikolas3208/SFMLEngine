using GameEngine.Core.Graphics;
using GameEngine.Core.Gui;
using GameEngine.Core.Scenes;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using System.Numerics;

namespace GameEngine.Core
{
    public class Application : Drawable
    {
        protected Window _window;
        protected WindowSettings _windowSettings;

        protected ScenesStack _scenesStack;

        public Application(WindowSettings settings)
        {
            _windowSettings = settings;
            _window = Window.Create(this, settings.VideoMode, settings.Title);
            _window.SetVerticalSyncEnabled(settings.VSinc);
            _window.SetFramerateLimit(settings.FramerateLimit);

            _scenesStack = new ScenesStack("Stack");
        }

        public ScenesStack GetScenesStack() => _scenesStack;

        public virtual void Run()
        {
            _scenesStack.SetCamera(new Camera(_window.GetView()));
            _scenesStack.Start();

            _window.Run();
        }

        public virtual void Update(Time deltaTime)
        {
            _scenesStack.Update(deltaTime);
        }

        

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            _scenesStack.Draw(target, states);
        }

        public virtual Camera Resize(Vector2u size)
        {
            return _scenesStack.GetCamera().Resize((Vector2f)size);
        }

        public virtual void Close()
        {
            _scenesStack.Close();
        }
    }
}
