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
        private PictureBox playerPictureBoxPlayer;
        private Form form1;
        private int spriteSize;
        private List<PictureBox> shots = new List<PictureBox>();

        public Player(Rectangle formRectangle, Form form1, int spriteSize)
        {
            this.formRectangle = formRectangle;
            this.form1 = form1;
            this.spriteSize = spriteSize;
            makePlayer();

        }

        public void Shoot()
        {
            PictureBox tempPictureBoxShot;

            if (shots.Count < 16)
            {
                tempPictureBoxShot = new PictureBox();
                tempPictureBoxShot.Width = spriteSize;
                tempPictureBoxShot.Height = spriteSize;
                tempPictureBoxShot.Image = Properties.Resources.shot;
                tempPictureBoxShot.SizeMode = PictureBoxSizeMode.StretchImage;
                tempPictureBoxShot.Location = new Point(playerPictureBoxPlayer.Left + (spriteSize / 2), playerPictureBoxPlayer.Top + spriteSize);
                shots.Add(tempPictureBoxShot);
                form1.Controls.Add(tempPictureBoxShot);

            }
            
                
        }


        private void makePlayer()
        {
            playerPictureBoxPlayer = new PictureBox();
            playerPictureBoxPlayer.Width = spriteSize;
            playerPictureBoxPlayer.Height = spriteSize;
            playerPictureBoxPlayer.Image = Properties.Resources.player;
            playerPictureBoxPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
            playerPictureBoxPlayer.Location = new Point(formRectangle.Width / 2, formRectangle.Bottom - spriteSize);
            form1.Controls.Add(playerPictureBoxPlayer);
        }

        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft)
            {
                if (playerPictureBoxPlayer.Left != formRectangle.Left) playerPictureBoxPlayer.Left -= SPEEDOFPLAYER;
                if (playerPictureBoxPlayer.Left < formRectangle.Left) playerPictureBoxPlayer.Left = formRectangle.Left;
            }
            if (!moveLeft)
            {
                if (playerPictureBoxPlayer.Right != formRectangle.Right) playerPictureBoxPlayer.Left += SPEEDOFPLAYER;
                if (playerPictureBoxPlayer.Right > formRectangle.Right) playerPictureBoxPlayer.Left = formRectangle.Right - playerPictureBoxPlayer.Width;
            }
        }


    }
}
