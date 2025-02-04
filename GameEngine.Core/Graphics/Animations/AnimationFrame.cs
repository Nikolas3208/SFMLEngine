namespace GameEngine.Core.Graphics.Animations
{
    public class AnimationFrame
    {
        public int i = -1, j = -1;
        public float time;

        public AnimationFrame(int i, int j, float time)
        {
            this.i = i;
            this.j = j;
            this.time = time;
        }

        public AnimationFrame(int i, float time)
        {
            this.i = i;
            this.time = time;
        }
    }
}
