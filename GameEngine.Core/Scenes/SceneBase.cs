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
        protected List<GameObject> GameObjects { get; set; }
        protected Camera Camera {  get; set; }
        protected LayerStack LayerStack { get; set; }

        public ScenesStack Perent;

        public int Id;
        public string Name { get; set; }
        public bool IsLoad = false;

        public SceneBase(string name = "Scene")
        {
            GameObjects = new List<GameObject>();
            LayerStack = new LayerStack(name + "_layerStack");
            LayerStack.AddLayer(new LayerBase("", 0, LayerStack));
            Name = name;
        }

        public SceneBase(LayerStack layerStack, string name = "Scene")
        {
            GameObjects = new List<GameObject>();
            LayerStack = layerStack;
            Name = name;
        }

        public void AddGameObject(GameObject gameObject, int layerId = 0)
        {
            int index = 2;
            foreach (GameObject go in GameObjects)
            {
                if (go.Name == gameObject.Name)
                {
                    if (index == 2)
                    {
                        gameObject.Name += $" ({index})";
                    }
                    else
                    {
                        gameObject.Name = gameObject.Name.Replace($"({index - 1})", $"({index})");
                    }
                    index++;
                }
            }

            GameObjects.Add(gameObject);

            LayerStack.GetLayer(layerId).AddDrawObject(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }

        public T GetGameObjectById<T>(Guid id) where T : GameObject
        {
            return (T)GameObjects.FirstOrDefault(g => g.Id == id);
        }

        public T GetGameObject<T>() where T : GameObject
        {
            return (T)GameObjects.FirstOrDefault(g => g.GetType() == typeof(T));
        }

        public List<GameObject> GetGameObjects() => GameObjects;
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
