using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Scenes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects
{
    public interface IGameObject
    {
        Guid Id { get; set; }
        string Name { get; set; }
        IScene Perent {  get; set; }

        void AddComponent(IComponent component);
        void RemoveComponent(IComponent component);
        T GetComponent<T>() where T : IComponent;
        T GetComponentById<T>(Guid id) where T : IComponent;
        List<IComponent> GetComponents();

        void Start();
        void Update(Time time);
        void Draw(RenderTarget target, RenderStates states);
    }
}
