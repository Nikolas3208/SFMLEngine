using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using GameEngine.Core.GameObjects;
using GameEngine.Core.GameObjects.Components;
using GameEngine.Core.Graphics.Animations;
using GameEngine.Core.Utils.ImGuiImple;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using System.Numerics;

namespace GameEngine.Editor.EditorInterface
{
    public class PropertieViewer
    {
        private EditorInterface _interface;
        private GameObject _gameObject;

        private List<AnimationFrame> _frames = new List<AnimationFrame>();

        private Vector4 _spriteRenderColor = new Vector4(1);
        private string[] _components = Enum.GetNames(typeof(ComponentType)).ToArray();

        private bool _openSelectImage = false;
        private bool _openColorPicker = false;
        private bool _openAddComponent = false;
        private bool _openAddAnimation = false;
        private bool _openEditAnimation = false;

        private int _selectCurrentAnimationId = 0;
        private int _setCurrentFrame = 0;
        private int _editCurrentFrame = 0;

        private string _selectImageSearch = string.Empty;
        private string _componentsSearch = string.Empty;
        private string _addAnimationName = string.Empty;
        private string _editAnimationName = string.Empty;

        public PropertieViewer(EditorInterface @interface)
        {
            _interface = @interface;
        }

        public void PropertieView()
        {
            if (ImGui.Begin("Propertie"))
            {
                if (_interface.SelectGameObjectId != Guid.Empty)
                {
                    _gameObject = _interface.Scene.GetGameObjectById<GameObject>(_interface.SelectGameObjectId);

                    GameObjectPropertie(_gameObject);
                }
                else if (_interface.ScelectCurrentAssetName != string.Empty)
                {
                    var asset = AssetsMenager.GetAsset(_interface.ScelectCurrentAssetName);

                    AssetPropertie(asset);
                }

                ImGui.End();
            }
        }

        private void AssetPropertie(IAsset asset)
        {
            switch (asset.Type)
            {
                case AssetType.Image:
                case AssetType.Sprite:
                    ImageAssetPropertie((ImageAsset)asset);
                    break;
            }
        }

        private void ImageAssetPropertie(ImageAsset asset)
        {
            bool isSprite = asset.IsSprite;

            ImGui.Text(asset.Name);
            ImGui.Spacing();
            ImGui.Image((nint)asset.Texture!.NativeHandle, new Vector2(64, 64));
            ImGui.Spacing();
            if (ImGui.Checkbox("Sprite", ref isSprite))
                asset.IsSprite = isSprite;
            ImGui.Spacing();

            if (asset.IsSprite)
            {
                bool isMultiplaySprite = asset.IsMultiplaySprite;
                if (ImGui.Checkbox("Multiplay", ref isMultiplaySprite))
                    asset.IsMultiplaySprite = isMultiplaySprite;
                ImGui.Spacing();

                if (asset.IsMultiplaySprite)
                {
                    bool abIsCount = asset.SpriteSheet!.AbIsCount;
                    if (ImGui.Checkbox("AbIsCount", ref abIsCount))
                        asset.SpriteSheet.AbIsCount = abIsCount;
                    ImGui.Spacing();
                    Vector2 tileSize = new Vector2(asset.SpriteSheet!.GetTileSize().X, asset.SpriteSheet!.GetTileSize().Y);
                    ImGui.InputFloat2("Tile size: ", ref tileSize);
                    asset.SpriteSheet!.SetTileSize(new Vector2i((int)tileSize.X, (int)tileSize.Y));
                    ImGui.Spacing();
                    int borderSize = asset.SpriteSheet.GetBorderSize();
                    if (ImGui.InputInt("Border size", ref borderSize))
                        asset.SpriteSheet.SetBorderSize(borderSize);
                }
            }
            bool smooth = asset.IsSmooth;
            if (ImGui.Checkbox("Smooth", ref smooth))
                asset.IsSmooth = smooth;
        }

        private void GameObjectPropertie(GameObject gameObject)
        {
            if (gameObject == null)
                return;

            string goName = gameObject.Name;
            Vector2 goPos = new Vector2(gameObject.Position.X, gameObject.Position.Y);
            float goRot = gameObject.Rotation;
            Vector2 goScale = new Vector2(gameObject.Scale.X, gameObject.Scale.Y);

            if (ImGui.InputText("Object name: ", ref goName, 50))
                gameObject.Name = goName;

            ImGui.Spacing();
            if (ImGui.TreeNodeEx("Components", ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.Spacing();
                if (ImGui.TreeNodeEx("Transform", ImGuiTreeNodeFlags.DefaultOpen))
                {
                    ImGui.Spacing();
                    ImGui.DragFloat2("Position", ref goPos);
                    ImGui.Spacing();
                    ImGui.DragFloat("Rotation", ref goRot);
                    ImGui.Spacing();
                    ImGui.DragFloat2("Scale", ref goScale);
                    ImGui.Spacing();

                    gameObject.Position = new Vector2f(goPos.X, goPos.Y);
                    gameObject.Rotation = goRot;
                    gameObject.Scale = new Vector2f(goScale.X, goScale.Y);

                    ImGui.TreePop();
                }


                foreach (var component in gameObject.GetComponents())
                {
                    bool clouse = false;

                    if (ImGuiImple.ClosedTreeNodeEx(component.Name, ImGuiTreeNodeFlags.DefaultOpen, ref clouse))
                    {
                        if(clouse)
                        {
                            gameObject.RemoveComponent(component);
                            break;
                        }
                        if (component.GetType() == typeof(SpriteRender))
                        {
                            var spriteRender = (SpriteRender)component;

                            bool flipX = spriteRender.FlipX;
                            bool flipY = spriteRender.FlipY;

                            ImGui.Spacing();
                            float spriteButtonY = ImGui.GetCursorPos().Y;
                            ImGui.Text("Sprite: ");
                            float textSpriteSizeX = ImGui.GetItemRectSize().X;

                            ImGui.SetCursorPos(new Vector2(textSpriteSizeX + 50, spriteButtonY));

                            string spriteName = spriteRender.SpriteName != string.Empty ? spriteRender.SpriteName : "None";
                            if (ImGui.Button(spriteName))
                            {
                                _openSelectImage = true;
                            }

                            if(_openSelectImage)
                            {
                                var asset = (ImageAsset)ImGuiImple.SelectAsset(AssetType.Sprite)!;

                                if (asset != null)
                                {
                                    spriteRender.UpdateSprite(asset.Sprite);
                                    spriteRender.SpriteName = asset.Name;
                                    _openSelectImage = false;
                                }
                            }
                            ImGui.Spacing();
                            float textColorPosX = ImGui.GetCursorPos().Y;
                            ImGui.Text("Color: ");
                            float textColorSizeX = ImGui.GetItemRectSize().X;
                            ImGui.SetCursorPos(new Vector2(textColorSizeX + 50, textColorPosX));
                            if (ImGui.ColorButton("  ", _spriteRenderColor))
                            {
                                _openColorPicker = true;
                            }

                            ImGui.Spacing();
                            var flipPos = ImGui.GetCursorPos();
                            ImGui.Text("Flip ");
                            float flipTextSizeX = ImGui.GetItemRectSize().X;
                            ImGuiImple.CheckBox("X", ref flipX, new Vector2(flipPos.X + 40 + flipTextSizeX, flipPos.Y));

                            float flipXSizeX = ImGui.GetItemRectSize().X;

                            ImGuiImple.CheckBox("Y", ref flipY, new Vector2(flipPos.X + 40 + flipTextSizeX + 10 + flipXSizeX, flipPos.Y));

                            spriteRender.FlipX = flipX;
                            spriteRender.FlipY = flipY;
                        }
                        else if (component.GetType() == typeof(AnimRender))
                        {
                            var animRender = (AnimRender)component;

                            var listAnim = animRender.GetAnimations();

                            Animation currentAnim = null;

                            if (listAnim.Count > 0)
                            {
                                if (ImGui.Combo("Animations", ref _selectCurrentAnimationId, animRender.GetAnimationsName().ToArray(), animRender.GetAnimationsName().Count))
                                {
                                    currentAnim = animRender.GetAnimations()[_selectCurrentAnimationId];
                                    animRender.SetAnimPlay(animRender.GetAnimationsName()[_selectCurrentAnimationId]);
                                }

                                _editAnimationName = animRender.GetAnimPlay();

                                float posX = ImGui.GetCursorPosX();
                                float editButtonSizeY = 0;
                                ImGui.SetCursorPosX(ImGui.GetWindowSize().X - editButtonSizeY - 50);
                                if (ImGui.SmallButton("Edit"))
                                {
                                    _openEditAnimation = true;
                                }
                                editButtonSizeY = ImGui.GetItemRectSize().Y;
                            }

                            if (ImGui.Button("Add animation"))
                            {
                                _openAddAnimation = true;
                            }
                        }
                        else if(component.GetType() == typeof(TileMapRender))
                        {

                        }
                        ImGui.TreePop();
                    }

                    if (clouse)
                    {
                        gameObject.RemoveComponent(component);
                        break;
                    }
                }

                ImGui.TreePop();
            }

            if (ImGui.Button("Add component"))
            {
                _openAddComponent = true;
            }
        }

        private Vector4 color = new Vector4();
        private void OpenColorPicker()
        {
            if (_openColorPicker)
            {
                ImGui.OpenPopup("Color picker");
                _openColorPicker = false;
            }

            if (ImGui.BeginPopup("Color picker"))
            {
                if (ImGui.ColorPicker4("Sprite color", ref _spriteRenderColor, ImGuiColorEditFlags.DisplayRGB))
                {
                    color = _spriteRenderColor * byte.MaxValue;
                    _gameObject.GetComponent<SpriteRender>().Color = new Color((byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);

                }

                ImGui.EndPopup();
            }
        }

        private void OpenAddComponent()
        {
            if (_openAddComponent)
            {
                ImGui.OpenPopup("Add component");
                _openAddComponent = false;
            }

            if (ImGui.BeginPopup("Add component"))
            {
                ImGui.InputText("Components search", ref _componentsSearch, 50);

                var componets = _components.Where(c => c.ToLower().Contains(_componentsSearch.ToLower())).ToArray();
                int idComponentSelect = 0;
                if (ImGui.Combo("Components", ref idComponentSelect, componets, componets.Length))
                {
                    _gameObject.AddComponent((ComponentType)Enum.Parse(typeof(ComponentType), componets[idComponentSelect]));

                    ImGui.CloseCurrentPopup();
                }
            }
        }

        private void OpenEditAnimation()
        {
            if (_openEditAnimation)
            {
                ImGui.OpenPopup("Edit animation");

                _openEditAnimation = false;
            }

            if (ImGui.BeginPopup("Edit animation"))
            {
                var animRender = _gameObject.GetComponent<AnimRender>();
                if (animRender != null)
                {
                    string name = _editAnimationName;

                    ImGui.InputText("Anim name: ", ref name, 50);

                    var animation = animRender.GetAnimationByName(_editAnimationName);

                    _frames = animation.frames.ToList();

                    string[] ids = new string[_frames.Count];

                    for (int i = 0; i < _frames.Count; i++)
                        ids[i] = i.ToString();

                    ImGui.Combo("Frames", ref _editCurrentFrame, ids, ids.Length);

                    if (_frames.Count > 0)
                    {
                        ImGui.Spacing();
                        ImGui.Spacing();
                        int id = _frames[_editCurrentFrame].i;
                        ImGui.InputInt("Tile id: ", ref id);
                        _frames[_editCurrentFrame].i = id;
                        ImGui.Spacing();
                        float time = _frames[_editCurrentFrame].time;
                        ImGui.InputFloat("Frame time: ", ref time, 0.1f);
                        _frames[_editCurrentFrame].time = time;
                    }

                    if (ImGui.Button("Aplay"))
                    {
                        animRender.Name = name;
                        animation.frames = _frames.ToArray();
                        _editCurrentFrame = 0;
                        _frames.Clear();

                        ImGui.CloseCurrentPopup();
                    }
                }

                ImGui.EndPopup();
            }
        }

        private void OpenAddAnimation()
        {
            if (_openAddAnimation)
            {
                ImGui.OpenPopup("Add animation");
                _openAddAnimation = false;
            }

            if (ImGui.BeginPopup("Add animation", ImGuiWindowFlags.Popup))
            {
                var animRender = _gameObject.GetComponent<AnimRender>();

                string[] ids = new string[_frames.Count];

                for(int i = 0; i < _frames.Count; i++)
                    ids[i] = i.ToString();

                string name = _addAnimationName;
                ImGui.InputText("Anim name: ", ref name, 50);
                _addAnimationName = name;

                if (ImGui.Combo("frames", ref _setCurrentFrame, ids, ids.Length))
                {

                }

                if (_frames.Count > 0)
                {
                    ImGui.Spacing();
                    ImGui.Spacing();
                    int id = _frames[_setCurrentFrame].i;
                    ImGui.InputInt("Tile id: ", ref id);
                    _frames[_setCurrentFrame].i = id;
                    ImGui.Spacing();
                    float time = _frames[_setCurrentFrame].time;
                    ImGui.InputFloat("Frame time: ", ref time, 0.1f);
                    _frames[_setCurrentFrame].time = time;
                }

                //ImGui.SetCursorPosY(ImGui.GetWindowSize().Y);
                if (ImGui.Button("Add frame"))
                {
                    _frames.Add(new AnimationFrame(_frames.Count, 0.5f));
                }

                if (ImGui.Button("Aplay"))
                {
                    animRender.AddAnimation(_addAnimationName, new Animation(_frames.ToArray()));
                    animRender.SetAnimPlay(_addAnimationName);

                    _frames.Clear();
                    _setCurrentFrame = 0;

                    ImGui.CloseCurrentPopup();
                }
            }
        }

        private void PopupDraw()
        {
            //OpenSelect();

            OpenColorPicker();

            OpenAddComponent();

            OpenEditAnimation();

            OpenAddAnimation();
        }

        public void Draw()
        {
            PropertieView();
            PopupDraw();
        }
    }
}
