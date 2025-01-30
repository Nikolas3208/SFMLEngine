using GameEngine.Core;
using GameEngine.Core.Layers;
using GameEngine.Core.Scenes;
using MyRPG.GameObjects;
using SFML.System;

namespace MyRPG
{
    public class GameScene : SceneBase
    {
        public GameScene(string name) : base(name)
        {

        }

        public override void Start()
        {
            Perent.AssetsMenager = new AssetsMenager("\\Assets\\");
            Perent.AssetsMenager.Load();
            AddGameObject(new Cube(this));
            GetGameObject(0).Position = new Vector2f(220, 220);

            base.Start();
        }

        public override void Update(float deltaTime)
        {

            base.Update(deltaTime);
        }
    }
}
