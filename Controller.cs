using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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

        private Player player;
        private List<Sprite> enemies;
        private List<Sprite> shots;
        private List<Sprite> bombs;

        private int spriteSize, enemyDownCounter;
        private bool goRight, isSideOfScreen;

        // Class Constructor
        public Controller(Rectangle formRectangle, Form form, Random random)
        {
            this.formRectangle = formRectangle;
            this.form = form;
            this.random = random;
            spriteSize = formRectangle.Width / SCALEOFSPRITE;

            player = new Player(spriteSize, form, formRectangle.Width / 2, formRectangle.Bottom - spriteSize);
            enemies = new List<Sprite>();
            makeEnemy();
            shots = new List<Sprite>();
            bombs = new List<Sprite>();

            shots = new List<Sprite>();

            goRight = true;
            isSideOfScreen = false;

        }


        // This is called from the constructor and makes the enemies
        // 4 rows and 10 columns
        private void makeEnemy()
        {
            const double ENEMYGAP = 1.5;
            int xPosition = formRectangle.Left, yPosition;
            
            // This loop is for the rows of enemies
            for (int i = 0; i < COLUMNS; i++)
            {
                yPosition = formRectangle.Top + spriteSize;

                // This loop is for the columns enemies
                for (int j = 0; j < ROWS; j++)
                {
                    enemies.Add(new Enemy(spriteSize, form, xPosition, yPosition, SPEED));
                    yPosition += (int) (spriteSize * ENEMYGAP);
                }
                // This moves to the next column
                xPosition += spriteSize + (int)(spriteSize * ENEMYGAP);
            }
        }


        // This runs the game using the timer tick from the form
        public void RunGame()
        {
            // Calls method that moves the enemys
            moveEnemy();
            // Moves the shot each timer tick
            foreach (Shot shot in shots) if (shot.SpriteBox != null) shot.MoveSprite("UP");
            // Calls method for colision detection between PictureBoxs
            ColisionDetection();
            if (enemyDownCounter == RESETCOUNTER) DropBomb();
            moveBombs();
            removeSprites();
        }





        // Code for moving the enemies
        private void moveEnemy()
        {
            // This will move each enemy Left and Right
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox != null && goRight) enemy.MoveSprite("RIGHT");
                    if (enemy.SpriteBox != null && !goRight) enemy.MoveSprite("LEFT");
                }

            // This moves the enemy down the size of the sprites
            if (isSideOfScreen && enemyDownCounter <= spriteSize)
            {
                foreach (Enemy enemy in enemies) if (enemy.SpriteBox != null)
                    {
                        enemy.MoveSprite("DOWN");
                    }
                enemyDownCounter += SPEED;
            }

            // Checks if any enemies are at the side if not already at the side
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox != null && enemy.SpriteBox.Right >= formRectangle.Right ||
                        enemy.SpriteBox != null && enemy.SpriteBox.Left <= formRectangle.Left)
                    {
                        isSideOfScreen = true;
                    }
                }

            // Stops the enemys from moving down once they have gone their own size and inverts goRight
            if (enemyDownCounter >= spriteSize)
            {
                enemyDownCounter = RESETCOUNTER;
                goRight = !goRight;
                isSideOfScreen = !isSideOfScreen;
            }
        }

        private void colisionChecker(ref List<Sprite> spritesListA, ref List<Sprite> spritesListB)
        {
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

        
        public void ColisionDetection()
        {

            colisionChecker(ref shots, ref enemies);
            /*
            // This checks for a colision between the shot and the enemy sprite
            foreach (Shot shot in shots)
            {
                foreach (Enemy enemy in enemies)
                {
                    // This starts by checking if shot and enemy PictureBox exist before checking the rest
                    // if they don't exist it won't look passed the first false
                    if (shot.SpriteBox != null && enemy.SpriteBox != null &&
                        shot.SpriteBox.Top <= enemy.SpriteBox.Bottom && shot.SpriteBox.Top >= enemy.SpriteBox.Top &&
                        shot.SpriteBox.Left <= enemy.SpriteBox.Right && shot.SpriteBox.Right >= enemy.SpriteBox.Left)
                    {
                        shot.RemoveSprite(shot);
                        enemy.RemoveSprite(enemy);
                    }
                }
            }

            // This checks for a colision between the bomb and the shots
            foreach (Bomb bomb in bombs)
            {
                foreach (Shot shot in shots)
                {
                    if (shot.SpriteBox != null && bomb.SpriteBox != null &&
                        shot.SpriteBox.Top <= bomb.SpriteBox.Bottom && shot.SpriteBox.Top >= bomb.SpriteBox.Top &&
                        shot.SpriteBox.Left <= bomb.SpriteBox.Right && shot.SpriteBox.Right >= bomb.SpriteBox.Left)
                    {
                        shot.RemoveSprite(shot);
                        bomb.RemoveSprite(bomb);
                    }
                }
            }
            */

            // This checks for a colision between the Bombs and the player
            foreach (Bomb bomb in bombs)
            {
                if (bomb.SpriteBox != null && player.SpriteBox != null &&
                    bomb.SpriteBox.Bottom >= player.SpriteBox.Top &&
                    bomb.SpriteBox.Left <= player.SpriteBox.Right &&
                    bomb.SpriteBox.Right >= player.SpriteBox.Left)
                {
                    player.RemoveSprite(player);
                }
            }

        }

        

        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft && player.SpriteBox != null) player.MoveSprite("LEFT");
            if (!moveLeft && player.SpriteBox != null) player.MoveSprite("RIGHT");
        }

        // Fires a shot from the player
        public void Shot()
        {
            if (shots.Count < MAXSHOTS && player.SpriteBox != null)
            {
                shots.Add(new Shot(spriteSize,form,random, player.SpriteBox.Left, player.SpriteBox.Top - spriteSize));
            }
        }









        // Removes the sprites
        private void removeSprites()
        {
            // Removes enemies from the list
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].SpriteBox == null)
                {

                    enemies.RemoveAt(i);
                    
                }
            }

            // Removes shots from the list
            for (int i = 0; i < shots.Count; i++)
            {
                if (shots[i].SpriteBox == null)
                {
                    shots.RemoveAt(i);
                }
            }

            // Removes the bomb from the list if the sprite is gone
            for (int i = 0; i < bombs.Count; i++)
            {
                if (bombs[i].SpriteBox == null) bombs.RemoveAt(i);
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

        // This Moves the Bombs
        private void moveBombs()
        {
            foreach (Bomb bomb in bombs) if (bomb.SpriteBox != null) bomb.MoveSprite("DOWN");
        }
    }
}
