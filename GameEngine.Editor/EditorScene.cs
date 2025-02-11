using GameEngine.Core;
using GameEngine.Core.GameObjects;
using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Scenes;
using GameEngine.Core.Utils.TMXLoader;
using GameEngine.Core.Utils.TMXLoader.Loaders;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Editor
{
    public class EditorScene : SceneBase
    {
        private Map map;
        public EditorScene()
        {
            map = TmxLoader.Load("Assets/Levels/lavel_test_1.tmx");
        }

        public override void Start()
        {
            GameObject gameObject = new GameObject(this, "Tile map");
            gameObject.Position = new SFML.System.Vector2f(-1000, -580);

            TileMapRender tileMapRender = new TileMapRender(map, gameObject);

            gameObject.AddComponent(tileMapRender);

            AddGameObject(gameObject);

            base.Start();
        }

        public override void Update(Time deltaTime)
        {
            base.Update(deltaTime);

            ImGui.LabelText((1 / deltaTime.AsSeconds()).ToString(), "FPS: ");
            ImGui.LabelText(deltaTime.AsSeconds().ToString(), "Ms: ");
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            RectangleShape shape = new RectangleShape(new SFML.System.Vector2f(0, 0));

            shape.Draw(target, states);
            base.Draw(target, states);
        }
    }
}
