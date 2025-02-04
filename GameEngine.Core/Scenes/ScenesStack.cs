using GameEngine.Core.Contents;
using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Scenes
{
    public class ScenesStack
    {
        private List<SceneBase> _scenes;

        private int scenesId = 0;

        public AssetsMenager AssetsMenager { get; set; }

        public string Name { get; }

        public ScenesStack(string name)
        {
            Name = name;

            _scenes = new List<SceneBase>();
            AssetsMenager = new AssetsMenager("\\Assets\\");
            AssetsMenager.Load();
        }

        public void AddScene(SceneBase scene)
        {
            scene.Id = scenesId;
            scenesId++;

            scene.Perent = this;

            _scenes.Add(scene);
        }

        public void RemoveScene(SceneBase scene)
        {
            _scenes.Remove(scene);
        }

        public SceneBase GetScene(int id)
        {
            return _scenes.FirstOrDefault(s => s.Id == id);
        }

        public void Start()
        {
            foreach (SceneBase scene in _scenes)
            {
                scene.Start();
            }
        }

        public void Update(Time deltaTime)
        {
            foreach(SceneBase scene in _scenes)
            {
                scene.Update(deltaTime);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(SceneBase scene in _scenes)
            {
                scene.Draw(target, states);
            }
        }

        public void Close()
        {

        }

        public void SetCamera(Camera camera)
        {
            foreach(var scene in _scenes)
            {
                if(scene.IsLoad)
                {
                    scene.SetCamera(camera);
                    break;
                }
            }

            if (_scenes.Count == 1)
                _scenes[0].SetCamera(camera);
        }

        public Camera GetCamera()
        {
            foreach (var scene in _scenes)
            {
                if (scene.IsLoad)
                {
                    return scene.GetCamera();
                }
            }

            if (_scenes.Count == 1)
                return _scenes[0].GetCamera();

            return null;
        }
    }
}
