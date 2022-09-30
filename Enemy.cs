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
        }

        private void makeEnemy()
        {
            int nextPlaceRight = 20;
            while (nextPlaceRight < formRectangle.Right - GAPRIGHT)
            {
                int nextPlaceDown = 20, index = 0;
                while (nextPlaceDown < formRectangle.Height)
                {
                    enemies.Add(new PictureBox());
                    enemies[index].Width = spriteSize;
                    enemies[index].Height = spriteSize;
                    enemies[index].Image = Properties.Resources.enemy;
                    enemies[index].SizeMode = PictureBoxSizeMode.StretchImage;
                    form1.Controls.Add(enemies[index]);
                    index++;
                }

            }
        }
    }
}
