using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents.Assets
{
    public interface IAsset
    {
        string Name { get; set; }
        string FullPath { get; set; }
        Guid Id { get; }
        AssetType Type { get; set; }
    }
}
