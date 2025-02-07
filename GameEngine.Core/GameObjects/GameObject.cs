using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Scenes;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.GameObjects
{
    public class GameObject : Transformable, IGameObject
    {
        private Guid _id;
        private string _name = nameof(GameObject);
        private SceneBase _perent;

        private List<IComponent> _components;

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public SceneBase Perent { get => _perent; set => _perent = value; }

        public GameObject(SceneBase perent, string name)
        {
            _components = new List<IComponent>();

            Perent = perent;
            Name = name;
            Id = Guid.NewGuid();
        }

        public virtual void Start()
        {
            foreach(var component in _components)
            {
                component.Start();
            }
        }
        public virtual void Update(Time deltaTime)
        {
            foreach(var component in _components)
            {
                component.Update(deltaTime);
            }
        }
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            foreach (var component in _components)
            {
                component.Draw(target, states);
            }
        }

        public void AddComponent(IComponent component)
        {
            if (_components.Where(c => c.Name == component.Name).Count() == 0)
            {
                _components.Add(component);

                Start();
            }
        }

        public void AddComponent(ComponentType component)
        {
            switch (component)
            {
                case ComponentType.SpriteRender:
                    AddComponent(new SpriteRender(this));
                    break;
                case ComponentType.AnimRender:
                    AddComponent(new AnimRender(this));
                    break;
            }
        }

        public void RemoveComponent(IComponent component)
        {
            _components.Remove(component);
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)_components.FirstOrDefault(c => c.GetType() == typeof(T));
        }

        public T GetComponentById<T>(Guid id) where T : IComponent
        {
            return (T)_components.FirstOrDefault(c => c.Id == id);
        }

        public List<IComponent> GetComponents()
        {
            return _components;
        }
    }
}