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

        public void RunGame()
        {
            PictureBox[] tempPictureBoxes = player.GetShots;
            bool isSideOfScreen = false;

            foreach (Enemy enemy in enemies) if (enemy.GetPictureBox.Right >= formRectangle.Right && goRight) isSideOfScreen = true;

            foreach (Enemy enemy in enemies) if (!isSideOfScreen && enemy.GetPictureBox.Right <= formRectangle.Right && goRight) enemy.MoveRight();

            if (isSideOfScreen && enemyDownCounter <= enemyMoveDownSize)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.MoveDown();
                }
                enemyDownCounter += SPEED;
            }


            for (int i = 0; i < player.GetShots.Length; i++) if (tempPictureBoxes[i] != null) tempPictureBoxes[i].Top -= 10;

            player.GetRidOfShot();
        }

        public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }

        public void Shot()
        {
            player.Shot();
        }
        
    }
}
