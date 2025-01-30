using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Layers
{
    public class LayerStack : Drawable
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;

        private List<LayerBase> _layers;
        private Camera Camera { get; set; }

        private int _layerId;

        public LayerStack(string name)
        {
            Name = name;
            _layers = new List<LayerBase>();
        }

        public void AddLayer(LayerBase layer)
        {

            layer.Id = _layerId;

            _layerId++;

            _layers.Add(layer);
        }

        public void RemoveLayer(LayerBase layer)
        {
            _layers.Remove(layer);
        }

        public LayerBase GetLayer(int id) => _layers.First(l => l.Id == id);

        public void Start()
        {
            foreach (LayerBase layer in _layers)
            {
                layer.Start();
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var layer in _layers)
            {
                layer.Update(deltaTime);
            }
        }

        public void Draw(RenderTarget target, RenderStates state)
        {
            foreach(var layer in _layers)
            {
                layer.Draw(target, state);
            }
        }

        public Camera GetCamera()
        {
            return Camera;
        }

        public void SetCamera(Camera camera)
        {
            Camera = camera;
        }

        public Camera Resize(Vector2u size)
        {
            foreach (LayerBase layer in _layers)
            {
                layer.Resize(size);
            }

            return Camera = Camera.Resize((Vector2f)size);
        }

        public void Close()
        {
            foreach(var layer in _layers)
            {
                layer.Close();
            }
        }
    }
}
