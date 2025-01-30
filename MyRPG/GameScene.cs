using GameEngine.Core;
using GameEngine.Core.Layers;
using GameEngine.Core.Scenes;
using MyRPG.GameObjects;

namespace MyRPG
{
    public class GameScene : SceneBase
    {
        public GameScene(LayerBase layerBase) : base(layerBase)
        {
            AssetsMenager = new AssetsMenager("\\Assets\\");
        }

        public override void Start()
        {
            AssetsMenager.Load();
            AddGameObject(new Cube(this));
            GetGameObject(0).Position = new SFML.System.Vector2f(10, 10);
            AddGameObject(new Cube(this));
            GetGameObject(1).Position = new SFML.System.Vector2f(220, 220);

            base.Start();
        }

        public override void Update(float deltaTime)
        {

            base.Update(deltaTime);
        }
    }
}
