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
            animation.Name = name;
            currAnim = animation;
            currAnimName = name;
        }

        // Проигрывает указанную анимацию
        public void Play(string name)
        {
            if (animations.Count < 1 || name == null || name == "")
                return;

            if (currAnimName == name)
                return;

            currAnim = animations[name];
            currAnimName = name;
            currAnim.Reset();
        }

        public IntRect GetTextureRect()
        {
            var currFrame = currAnim.GetFrame(Speed);
            if (currFrame.i > 0 && currFrame.j > 0)
                return ss.GetTextureRect(currFrame.i, currFrame.j);

            return ss.GetTextureRect(currFrame.i);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (animations.Count > 0)
            {
                rectShape.TextureRect = GetTextureRect();

                states.Transform *= Transform;
                target.Draw(rectShape, states);
            }
        }

        public List<string> GetAnimationsName() => animations.Keys.ToList();

        public List<Animation> GetAnimations() => animations.Values.ToList();

        public void UpdateSpriteSheet(SpriteSheet spriteSheet)
        {
            ss.SetTexture(spriteSheet.GetTexture());
            ss = spriteSheet;
        }

        internal void AddAnimation(object name, Animation animation)
        {
            throw new NotImplementedException();
        }
    }
}