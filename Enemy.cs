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
    internal class Enemy : Sprite
    {
        private int speed;

        public PictureBox GetPictureBox { get => pictureBox; set => pictureBox = value; }

        public Enemy(int spriteSize, Form form, int x, int y, int speed) : base (spriteSize, form, x, y)
        {
            this.speed = speed;
            MakeSprite(x, y);
        }

        private void MakeSprite(int x, int y)
        {
            pictureBox = new PictureBox();
            pictureBox.Width = spriteSize;
            pictureBox.Height = spriteSize;
            pictureBox.Image = Properties.Resources.enemy;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Location = new Point(x, y);
            form.Controls.Add(pictureBox);
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
