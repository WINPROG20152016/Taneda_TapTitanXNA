using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_JamesTaneda
{
    public enum frames { HERO_IDLE = 4, HERO_ATTACK = 2,
                  SUPPORT_IDLE_1 = 6, 
                  SUPPORT_IDLE_2 = 6,
                  SUPPORT_IDLE_3 = 6,
                  SUPPORT_IDLE_4 = 6}

    public class Hero
    {
  

        #region Properties
        Vector2 position;
        Texture2D spriteIdle;
        Texture2D spriteAttack;
        ContentManager content;
        string name;
        int attackPower;
        Level level;
        Animation idleAnimation;
        Animation attackAnimation;
        AnimationPlayer spritePlayer;
        #endregion

        public Hero(ContentManager content, Level level, string name)
        {
            this.content = content;
            this.level = level;
            this.name = name;
        }

        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }
        
        public void LoadContent()
        {
            string imageIdle = "";
            string imageAttack = "";
            float positionAdjustX = 0.0f;
            float positionAdjustY = 0.0f;
            int idleFrames = 1;
            int attackFrames = 1;

            switch (name)
            {
                case "HERO":
                    imageIdle = "HeroSprite/heroIdleTPR";
                    imageAttack = "HeroSprite/heroSheetTPR";
                    positionAdjustX = 0.0f;
                    positionAdjustY = 0.0f;
                    idleFrames = (int)frames.HERO_IDLE;
                    attackFrames = (int)frames.HERO_ATTACK;
                    attackPower = 200;
                    break;
                case "SUPPORT":
                    imageIdle = "SupportSprite/support";
                    positionAdjustX = -100.0f;
                    positionAdjustY = 0.0f;
                    idleFrames = (int)frames.SUPPORT_IDLE_1;
                    attackPower = 50;
                    break;
                case "SUPPORT_2":
                    imageIdle = "SupportSprite/support2";
                    positionAdjustX = 100.0f;
                    positionAdjustY = 0.0f;
                    idleFrames = (int)frames.SUPPORT_IDLE_2;
                    attackPower = 75;
                    break;
                case "SUPPORT_3":
                    imageIdle = "SupportSprite/support3";
                    positionAdjustX = -100.0f;
                    positionAdjustY = -100.0f;
                    idleFrames = (int)frames.SUPPORT_IDLE_3;
                    attackPower = 125;
                    break;
                case "SUPPORT_4":
                    imageIdle = "SupportSprite/support4";
                    positionAdjustX = 100.0f;
                    positionAdjustY = -100.0f;
                    idleFrames = (int)frames.SUPPORT_IDLE_4;
                    attackPower = 150;
                    break;
                default:
                    imageIdle = "SupportSprite/support2";
                    imageAttack = "SupportSprite/support";
                    break;
            }

            spriteIdle = content.Load<Texture2D>(imageIdle);
            idleAnimation = new Animation(spriteIdle, 1.0f, true, idleFrames);
            if (name == "HERO")
            {
                spriteAttack = content.Load<Texture2D>(imageAttack);
                attackAnimation = new Animation(spriteAttack, 1.0f, true, attackFrames);
            }
            int positionX = (Level.windowWidth / 2) - (spriteIdle.Width / 12);
            int positionY = (Level.windowHeight / 2) - (spriteIdle.Height / 4);
            position = new Vector2((float)positionX + positionAdjustX, (float)positionY + positionAdjustY);
            spritePlayer.PlayAnimation(idleAnimation);
        }

        public void Update(GameTime gameTime)
        {
            if (name == "HERO" && level.mouseState.LeftButton == ButtonState.Pressed && level.oldMouseState.LeftButton == ButtonState.Released)
            {
                //position.X++;
                spritePlayer.PlayAnimation(attackAnimation);
            }
            else if (name == "HERO" && spritePlayer.FrameIndex == (int)frames.HERO_ATTACK - 1)
                spritePlayer.PlayAnimation(idleAnimation);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spritePlayer.Draw(gameTime, spriteBatch, position, SpriteEffects.None);
        }
    }
}
