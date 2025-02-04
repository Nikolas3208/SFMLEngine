using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents
{
    public class AssetsMenager
    {
        private static Dictionary<string, IAsset> _assets = [];
        private string _path;

        public AssetsMenager(string path)
        {
            _path = path;
            _assets = new Dictionary<string, IAsset>();
        }

        public void Load()
        {
            var files = Directory.GetFiles(_path, "*.png", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                fileName = fileName.Remove(fileName.Length - 4);

                SpriteAsset asset = new SpriteAsset(new Texture(file));
                asset.Name = fileName;
                asset.FullPath = file;

                if (!_assets.ContainsKey(fileName))
                    _assets.Add(fileName, asset);
            }

            files = Directory.GetFiles(_path, "*.jpg", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                fileName = fileName.Remove(fileName.Length - 4);

                SpriteAsset asset = new SpriteAsset(new Texture(file));
                asset.Name = fileName;
                asset.FullPath = file;

                if (!_assets.ContainsKey(fileName))
                    _assets.Add(fileName, asset);
            }
        }

        public static IAsset? GetAsset(string key)
        {
            if (_assets.ContainsKey(key))
                return _assets[key];

            return null;
        }

        public static T GetAsset<T>(string key) where T : IAsset
        {
            if (_assets.ContainsKey(key))
                return (T)_assets[key];

            throw new Exception("No found asset");
        }

        public static string GetAssetKeyByIndex(int index) => _assets.Keys.ToArray()[index];
        public static IAsset GetAssetByIndex(int index)
        {
            var key = _assets.Keys.ToArray()[index];

            return _assets[key];
        }

        public static IAsset GetAssetById(Guid id)
        {
            return _assets.FirstOrDefault(a => a.Value.Id == id).Value;
        }

        public static int GetAssetCount() => _assets.Count;

        public static List<string> GetAssetNames() => _assets.Keys.ToList();
    }
}
