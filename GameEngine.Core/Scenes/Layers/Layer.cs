using GameEngine.Core.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes.Layers
{
    public class Layer : ILayer
    {
        private Guid _id;
        private string _name = string.Empty;

        public Guid Id => _id;
        public string Name { get => _name; set => _name = value; }
        public ILayerStack Perent { get; set; }
        public List<GameObject> refGameObjects { get; }

        public Layer(string name)
        {
            _name = name;
            refGameObjects = new List<GameObject>();
            _id = Guid.NewGuid();
        }

        public void AddObjectReference(ref GameObject gameObject)
        {
            gameObject.Layer = this;
            refGameObjects.Add(gameObject);
        }

        public void RemoveObjectReference(GameObject gameObject)
        {
            gameObject.Layer = null;
            refGameObjects.Remove(gameObject);
        }

        public void Update(Time deltaTime)
        {

        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (GameObject gameObject in refGameObjects)
            {
                gameObject.Draw(target, states);
            }
        }
    }
}
