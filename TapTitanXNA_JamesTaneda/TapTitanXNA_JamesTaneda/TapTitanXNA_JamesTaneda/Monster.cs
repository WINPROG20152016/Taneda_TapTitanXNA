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
    class Monster
    {
        #region Properties
        Vector2 position;
        Texture2D spriteIdle;
        Texture2D spriteAttacked;
        Texture2D spriteDead;
        ContentManager content;
        string name;
        int lifePoints;
        Level level;
        Animation idleAnimation;
        Animation attackedAnimation;
        Animation deadAnimation;
        AnimationPlayer spritePlayer;
        #endregion

        public Monster(ContentManager content, Level level, string name)
        {
            this.content = content;
            this.level = level;
            this.name = name;
        }
        
        public void LoadContent()
        {
            string imageIdle = "";
            string imageAttacked = "";
            string imageDead = "";
            float positionAdjustX = -30.0f;
            float positionAdjustY = -40.0f;
            int idleFrames = 1;
            int attackedFrames = 1;
            int deadFrames = 1;

            switch (name)
            {
                case "GREEN MONSTER":
                    imageIdle = "GreenMonsterSprite/greenMonsterIdle";
                    imageAttacked = "GreenMonsterSprite/greenMonsterAttacked";
                    imageDead = "GreenMonsterSprite/greenMonsterDead";
                 //   positionAdjustX = -30.0f;
                 //   positionAdjustY = -40.0f;
                    idleFrames = 2;
                    attackedFrames = 2;
                    deadFrames = 2;
                    lifePoints = 1000;
                    break;
                case "RED SLIME":
                    imageIdle = "RedSlimeSprite/redSlimeIdle";
                    imageAttacked = "RedSlimeSprite/redSlimeAttacked";
                    imageDead = "RedSlimeSprite/redSlimeDead";
                    idleFrames = 2;
                    attackedFrames = 2;
                    deadFrames = 2;
                    lifePoints = 2500;
                    break;
                case "BOSS DEMON":
                    imageIdle = "BossDemonSprite/bossDemonIdle";
                    imageAttacked = "BossDemonSprite/bossDemonAttacked";
                    imageDead = "BossDemonSprite/bossDemonDead";
                    idleFrames = 2;
                    attackedFrames = 2;
                    deadFrames = 2;
                    lifePoints = 4000;
                    break;
                default:
                    imageIdle = "SupportSprite/support2";
                    imageAttacked = "SupportSprite/support";
                    break;
            }

            spriteIdle = content.Load<Texture2D>(imageIdle);
            spriteAttacked = content.Load<Texture2D>(imageAttacked);
            spriteDead = content.Load<Texture2D>(imageDead);

            idleAnimation = new Animation(spriteIdle, 1.0f, true, idleFrames);
            attackedAnimation = new Animation(spriteAttacked, 1.0f, false, attackedFrames);
            deadAnimation = new Animation(spriteDead, 1.0f, false, deadFrames);
        
            int positionX = (Level.windowWidth / 2) - (spriteIdle.Width / 12);
            int positionY = (Level.windowHeight / 2) - (spriteIdle.Height / 4);
            position = new Vector2((float)positionX + positionAdjustX, (float)positionY + positionAdjustY);
            spritePlayer.PlayAnimation(idleAnimation);
        }

        public void Update(GameTime gameTime)
        {
            if (lifePoints > 0)
            {
                if (level.mouseState.LeftButton == ButtonState.Pressed && level.oldMouseState.LeftButton == ButtonState.Released)
                {
                    //position.X++;
                    spritePlayer.PlayAnimation(attackedAnimation);
                }
                else if (spritePlayer.FrameIndex == 1)
                    spritePlayer.PlayAnimation(idleAnimation);
            }
            else
            {
                spritePlayer.PlayAnimation(deadAnimation);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spritePlayer.Draw(gameTime, spriteBatch, position, SpriteEffects.None);
        }

        public int LifePoints
        {
            set { lifePoints = value; }
            get { return lifePoints; }
        }

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

    }
}
