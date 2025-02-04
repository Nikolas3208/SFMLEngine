
namespace GameEngine.Core.Graphics.Animations
{
    public class Animation
    {
        // Кадры
        public AnimationFrame[] frames;

        public string Name { get; set; }

        float timer;
        AnimationFrame currFrame;
        int currFrameIndex;

        public Animation(params AnimationFrame[] frames)
        {
            if (frames != null)
                this.frames = frames;
            Reset();
        }

        // Начать проигрывание анимации сначала
        public void Reset()
        {
            timer = 0f;
            currFrameIndex = 0;
            currFrame = frames[currFrameIndex];
        }

        // Следующий кадр
        public void NextFrame()
        {
            timer = 0f;
            currFrameIndex++;

            if (currFrameIndex == frames.Length)
                currFrameIndex = 0;

            currFrame = frames[currFrameIndex];
        }

        // Получить текущий кадр
        public AnimationFrame GetFrame(float speed)
        {
            timer += speed;

            if (timer >= currFrame.time)
                NextFrame();

            return currFrame;
        }
    }
}
