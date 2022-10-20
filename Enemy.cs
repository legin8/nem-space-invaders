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

        public Enemy(int spriteSize, Form form, int xPosition, int yPosition, int speed) : base (spriteSize, form, xPosition, yPosition)
        {
            this.speed = speed;
            MakeSprite(xPosition, yPosition);
        }

        private void MakeSprite(int xPosition, int yPosition)
        {
            SpriteBox = new PictureBox();
            SpriteBox.Width = spriteSize;
            SpriteBox.Height = spriteSize;
            SpriteBox.Image = Properties.Resources.enemy;
            SpriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SpriteBox.Location = new Point(xPosition, yPosition);
            form.Controls.Add(SpriteBox);
        }


        public void MoveRight()
        {
            SpriteBox.Left += speed;
        }

        public void MoveLeft()
        {
            SpriteBox.Left -= speed;
        }

        public void MoveDown()
        {
            SpriteBox.Top += speed;
        }
    }
}
