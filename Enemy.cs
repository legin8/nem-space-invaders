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
        private int speed;
        private Rectangle formRectangle;
        private int spriteSize;
        private PictureBox pictureBox;

        public PictureBox GetPictureBox => pictureBox;

        public Enemy(Rectangle formRectangle, int spriteSize, PictureBox pictureBox, int speed)
        {
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            this.pictureBox = pictureBox;
            this.speed = speed;
        }


        public void MoveRight()
        {
            pictureBox.Left += speed;
        }

        public void MoveLeft()
        {
            pictureBox.Left -= speed;
        }

        public void MoveDown()
        {
            pictureBox.Top += speed;
        }
    }
}
