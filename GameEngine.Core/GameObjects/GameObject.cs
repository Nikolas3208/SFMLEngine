using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Scenes;
using GameEngine.Core.Scenes.Layers;
using SFML.Graphics;
using SFML.System;
using System.Text.Json.Serialization;

namespace GameEngine.Core.GameObjects
{
    public class GameObject : Transformation, IGameObject
    {
        [JsonInclude]
        private Guid _id;
        [JsonInclude]
        private string _name = nameof(GameObject);
        private IScene? _perent;

        [JsonInclude]
        private List<IComponent> _components;

        [JsonIgnore]
        public Guid Id { get => _id; set => _id = value; }
        [JsonIgnore]
        public string Name { get => _name; set => _name = value; }
        [JsonIgnore]
        public IScene? Perent { get => _perent; set => _perent = value; }
        public ILayer? Layer { get; set; }

        public GameObject()
        {

        }

        public GameObject(string name)
        {
            _components = new List<IComponent>();

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
                component.Perent = this;
                _components.Add(component);

                Start();
            }
        }

        public void AddComponent(ComponentType component)
        {
            switch (component)
            {
                case ComponentType.SpriteRender:
                    AddComponent(new SpriteRender());
                    break;
                case ComponentType.AnimRender:
                    AddComponent(new AnimRender());
                    break;
                case ComponentType.Audio:
                    AddComponent(new Audio());
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

        public FloatRect GetFloatRect()
        {
            var spriteRener = GetComponent<SpriteRender>();

            if (spriteRener != null)
            {
                return new FloatRect(new Vector2f(Position.X - (spriteRener.GetOrigin().X * Scale.X), Position.Y - (spriteRener.GetOrigin().Y * Scale.Y)) , new Vector2f(spriteRener.GetSize().X * Scale.X, spriteRener.GetSize().Y * Scale.Y));
            }

            return new FloatRect(Position, new Vector2f(5, 5));
        }
    }
}