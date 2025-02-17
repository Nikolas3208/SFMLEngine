using GameEngine.Core.Contents;
using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes
{
    public class SceneStack : ISceneStack
    {
        private Guid _id;
        private List<IScene> _scenes;
        private Guid _currentSceneId;
        public AssetsMenager AssetsMenager { get; set; }

        public string Name { get; }
        public Guid Id { get => _id; }

        public SceneStack(string name)
        {
            Name = name;
            _id = Guid.NewGuid();

            _scenes = new List<IScene>();
            AssetsMenager = new AssetsMenager("Assets");
            AssetsMenager.Load();
        }

        public void AddScene(IScene scene)
        {
            _currentSceneId = scene.Id;
            _scenes.Add(scene);
        }
        public void RemoveScene(IScene scene)
        {
            _scenes.Remove(scene);
        }
        public IScene? GetScene(Guid id)
        {
            return _scenes.First(s => s.Id == id);
        }
        public T? GetScene<T>(Guid id) where T : IScene
        {
            return (T)_scenes.First(s => s.Id == id && s.GetType() == typeof(T))!;
        }

        public void SetCurrentScene(Guid id) => _currentSceneId = id;
        public Guid GetCurentScene() => _currentSceneId;

        public virtual void Start()
        {
            foreach (var scene in _scenes)
            {
                scene.Start();
            }
        }
        public virtual void Update(Time deltaTime)
        {
            foreach (var scene in _scenes)
            {
                scene.Update(deltaTime);
            }
        }
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var scene in _scenes)
            {
                scene.Draw(target, states);
            }
        }


        public Camera GetCamera()
        {
            var camera = GetScene(_currentSceneId)!.Camera;

            return new Camera(camera.Position, camera.Size);
        }
        public void Resize(Vector2f size)
        {
            if (GetScene(_currentSceneId) != null)
                GetScene(_currentSceneId)!.Size = size;
        }
        public virtual void Close()
        {

        }
    }
}
