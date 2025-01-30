using GameEngine.Core.GameObjects;
using GameEngine.Core.Graphics;
using GameEngine.Core.Layers;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Scenes
{
    public class SceneBase
    {
        protected virtual List<GameObject> GameObjects { get; set; }
        protected Camera Camera {  get; set; }
        protected LayerStack LayerStack { get; set; }

        protected int gameObjectId;

        public ScenesStack Perent;

        public int Id;
        public string Name { get; set; }
        public bool IsLoad = false;

        public SceneBase(string name)
        {
            GameObjects = new List<GameObject>();
            LayerStack = new LayerStack(name + "_layerStack");
            LayerStack.AddLayer(new LayerBase("", 0, LayerStack));
            Name = name;
        }

        public void AddGameObject(GameObject gameObject, int layerId = 0)
        {
            gameObject.Id = gameObjectId;
            gameObjectId++;
            GameObjects.Add(gameObject);

            LayerStack.GetLayer(layerId).AddDrawObject(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }

        public GameObject GetGameObject(int id)
        {
            return GameObjects.FirstOrDefault(g => g.Id == id);
        }

        public T GetGameObject<T>() where T : GameObject
        {
            return (T)GameObjects.FirstOrDefault(g => g.GetType() == typeof(T));
        }

        public virtual void Start()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Start();
            }
        }

        public virtual void Update(float deltaTime)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(deltaTime);
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Draw(target, states);
            }
        }

        public void SetCamera(Camera camera)
        {
            Camera = camera;
        }
        public Camera GetCamera() => Camera;

        public virtual void Close()
        {
            
        }
    }
}
