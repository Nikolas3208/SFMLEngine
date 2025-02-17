using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public class Audio : IComponent
    {
        public Guid Id { get; }
        public string Name { get; set; } = nameof(Audio);
        public IGameObject Perent { get; set; }

        public Audio()
        {
            Id = Guid.NewGuid();
        }

        public void Start()
        {

        }

        public void Update(Time deltaTime)
        {

        }

        public void Draw(RenderTarget target, RenderStates state)
        {

        }
    }
}
