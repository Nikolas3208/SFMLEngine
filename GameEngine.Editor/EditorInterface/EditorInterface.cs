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
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("New"))
                    {

                    }
                    else if (ImGui.MenuItem("Open"))
                    {

                    }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }

        public static Vector2 size;

        private void SceneViewer()
        {
            ImGui.Begin("SceneViewer");

            ImGui.Image(SceneTexture, ImGui.GetContentRegionAvail(), new Vector2(0, 1), new Vector2(1, 0));
            size = ImGui.GetItemRectMin();
            ImGui.End();
        }

        private void SceneTreeViewer()
        {
            ImGui.Begin("Scene objects");

            if (ImGui.TreeNodeEx("GameObjects", ImGuiTreeNodeFlags.DefaultOpen))
            {
                foreach (var go in Scene.GetGameObjects())
                {
                    if (ImGui.Selectable(go.Name))
                    {
                        SelectGameObjectId = go.Id;
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
                        GameObject go = new GameObject(Scene, "GameObject");
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
