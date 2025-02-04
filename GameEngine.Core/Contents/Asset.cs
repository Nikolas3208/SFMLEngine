using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents
{
    public class Asset : IAsset
    {
        private string _name = string.Empty;
        private string _fullPath = string.Empty;
        private Guid _id;
        private AssetType _type;

        public string Name { get => _name; set => _name = value; }
        public string FullPath { get => _fullPath; set => _fullPath = value; }
        public Guid Id { get => _id; }
        public AssetType Type { get => _type; set => _type = value; }

        public Asset()
        {
            _id = Guid.NewGuid();
        }
    }
}
