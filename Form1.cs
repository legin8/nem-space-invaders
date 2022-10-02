﻿using System;
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
        private Controller controller;
        //private SoundPlayer soundPlayer = new SoundPlayer(@"./Resources/mainGameMusic.mp3");

        public Form1()
        {
            InitializeComponent();
            controller = new Controller(ClientRectangle, this, ClientRectangle.Width/ 26);
            //soundPlayer.SoundLocation = @"./Resources/mainGameMusic.mp3";
            //soundPlayer.Play();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) controller.MovePlayer(true);
            if (e.KeyCode == Keys.Right) controller.MovePlayer(false);
            if (e.KeyCode == Keys.Space) controller.Shot();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            controller.RunGame();
        }
    }
}
