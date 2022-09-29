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
        private Player player;

        public Controller(Rectangle formRectangle, PictureBox playerPictureBox)
        {
            player = new Player(formRectangle, playerPictureBox);
        }

        public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }
    }
}
