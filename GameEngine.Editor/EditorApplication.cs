using GameEngine.Core;
using GameEngine.Core.Graphics;
using GameEngine.Core.Gui;
using GameEngine.Core.Scenes;
using GameEngine.Editor.EditorInterface;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Editor
{
    public class EditorApplication : Application
    {
        private Texture _renderWindowTexture;
        private EditorInterface.EditorInterface _interface;
        private EditorScene _scene;
        public EditorApplication(WindowSettings settings) : base(settings)
        {
            GuiImpl.Init(_window.GetRenderWindow());

            _scene = new EditorScene();

            _scenesStack.AddScene(_scene);

            _renderWindowTexture = new Texture(settings.VideoMode.Width, settings.VideoMode.Height);

            _interface = new EditorInterface.EditorInterface(_scene);
        }

        public override void Update(Time deltaTime)
        {
            GuiImpl.Update(_window.GetRenderWindow(), deltaTime);

            base.Update(deltaTime);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            _renderWindowTexture.Update(_window.GetRenderWindow());
            //_window.Clear(new Color(128, 128, 128));

            _interface.SceneTexture = (nint)_renderWindowTexture.NativeHandle;

            _interface.Draw();

            GuiImpl.Render();
        }

        public override Camera Resize(Vector2u size)
        {
            _renderWindowTexture = new Texture(size.X, size.Y);

            return base.Resize(size);
        }
    }
}
