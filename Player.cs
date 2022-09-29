using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class Player
    {
        private const int SPEEDOFPLAYER = 20;
        private Rectangle formRectangle;
        private PictureBox playerPictureBox;

        public Player(Rectangle formRectangle, PictureBox playerPictureBox)
        {
            this.formRectangle = formRectangle;
            this.playerPictureBox = playerPictureBox;
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
