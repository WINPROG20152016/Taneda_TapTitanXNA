using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TapTitanXNA_JamesTaneda
{
    public class Animation
    {
        public Texture2D texture;
        public float frameTime;
        public bool isLooping;
        public int frames;

        public int FrameCount
        {
            get { return texture.Width / FrameWidth; }
        }

        public int FrameWidth
        {
            get { return texture.Width / frames; }
        }

        public int FrameHeight
        {
            get { return texture.Height; }
        }

        public Animation(Texture2D texture, float frameTime, bool isLooping, int frames)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
            this.frames = frames;
        }

    }
}
