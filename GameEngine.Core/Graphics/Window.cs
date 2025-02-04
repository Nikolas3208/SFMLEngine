using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine.Core.Graphics
{
    public class Window
    {
        private Application _application;
        private RenderWindow _renderWindow;
        private Clock _clock;

        private Window(Application application, RenderWindow renderWindow)
        {
            _application = application;

            _renderWindow = renderWindow;
            _renderWindow.Closed += Closed;
            _renderWindow.Resized += Resized;

            _renderWindow.SetView(new View(new FloatRect(new Vector2f(-_renderWindow.Size.X / 2, -_renderWindow.Size.Y / 2), (Vector2f)_renderWindow.Size)));

            _clock = new Clock();
        }

        public static Window Create(Application application, RenderWindow renderWindow) => new Window(application, renderWindow);
        public static Window Create(Application application, VideoMode videoMode, string title) => new Window(application, new RenderWindow(videoMode, title));

        private void Closed(object? sender, EventArgs e)
        {
            _application.Close();
            _renderWindow.Close();
        }

        private void Resized(object? sender, SizeEventArgs e)
        {
            _renderWindow.SetView(_application.Resize(new Vector2u(e.Width, e.Height)).GetView());
        }

        public void Run()
        {
            while(_renderWindow.IsOpen)
            {
                _renderWindow.DispatchEvents();

                _application.Update(_clock.Restart());

                if (_application.GetScenesStack().GetCamera() != null)
                    _renderWindow.SetView(_application.GetScenesStack().GetCamera().GetView() ?? _renderWindow.GetView());

                _renderWindow.Clear();

                _renderWindow.Draw(_application);

                _renderWindow.Display();
            }
        }

        public Clock GetClock() => _clock;

        public View GetView()
        {
            return _renderWindow.GetView();
        }

        public RenderWindow GetRenderWindow()
        {
            return _renderWindow;
        }

        public void SetFramerateLimit(uint framerateLimit)
        {
            _renderWindow.SetFramerateLimit(framerateLimit);
        }

        public void SetVerticalSyncEnabled(bool vSinc)
        {
            _renderWindow.SetVerticalSyncEnabled(vSinc);
        }

        public void Clear() => _renderWindow.Clear();

        public void Clear(Color color) => _renderWindow.Clear(color);
    }
}
