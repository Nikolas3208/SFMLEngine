using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core.Graphics
{
    public struct WindowSettings
    {
        public string Title {  get; set; }
        public VideoMode VideoMode { get; set; }

        public Styles Styles { get; set; } = Styles.Default;

        public ContextSettings ContextSettings { get; set; }

        public WindowSettings(VideoMode videoMode, string title)
        {
            VideoMode = videoMode;
            Title = title;
        }

        public WindowSettings(VideoMode videoMode, string title, Styles styles)
        {
            VideoMode = videoMode;
            Title = title;
            Styles = styles;
        }

        public WindowSettings(VideoMode videoMode, string title, Styles styles, ContextSettings contextSettings)
        {
            VideoMode = videoMode;
            Title = title;
            Styles = styles;
            ContextSettings = contextSettings;
        }
    }
}
