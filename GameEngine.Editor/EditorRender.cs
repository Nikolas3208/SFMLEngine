using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Editor
{
    public static class EditorRender
    {
        private static List<Drawable> drawables = new List<Drawable>();

        public static void AddRectangle(FloatRect? rect, Color color)
        {
            if (rect == null)
                return;

            var shape = new RectangleShape();
            shape.Size = new Vector2f(rect.Value.Width, rect.Value.Height);
            shape.Position = new Vector2f(rect.Value.Left, rect.Value.Top);
            shape.FillColor = Color.Transparent;
            shape.OutlineColor = color;
            shape.OutlineThickness = 2;

            drawables.Add(shape);
        }

        public static void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var drawable in drawables)
                drawable.Draw(target, states);

            drawables.Clear();
        }
    }
}
