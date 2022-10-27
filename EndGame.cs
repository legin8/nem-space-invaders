using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class EndGame
    {
        private readonly Bitmap youWin, youLose;
        private Form form;

        public EndGame(Form form, bool isWinner)
        {
            youWin = Properties.Resources.youwin;
            youLose = Properties.Resources.youlose;
            form.BackgroundImage = isWinner ? youWin : youLose;
        }


    }
}
