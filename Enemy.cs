using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace project_2_space_invaders_legin8
{
    internal class Enemy
    {
        private const int GAPRIGHT = 200;
        private List<PictureBox> enemies = new List<PictureBox>();
        private Rectangle formRectangle;
        private Form form1;
        private int spriteSize;

        public Enemy(Rectangle formRectangle, int spriteSize, Form form1)
        {
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            this.form1 = form1;
            makeEnemy();
        }

        private void makeEnemy()
        {
            int gap = 20, x = formRectangle.Left + gap, y, index = 0;

            while (x < formRectangle.Right - GAPRIGHT)
            {
                
                y = formRectangle.Top + gap;
                while (y < formRectangle.Height / 2)
                {
                    enemies.Add(new PictureBox());
                    enemies[index].Width = spriteSize;
                    enemies[index].Height = spriteSize;
                    enemies[index].Image = Properties.Resources.enemy;
                    enemies[index].SizeMode = PictureBoxSizeMode.StretchImage;
                    enemies[index].Location = new Point(x, y);
                    form1.Controls.Add(enemies[index]);
                    index++;
                    y += spriteSize + gap;
                }
                x += spriteSize + gap;
            }
        }
    }
}
