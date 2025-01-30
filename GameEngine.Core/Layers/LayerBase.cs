using GameEngine.Core.Graphics;
using GameEngine.Core.Scenes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Layers
{
    public class LayerBase : Drawable
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;

        public readonly LayerStack Perent;

        private SceneBase Scene { get; set; }

        public LayerBase(LayerStack perent)
        {
            Perent = perent;
        }

        public virtual void AddScene(SceneBase scene) => Scene = scene;

        public virtual void RemoveScene() => Scene = null;

        public virtual SceneBase GetScene() => Scene;

        public virtual void Resize(Vector2u size)
        {
            Scene.Resize(size);
        }

        public virtual void Start()
        {
            if (Scene != null)
                Scene.Start();
        }

        public virtual void Update(float deltaTime)
        {
            if (Scene != null)
                Scene.Update(deltaTime);
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            if (Scene != null)
                Scene.Draw(target, states);
        }

        public Camera GetCamera()
        {
            return Perent.GetCamera();
        }

        public void Close()
        {
            Scene.Close();
        }
    }
}
