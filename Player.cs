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
        private PictureBox[] shots;
        private int[] shotsTimeToLive;
        private Controller controller;
        private Random random;

        public PictureBox[] GetShots => shots;

        public Player(Rectangle formRectangle, Form form1, int spriteSize, Controller controller, Random random)
        {
            this.formRectangle = formRectangle;
            this.form1 = form1;
            this.spriteSize = spriteSize;
            this.controller = controller;
            this.random = random;
            makePlayer();
            shots = new PictureBox[15];
            shotsTimeToLive = new int[15];

        }

        public void Shot()
        {
            PictureBox tempPictureBoxShot;
            
            for(int i = 0; i < shots.Length; i++)
            {
                if (shots[i] == null)
                {
                    tempPictureBoxShot = new PictureBox();
                    tempPictureBoxShot.Width = spriteSize;
                    tempPictureBoxShot.Height = spriteSize;
                    tempPictureBoxShot.Image = Properties.Resources.shot;
                    tempPictureBoxShot.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBoxShot.Location = new Point(playerPictureBoxPlayer.Left, playerPictureBoxPlayer.Top - spriteSize);
                    shots[i] = tempPictureBoxShot;
                    shotsTimeToLive[i] = random.Next(1, 71);
                    form1.Controls.Add(tempPictureBoxShot);
                    break;
                }
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

        public void GetRidOfShot()
        {
            List<Enemy> tempEnemie = controller.GetEnemies;

            // Checks if the shot is hitting the top of the screen
            for(int i = 0; i < shots.Length; i++)
            {
                if (shots[i] != null && shots[i].Top <= formRectangle.Top) RemoveShot(i);
                shotsTimeToLive[i]--;
            }

            // Checks if the shots are hitting the enemy
            for (int j = 0; j < shots.Length; j++)
            {
                for (int k = 0; k < tempEnemie.Count; k++)
                {
                    if (tempEnemie[k] != null && shots[j] != null && tempEnemie[k].GetPictureBox != null &&
                        shots[j].Top <= tempEnemie[k].GetPictureBox.Bottom && shots[j].Top >= tempEnemie[k].GetPictureBox.Top &&
                        shots[j].Left <= tempEnemie[k].GetPictureBox.Right && shots[j].Right >= tempEnemie[k].GetPictureBox.Left)
                    {
                        RemoveShot(j);
                        controller.DestroyEnemy(k);
                    }
                }
            }

            for(int shot = 0; shot < shots.Length; shot++)
            {
                if (shotsTimeToLive[shot] == 0)
                {
                    RemoveShot(shot);
                    shotsTimeToLive[shot]--;
                }
            }
        }



        private void RemoveShot(int shot)
        {
            form1.Controls.Remove(shots[shot]);
            shots[shot] = null;
        }


    }
}
