using GameEngine.Core.Layers;
using GameEngine.Core.Scenes;
using MyRPG.GameObjects;

namespace MyRPG
{
    public class GameScene : SceneBase
    {
        public GameScene(LayerBase layerBase) : base(layerBase)
        {
        }

        public override void Start()
        {
            AddGameObject(new Cube());
            AddGameObject(new Cube());

            base.Start();
        }

        public override void Update(float deltaTime)
        {
            Perent.GetCamera().MoveToPosition(GetGameObject(0).Position);

            base.Update(deltaTime);
        }
    }
}
