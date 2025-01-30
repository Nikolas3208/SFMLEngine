namespace GameEngine.Core.Graphics.Animations
{
    public class AnimationFrame
    {
        public int i, j;
        public float time;

        public AnimationFrame(int i, int j, float time)
        {
            this.i = i;
            this.j = j;
            this.time = time;
        }
    }
}
