using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Graphics
{
    public class SpriteSheet
    {
        public int SubWidth { get; private set; }
        public int SubHeight { get; private set; }
        public int SubCountX { get; private set; }
        public int SubCountY { get; private set; }

        private int _borderSize;

        private Texture _texture;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="a">Кол-во фрагментов по X или размер одного фрагмента в пикселях по ширине</param>
        /// <param name="b">Кол-во фрагментов по Y или размер одного фрагмента в пикселях по высоте</param>
        /// <param name="abIsCount">a и b - это кол-во фрагментов по X и Y?</param>
        /// <param name="borderSize">Толщина рамки между фрагментами</param>
        /// <param name="texW">Ширина текстуры</param>
        /// <param name="texH">Высота текстуры</param>
        public SpriteSheet(int a, int b, bool abIsCount, int borderSize, Texture texture, bool isSmooth = false)
        {
            _texture = texture;
            _texture.Smooth = isSmooth;

            if (borderSize > 0)
            {
                // Сразу увеличим значение на 1, что бы не делать это в просчёте
                _borderSize = borderSize + 1;
            }
            else
                _borderSize = 0;

            // a и b - это кол-во фрагментов по X и Y?
            if (abIsCount)
            {
                SubWidth = (int)Math.Ceiling((float)texture.Size.X / a);
                SubHeight = (int)Math.Ceiling((float)texture.Size.Y / b);
                SubCountX = a;
                SubCountY = b;
            }
            else
            {
                SubWidth = a;
                SubHeight = b;
                SubCountX = (int)Math.Ceiling((float)texture.Size.X / a);
                SubCountY = (int)Math.Ceiling((float)texture.Size.Y / b);
            }
        }

        public void Dispose()
        {
            _texture.Dispose();
            _texture = null;
        }

        public Texture GetTexture() => _texture;

        // Получаем фрагмент текстуры по номеру столбца и строки
        public IntRect GetTextureRect(int i, int j)
        {
            int x = i * SubWidth + i * _borderSize;
            int y = j * SubHeight + j * _borderSize;
            return new IntRect(x, y, SubWidth, SubHeight);
        }
    }
}
