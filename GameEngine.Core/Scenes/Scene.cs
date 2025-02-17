using GameEngine.Core.GameObjects;
using GameEngine.Core.Scenes.Layers;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes
{
    public class Scene : IScene
    {
        private SceneStack? _sceneStack;
        private ILayerStack? _layerStack;

        private Vector2f _size;
        private Camera _camera;
        private Guid _id;

        public string Name { get; set; } = string.Empty;
        public Vector2f Size { get => _size; set { _size = value; _camera.Size = value; } }
        public Guid Id { get => _id; }
        public Camera Camera
        {
            get => _camera; 
            set
            {
                _camera = value;
            }
        }

        public SceneStack? SceneStack { get => _sceneStack; }

        public ILayerStack? LayerStack { get => _layerStack; set => _layerStack = value; }

        public List<GameObject> GameObjects { get; }

        public Scene(Vector2f size)
        {
            GameObjects = new List<GameObject>();
            LayerStack = new LayerStack("Main scene layers");
            LayerStack.AddLayer(new Layer("Default"));
            Name = "Main scene";
            Camera = new Camera();
            Size = size;
            _id = Guid.NewGuid();
        }

        public virtual void AddGameObject(GameObject gameObject)
        {
            gameObject.Perent = this;
            LayerStack!.GetLayerByName("Default").AddObjectReference(ref gameObject);
            GameObjects.Add(gameObject);
        }

        public virtual T GetGameObject<T>() where T : GameObject
        {
            return (T)GameObjects.First(g => g.GetType() == typeof(T));
        }

        public virtual T GetGameObjectById<T>(Guid id) where T : GameObject
        {
            return (T)GameObjects.First(g => g.GetType() == typeof(T) && g.Id == id);
        }

        public virtual bool RemoveGameObject(GameObject gameObject)
        {
            return GameObjects.Remove(gameObject);
        }

        public virtual void Start()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Start();
            }
        }

        public virtual void Update(Time deltaTime)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            LayerStack!.Draw(target, states);
        }

        public virtual void Close()
        {

        }
    }
}
