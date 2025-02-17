﻿using GameEngine.Core.Contents.Assets;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameEngine.Core.Contents
{
    public class AssetsMenager
    {
        private static SortedDictionary<string, IAsset> _assets = [];
        private string _path;

        [JsonConstructor]
        private AssetsMenager() { }

        public AssetsMenager(string path)
        {
            _path = path;
            _assets = new SortedDictionary<string, IAsset>();
        }

        public void Load()
        {
            var files = Directory.GetFiles(_path, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                fileName = fileName.Remove(fileName.Length - 4);

                if (!_assets.ContainsKey(fileName))
                {
                    try
                    {
                        if (file.Contains(".png") || file.Contains(".jpg"))
                        {
                            var asset = new ImageAsset(new Texture(file), fileName);
                            asset.FullPath = file;

                            _assets.Add(fileName, asset);
                        }
                        else if (file.Contains(".wav"))
                        {
                            var asset = new AudioAsset(file, fileName);

                            _assets.Add(fileName, asset);
                        }
                    }
                    catch { }
                }
            }
        }

        public static IAsset? GetAsset(string key)
        {
            if (_assets.ContainsKey(key))
                return _assets[key];

            return null;
        }

        public static T? GetAsset<T>(string key) where T : IAsset
        {
            if (_assets.ContainsKey(key))
                return (T)_assets[key];

            throw new Exception("No found asset");
        }

        public static string GetAssetKeyByIndex(int index) => _assets.ToList()[index].Value.Name;
        public static IAsset GetAssetByIndex(int index, string keySearch = null, AssetType type = AssetType.None)
        {
            if (keySearch == null)
            {
                var key = _assets.Where(a => a.Value.Type == type).ToList()[index].Key;

                return _assets[key];
            }
            else
            {
                var key = _assets.Where(a => a.Value.Type == type && a.Key.ToLower().Contains(keySearch.ToLower())).ToList()[index].Key;

                return _assets[key];
            }
        }

        public static T GetAssetByIndex<T>(int index, string keySearch = null, AssetType type = AssetType.None) where T : IAsset
        {
            if (keySearch == null)
            {
                var key = _assets.Where(a => a.Value.Type == type).ToList()[index].Key;

                return (T)_assets[key];
            }
            else
            {
                var key = _assets.Where(a => a.Value.Type == type && a.Key.ToLower().Contains(keySearch.ToLower())).ToList()[index].Key;

                return (T)_assets[key];
            }
        }

        public static IAsset GetAssetById(Guid id)
        {
            return _assets.FirstOrDefault(a => a.Value.Id == id).Value;
        }

        public static int GetAssetsCount(AssetType type = AssetType.None)
        {
           return _assets.Where(a => a.Value.Type == type).Count();
        }

        public static int GetAssetsCount<T>(string keySearch = null, AssetType type = AssetType.None) where T : IAsset
        {
            if (keySearch == null)
                return _assets.Where(a => a.Value.GetType() == typeof(T) && a.Value.Type == type).Count();

            return _assets.Where(a => a.Value.GetType() == typeof(T) && a.Value.Type == type && a.Key.ToLower().Contains(keySearch.ToLower())).Count();
        }

        public static List<string> GetAssetNames(AssetType type = AssetType.None) => 
            _assets.Where(a => a.Value.Type == type).ToDictionary().Keys.ToList();
    }
}
