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

using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace project_2_space_invaders_legin8
{
    // This class is responsible for saving, loading and displaying the high score.
    public class HighScore
    {
        // Class variables
        private const int MAXSCORELIST = 5;
        private Form form;
        private string[] highScoreArr;
        private Label[] highScoreLabels;
        private int playerScore, enemyScore;
        private string winnerName;


        public HighScore(bool winnerIsPlayer, Form form)
        {
            this.form = form;
            highScoreArr = new string[5];
            highScoreLabels = new Label[5];
            winnerName = winnerIsPlayer ? "Player" : "Aliens";
            playerScore = winnerIsPlayer ? 1 : 0;
            enemyScore = !winnerIsPlayer ? 1 : 0;
            // These 3 method calls run all the code in this class.
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
        private string makeNewHighScore() => $"Player: {playerScore} | Aliens: {enemyScore} | Winner is {winnerName}";
        
        // Displays 5 messageBoxs with the highScores
        private void displayLabels()
        {
            int labelHeight = form.Height / 20, labelWidth = form.ClientRectangle.Width / 2;
            int xPosistion = (form.Width / 2) - (labelWidth / 2), yPosistion = form.Top + labelHeight;
            // loops with the highScoreLabels array
            for (int i = 0; i < highScoreLabels.Length; i++)
            {
                // Make Label, set hight, width font and size of text, then center. Add space, add to form.
                highScoreLabels[i] = new Label();
                highScoreLabels[i].Text = highScoreArr[i];
                highScoreLabels[i].Height = labelHeight;
                highScoreLabels[i].Left = xPosistion;
                highScoreLabels[i].Top = yPosistion;
                highScoreLabels[i].Width = labelWidth;
                highScoreLabels[i].Font = new Font("Ariel", 18);
                highScoreLabels[i].TextAlign = ContentAlignment.MiddleCenter;
                form.Controls.Add(highScoreLabels[i]);
                yPosistion += labelHeight * 2;
            }
        }
    }
}
