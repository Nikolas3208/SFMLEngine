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

        public void Draw(RenderTarget target, RenderStates state)
        {
            foreach(var layer in _layers)
            {
                layer.Draw(target, state);
            }
        }
    }
}
