using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace project_2_space_invaders_legin8
{
    internal class Player : Sprite
    {
        private const int SPEEDOFPLAYER = 20;
        private Rectangle formRectangle;
        private Controller controller;
        private Random random;

        
        public Player(int spriteSize, Form form, int xPosition, int yPosition, Rectangle formRectangle, Controller controller, Random random) :
            base (spriteSize, form, xPosition, yPosition)
        {
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            this.controller = controller;
            this.random = random;
            makePlayer();
        }


        private void makePlayer()
        {
            spriteBox = new PictureBox();
            spriteBox.Width = spriteSize;
            spriteBox.Height = spriteSize;
            spriteBox.Image = Properties.Resources.player;
            spriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spriteBox.Location = new Point(formRectangle.Width / 2, formRectangle.Bottom - spriteSize);
            form.Controls.Add(spriteBox);

        }

        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft)
            {
                if (spriteBox.Left != formRectangle.Left) spriteBox.Left -= SPEEDOFPLAYER;
                if (spriteBox.Left < formRectangle.Left) spriteBox.Left = formRectangle.Left;
            }
            if (!moveLeft)
            {
                if (spriteBox.Right != formRectangle.Right) spriteBox.Left += SPEEDOFPLAYER;
                if (spriteBox.Right > formRectangle.Right) spriteBox.Left = formRectangle.Right - spriteBox.Width;
            }
        }
    }
}
