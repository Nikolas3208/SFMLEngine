using GameEngine.Core.GameObjects;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes.Layers
{
    public interface ILayer
    {
        Guid Id { get; }
        string Name { get; set; }
        ILayerStack Perent { get; set; }
        List<GameObject> refGameObjects { get; }

        void AddObjectReference(ref GameObject obj);
        void RemoveObjectReference(GameObject obj);

        void Update(Time deltaTime);
        void Draw(RenderTarget target, RenderStates states);
    }
}
