using GameEngine.Core.Contents;
using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Scenes
{
    public interface ISceneStack
    {
        AssetsMenager AssetsMenager { get; set; }
        Guid Id { get; }
        string Name { get; }

        void AddScene(IScene scene);
        void Close();
        void Draw(RenderTarget target, RenderStates states);
        Camera GetCamera();
        Guid GetCurentScene();
        IScene? GetScene(Guid id);
        T? GetScene<T>(Guid id) where T : IScene;
        void RemoveScene(IScene scene);
        void Resize(Vector2f size);
        void SetCurrentScene(Guid id);
        void Start();
        void Update(Time deltaTime);
    }
}