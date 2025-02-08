using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Editor.EditorInterface
{
    public class AssetsViewer
    {
        private string _currentFolder = string.Empty;
        private string[] _dirs = [];
        private string[] _filesCurrentDir = [];
        private List<string> _paths = [];
        private nint _folderIcon = 0;
        private nint _textFileIcon = 0;
        private nint _audioFile = 0;
        private EditorInterface _interface;
        private int _currentPath = 0;
        private string[] _comboPaths = [];

        public string CurrentFolder
        {
            get => _currentFolder;
            set
            {
                _currentFolder = value;
                Update(value);
            }
        }

        public AssetsViewer(EditorInterface @interface, string path)
        {
            _interface = @interface;
            CurrentFolder = path;
            _folderIcon = (nint)AssetsMenager.GetAsset<ImageAsset>("folder").Texture!.NativeHandle;
            _textFileIcon = (nint)AssetsMenager.GetAsset<ImageAsset>("text_icon").Texture!.NativeHandle;
            _audioFile = (nint)AssetsMenager.GetAsset<ImageAsset>("audio_icon").Texture!.NativeHandle;
        }

        public void AssetsView()
        {
            if (ImGui.Begin("Assets"))
            {

                var startPos = ImGui.GetCursorPos();

                if (ImGui.Combo("path", ref _currentPath, _comboPaths, _comboPaths.Length) && _currentPath != 0)
                {
                    CurrentFolder = _comboPaths[_currentPath];
                    _currentPath = 0;
                }
                var comboSize = ImGui.GetItemRectSize();
                ImGui.SetCursorPos(new Vector2(startPos.X + comboSize.X + 10, startPos.Y));
                if (ImGui.Button("Brek") && _comboPaths.Length > 1)
                {
                    CurrentFolder = _comboPaths[1];
                }

                startPos = ImGui.GetCursorPos();
                int y = 0;
                int x = 0;
                for (int i = 0; i < _paths.Count; i++)
                {
                    if (startPos.X + x * 75 > ImGui.GetWindowWidth() - 75)
                    {
                        x = 0;
                        y++;
                    }
                    ImGui.SetCursorPos(new Vector2(startPos.X + x * 75, startPos.Y + 20 * y + (y * 75)));
                    if (!_paths[i].Contains('.'))
                    {
                        ImGui.PushID(_paths[i]);
                        if (ImGui.ImageButton(_folderIcon, new Vector2(64, 64)))
                        {
                            CurrentFolder += "\\" + _paths[i];
                            return;
                        }
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip(_paths[i]);
                    }
                    else if (_paths[i].Contains(".png") || _paths[i].Contains(".jpg"))
                    {
                        var txImage = (nint)AssetsMenager.GetAsset<ImageAsset>(_paths[i].Remove(_paths[i].IndexOf('.'))).Texture!.NativeHandle;

                        ImGui.PushID(_paths[i]);
                        if (ImGui.ImageButton(txImage, new Vector2(64, 64)))
                        {
                            _interface.ScelectCurrentAssetName = _paths[i].Remove(_paths[i].IndexOf("."));
                            _interface.SelectGameObjectId = Guid.Empty;
                        }
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip(_paths[i]);
                    }
                    else if (_paths[i].Contains(".wav"))
                    {
                        ImGui.PushID(_paths[i]);
                        if (ImGui.ImageButton(_audioFile, new Vector2(64, 64)))
                        {

                        }
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip(_paths[i]);
                    }
                    else
                    {
                        ImGui.PushID(_paths[i]);
                        if (ImGui.ImageButton(_textFileIcon, new Vector2(64, 64)))
                        {

                        }
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip(_paths[i]);
                    }
                    string name = "";
                    if (_paths[i].Length > 7)
                    {
                        name = _paths[i].Remove(7);
                        name += "...";
                    }
                    else
                        name = _paths[i];
                    var textPos = ImGui.GetCursorPos();

                    ImGui.SetCursorPos(new Vector2(startPos.X + x * 75, textPos.Y - 75 * y + (y * 75)));
                    ImGui.Text(name);

                    x++;
                }

                ImGui.End();
            }
        }

        public void Update(string currentPath)
        {
            _paths.Clear();

            _dirs = Directory.GetDirectories(currentPath);

            foreach (var dir in _dirs)
            {
                var dirs = dir.Split('\\');
                foreach (var path in dirs)
                {
                    if (!_paths.Contains(path) && !currentPath.Contains(path))
                        _paths.Add(path);
                }
            }

            _filesCurrentDir = Directory.GetFiles(currentPath);

            foreach (var file in _filesCurrentDir)
            {
                var fileName = Path.GetFileName(file);

                _paths.Add(fileName);
            }

            var currentDir = Path.GetFullPath(currentPath);

            currentDir = currentDir.Remove(0, currentDir.IndexOf("Assets"));
            if (currentDir.Contains('\\'))
            {
                var dirs = currentDir.Split('\\');
                _comboPaths = new string[dirs.Length];
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (i == 0)
                        _comboPaths[i] = dirs[i];
                    else
                        _comboPaths[i] = _comboPaths[i - 1] + "\\" + dirs[i];
                }
                _comboPaths = _comboPaths.Reverse().ToArray();
            }
            else
            {
                _comboPaths = new string[1] { "Assets" };
            }
        }

        public void Draw()
        {
            AssetsView();
        }
    }
}
