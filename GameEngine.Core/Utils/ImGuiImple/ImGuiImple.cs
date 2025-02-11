using GameEngine.Core.Contents;
using GameEngine.Core.Contents.Assets;
using ImGuiNET;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Utils.ImGuiImple
{
    public class ImGuiImpl
    {
        public static void Image(Sprite sprite, Vector2 size, string str_id = "")
        {
            nint texuteHandle = (nint)sprite.Texture.NativeHandle;

            if (str_id != "")
                ImGui.PushID(str_id);

            ImGui.Image(texuteHandle, size);
        }

        public static void Image(Sprite sprite, Vector2 size, Vector2 uv0, Vector2 uv1, string str_id = "")
        {
            nint texuteHandle = (nint)sprite.Texture.NativeHandle;

            if (str_id != "")
                ImGui.PushID(str_id);

            ImGui.Image(texuteHandle, size, uv0, uv1);
        }

        public static void Image(Texture texture, Vector2 size, string str_id = "")
        {
            nint texuteHandle = (nint)texture.NativeHandle;

            if (str_id != "")
                ImGui.PushID(str_id);

            ImGui.Image(texuteHandle, size);
        }

        public static void Image(Texture texture, Vector2 size, Vector2 uv0, Vector2 uv1, string str_id = "")
        {
            nint texuteHandle = (nint)texture.NativeHandle;

            if (str_id != "")
                ImGui.PushID(str_id);

            ImGui.Image(texuteHandle, size, uv0, uv1);
        }

        public static bool ImageButton(Texture texture, Vector2 size, string str_id = "")
        {
            nint texuteHandle = (nint)texture.NativeHandle;

            if(str_id != "")
                ImGui.PushID(str_id);

            return ImGui.ImageButton(texuteHandle, size);
        }

        public static bool ImageButton(Sprite sprite, Vector2 size, Vector2 uv0, Vector2 uv1, string str_id = "")
        {
            nint texuteHandle = (nint)sprite.Texture.NativeHandle;

            if (str_id != "")
                ImGui.PushID(str_id);

            return ImGui.ImageButton(texuteHandle, size, uv0, uv1);
        }

        public static void DragFloat2(string label, ref Vector2f v)
        {
            var v2 = new Vector2(v.X, v.Y);

            ImGui.DragFloat2(label, ref v2);

            v = new Vector2f(v2.X, v2.Y);
        }

        public static bool CheckBox(string label, ref bool v, Vector2 pos)
        {
            ImGui.SetCursorPos(pos);

            if(ImGui.Checkbox(label, ref v))
                return true;

            return false;

        }

        public static bool ClosedTreeNode(string label, ref bool clouse)
        {
            var startPos = ImGui.GetCursorPos();
            float buttonPosX = ImGui.GetWindowSize().X;
            if(ImGui.TreeNode(label))
            {
                ImGui.SetCursorPos(new Vector2(buttonPosX - 75, startPos.Y));
                clouse = ImGui.SmallButton("remove");
                return true;
            }
            ImGui.SetCursorPos(new Vector2(buttonPosX - 75, startPos.Y));
            clouse = ImGui.SmallButton("remove");

            return false;
        }

        public static bool ClosedTreeNodeEx(string label, ImGuiTreeNodeFlags flags, ref bool clouse)
        {
            var startPos = ImGui.GetCursorPos();
            float buttonPosX = ImGui.GetWindowSize().X;
            if (ImGui.TreeNodeEx(label, flags))
            {
                ImGui.SetCursorPos(new Vector2(buttonPosX - 75, startPos.Y));
                clouse = ImGui.SmallButton("remove");
                return true;
            }
            ImGui.SetCursorPos(new Vector2(buttonPosX - 75, startPos.Y));
            clouse = ImGui.SmallButton("remove");

            return false;
        }

        private static string _selectAssetSearch;

        public static IAsset SelectAsset(AssetType type)
        {
            if (ImGui.Begin("Select asset"))
            {
                ImGui.InputText("", ref _selectAssetSearch, 50);
                ImGui.Spacing();
                var startPos = ImGui.GetCursorPos();

                int x = 0, y = 0;
                int count = 0;
                string? nameAsset = null;

                if (type == AssetType.Sprite)
                    count = AssetsMenager.GetAssetsCount<ImageAsset>(keySearch: _selectAssetSearch, type: type);

                for (int i = 0; i < count; i++)
                {
                    if (startPos.X + x * 75 > ImGui.GetWindowWidth() - 75)
                    {
                        x = 0;
                        y++;
                    }

                    ImGui.SetCursorPos(new Vector2(startPos.X + x * 75, startPos.Y + y * 75 + 20 * y));

                    if (type == AssetType.Sprite)
                    {
                        var asset = AssetsMenager.GetAssetByIndex<ImageAsset>(i, keySearch: _selectAssetSearch, type: AssetType.Sprite);
                        if (ImGui.ImageButton((nint)asset.Texture!.NativeHandle, new Vector2(64, 64)))
                        {
                            return asset;
                        }
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip(asset.Name);

                        nameAsset = asset.Name;
                    }
                    else if(type == AssetType.Sound)
                    {
                        var audioIcon = (nint)AssetsMenager.GetAsset<ImageAsset>("audio_icon").Texture!.NativeHandle;
                        var asset = AssetsMenager.GetAssetByIndex<AudioAsset>(i, keySearch: _selectAssetSearch, type: AssetType.Sprite);
                        if (ImGui.ImageButton(audioIcon, new Vector2(64, 64)))
                        {
                            return asset;
                        }
                        if (ImGui.IsItemHovered())
                            ImGui.SetTooltip(asset.Name);

                        nameAsset = asset.Name;
                    }
                    else if(type == AssetType.Text)
                    {

                    }

                    string name = nameAsset;
                    if (name.Length > 7)
                    {
                        name = name.Remove(7);
                        name += "...";
                    }
                    var textPos = ImGui.GetCursorPos();

                    ImGui.SetCursorPos(new Vector2(startPos.X + x * 75, textPos.Y - 75 * y + y * 75));
                    ImGui.Text(name);

                    x++;
                }

                ImGui.End();
            }

            return null;
        }
    }
}
