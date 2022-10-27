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

namespace project_2_space_invaders_legin8
{
    internal class HighScore
    {
        private const int MAXSCORELIST = 5;
        private string[] highScoreArr;
        private int playerScore, enemyScore;
        private string winnerName;

        public HighScore(int playerScore, int enemyScore, bool winnerIsPlayer)
        {
            this.playerScore = playerScore;
            this.enemyScore = enemyScore;
            highScoreArr = new string[5];
            winnerName = winnerIsPlayer ? "Player" : "Aliens";
            fillArrayFromFile();
            saveToTXTFile();
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
        
        private string makeNewHighScore() => $"Player Destroyed: {playerScore} Aliens || Aliens Destroyed: {enemyScore} || Winner is {winnerName}";
        
    }
}
