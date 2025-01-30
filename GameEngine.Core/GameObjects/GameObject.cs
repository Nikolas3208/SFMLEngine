using SFML.Graphics;

namespace GameEngine.Core.GameObjects
{
    public abstract class GameObject : Transformable, Drawable
    {
        public int Id { get; set; }

        public abstract void Start();
        public abstract void Update(float deltaTime);
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}