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
    internal class Player
    {
        private const int SPEEDOFPLAYER = 20;
        private Rectangle formRectangle;
        private PictureBox playerPictureBox;
        private Form form1;
        private int spriteSize;

        public Player(Rectangle formRectangle, Form form1, int spriteSize)
        {
            this.formRectangle = formRectangle;
            this.form1 = form1;
            this.spriteSize = spriteSize;
            makePlayer();
        }


        private void makePlayer()
        {
            playerPictureBox = new PictureBox();
            playerPictureBox.Width = spriteSize;
            playerPictureBox.Height = spriteSize;
            playerPictureBox.Image = Properties.Resources.player;
            playerPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            playerPictureBox.Location = new Point(formRectangle.Width / 2, formRectangle.Bottom - spriteSize);
            form1.Controls.Add(playerPictureBox);
        }

        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft)
            {
                if (playerPictureBox.Left != formRectangle.Left) playerPictureBox.Left -= SPEEDOFPLAYER;
                if (playerPictureBox.Left < formRectangle.Left) playerPictureBox.Left = formRectangle.Left;
            }
            if (!moveLeft)
            {
                if (playerPictureBox.Right != formRectangle.Right) playerPictureBox.Left += SPEEDOFPLAYER;
                if (playerPictureBox.Right > formRectangle.Right) playerPictureBox.Left = formRectangle.Right - playerPictureBox.Width;
            }
        }


    }
}
