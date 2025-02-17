using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using GameEngine.Core.Graphics;
using GameEngine.Core.Graphics.Animations;
using GameEngine.Core.Utils.TMXLoader;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.GameObjects.Components
{
    public class TileMapRender : IComponent
    {
        public Guid Id { get; }
        public string Name { get; set; } = nameof(TileMapRender);
        public IGameObject Perent { get; set; }

        private Map _map;
        private Dictionary<string, Vertex[]> _vertices = new Dictionary<string, Vertex[]>();
        private Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();
        private Dictionary<string, List<Tile>> _tiles = new Dictionary<string, List<Tile>>();

        private Texture Texture;

        public TileMapRender(Map map)
        {
            Id = Guid.NewGuid();
            _map = map;
        }

        public void Start()
        {
            if (_map != null)
            {
                foreach (var mapLayer in _map.MapLayers)
                {
                    var tiles = new List<Tile>();
                    SpriteSheet ss = null;
                    int firstId = 0;

                    for (int x = 0; x < mapLayer.Width; x++)
                    {
                        for (int y = 0; y < mapLayer.Height; y++)
                        {
                            int tileId = x + y * mapLayer.Width;
                            if (tileId < mapLayer.TileIds.Length)
                            {
                                var id = mapLayer.TileIds[tileId];
                                if (id > 0)
                                {
                                    AnimSprite animSprite = null;
                                    foreach (var tileSet in _map.TileSets)
                                    {
                                        if (tileSet.ContainsTile(id))
                                        {
                                            firstId = tileSet.FirstId;
                                            ss = tileSet.SpriteSheet!;
                                            if(tileSet.AnimSprite != null)
                                                animSprite = tileSet.AnimSprite;
                                            break;
                                        }
                                    }

                                    if (ss != null)
                                    {
                                        var sprite = new Sprite(ss.GetSprite());
                                        sprite.TextureRect = ss.GetTextureRect(id - firstId);
                                        //sprite.Origin = (Vector2f)ss.GetTileSize() / 2;
                                        var tile = new Tile(id, sprite);
                                        if (animSprite != null)
                                            tile = new Tile(id, sprite, animSprite);

                                        int xOffset = ss.GetTileSize().X / _map.TileWidth - 1;
                                        int yOffset = ss.GetTileSize().Y / _map.TileHeight - 1;

                                        tile.Position = new Vector2f((x) * _map.TileWidth, (y - yOffset) * _map.TileHeight);

                                        tiles.Add(tile);
                                    }
                                }
                            }
                        }
                    }

                    if (!_tiles.ContainsKey(mapLayer.Name))
                        _tiles.Add(mapLayer.Name, tiles);
                }
            }
        }

        public void Update(Time deltaTime)
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(var tiles in _tiles)
            {
                foreach (var tile in tiles.Value)
                {
                    tile.Draw(target, states);
                }
            }
        }
    }
}
