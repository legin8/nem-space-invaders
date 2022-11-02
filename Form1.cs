/* Program name: project-2-space-invaders-legin8
Project file name: Form1.cs
Author: Nigel Maynard
Date: 25/10/22
Language: C#
Platform: Microsoft Visual Studio 2022
Purpose: Class work
Description: Assessment game: Space Invaders
Known Bugs:
Additional Features:
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace project_2_space_invaders_legin8
{
    // This is the Form for the space invaders game.
    public partial class Form1 : Form
    {
        // Class variables
        private Controller controller;
        private Random random;
        private SoundPlayer themeMusic;
        private Label menuLabel;

        // Class Constructor
        // Plays the music before the game
        public Form1()
        {
            InitializeComponent();
            themeMusic = new SoundPlayer(@"..\..\Resources\mainGameMusic.wav");
            themeMusic.PlayLooping();
            random = new Random();
            menuLabelMaker();
        }

        // Event handler for key input
        // Moving Left, Right, shooting, pause/resume and reset game
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) controller.MovePlayer(EDirection.LEFT);
            if (e.KeyCode == Keys.Right) controller.MovePlayer(EDirection.RIGHT);
            if (e.KeyCode == Keys.Space) controller.Shot();
            if (e.KeyCode == Keys.Escape) pauseGame();
            if (e.KeyCode == Keys.R) resetGame();
        }

        // This pauses the game by stopping the timer
        private void pauseGame()
        {
            timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled) themeMusic.PlayLooping();
            if (timer1.Enabled) themeMusic.Stop();
        }

        // Timer Event handler, Runs the game
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!controller.RunGame()) timer1.Stop();
            if (!timer1.Enabled) themeMusic.PlayLooping();
        }

        // Click handler for the start button, Makes controller and stops sound and starts the timer
        private void button1_Click(object sender, EventArgs e)
        {
            newGame();
            themeMusic.Stop();
        }

        // This resets the game by clearing everything off the controls and calls newGame
        private void resetGame()
        {
            Controls.Clear();
            BackgroundImage = null;
            menuLabelMaker();
            newGame();
        }

        // This makes the game by creating the controller
        private void newGame()
        {
            controller = new Controller(this, random);
            timer1.Start();
            start.Visible = false;
            Focus();
        }


        private void menuLabelMaker()
        {
            menuLabel = new Label();
            menuLabel.Left = 0;
            menuLabel.Top = 0;
            menuLabel.Width = ClientRectangle.Width;
            menuLabel.Text = "Press Esc to Pause and Resume, Press R to reset/ new game";
            Controls.Add(menuLabel);
        }
    }
}
