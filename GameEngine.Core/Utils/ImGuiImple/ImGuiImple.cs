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
    }
}
