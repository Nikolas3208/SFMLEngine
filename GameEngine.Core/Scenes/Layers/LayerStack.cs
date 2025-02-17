using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes.Layers
{
    public class LayerStack : ILayerStack
    {
        private Guid _id;
        private string _name = string.Empty;

        public Guid Id => _id;
        public string Name { get => _name; set => _name = value; }
        public List<ILayer> Layers { get; set; }

        public LayerStack(string name)
        {
            Name = name;
            _id = Guid.NewGuid();
            Layers = new List<ILayer>();
        }

        public void AddLayer(ILayer layer)
        {
            layer.Perent = this;
            Layers.Add(layer);
        }

        public ILayer GetLayer(Guid id)
        {
            return Layers.First(l => l.Id == id);
        }

        public ILayer GetLayerByName(string name)
        {
            return Layers.First(l => l.Name == name);
        }

        public void RemoveLayer(ILayer layer)
        {
            Layers.Remove(layer);
        }

        public void Update(Time deltaTime)
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {

        }
    }
}
