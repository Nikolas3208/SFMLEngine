using GameEngine.Core;
using GameEngine.Core.Graphics;
using GameEngine.Core.Layers;
using SFML.Window;

namespace MyRPG;

public class Program
{ 
    public static void Main()
    {
        var settings = new WindowSettings(VideoMode.DesktopMode, "My Rpg");

        var game = new Game(settings);

        game.Run();
    }
}