using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using project_2_space_invaders_legin8.Properties;

namespace project_2_space_invaders_legin8
{
    public partial class Form1 : Form
    {
        // Class variables
        private Controller controller;
        private Random random = new Random();
        private SoundPlayer soundPlayer = new SoundPlayer(@"..\..\Resources\mainGameMusic.wav");

        // Class Constructor
        public Form1()
        {
            InitializeComponent();
            controller = new Controller(ClientRectangle, this, random);
            soundPlayer.PlayLooping();
        }

        // Event handler for key input
        // Moving Left, Right and Shooting
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) controller.MovePlayer(true);
            if (e.KeyCode == Keys.Right) controller.MovePlayer(false);
            if (e.KeyCode == Keys.Space) controller.Shot();
        }

        // Timer Event handler, Runs the game
        private void timer1_Tick(object sender, EventArgs e)
        {
            controller.RunGame();
        }

        // Event handler for resizing the form
        private void Form1_Resize(object sender, EventArgs e)
        {
            controller.FormRectangle = ClientRectangle;
            controller.SpriteSize = ClientRectangle.Width / 26;
        }
    }
}
