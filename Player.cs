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
        private Rectangle formRectangle;
        private PictureBox playerPictureBox;
        public Player(Rectangle formRectangle, PictureBox playerPictureBox)
        {
            this.formRectangle = formRectangle;
            this.playerPictureBox = playerPictureBox;
        }


        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft) playerPictureBox.Left -= 20;
            if (!moveLeft) playerPictureBox.Left += 20;
        }


    }
}
