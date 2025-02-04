using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Graphics
{
    public class SpriteSheet
    {
        // Размер плитки
        public int SubWidth { get; private set; }
        public int SubHeight { get; private set; }

        //Количиство плиток по осям X, Y
        public int SubCountX { get; private set; }
        public int SubCountY { get; private set; }

        public string Name { get; set; } = string.Empty;

        private int _borderSize;
        private bool _abIsCount;

        private Texture _texture;
        public SpriteSheet()
        {
            
        }

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

            _abIsCount = abIsCount;

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

        // Удаляем текстуру
        public void Dispose()
        {
            _texture.Dispose();
            _texture = null;
        }

        // Получаем текстуру
        public Texture GetTexture() => _texture;

        // Получаем размер плитки
        public Vector2u GetTextureSize() => _texture.Size;

        // Получаем фрагмент текстуры по номеру столбца и строки
        public IntRect GetTextureRect(int i, int j)
        {
            int x = i * SubWidth + i * _borderSize;
            int y = j * SubHeight + j * _borderSize;
            return new IntRect(x, y, SubWidth, SubHeight);
        }

        // Получаем фрагмент текстуры по номеру плитки
        public IntRect GetTextureRect(int i)
        {
            int y = (i / SubCountX);
            int x = i - (y * SubCountX);

            y = y * SubHeight + (i / SubCountY) * _borderSize;
            x = x * SubWidth + (i / SubCountX) * _borderSize;

            return new IntRect(x, y, SubWidth, SubHeight);
        }

        // Получаем размер плитки
        public IntRect GetRectangle()
        {
            return new IntRect(0, 0, SubWidth, SubHeight);
        }

        // Устанавлиаем размер отступа между плитками
        public void SetBorderSize(int borderSize)
        {
            _borderSize = borderSize > 0 ? borderSize + 1 : 0;
        }

        // Устанавливаем размер плитки если !_abIsCount
        // В противном случае устаналваем количиство плиток
        public void SetTileSize(Vector2i size)
        {
            if (_abIsCount)
            {
                SubWidth = (int)Math.Ceiling((float)_texture.Size.X / size.X);
                SubHeight = (int)Math.Ceiling((float)_texture.Size.Y / size.Y);
                SubCountX = size.X;
                SubCountY = size.Y;
            }
            else
            {
                SubWidth = size.X;
                SubHeight = size.Y;
                SubCountX = (int)Math.Ceiling((float)_texture.Size.X / size.X);
                SubCountY = (int)Math.Ceiling((float)_texture.Size.Y / size.Y);
            }
        }

        public Vector2i GetTileSize() => new Vector2i(SubWidth, SubHeight);

        public int GetBorderSize() => _borderSize;

        public void SetTexture(Texture texture)
        {
            _texture = texture;
            SetTileSize((Vector2i)texture.Size);
        }
    }
}
