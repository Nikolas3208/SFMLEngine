using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents.Assets
{
    public class AudioAsset : Asset
    {
        private SoundBuffer _soundBuffer;

        public SoundBuffer SoundBuffer { get => _soundBuffer; }

        public AudioAsset(string filePath, string name)
        {
            _soundBuffer = new SoundBuffer(filePath);

            FullPath = filePath;
            Name = name;
        }
    }
}
