using GameEngine.Core.Scenes;
using SFML.Graphics;

namespace GameEngine.Core.GameObjects
{
    public abstract class GameObject : Transformable, Drawable
    {
        protected SceneBase Perent;

        protected GameObject(SceneBase perent)
        {
            Perent = perent;
        }

        public int Id { get; set; }
        public abstract void Start();
        public abstract void Update(float deltaTime);
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}