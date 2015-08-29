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
        

        const int MINUTE = 3600;
        const int DAMAGE_TEXT_DURATION = 60;
        const int SUPPORT_DURATION = 180;
        const int UPGRADE_ATTACK = 20000;
        const int ADD_SUPPORT = 15000;
        const int UPGRADE_SUPPORT = 17500;

        public int damageMod = 10;
        public int damage;
        public int supportDamage1;
        public int supportDamage2;
        public int supportDamage3;
        public int supportDamage4;

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
        int time;
        int money;
        int level;
        int swordLevel;
        int supportCount;
        int supportLevel;
        string currentMonster;
        int currentMonsterHP;
        int currentMonsterMaxHP;

        Hero hero;
        Hero support1;
        Hero support2;
        Hero support3;
        Hero support4;
        Monster monster1;
        Monster monster2;
        Monster monster3;
        Monster monster4;
        Monster monster5;

        List<Hero> heroes;

        SpriteFont damageStringFont;
        SpriteFont priceStringFont;
        
        Button playButton;
        Button attackButton;
        Button upgradeAttackButton;
        Button buySupportButton;
        Button upgradeSupportButton;

        #endregion

        public Level(ContentManager content)
        {
            this.content = content;

            time = MINUTE;
            money = 0;
            level = 0;
            swordLevel = 1;
            supportCount = 0;
            supportLevel = 1;
            currentMonster = "";
            currentMonsterHP = 0;
            currentMonsterMaxHP = 0;

            hero = new Hero(content, this, "HERO");
            support1 = new Hero(content, this, "SUPPORT");
            support2 = new Hero(content, this, "SUPPORT_2");
            support3 = new Hero(content, this, "SUPPORT_3");
            support4 = new Hero(content, this, "SUPPORT_4");
            monster1 = new Monster(content, this, "GREEN OGRE");
            monster2 = new Monster(content, this, "RED SLIME");
            monster3 = new Monster(content, this, "BROWN DEMON");
            monster4 = new Monster(content, this, "MAGIC FIEND");
            monster5 = new Monster(content, this, "MINUTE MONSTER");

            /*heroes = new List<Hero>();

            heroes.Add(hero);

            int count = 0;
            foreach (Hero hero1 in heroes)
            {
                hero1 = new Hero(content, this, HeroType.HERO);
                count++;
            }*/

        }

        public void LoadContent()
        {
            background = content.Load<Texture2D>("BackgroundSprite/bg");
            damageStringFont = content.Load<SpriteFont>("Font");
            priceStringFont = content.Load<SpriteFont>("Font");

            playButton = new Button(content, "red_button", Vector2.Zero);
            attackButton = new Button(content, "attack_button", new Vector2(130, 350));
            upgradeAttackButton = new Button(content, "upgradeAtk_button", new Vector2(280, 375));
            buySupportButton = new Button(content, "buySupport_button", new Vector2(15, 375));
            upgradeSupportButton = new Button(content, "upgradeSupport_button", new Vector2(1, 450));

            hero.LoadContent();
            support1.LoadContent();
            support2.LoadContent();
            support3.LoadContent();
            support4.LoadContent();
            monster1.LoadContent();
            monster2.LoadContent();
            monster3.LoadContent();
            monster4.LoadContent();
            monster5.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            
            mouseState = Mouse.GetState();

            mouseY = mouseState.Y;
            mouseX = mouseState.X;
            prev_mpressed = mpressed;
            mpressed = mouseState.LeftButton == ButtonState.Pressed;

            hero.Update(gameTime);
          

            // DETERMINING WHICH MONSTER TO CHOOSE BY LEVEL
            
            if (currentMonsterHP < 1)
            {
                level++;
                time = MINUTE;
                switch (level)
                {
                    case 1:
                        currentMonster = monster1.Name;
                        currentMonsterHP = monster1.LifePoints;
                        break;
                    case 2:
                        currentMonster = monster2.Name;
                        currentMonsterHP = monster2.LifePoints;
                        break;
                    case 3:
                        currentMonster = monster3.Name;
                        currentMonsterHP = monster3.LifePoints;
                        break;
                    case 4:
                        currentMonster = monster4.Name;
                        currentMonsterHP = monster4.LifePoints;
                        break;
                    case 5:
                        currentMonster = monster5.Name;
                        currentMonsterHP = monster5.LifePoints;
                        break;
                    default:
                        break;
                }
                currentMonsterMaxHP = currentMonsterHP;
            }

            // TIMER

            if (time > 0)
                time--;
            else
            {
                currentMonsterHP = currentMonsterMaxHP;
                time = MINUTE;
            }

            // CHOOSING MONSTER BY LEVEL
           
            switch (level)
            {
                case 1:
                    monster1.Update(gameTime);
                    break;
                case 2:
                    monster2.Update(gameTime);
                    break;
                case 3:
                    monster3.Update(gameTime);
                    break;
                case 4:
                    monster4.Update(gameTime);
                    break;
                case 5:
                    monster5.Update(gameTime);
                    break;
                default:
                    break;
            }

            // HOW MANY SUPPORT CHARACTERS WILL APPEAR

            switch (supportCount)
            {
                case 0:
                    break;
                case 1:
                    support1.Update(gameTime);
                    break;
                case 2:
                    support1.Update(gameTime);
                    support2.Update(gameTime);
                    break;
                case 3:
                    support1.Update(gameTime);
                    support2.Update(gameTime);
                    support3.Update(gameTime);
                    break;
                case 4:
                    support1.Update(gameTime);
                    support2.Update(gameTime);
                    support3.Update(gameTime);
                    support4.Update(gameTime);
                    break;
                default:
                    break;
            }

            oldMouseState = mouseState;

            if (damageMod <= 43 * (swordLevel * supportLevel))
                damageMod++;
            else
                damageMod = 10;

            playButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed);

            if (attackButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed))
            {
                damage = hero.AttackPower + ((hero.AttackPower * 30) / damageMod);
                currentMonsterHP -= damage;
                money += (int)((25 * level) + damage);
                isAttacking = true;
            }

            if (upgradeAttackButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed) && (money >= (UPGRADE_ATTACK * swordLevel)) && (swordLevel < 5))
            {
                swordLevel++;
                hero.AttackPower *= swordLevel;
                money -= UPGRADE_ATTACK * (swordLevel - 1);
            }

            if (buySupportButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed) && (money >= (ADD_SUPPORT * supportCount) + 500) && (supportCount < 4))
            {
                supportCount++;
                money -= (ADD_SUPPORT * (supportCount - 1) + 500);
            }

            if (upgradeSupportButton.Update(gameTime, mouseX, mouseY, mpressed, prev_mpressed) && (money >= (UPGRADE_SUPPORT * supportLevel)) && (supportLevel < 5))
            {
                supportLevel++;
                support1.AttackPower *= supportLevel;
                support2.AttackPower *= supportLevel;
                support3.AttackPower *= supportLevel;
                support4.AttackPower *= supportLevel;
                money -= UPGRADE_SUPPORT * (supportLevel - 1);
            }

            if (supportDamageTime < 1)
            {
                switch (supportCount)
                {
                    case 1:
                        supportDamage1 = (support1.AttackPower + (support1.AttackPower * 20) / damageMod);
                        currentMonsterHP -= supportDamage1;
                        break;
                    case 2:
                        supportDamage1 = (support1.AttackPower + (support1.AttackPower * 20) / damageMod);
                        supportDamage2 = (support2.AttackPower + (support2.AttackPower * 20) / damageMod);
                        currentMonsterHP -= supportDamage1 + supportDamage2;
                        break;
                    case 3:
                        supportDamage1 = (support1.AttackPower + (support1.AttackPower * 20) / damageMod);
                        supportDamage2 = (support2.AttackPower + (support2.AttackPower * 20) / damageMod);
                        supportDamage3 = (support3.AttackPower + (support3.AttackPower * 20) / damageMod);
                        currentMonsterHP -= supportDamage1 + supportDamage2 + supportDamage3;
                        break;
                    case 4:
                        supportDamage1 = (support1.AttackPower + (support1.AttackPower * 20) / damageMod);
                        supportDamage2 = (support2.AttackPower + (support2.AttackPower * 20) / damageMod);
                        supportDamage3 = (support3.AttackPower + (support3.AttackPower * 20) / damageMod);
                        supportDamage4 = (support4.AttackPower + (support4.AttackPower * 20) / damageMod);
                        currentMonsterHP -= supportDamage1 + supportDamage2 + supportDamage3 + supportDamage4;
                        break;
                    default:
                        break;
                }
                
                isSupportAttacking = true;
                supportDamageTime = SUPPORT_DURATION;
            }
            else
                supportDamageTime--;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            
            if (level <= 5)
                spriteBatch.DrawString(damageStringFont, currentMonster + "\nHP: " + currentMonsterHP, Vector2.Zero, Color.Red);
            else
                spriteBatch.DrawString(damageStringFont, "You have defeated the Minute Monster! Congratulations!", Vector2.Zero, Color.Red);

            spriteBatch.DrawString(damageStringFont, "Time: " + (time / 60) , new Vector2(0, 50), Color.Red);

            if (isAttacking)
            {
                spriteBatch.DrawString(damageStringFont, damage + "!", new Vector2(180, 100), Color.Red);
                damageTextTime--;
                if (damageTextTime < 1)
                {
                    isAttacking = false;
                    damageTextTime = DAMAGE_TEXT_DURATION;
                }
            }

            if (isSupportAttacking && supportCount > 0)
            {
                switch (supportCount)
                {
                    case 1:
                        spriteBatch.DrawString(damageStringFont, supportDamage1 + "!", new Vector2(75, 200), Color.Red);
                        break;
                    case 2:
                        spriteBatch.DrawString(damageStringFont, supportDamage1 + "!", new Vector2(75, 200), Color.Red);
                        spriteBatch.DrawString(damageStringFont, supportDamage2 + "!", new Vector2(280, 200), Color.Red);
                        break;
                    case 3:
                        spriteBatch.DrawString(damageStringFont, supportDamage1 + "!", new Vector2(75, 200), Color.Red);
                        spriteBatch.DrawString(damageStringFont, supportDamage2 + "!", new Vector2(280, 200), Color.Red);
                        spriteBatch.DrawString(damageStringFont, supportDamage3 + "!", new Vector2(75, 100), Color.Red);
                        break;
                    case 4:
                        spriteBatch.DrawString(damageStringFont, supportDamage1 + "!", new Vector2(75, 200), Color.Red);
                        spriteBatch.DrawString(damageStringFont, supportDamage2 + "!", new Vector2(280, 200), Color.Red);
                        spriteBatch.DrawString(damageStringFont, supportDamage3 + "!", new Vector2(75, 100), Color.Red);
                        spriteBatch.DrawString(damageStringFont, supportDamage4 + "!", new Vector2(280, 100), Color.Red);
                        break;
                    default:
                        break;
                }

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
                    monster1.Draw(gameTime, spriteBatch);
                    break;
                case 2:
                    monster2.Draw(gameTime, spriteBatch);
                    break;
                case 3:
                    monster3.Draw(gameTime, spriteBatch);
                    break;
                case 4:
                    monster4.Draw(gameTime, spriteBatch);
                    break;
                case 5:
                    monster4.Draw(gameTime, spriteBatch);
                    break;
                default:
                    break;
            }

            hero.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(priceStringFont, "P: " + money, new Vector2(250, 50), Color.Black);
                    

            if (swordLevel < 5)
                spriteBatch.DrawString(priceStringFont, "P " + (UPGRADE_ATTACK * swordLevel), new Vector2(300, 350), Color.Black);
            else
                spriteBatch.DrawString(priceStringFont, "UP MAX", new Vector2(300, 350), Color.Black);
            
            if (supportCount < 4)
                spriteBatch.DrawString(priceStringFont, "P " + ((ADD_SUPPORT * supportCount) + 500), new Vector2(35, 350), Color.Black);
            else
                spriteBatch.DrawString(priceStringFont, "UP MAX", new Vector2(35, 350), Color.Black);

            if (supportLevel < 5)
                spriteBatch.DrawString(priceStringFont, "P " + (UPGRADE_SUPPORT * supportLevel), new Vector2(25, 430), Color.Black);
            else
                spriteBatch.DrawString(priceStringFont, "UP MAX", new Vector2(25, 430), Color.Black);

            spriteBatch.DrawString(priceStringFont, "Hero Lv." + swordLevel, new Vector2(280, 430), Color.Blue);
            spriteBatch.DrawString(priceStringFont, "Supt.Lv." + supportLevel, new Vector2(280, 460), Color.Blue);
                    
                
            switch (supportCount)
            {
                case 0:
                    break;
                case 1:
                    support1.Draw(gameTime, spriteBatch);
                    break;
                case 2:
                    support1.Draw(gameTime, spriteBatch);
                    support2.Draw(gameTime, spriteBatch);
                    break;
                case 3:
                    support1.Draw(gameTime, spriteBatch);
                    support2.Draw(gameTime, spriteBatch);
                    support3.Draw(gameTime, spriteBatch);
                    break;
                case 4:
                    support1.Draw(gameTime, spriteBatch);
                    support2.Draw(gameTime, spriteBatch);
                    support3.Draw(gameTime, spriteBatch);
                    support4.Draw(gameTime, spriteBatch);
                    break;
                default:
                    break;
            }
          //  playButton.Draw(gameTime, spriteBatch);
            attackButton.Draw(gameTime, spriteBatch);
            upgradeAttackButton.Draw(gameTime, spriteBatch);
            buySupportButton.Draw(gameTime, spriteBatch);
            upgradeSupportButton.Draw(gameTime, spriteBatch);

        }
    }
}
