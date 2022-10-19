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
        private PictureBox pictureBox;

        public PictureBox GetPictureBox { get => pictureBox; set => pictureBox = value; }

        public Enemy(PictureBox pictureBox, int speed)
        {
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
