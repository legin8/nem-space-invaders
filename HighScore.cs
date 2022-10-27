/* Program name: project-2-space-invaders-legin8
Project file name: HighScore.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class HighScore
    {
        private const int MAXSCORELIST = 5;
        private Form form;
        private string[] highScoreArr;
        private Label[] highScoreLabels;
        private int playerScore, enemyScore;
        private string winnerName;

        public HighScore(int playerScore, int enemyScore, bool winnerIsPlayer, Form form)
        {
            this.playerScore = playerScore;
            this.enemyScore = enemyScore;
            this.form = form;
            highScoreArr = new string[5];
            highScoreLabels = new Label[5];
            winnerName = winnerIsPlayer ? "Player" : "Aliens";
            fillArrayFromFile();
            saveToTXTFile();
            displayLabels();
        }

        // This fills the array form the file
        private void fillArrayFromFile()
        {
            StreamReader sr = new StreamReader(@"../../HighScores.txt");
            highScoreArr[0] = makeNewHighScore();

            for (int i = 1; i < MAXSCORELIST; i++) highScoreArr[i] = sr.ReadLine();
            sr.Close();
        }

        // This saves the array to a file
        private void saveToTXTFile()
        {
            StreamWriter sr = new StreamWriter(@"../../HighScores.txt");

            for (int i = 0; i < highScoreArr.Length; i++)
            {
                sr.WriteLine(highScoreArr[i]);
            }
            sr.Close();
        }

        // This creates a new string for the current finished game
        private string makeNewHighScore() => $"Player Destroyed: {playerScore} Aliens || Aliens Destroyed: {enemyScore} Player || Winner is {winnerName}";
        
        // Displays a messageBox with the highScores
        private void displayLabels()
        {
            int labelHeight = form.Height / 20;
            int xPosistion = form.Width / 2, yPosistion = form.Top + labelHeight;
            for (int i = 0; i < highScoreLabels.Length; i++)
            {
                highScoreLabels[i] = new Label();
                highScoreLabels[i].Text = highScoreArr[i];
                highScoreLabels[i].Height = labelHeight;
                highScoreLabels[i].Left = xPosistion;
                highScoreLabels[i].Top = yPosistion;
                form.Controls.Add(highScoreLabels[i]);
                yPosistion += labelHeight * 2;
            }
        }
    }
}
