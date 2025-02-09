using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using GameEngine.Core.Graphics;
using GameEngine.Core.Utils.TMXLoader;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public class TileMapRender : IComponent
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = nameof(TileMapRender);
        public IGameObject Perent { get; set; }

        private Map _map;
        private Dictionary<string, Vertex[]> _vertices = new Dictionary<string, Vertex[]>();
        private Dictionary<string, Texture> _textures = new Dictionary<string, Texture>(); 

        private Texture Texture;


        public TileMapRender(Map map, IGameObject perent)
        {
            Perent = perent;
            Id = Guid.NewGuid();
            _map = map;
        }

        public void Start()
        {
            int index = 0;

            if (_map != null)
            {
                foreach (var mapLayer in _map.MapLayers)
                {
                    var vertices = new Vertex[mapLayer.Width * mapLayer.Height * 6];
                    SpriteSheet ss = null;
                    int firstId = 0;


                    for (int x = 0; x < mapLayer.Width; x++)
                    {
                        for (int y = 0; y < mapLayer.Height; y++)
                        {
                            if (index < vertices.Length)
                            {
                                int tileId = x + y * mapLayer.Width;
                                if (tileId < mapLayer.TileIds.Length)
                                {
                                    var id = mapLayer.TileIds[tileId];
                                    if (id > 0)
                                    {
                                        if (ss == null)
                                        {
                                            foreach (var tileSet in _map.TileSets)
                                            {
                                                if (tileSet.ContainsTile(id))
                                                {
                                                    firstId = tileSet.FirstId;
                                                    ss = tileSet.SpriteSheet;
                                                }
                                            }
                                        }

                                        if (ss != null)
                                        {
                                            var rect = ss.GetTextureRect(id - firstId);
                                            Vector2f position = new Vector2f(rect.Left, rect.Top);
                                            Vector2f size = new Vector2f(rect.Width, rect.Height);

                                            vertices[index] = new Vertex(new Vector2f(x * 64, y * 64), position);
                                            vertices[index + 1] = new Vertex(new Vector2f(x * 64, y * 64 + size.Y), position + new Vector2f(0, size.Y));
                                            vertices[index + 2] = new Vertex(new Vector2f(x * 64 + size.X, y * 64), position + new Vector2f(size.X, 0));
                                            vertices[index + 3] = new Vertex(new Vector2f(x * 64, y * 64 + size.Y), position + new Vector2f(0, size.Y));
                                            vertices[index + 4] = new Vertex(new Vector2f(x * 64 + size.X, y * 64), position + new Vector2f(size.X, 0));
                                            vertices[index + 5] = new Vertex(new Vector2f(x * 64, y * 64) + size, position + size);
                                        }
                                    }
                                }
                            }

                            index += 6;
                        }
                    }

                    if (!_vertices.ContainsKey(mapLayer.Name))
                        _vertices.Add(mapLayer.Name, vertices);
                    if (ss != null && !_textures.ContainsKey(mapLayer.Name))
                        _textures.Add(mapLayer.Name, ss!.GetTexture());

                    index = 0;
                    vertices = new Vertex[mapLayer.Width * mapLayer.Height * 6];
                }
            }
        }

        public void Update(Time deltaTime)
        {
            Start();
        }

        public void Draw(RenderTarget target, RenderStates state)
        {
            foreach(var vertices in  _vertices)
            {
                if (_textures.ContainsKey(vertices.Key))
                    state.Texture = _textures[vertices.Key];

                target.Draw(vertices.Value, PrimitiveType.Triangles, state);
            }    
        }
    }
}
