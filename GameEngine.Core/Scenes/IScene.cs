using GameEngine.Core.GameObjects;
using GameEngine.Core.Graphics;
using GameEngine.Core.Scenes.Layers;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes
{
    public interface IScene
    {
        string Name { get; set; }

        Guid Id { get; }
        Vector2f Size { get; set; }
        Camera Camera { get; set; }

        SceneStack SceneStack { get; }
        ILayerStack LayerStack { get; }
        List<GameObject> GameObjects { get; }


        void AddGameObject(GameObject gameObject);
        T GetGameObject<T>() where T : GameObject;
        T GetGameObjectById<T>(Guid id) where T : GameObject;
        bool RemoveGameObject(GameObject gameObject);
        void Start();
        void Update(Time deltaTime);
        void Draw(RenderTarget target, RenderStates states);
        void Close();
    }
}