using SFML.Graphics;
using SFML.System;

namespace GameEngine.Core.Graphics.Animations
{
    public class AnimSprite : Transformable, Drawable
    {
        public float Speed = 0.05f;

        RectangleShape rectShape;
        SpriteSheet ss; // Набор спрайтов
        SortedDictionary<string, Animation> animations = new SortedDictionary<string, Animation>(); // Список анимаций
        Animation currAnim; // Текущая анимация
        string currAnimName;        // Имя текущей анимации

        // Цвет спрайта
        public Color Color
        {
            set { rectShape.FillColor = value; }
            get { return rectShape.FillColor; }
        }

        // Конструктор
        public AnimSprite(SpriteSheet ss)
        {
            this.ss = ss;

            rectShape = new RectangleShape(new Vector2f(ss.SubWidth, ss.SubHeight));
            rectShape.Origin = new Vector2f(ss.SubWidth / 2, ss.SubHeight / 2);
            rectShape.Texture = ss.GetTexture();

        }

        // Добавить анимацию
        public void AddAnimation(string name, Animation animation)
        {
            animations[name] = animation;
            currAnim = animation;
            currAnimName = name;
        }

        // Проигрывает указанную анимацию
        public void Play(string name)
        {
            if (currAnimName == name)
                return;

            currAnim = animations[name];
            currAnimName = name;
            currAnim.Reset();
        }

        public IntRect GetTextureRect()
        {
            var currFrame = currAnim.GetFrame(Speed);
            return ss.GetTextureRect(currFrame.i, currFrame.j);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            rectShape.TextureRect = GetTextureRect();

            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
    }
}
