using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public class AssetsMenager
    {
        private Dictionary<string, Texture> _textures;

        private string _path;

        public AssetsMenager(string path)
        {
            _path = path;
            _textures = new Dictionary<string, Texture>();
        }

        public void Load()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDir + _path, "*.png", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                string name = Path.GetFileName(file);
                name = name.Remove(name.Length - 4, 4);

                if(!_textures.ContainsKey(name))
                    _textures.Add(name, new Texture(file));
                else
                {
                    int i = 1;
                    while(true)
                    {
                        if (!_textures.ContainsKey(name + $" ({i})"))
                        {
                            _textures.Add(name + $" ({i})", new Texture(file));
                            break;
                        }
                    }
                }
            }
        }

        public Texture GetTexture(string name)
        {
            if (!_textures.ContainsKey(name))
            {
                return null;
            }

            return _textures[name];
        }
    }
}
