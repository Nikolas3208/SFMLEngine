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

    public class Interface
    {
        private EditorScene _scene;

        public nint SceneTexture;

        public Guid SelectGameObjectId { get; private set; }

        public Interface(EditorScene scene)
        {
            _scene = scene;
        }

        public void Draw()
        {
            ImGui.DockSpaceOverViewport();

            MenuBar();
            SceneViewer();
            PropertiViewer();
            SceneTreeViewer();
            AssetsViewer();
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
        private List<Anim> anims = new List<Anim>();
        private string _animName = string.Empty;
        private int _addAnimCurrentId = -1;
        private AnimRender animRender;
        private void PropertiViewer()
        {
            ImGui.Begin("Properti");

            var gameObject = _scene.GetGameObjectById<GameObject>(SelectGameObjectId);

            if (gameObject == null)
            {
                ImGui.End();
                return;
            }

            if(ImGui.TreeNodeEx("Transform", ImGuiTreeNodeFlags.DefaultOpen))
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


                            for (int i = 0; i < AssetsMenager.GetTextureCount(); i++)
                            {
                                int y = i / 3;
                                int x = i - (y * 3);

                                string localName = AssetsMenager.GetTextureNameById(i);

                                ImGui.SetCursorPos(new Vector2(x * 78 + startPoint.X, y * 85 + startPoint.Y));
                                if (ImGui.ImageButton((nint)AssetsMenager.GetTexture(AssetsMenager.GetTextureNameById(i)).NativeHandle, new Vector2(64, 64)))
                                {
                                    _textureName = localName;

                                    gameObject.GetComponent<SpriteRender>().UpdateTexture(AssetsMenager.GetTexture(_textureName));
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
                            }
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
            if (ImGui.Button("Add component"))
            {
                ImGui.OpenPopup(InterfaceAction.AddComponent.ToString());
            }

            if(ImGui.BeginPopup(InterfaceAction.AddComponent.ToString()))
            {
                int componentId = 0;

                var components = Enum.GetNames(typeof(ComponentType)).ToArray();

                if(ImGui.Combo("Components", ref componentId, components, components.Length))
                {
                    gameObject.AddComponent((ComponentType)componentId);

                    ImGui.CloseCurrentPopup();
                }

                ImGui.EndPopup();
            }


            ImGui.End();

            if (_addAnim)
            {
                if (ImGui.Begin("Add animation"))
                {
                    ImGui.InputText("Anim name: ", ref _animName, 50);


                    if (anims.Count > 0)
                    {
                        string[] ids = new string[anims.Count];

                        for (int i = 0; i < anims.Count; i++)
                        {
                            ids[i] = i.ToString();
                        }

                        ImGui.Combo("Frames", ref _addAnimCurrentId, ids, ids.Length);
                        if (_addAnimCurrentId >= 0)
                        {
                            ImGui.Spacing();
                            ImGui.Spacing();
                            int id = anims[_addAnimCurrentId].Id;
                            ImGui.InputInt("Tile id: ", ref id);
                            anims[_addAnimCurrentId].Id = id;
                            ImGui.Spacing();
                            float time = anims[_addAnimCurrentId].Time;
                            ImGui.InputFloat("Frame time: ", ref time, 0.1f);
                            anims[_addAnimCurrentId].Time = time;
                        }
                    }

                    if (ImGui.Button("Add anim frame"))
                    {
                        anims.Add(new Anim(0, 1));
                    }

                    if (ImGui.Button("Aply"))
                    {
                        if(animRender != null)
                        {
                            var frames = new List<AnimationFrame>();

                            foreach(var frame in anims)
                            {
                                frames.Add(new AnimationFrame(frame.Id, frame.Time));
                            }

                            animRender.AddAnimation(_animName, new Animation(frames.ToArray()));
                            animRender.SetAnimPlay(_animName);
                        }
                        anims = new List<Anim>();
                        _animName = string.Empty;
                        _addAnimCurrentId = 0;
                        _addAnim = false;
                    }

                    ImGui.End();
                }
            }
        }

        private void SceneTreeViewer()
        {
            ImGui.Begin("Scene objects");

            if(ImGui.TreeNodeEx("GameObjects", ImGuiTreeNodeFlags.DefaultOpen))
            {
                foreach(var go in _scene.GetGameObjects())
                {
                    if(ImGui.Selectable(go.Name))
                    {
                        SelectGameObjectId = go.Id;
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
                        GameObject go = new GameObject(_scene, "GameObject");
                        SelectGameObjectId = go.Id;
                        _scene.AddGameObject(go);
                    }
                }
            }

            ImGui.End();
        }

        private void AssetsViewer()
        {
            ImGui.Begin("Assets");

            ImGui.End();
        }
    } 

    public class Anim(int id, float time)
    {
        public int Id { get => id; set => id = value; }
        public float Time { get => time; set => time = value; }
    }
}
