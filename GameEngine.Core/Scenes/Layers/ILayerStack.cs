using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Scenes.Layers
{
    public interface ILayerStack
    {
        Guid Id { get; }
        string Name { get; set; }
        List<ILayer> Layers { get; set; }

        void AddLayer(ILayer layer);
        void RemoveLayer(ILayer layer);
        ILayer GetLayer(Guid id);
        ILayer GetLayerByName(string name);
        void Update(Time deltaTime);
        void Draw(RenderTarget target, RenderStates states);
    }
}
