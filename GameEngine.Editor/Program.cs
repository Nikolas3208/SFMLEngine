using GameEngine.Core.Graphics;
using SFML.Window;

namespace GameEngine.Editor;

public class Program
{
    public static void Main()
    {
        var settings = new WindowSettings
        {
            VideoMode = VideoMode.DesktopMode,
            Title = "Editor",
            FramerateLimit = 60,
        };

        var app = new EditorApplication(settings);
        app.Run();
    }
}