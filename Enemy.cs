using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class Enemy
    {
        private const int GAPRIGHT = 200;
        private List<PictureBox> enemies = new List<PictureBox>();
        private Rectangle formRectangle;
        public Enemy(Rectangle formRectangle)
        {
            this.formRectangle = formRectangle;
        }

        private void makeEnemy()
        {
            int nextPlaceRight = 20;
            while (nextPlaceRight < formRectangle.Right - GAPRIGHT)
            {
                int nextPlaceDown = 20;
                while (nextPlaceDown < formRectangle.Height)
                {

                }

            }
        }
    }
}
