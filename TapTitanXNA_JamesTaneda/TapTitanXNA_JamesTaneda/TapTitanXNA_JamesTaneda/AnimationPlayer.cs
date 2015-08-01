using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TapTitanXNA_JamesTaneda
{
    struct AnimationPlayer
    {
        public Animation Animation
        {
            get { return animation; }
        }
        Animation animation;

        public int FrameIndex
        {
            get { return frameIndex; }
        }
        int frameIndex;

        private float time;

        public Vector2 Origin
        {
            get { return new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight); }
        }

        public void PlayAnimation(Animation animation)
        {
            if (Animation == animation)
            {
                return;
            }

            this.animation = animation;
            this.frameIndex = 0;
            this.time = 0.0f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
            {
                throw new NotSupportedException("No animation is currently playing.");
            }

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > Animation.frameTime)
            {
                time -= Animation.frameTime;

                if (Animation.isLooping)
                {
                    frameIndex = (frameIndex + 1) % Animation.FrameCount;
                } 
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, Animation.FrameCount - 1);

                }
            }

            Rectangle source = new Rectangle(FrameIndex * Animation.FrameWidth, 0, Animation.FrameWidth, Animation.FrameHeight);

            //spriteBatch.Draw(Animation.texture, position, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.0f);
            spriteBatch.Draw(animation.texture, position, source, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
        }
    }
}
