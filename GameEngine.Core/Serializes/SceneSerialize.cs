using GameEngine.Core.GameObjects;
using GameEngine.Core.Scenes;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameEngine.Core.Serializes
{
    public class SceneSerialize
    {
        public static Scene? Serialize(string path, IScene scene)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            string json = JsonSerializer.Serialize(scene, options);

            var scene2 = JsonSerializer.Deserialize<Scene>(json, options);

            return scene2;
        }
    }
}
