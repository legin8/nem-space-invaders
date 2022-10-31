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
        private Random random;
        //private SoundPlayer soundPlayer;

        // Class Constructor
        public Form1()
        {
            InitializeComponent();
            //soundPlayer = new SoundPlayer(@"..\..\Resources\mainGameMusic.wav");
            random = new Random();
        }

        // Event handler for key input
        // Moving Left, Right and Shooting
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) controller.MovePlayer(true);
            if (e.KeyCode == Keys.Right) controller.MovePlayer(false);
            if (e.KeyCode == Keys.Space) controller.Shot();
            if (e.KeyCode == Keys.Escape) pauseGame();
        }

        private void pauseGame()
        {
            timer1.Enabled = !timer1.Enabled;
        }

        // Timer Event handler, Runs the game
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!controller.RunGame()) timer1.Stop();
        }

        // Click handler for the start button, Makes controller and starts sound and the timer
        private void button1_Click(object sender, EventArgs e)
        {
            controller = new Controller(this, random);
            //soundPlayer.PlayLooping();
            timer1.Start();
            start.Visible = false;
            Focus();
        }
    }
}
