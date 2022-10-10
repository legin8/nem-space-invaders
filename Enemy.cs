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
        private const int SPEED = 10;
        private Rectangle formRectangle;
        private int spriteSize;
        private PictureBox pictureBox;

        public PictureBox GetPictureBox => pictureBox;

        public Enemy(Rectangle formRectangle, int spriteSize, PictureBox pictureBox)
        {
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            this.pictureBox = pictureBox;
            
        }


        public void MoveRight()
        {
            pictureBox.Left += SPEED;
        }

        public void MoveLeft()
        {
            pictureBox.Left -= SPEED;
        }

        public void MoveDown()
        {
            pictureBox.Top += SPEED;
        }
    }
}
