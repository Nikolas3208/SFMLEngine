using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public interface IComponent
    {
        Guid Id { get; }
        string Name { get; set; }
        IGameObject Perent { get; set; }

        void Start();
        void Update(Time deltaTime);
        void Draw(RenderTarget target, RenderStates state);
    }
}
