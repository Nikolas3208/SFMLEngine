using GameEngine.Core;
using GameEngine.Core.GameObjects;
using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Scenes;
using GameEngine.Core.Utils.TMXLoader;
using SFML.Graphics;

namespace GameEngine.Editor
{
    public class EditorScene : SceneBase
    {
        private Map map;
        public EditorScene()
        {
            //map = TmxLoader.Load("../net8.0/Assets/Levels/level1.tmx");
        }

        public override void Start()
        {
            GameObject gameObject = new GameObject(this, "Fire");
            gameObject.Position = new SFML.System.Vector2f(-1000, -700);

            TileMapRender tileMapRender = new TileMapRender(map, gameObject);

            gameObject.AddComponent(tileMapRender);

            AddGameObject(gameObject);

            base.Start();
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            RectangleShape shape = new RectangleShape(new SFML.System.Vector2f(0, 0));

            shape.Draw(target, states);
            base.Draw(target, states);
        }
    }
}
