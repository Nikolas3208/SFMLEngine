using GameEngine.Core;
using GameEngine.Core.Graphics;
using GameEngine.Core.Layers;
using SFML.Window;
namespace MyRPG
{
    public class Game
    {
        private readonly Application Application;
        private LayerStack _layerStack;

        public Game(WindowSettings settings)
        {
            _layerStack = new LayerStack("");

            var layer = new LayerBase(_layerStack);
            layer.AddScene(new GameScene(layer));

            _layerStack.AddLayer(layer);

            Application = new Application(settings, _layerStack);
        }

        public void Run()
        {
            Application.Run();
        }
    }
}
