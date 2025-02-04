using GameEngine.Core.GameObjects;
using GameEngine.Core.Graphics;
using GameEngine.Core.Scenes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Layers
{
    public class LayerBase
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;

        public readonly LayerStack Perent;

        private List<GameObject> _gameObjects;

        public LayerBase(string name, int id, LayerStack perent)
        {
            Name = name;
            Id = id;
            Perent = perent;

            _gameObjects = new List<GameObject>();
        }

        public void AddDrawObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void RemoveDrawObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(var go in _gameObjects)
            {
                go.Draw(target, states);
            }
        }
    }
}
