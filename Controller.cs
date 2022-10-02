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
        private const int GAPRIGHT = 200;
        private Player player;
        private List<Enemy> enemies = new List<Enemy>();
        private Form form1;
        private Rectangle formRectangle;
        private int spriteSize;


        public Controller(Rectangle formRectangle, Form form1, int spriteSize)
        {
            player = new Player(formRectangle, form1, spriteSize);
            this.form1 = form1;
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            makeEnemy();
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
                    enemies.Add(new Enemy(formRectangle,spriteSize, tempPictureBox));
                    form1.Controls.Add(tempPictureBox);
                    index++;
                    y += spriteSize + gap;
                }
                x += spriteSize + gap;
            }
        }

            public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }
    }
}
