﻿using System;
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
        private const int SPEED = 5, SCALEOFSPRITE = 26;
        private Rectangle formRectangle;
        private Form form;

        private Player player;
        private List<Enemy> enemies;
        private Shot[] shots;

        private int spriteSize, enemyDownCounter;
        private bool goRight, isSideOfScreen;

        // Class Constructor
        public Controller(Rectangle formRectangle, Form form, Random random)
        {
            this.formRectangle = formRectangle;
            this.form = form;
            spriteSize = formRectangle.Width / SCALEOFSPRITE;

            player = new Player(spriteSize, form, formRectangle, formRectangle.Width / 2, formRectangle.Bottom - spriteSize);
            enemies = new List<Enemy>();
            makeEnemy();
            shots = new Shot[15];

            for (int i = 0; i < shots.Length; i++) shots[i] = new Shot(spriteSize, form, formRectangle, random);

            goRight = true;
            isSideOfScreen = false;

        }


        // This is called from the constructor and makes the enemies
        // 4 rows and 10 columns
        private void makeEnemy()
        {
            const int COLUMNS = 10, ROWS = 4;
            const double ENEMYGAP = 1.5;
            int xPosition = formRectangle.Left, yPosition;
            
            // This loop is for the rows of enemies
            for (int i = 0; i < COLUMNS; i++)
            {
                yPosition = formRectangle.Top + spriteSize;

                // This loop is for the columns enemies
                for (int j = 0; j < ROWS; j++)
                {
                    enemies.Add(new Enemy(spriteSize, form, formRectangle, xPosition, yPosition, SPEED));
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
        }

        public void ColisionDetection()
        {
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
        }

        // Code for moving the enemies
        private void moveEnemy()
        {
            const int RESETCOUNTER = 0;
            
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

        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft) player.MoveSprite("LEFT");
            if (!moveLeft) player.MoveSprite("RIGHT");
        }

        // Fires a shot from the player
        public void Shot()
        {
            for (int i = 0; i < shots.Length; i++)
            {
                if (shots[i].SpriteBox == null)
                {
                    shots[i].MakeSprite(player.SpriteBox.Left, player.SpriteBox.Top - spriteSize);
                    break;
                }
            }
        }

        // Drops Bombs on player
        public void DropBomb()
        {

        }
    }
}
