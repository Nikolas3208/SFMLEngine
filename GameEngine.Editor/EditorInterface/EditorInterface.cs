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
using GameEngine.Core.Contents.Assets;
using GameEngine.Core.Serializes;
using GameEngine.Core.Utils.ImGuiImple;
using GameEngine.Core.Graphics;
using SFML.Graphics;
using SFML.Window;

namespace GameEngine.Editor.EditorInterface
{
    public class EditorInterface
    {
        public EditorScene Scene { get; set; }
        public Gizmo Gizmo { get; set; }

        public nint SceneTexture;

        public Guid SelectGameObjectId { get; set; }
        public GameObject SelectGameObject { get; set; }

        private AssetsViewer AssetViewer;
        private PropertieViewer PropertieViewer;


        public EditorInterface(EditorScene scene)
        {
            Gizmo = new Gizmo();
            Scene = scene;
            AssetViewer = new AssetsViewer(this, "Assets");
            PropertieViewer = new PropertieViewer(this);
        }

        Vector2f mousePos;
        public void Draw(RenderTarget target, RenderStates states)
        {
            ImGui.DockSpaceOverViewport();

            Gizmo.SetGameObject(SelectGameObject);

            mousePos = (Vector2f)Mouse.GetPosition((RenderWindow)target);
            mousePos = new Vector2f(mousePos.X - sizeMax.X / 2 - sizeMin.X / 2, mousePos.Y - sizeMax.Y / 2 - sizeMin.Y / 2);

            MenuBar();
            SceneViewer();
            PropertieViewer.Draw();
            SceneTreeViewer();
            AssetViewer.Draw();
        }

        private void MenuBar()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("New"))
                    {

                    }
                    else if (ImGui.MenuItem("Open"))
                    {
                        //Scene = SerializeEditorScene.Deserialize(File.ReadAllText("Scene.scene"));
                    }
                    else if(ImGui.MenuItem("Save"))
                    {
                        /*string json = SerializeEditorScene.Serialize(Scene);
                        if (!File.Exists("Scene.scene"))
                        {
                            var sceneFile = File.Create("Scene.scene");
                            sceneFile.Close();
                        }

                        File.WriteAllText("Scene.scene", json);*/
                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }

        public static Vector2 size;
        public static Vector2 sizeMin;
        public static Vector2 sizeMax;

        private void SceneViewer()
        {
            var flag = ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoScrollbar;

            ImGui.Begin("SceneViewer", flag);
            var size = ImGui.GetContentRegionAvail();
            var startPos = ImGui.GetCursorPos();
            ImGui.Image(SceneTexture, size, new Vector2(0, 1), new Vector2(1, 0));
            sizeMin = ImGui.GetItemRectMin();
            sizeMax = ImGui.GetItemRectMax();
            Scene.Camera = new Camera(-new Vector2f(size.X, size.Y) / 2, new Vector2f(size.X, size.Y));
            size = ImGui.GetItemRectMin();
            ImGui.SetCursorPos(startPos);
            if(ImGui.Button("Select"))
            {
                Gizmo.Type = GizmoType.None;
            }
            var size2 = ImGui.GetItemRectSize();
            ImGui.SetCursorPos(new Vector2(startPos.X + size2.X + 5, startPos.Y));
            if(ImGui.Button("Move"))
            {
                Gizmo.Type = GizmoType.Move;
            }
            size2 = ImGui.GetItemRectSize();
            ImGui.SetCursorPos(new Vector2(startPos.X + size2.X + 60, startPos.Y));
            if (ImGui.Button("Scale"))
            {
                Gizmo.Type = GizmoType.Scale;
            }
            size2 = ImGui.GetItemRectSize();
            ImGui.SetCursorPos(new Vector2(startPos.X + size2.X + 100, startPos.Y));
            if (ImGui.Button("Rotation"))
            {
                Gizmo.Type = GizmoType.Rotate;
            }
            ImGui.End();

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                foreach (var go in Scene.GameObjects)
                {
                    if (go.GetFloatRect().Intersects(new FloatRect(mousePos, new Vector2f(5, 5))))
                    {
                        SelectGameObject = go;
                        SelectGameObjectId = go.Id;
                        ScelectCurrentAssetName = string.Empty;
                        return;
                    }
                }
            }
        }

        private void SceneTreeViewer()
        {
            ImGui.Begin("Scene objects");

            if (ImGui.TreeNodeEx("GameObjects", ImGuiTreeNodeFlags.DefaultOpen))
            {
                foreach (var go in Scene.GameObjects)
                {
                    if (ImGui.Selectable(go.Name))
                    {
                        SelectGameObjectId = go.Id;
                        SelectGameObject = go;
                        ScelectCurrentAssetName = string.Empty;
                    }
                }
            }

            if (ImGui.IsMouseClicked(ImGuiMouseButton.Right) && ImGui.IsWindowHovered())
            {
                ImGui.OpenPopup("Scene objects");
            }

            if (ImGui.BeginPopup("Scene objects"))
            {
                ImGui.Separator();
                if (ImGui.BeginMenu("Add"))
                {
                    if (ImGui.MenuItem("GameObject"))
                    {
                        GameObject go = new GameObject("GameObject");
                        SelectGameObjectId = go.Id;
                        ScelectCurrentAssetName = string.Empty;
                        Scene.AddGameObject(go);
                    }
                }
            }

            ImGui.End();
        }


        public string ScelectCurrentAssetName = string.Empty;
    }
}
