using ImGuiNET;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GameEngine.Core.GameObjects;
using GameEngine.Core.GameObjects.Components;
using SFML.System;
using GameEngine.Core;
using GameEngine.Core.Contents;
using GameEngine.Core.Graphics.Animations;

namespace GameEngine.Editor.EditorInterface
{
    public enum InterfaceAction
    {
        None,
        AddObject,
        SelectTexture,
        AddComponent
    }

    public class EditorInterface
    {
        public EditorScene Scene { get; set; }

        public nint SceneTexture;

        public Guid SelectGameObjectId { get; set; }

        private AssetsViewer AssetViewer;
        private PropertieViewer PropertieViewer;


        public EditorInterface(EditorScene scene)
        {
            Scene = scene;
            AssetViewer = new AssetsViewer(this, "Assets");
            PropertieViewer = new PropertieViewer(this);
        }

        public void Draw()
        {
            ImGui.DockSpaceOverViewport();

            MenuBar();
            SceneViewer();
            PropertieViewer.Draw();
            SceneTreeViewer();
            AssetViewer.Draw();
        }

        private void MenuBar()
        {
            if(ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if(ImGui.MenuItem("New"))
                    {
                        
                    }
                    else if(ImGui.MenuItem("Open"))
                    {

                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }

        private void SceneViewer()
        {
            ImGui.Begin("SceneViewer");

            ImGui.Image(SceneTexture, ImGui.GetContentRegionAvail(), new Vector2(0, 1), new Vector2(1, 0));
            
            ImGui.End();
        }

        private string _textureName = string.Empty;

        private int _animId = 0;
        private bool _addAnim = false;
        private List<AnimationFrame> frames = new List<AnimationFrame>();
        private string _animName = string.Empty;
        private int _addAnimCurrentFrame = -1;
        private AnimRender animRender;
        private void PropertiViewer2()
        {
            ImGui.Begin("Properti");

            if (ScelectCurrentAssetName == string.Empty)
            {
                var gameObject = Scene.GetGameObjectById<GameObject>(SelectGameObjectId);

                if (gameObject == null)
                {
                    ImGui.End();
                    return;
                }

                if (ImGui.TreeNodeEx("Transform", ImGuiTreeNodeFlags.DefaultOpen))
                {
                    var pos = new Vector2(gameObject.Position.X, gameObject.Position.Y);
                    ImGui.DragFloat2("Position", ref pos, 0.1f);
                    gameObject.Position = new Vector2f(pos.X, pos.Y);
                    ImGui.Spacing();

                    float rot = gameObject.Rotation;
                    ImGui.DragFloat("Rotation", ref rot, 0.1f);
                    gameObject.Rotation = rot;
                    ImGui.Spacing();

                    var scale = new Vector2(gameObject.Scale.X, gameObject.Scale.Y);
                    ImGui.DragFloat2("Scale", ref scale, 0.1f);
                    gameObject.Scale = new Vector2f(scale.X, scale.Y);

                    ImGui.TreePop();
                }

                foreach (var component in gameObject.GetComponents())
                {
                    if (ImGui.TreeNodeEx(component.Name, ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        if (component.GetType() == typeof(SpriteRender))
                        {
                            var spriteRender = (SpriteRender)component;

                            nint texture = -1;
                            if (spriteRender.GetTexture() != null)
                            {
                                texture = (nint)spriteRender.GetTexture().NativeHandle;
                            }
                            else
                            {
                                texture = (nint)0;
                            }


                            if (ImGui.ImageButton(texture, new Vector2(64, 64)))
                            {
                                ImGui.OpenPopup(InterfaceAction.SelectTexture.ToString());
                            }
                            var posX = ImGui.GetCursorPosX();

                            ImGui.SetNextWindowSize(new Vector2(250, 320));
                            if (ImGui.BeginPopup(InterfaceAction.SelectTexture.ToString(), ImGuiWindowFlags.AlwaysVerticalScrollbar))
                            {
                                ImGui.Text("Select texture");
                                Vector2 startPoint = ImGui.GetCursorPos();
                                ImGui.Spacing();


                                for (int i = 0; i < AssetsMenager.GetAssetsCount(); i++)
                                {
                                    int y = i / 3;
                                    int x = i - (y * 3);

                                    string localName = AssetsMenager.GetAssetKeyByIndex(i);

                                    ImGui.SetCursorPos(new Vector2(x * 78 + startPoint.X, y * 85 + startPoint.Y));
                                    if (ImGui.ImageButton((nint)AssetsMenager.GetAsset<ImageAsset>(AssetsMenager.GetAssetKeyByIndex(i)).Texture!.NativeHandle, new Vector2(64, 64)))
                                    {
                                        _textureName = localName;

                                        var asset = AssetsMenager.GetAsset<ImageAsset>(_textureName);
                                       // gameObject.GetComponent<SpriteRender>().UpdateTexture(asset.Texture!, _textureName);
                                       // gameObject.GetComponent<SpriteRender>().SpriteName = asset.Name;

                                        ImGui.CloseCurrentPopup();
                                    }

                                    ImGui.SetCursorPos(new Vector2(x * 78 + startPoint.X, y * 85 + startPoint.Y + 75));

                                    if (localName.Length > 7)
                                    {
                                        localName = localName.Remove(7, localName.Length - 7);
                                        localName += "...";
                                    }
                                    ImGui.Text(localName);

                                }

                                ImGui.EndPopup();
                            }

                            ImGui.Spacing();
                            //ImGui.SetCursorPosX(posX);
                            ImGui.LabelText(_textureName, "Texture name: ");

                        }
                        else if (component.GetType() == typeof(AnimRender))
                        {
                            animRender = (AnimRender)component;

                            var listAnim = new List<Animation>();
                            if (animRender.GetAnimations() != null)
                                listAnim = animRender.GetAnimations();

                            Animation currentAnim;

                            if (listAnim.Count > 0)
                            {
                                if (ImGui.Combo("Animations", ref _animId, animRender.GetAnimationsName().ToArray(), animRender.GetAnimationsName().Count))
                                {
                                    currentAnim = animRender.GetAnimations()[_animId];
                                    animRender.SetAnimPlay(animRender.GetAnimationsName()[_animId]);
                                }
                                float posX = ImGui.GetCursorPosX();
                                float editButtonSizeY = 0;
                                ImGui.SetCursorPosX(ImGui.GetWindowSize().X - editButtonSizeY - 50);
                                if (ImGui.SmallButton("Edit"))
                                {

                                }
                                editButtonSizeY = ImGui.GetItemRectSize().Y;
                            }



                            if (ImGui.Button("Add animation"))
                            {
                                _addAnim = true;
                            }
                        }
                    }

                    ImGui.TreePop();
                }

                ImGui.Spacing();
                ImGui.Spacing();
                float addComponetButtonSizeX = 0;
                ImGui.SetCursorPosX((ImGui.GetWindowSize().X / 3) + addComponetButtonSizeX * 2);
                if (ImGui.Button("Add component"))
                {
                    ImGui.OpenPopup(InterfaceAction.AddComponent.ToString());
                }
                addComponetButtonSizeX = ImGui.GetItemRectSize().X;

                if (ImGui.BeginPopup(InterfaceAction.AddComponent.ToString()))
                {
                    int componentId = 0;

                    var components = Enum.GetNames(typeof(ComponentType)).ToArray();

                    if (ImGui.Combo("Components", ref componentId, components, components.Length))
                    {
                        gameObject.AddComponent((ComponentType)componentId);

                        ImGui.CloseCurrentPopup();
                    }

                    ImGui.EndPopup();
                }

                ImGui.End();

                if (_addAnim)
                {
                    if (ImGui.Begin("Add animation", ImGuiWindowFlags.NoScrollbar))
                    {
                        ImGui.InputText("Anim name: ", ref _animName, 50);

                        if (frames.Count > 0)
                        {
                            string[] ids = new string[frames.Count];

                            for (int i = 0; i < frames.Count; i++)
                            {
                                ids[i] = i.ToString();
                            }

                            ImGui.ListBox("Frames", ref _addAnimCurrentFrame, ids, ids.Length);
                            if (_addAnimCurrentFrame >= 0)
                            {
                                ImGui.Spacing();
                                ImGui.Spacing();
                                int id = frames[_addAnimCurrentFrame].i;
                                ImGui.InputInt("Tile id: ", ref id);
                                frames[_addAnimCurrentFrame].i = id;
                                ImGui.Spacing();
                                float time = frames[_addAnimCurrentFrame].time;
                                ImGui.InputFloat("Frame time: ", ref time, 0.1f);
                                frames[_addAnimCurrentFrame].time = time;
                            }
                        }

                        if (ImGui.Button("Add anim frame"))
                        {
                            frames.Add(new AnimationFrame(frames.Count, 0.5f));
                        }

                        if (ImGui.Button("Aply"))
                        {
                            if (animRender != null)
                            {
                                animRender.AddAnimation(_animName, new Animation(frames.ToArray()));
                                animRender.SetAnimPlay(_animName);
                            }
                            frames = new List<AnimationFrame>();
                            _animName = string.Empty;
                            _addAnimCurrentFrame = 0;
                            _addAnim = false;
                        }

                        ImGui.End();
                    }
                }
            }
            else
            {
                var asset = AssetsMenager.GetAsset<ImageAsset>(ScelectCurrentAssetName);

                ImGui.Image((nint)asset.Texture!.NativeHandle, new Vector2(64, 64));
                ImGui.Spacing();
                ImGui.LabelText(asset.Name, "Asset name: ");
                ImGui.Spacing();
                bool multuplay = asset.IsMultiplaySprite;
                if(ImGui.Checkbox("Multiplay sprite", ref multuplay))
                    asset.IsMultiplaySprite = multuplay;
                if (asset.IsMultiplaySprite) 
                {
                    ImGui.Spacing();
                    bool countOrSize = asset.SpriteSheet.AbIsCount;
                    if (ImGui.Checkbox("a or b is count", ref countOrSize))
                        asset.SpriteSheet.AbIsCount = countOrSize;
                    var tileSize = asset.SpriteSheet.GetTileSize();
                    var refTileSize = new Vector2(tileSize.X, tileSize.Y);
                    ImGui.Spacing();
                    ImGui.InputFloat2("Tile size", ref refTileSize);
                    asset.SpriteSheet.SetTileSize(new Vector2i((int)refTileSize.X, (int)refTileSize.Y));
                }
            }
        }

        private void SceneTreeViewer()
        {
            ImGui.Begin("Scene objects");

            if(ImGui.TreeNodeEx("GameObjects", ImGuiTreeNodeFlags.DefaultOpen))
            {
                foreach(var go in Scene.GetGameObjects())
                {
                    if(ImGui.Selectable(go.Name))
                    {
                        SelectGameObjectId = go.Id;
                        ScelectCurrentAssetName = string.Empty;
                    }
                }
            }

            if(ImGui.IsMouseClicked(ImGuiMouseButton.Right) && ImGui.IsWindowHovered())
            {
                ImGui.OpenPopup("Scene objects");
            }

            if(ImGui.BeginPopup("Scene objects"))
            {
                ImGui.Separator();
                if(ImGui.BeginMenu("Add"))
                {
                    if(ImGui.MenuItem("GameObject"))
                    {
                        GameObject go = new GameObject(Scene, "GameObject");
                        SelectGameObjectId = go.Id;
                        ScelectCurrentAssetName = string.Empty;
                        Scene.AddGameObject(go);
                    }
                }
            }

            ImGui.End();
        }

        private string _folder = "Assets";
        private int _idCurrentDir = 0;
        public string ScelectCurrentAssetName = string.Empty;

        private void AssetsViewer()
        {
            //AssetsViewer.Draw();

            /*ImGui.Begin("Assets", ImGuiWindowFlags.AlwaysVerticalScrollbar);


            var dirs = Directory.GetDirectories(_folder);
            var files = Directory.GetFiles(_folder);

            ImGui.InputText("Current dir: ", ref _folder, 1000);
            var posY = ImGui.GetCursorPosY();
            if (ImGui.Combo(": folders", ref _idCurrentDir, dirs, dirs.Length))
            {
                _folder = dirs[_idCurrentDir];
            }
            var posX = ImGui.GetItemRectSize().X;
            ImGui.SetCursorPos(new Vector2(posX + 10, posY));
            if(ImGui.SmallButton("break"))
            {
                for (int i = 0; i < _folder.Length; i++)
                {
                    if(_folder.IndexOf('\\', _folder.Length - 1 - i) != -1)
                    {
                        _folder = _folder.Remove(_folder.Length - 1 - i);
                    }
                }
            }
            var startPos = ImGui.GetCursorPos();
            Vector2 rectSize = new Vector2();

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var indexPos = fileName.IndexOf('.');
                if(file.IndexOf(".png") != -1 || file.IndexOf(".jpg") != -1)
                {
                    var tx = AssetsMenager.GetAsset<SpriteAsset>(fileName.Remove(indexPos)).GetTexture();
                    ImGui.SetCursorPos(new Vector2(startPos.X + rectSize.X + 20, startPos.Y + 20));
                    if(ImGui.ImageButton((nint)tx.NativeHandle, new Vector2(64, 64)))
                    {
                        _scelectCurrentAssetName = fileName.Remove(indexPos);
                    }
                    rectSize = ImGui.GetItemRectSize();
                }
            }

            ImGui.End();*/
        }
            
    } 
}
