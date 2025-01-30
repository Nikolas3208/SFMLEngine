using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameEngine.Core.Graphics
{
    public class Window
    {
        private Application _application;
        private RenderWindow _renderWindow;

        public Window(Application application, RenderWindow renderWindow)
        {
            _application = application;
            _renderWindow = renderWindow;
        }

        public Window(Application application, VideoMode videoMode, string title)
        {
            _application = application;

            _renderWindow = new RenderWindow(videoMode, title);
            _renderWindow.Closed += Closed;
            _renderWindow.Resized += Resized;
        }

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
            var clock = new Clock();

            while(_renderWindow.IsOpen)
            {
                _renderWindow.DispatchEvents();

                _application.Update(clock.Restart().AsSeconds());

                _renderWindow.SetView(_application.GetScenesStack().GetCamera().GetView());

                _renderWindow.Clear();

                _renderWindow.Draw(_application);

                _renderWindow.Display();
            }
        }

        public View GetView()
        {
            return _renderWindow.GetView();
        }
    }
}
