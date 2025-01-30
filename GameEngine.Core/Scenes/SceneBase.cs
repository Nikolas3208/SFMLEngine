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

        protected LayerBase Perent {  get; set; }

        protected int gameObjectId;

        public SceneBase(LayerBase layerBase)
        {
            Perent = layerBase;
            GameObjects = new List<GameObject>();
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.Id = gameObjectId;
            gameObjectId++;
            GameObjects.Add(gameObject);
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

        public virtual void Resize(Vector2u size)
        {

        }
        public virtual void Close()
        {
            
        }
    }
}
