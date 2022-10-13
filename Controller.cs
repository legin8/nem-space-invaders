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
        private const int GAPRIGHT = 200, SPEED = 5;
        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private Form form1;
        private Rectangle formRectangle;
        private int spriteSize, enemyMoveDownSize, enemyDownCounter;
        private bool goRight;

        public List<Enemy> GetEnemies => enemies;

        public Controller(Rectangle formRectangle, Form form1, int spriteSize, Random random)
        {
            player = new Player(formRectangle, form1, spriteSize, this, random);
            this.form1 = form1;
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            makeEnemy();
            goRight = true;
            enemyMoveDownSize = spriteSize;
            enemyDownCounter = 0;
        }


        
        private void makeEnemy()
        {
            int gap = 20, x = formRectangle.Left + gap, y, index = 0;
            PictureBox tempPictureBox;

            while (x < formRectangle.Right - GAPRIGHT)
            {

                y = formRectangle.Top + gap;
                while (y < formRectangle.Height / 2)
                {
                    tempPictureBox = new PictureBox();
                    tempPictureBox.Width = spriteSize;
                    tempPictureBox.Height = spriteSize;
                    tempPictureBox.Image = Properties.Resources.enemy;
                    tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBox.Location = new Point(x, y);
                    enemies.Add(new Enemy(formRectangle,spriteSize, tempPictureBox, SPEED));
                    form1.Controls.Add(tempPictureBox);
                    index++;
                    y += spriteSize + gap;
                }
                x += spriteSize + gap;
            }
        }

        // This runs the game
        public void RunGame()
        {
            PictureBox[] tempPictureBoxes = player.GetShots;
            moveEnemy();

            for (int i = 0; i < player.GetShots.Length; i++) if (tempPictureBoxes[i] != null) tempPictureBoxes[i].Top -= 10;
            player.GetRidOfShot();
        }

        // Code for moving the enemies
        private void moveEnemy()
        {
            bool isSideOfScreen = false;
            // Only one of these will run each timer tick
            // These both check if any enemy is at the edge of the screen
            if (goRight) foreach (Enemy enemy in enemies) if (enemy.GetPictureBox.Right >= formRectangle.Right) isSideOfScreen = true;
            if (!goRight) foreach (Enemy enemy in enemies) if (enemy.GetPictureBox.Left <= formRectangle.Left) isSideOfScreen = true;

            // This will move each enemy Right
            if (!isSideOfScreen) foreach (Enemy enemy in enemies) if (goRight && enemy.GetPictureBox.Right <= formRectangle.Right) enemy.MoveRight();

            // This will move each enemy Left
            if (!isSideOfScreen) foreach (Enemy enemy in enemies) if (!goRight && enemy.GetPictureBox.Left >= formRectangle.Left) enemy.MoveLeft();

            // This moves the enemy down the size of the sprites
            if (isSideOfScreen && enemyDownCounter <= enemyMoveDownSize)
            {
                foreach (Enemy enemy in enemies) enemy.MoveDown();
                enemyDownCounter += SPEED;
            }

            // Stops the enemys from moving down once they have gone their own size and inverts goRight
            if (enemyDownCounter >= enemyMoveDownSize)
            {
                enemyDownCounter = 0;
                goRight = !goRight;
            }
        }

        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }

        // Fires a shot from the player
        public void Shot()
        {
            player.Shot();
        }
        
    }
}
