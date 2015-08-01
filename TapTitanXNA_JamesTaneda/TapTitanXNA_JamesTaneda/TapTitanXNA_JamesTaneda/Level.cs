using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_JamesTaneda
{
  
    public class Level
    {
        const int DAMAGE_TEXT_DURATION = 60;
        const int SUPPORT_DURATION = 180;

        public static int windowWidth = 400;
        public static int windowHeight = 500;

        public bool isAttacking = false;
        public bool isSupportAttacking = false;
        public int damageTextTime = DAMAGE_TEXT_DURATION;
        public int supportDamageTextTime = DAMAGE_TEXT_DURATION;
        public int supportDamageTime = SUPPORT_DURATION;

        #region Properties
        ContentManager content;

        Texture2D background;
        public MouseState oldMouseState;
        public MouseState mouseState;
        bool mpressed, prev_mpressed = false;
        int mouseX, mouseY;
        int level;
        string currentMonster;
        int currentMonsterHP;

        Hero hero;
        Hero support1;
        Hero support2;
        Monster monster;
        Monster monster2;
        Monster monster3;

        SpriteFont damageStringFont;
        
        Button playButton;
        Button attackButton;

        #endregion

        public Level(ContentManager content)
        {
            this.content = content;

            level = 0;
            currentMonster = "";
            currentMonsterHP = 0;

            hero = new Hero(content, this, "HERO");
            support1 = new Hero(content, this, "SUPPORT");
            support2 = new Hero(content, this, "SUPPORT_2");
            monster = new Monster(content, this, "GREEN MONSTER");
            monster2 = new Monster(content, this, "RED SLIME");
            monster3 = new Monster(content, this, "BOSS DEMON");
        }

        public void LoadContent()
        {
            background = content.Load<Texture2D>("BackgroundSprite/bg");
            damageStringFont = content.Load<SpriteFont>("Font");

            playButton = new Button(content, "red_button", Vector2.Zero);
            attackButton = new Button(content, "attack_button", new Vector2(130, 350));

            hero.LoadContent();
            support1.LoadContent();
            support2.LoadContent();
            monster.LoadContent();
            monster2.LoadContent();
            monster3.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            
            mouseState = Mouse.GetState();

            mouseY = mouseState.Y;
            mouseX = mouseState.X;
            prev_mpressed = mpressed;
            mpressed = mouseState.LeftButton == ButtonState.Pressed;

            hero.Update(gameTime);
            support1.Update(gameTime);
            support2.Update(gameTime);

            if (currentMonsterHP == 0)
            {
                level++;

                switch (level)
                {
                    case 1:
                        currentMonster = monster.Name;
                        currentMonsterHP = monster.LifePoints;
                        break;
                    case 2:
                        currentMonster = monster2.Name;
                        currentMonsterHP = monster2.LifePoints;
                        break;
                    case 3:
                        currentMonster = monster3.Name;
                        currentMonsterHP = monster3.LifePoints;
                        break;
                    default:
                        break;
                }
            }
           
            switch (level)
            {
                case 1:
                    monster.Update(gameTime);
                    break;
                case 2:
                    monster2.Update(gameTime);
                    break;
                case 3:
                    monster3.Update(gameTime);
                    break;
                default:
                    break;
            }

            oldMouseState = mouseState;

            playButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed);

            if (attackButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed))
            {
                currentMonsterHP -= hero.AttackPower;
                isAttacking = true;
            }

            if (supportDamageTime < 1)
            {
                currentMonsterHP -= (support1.AttackPower + support2.AttackPower);
                isSupportAttacking = true;
                supportDamageTime = SUPPORT_DURATION;
            }
            else
                supportDamageTime--;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            
            if (level <= 3)
                spriteBatch.DrawString(damageStringFont, currentMonster + "\nHP: " + currentMonsterHP, Vector2.Zero, Color.Red);
            else
                spriteBatch.DrawString(damageStringFont, "All monsters defeated!", Vector2.Zero, Color.Red);

            if (isAttacking)
            {
                spriteBatch.DrawString(damageStringFont, hero.AttackPower + "!", new Vector2(180, 100), Color.Red);
                damageTextTime--;
                if (damageTextTime < 1)
                {
                    isAttacking = false;
                    damageTextTime = DAMAGE_TEXT_DURATION;
                }
            }

            if (isSupportAttacking)
            {
                spriteBatch.DrawString(damageStringFont, support1.AttackPower + "!", new Vector2(75, 200), Color.Red);
                spriteBatch.DrawString(damageStringFont, support2.AttackPower + "!", new Vector2(280, 200), Color.Red);
                supportDamageTextTime--;
                if (supportDamageTextTime < 1)
                {
                    isSupportAttacking = false;
                    supportDamageTextTime = DAMAGE_TEXT_DURATION;
                    
                }
            }

         //   spriteBatch.DrawString(damageStringFont, fps + " fps", new Vector2(330, 0), Color.Red);

            switch (level)
            {
                case 1:
                    monster.Draw(gameTime, spriteBatch);
                    break;
                case 2:
                    monster2.Draw(gameTime, spriteBatch);
                    break;
                case 3:
                    monster3.Draw(gameTime, spriteBatch);
                    break;
                default:
                    break;
            }

            hero.Draw(gameTime, spriteBatch);
            support1.Draw(gameTime, spriteBatch);
            support2.Draw(gameTime, spriteBatch);
          //  playButton.Draw(gameTime, spriteBatch);
            attackButton.Draw(gameTime, spriteBatch);

        }
    }
}
