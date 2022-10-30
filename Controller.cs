using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace project_2_space_invaders_legin8
{
    internal class Controller
    {
        // Class Variables
        const int RESETCOUNTER = 0;
        private const int SPEED = 5, SCALEOFSPRITE = 26, COLUMNS = 10, ROWS = 4, MAXSHOTS = 15;
        private Rectangle formRectangle;
        private Form form;
        private Random random;

        private SpriteMaker spriteMaker;

        // Sprite Classes
        private Sprite player;
        private List<Sprite> enemies;
        private List<Sprite> shots;
        private List<Sprite> bombs;
        private EndGame endGame;
        private HighScore highScore;

        private int spriteSize, enemyDownCounter;
        private bool goRight, isSideOfScreen;

        // Class Constructor
        public Controller(Form form, Random random)
        {
            spriteMaker = new SpriteMaker(form);
            player = spriteMaker.MakePlayer();
            enemies = spriteMaker.MakeEnemies();

            formRectangle = form.ClientRectangle;
            this.form = form;
            this.random = random;
            spriteSize = formRectangle.Width / SCALEOFSPRITE;

            
            shots = new List<Sprite>();
            bombs = new List<Sprite>();

            shots = new List<Sprite>();

            goRight = true;
            isSideOfScreen = false;

        }


        


        // This runs the game using the timer tick from the form
        public void RunGame()
        {
            if (player != null && enemies != null)
            {
                // Calls method that moves the enemys
                moveEnemy();
                // Moves the shots and bombs each timer tick
                foreach (Shot shot in shots) if (shot.SpriteBox != null) shot.MoveSprite("UP");
                foreach (Bomb bomb in bombs) if (bomb.SpriteBox != null) bomb.MoveSprite("DOWN");
                // May or may not drop a bomb from the bottom enemy of each column
                if (enemyDownCounter == RESETCOUNTER) DropBomb();
                // Calls method for colision detection between PictureBoxs
                ColisionDetection();
                removeSprites();
            }
            if (player == null || enemies == null)
            {
                endGame = new EndGame(form, player == null);
                highScore = new HighScore(player == null, form);
                clearScreenSprites();
            }


        }


        // Code for moving the enemies
        private void moveEnemy()
        {
            // This will move each enemy Left or Right if not at the side of the screen
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (goRight) enemy.MoveSprite("RIGHT");
                    if (!goRight) enemy.MoveSprite("LEFT");
                }

            // This moves the enemy down
            if (isSideOfScreen && enemyDownCounter <= spriteSize)
            {
                foreach (Enemy enemy in enemies) enemy.MoveSprite("DOWN");
                enemyDownCounter += SPEED;
            }

            // Checks if any enemies are at the side if not already at the side
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox.Right >= formRectangle.Right ||
                        enemy.SpriteBox.Left <= formRectangle.Left)
                    {
                        isSideOfScreen = true;
                    }
                }

            // Stops the enemys from moving down once they have gone their own size and inverts goRight and isSideOfScreen
            if (enemyDownCounter >= spriteSize)
            {
                enemyDownCounter = RESETCOUNTER;
                goRight = !goRight;
                isSideOfScreen = !isSideOfScreen;
            }
        }

        
        // Uses another method that checks for colisions, doing it this way save on code.
        public void ColisionDetection()
        {
            colisionChecker(ref shots, ref enemies);
            colisionChecker(ref shots, ref bombs);
            colisionChecker(ref bombs, ref player);
        }

        // This checks 2 sprites against each other and removes both if they touch, takes 2 lists
        private void colisionChecker(ref List<Sprite> spritesListA, ref List<Sprite> spritesListB)
        {
            // This checks each sprite again every other sprite in the 2 lists
            foreach (Sprite spriteA in spritesListA)
            {
                foreach (Sprite spriteB in spritesListB)
                {
                    if (spriteA.SpriteBox != null && spriteB.SpriteBox != null &&
                        spriteA.SpriteBox.Top <= spriteB.SpriteBox.Bottom && spriteA.SpriteBox.Top >= spriteB.SpriteBox.Top &&
                        spriteA.SpriteBox.Left <= spriteB.SpriteBox.Right && spriteA.SpriteBox.Right >= spriteB.SpriteBox.Left)
                    {
                        spriteA.RemoveSprite(spriteA);
                        spriteB.RemoveSprite(spriteB);
                    }
                }
            }
        }

        // This checks 2 sprites against each other and removes both if they touch, takes 1 list and 1 sprite
        private void colisionChecker(ref List<Sprite> spritesListA, ref Sprite sprite)
        {
            // This checks all the sprites in the list against the single sprite
            foreach (Sprite spriteList in spritesListA)
            {
                if (spriteList.SpriteBox != null && sprite.SpriteBox != null &&
                    spriteList.SpriteBox.Bottom >= sprite.SpriteBox.Top &&
                    spriteList.SpriteBox.Left <= sprite.SpriteBox.Right &&
                    spriteList.SpriteBox.Right >= sprite.SpriteBox.Left)
                {
                    spriteList.RemoveSprite(spriteList);
                    form.Controls.Remove(sprite.SpriteBox);
                    sprite = null;
                    break;
                }
            }
        }


        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft && player != null) player.MoveSprite("LEFT");
            if (!moveLeft && player != null) player.MoveSprite("RIGHT");
        }

        // Fires a shot from the player
        public void Shot()
        {
            if (shots.Count < MAXSHOTS)
            {
                shots.Add(new Shot(spriteSize,form,random, player.SpriteBox.Left, player.SpriteBox.Top - spriteSize));
            }
        }


        // Removes the sprites that are not on the screen
        private void removeSprites()
        {
            if (enemies != null) removeFromList(ref enemies);
            if (shots != null) removeFromList(ref shots);
            if (bombs != null) removeFromList(ref bombs);
        }

        // This takes a list and removes the entry from the list
        private void removeFromList(ref List<Sprite> list)
        {
            // Removes shots from the list
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SpriteBox == null)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }


        // Drops Bombs on player
        public void DropBomb()
        {
            // this creates a list of the enemies on the bottom
            List<Sprite> tempEnemies = new List<Sprite>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (i == enemies.Count - 1 && enemies[i].SpriteBox != null)
                {
                    tempEnemies.Add(enemies[i]);
                    break;
                } else if (enemies[i].SpriteBox != null && enemies[i + 1].SpriteBox != null && enemies[i].SpriteBox.Bottom >= enemies[i+1].SpriteBox.Bottom) {
                    tempEnemies.Add(enemies[i]);
                }
            }

            // This uses the above list to create bomb sprites
            for (int i = 0; i < tempEnemies.Count; i++)
            {
                if (random.Next(100) == 99)
                {
                    Console.WriteLine("Bomb Dropped");
                    bombs.Add(new Bomb(spriteSize, form, tempEnemies[i].SpriteBox.Left,
                        tempEnemies[i].SpriteBox.Bottom, random));
                }
            }
        }
        
        private void clearScreenSprites()
        {
            if (shots != null) destroyEverySprite(ref shots);
            if (enemies != null) destroyEverySprite(ref enemies);
            if (bombs != null) destroyEverySprite(ref bombs);
            if (player != null) destroySprite(ref player);
        }

        private void destroyEverySprite(ref List<Sprite> spriteList)
        {
            for (int i = 0; i < spriteList.Count; i++) form.Controls.Remove(spriteList[i].SpriteBox);
            spriteList = null;
        }

        private void destroySprite(ref Sprite sprite)
        {
            form.Controls.Remove(sprite.SpriteBox);
            sprite = null;
        }

    }
}
