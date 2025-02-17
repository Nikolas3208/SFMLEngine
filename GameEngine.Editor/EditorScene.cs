using GameEngine.Core;
using GameEngine.Core.GameObjects;
using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Scenes;
using GameEngine.Core.Scenes.Layers;
using GameEngine.Core.Serializes;
using GameEngine.Core.Utils.TMXLoader;
using GameEngine.Core.Utils.TMXLoader.Loaders;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Editor
{
    public class EditorScene : IScene
    {
        private SceneStack? _sceneStack;
        private LayerStack? _layerStack;

        private Vector2f _size;
        private Guid _id;

        public string Name { get; set; } = string.Empty;
        public Vector2f Size { get => _size; set { _size = value; Camera = new Camera(Camera.Position, value); } }
        public Guid Id { get => _id; }
        public Camera Camera { get; set; }

        public SceneStack? SceneStack { get => _sceneStack; }

        public ILayerStack? LayerStack { get => _layerStack; }

        public List<GameObject> GameObjects { get; }

        public EditorScene(Vector2f size)
        {
            GameObjects = new List<GameObject>();
            Name = "Main scene";
            Size = size;
            _id = Guid.NewGuid();

            Camera = new Camera(new Vector2f(), size);
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.Perent = this;
            GameObjects.Add(gameObject);
        }
        public T GetGameObject<T>() where T : GameObject
        {
            return (T)GameObjects.First(g => g.GetType() == typeof(T));
        }
        public T GetGameObjectById<T>(Guid id) where T : GameObject
        {
            return (T)GameObjects.FirstOrDefault(g => g.GetType() == typeof(T) && g.Id == id);
        }
        public bool RemoveGameObject(GameObject gameObject)
        {
            return GameObjects.Remove(gameObject);
        }

        public void Start()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Start();
            }
        }
        public void Update(Time deltaTime)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }


        private RectangleShape shape = new RectangleShape(new Vector2f());
        public void Draw(RenderTarget target, RenderStates states)
        {
            shape.Draw(target, states);
            foreach (var gameObject in GameObjects)
            {
                gameObject.Draw(target, states);
                EditorRender.AddRectangle(gameObject.GetFloatRect(), Color.Red);
            }

            EditorRender.Draw(target, states);
        }

        public void Close()
        {

        }
    }
}
