using GameEngine.Core;
using GameEngine.Core.Graphics;
using GameEngine.Core.Layers;
using GameEngine.Core.Scenes;
using SFML.Window;
namespace MyRPG
{
    public class Game
    {
        private readonly Application Application;
        private ScenesStack _scenesStack;
        public Game(WindowSettings settings)
        {
            _scenesStack = new ScenesStack("");

            _scenesStack.AddScene(new GameScene(""));

            Application = new Application(settings, _scenesStack);
        }

        public void Run()
        {
            Application.Run();
        }
    }
}
