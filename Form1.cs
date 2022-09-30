using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    public partial class Form1 : Form
    {
        private Controller controller;
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            controller = new Controller(ClientRectangle, playerPictureBox);
            playerPictureBox.Top = ClientRectangle.Bottom - playerPictureBox.Width;

            PictureBox test = new PictureBox();
            test.Width = 100;
            test.Height = 100;
            
            test.Image = Properties.Resources.enemy;
            test.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(test);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) controller.MovePlayer(true);
            if (e.KeyCode == Keys.Right) controller.MovePlayer(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
