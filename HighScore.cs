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
        private bool winnerIsPlayer;

        // This fills the array form the file
        public void FillArrayFromFile()
        {
            StreamReader sr = new StreamReader(@"../../HighScores.txt");
            int index = 0;
            //highScoreArr[index] = makeNewHighScore();
            index++;

            while (index < MAXSCORELIST)
            {
                highScoreArr[index] = sr.ReadLine();
                index++;
            }
            sr.Close();
        }

        // This saves the array to a file
        public void SaveToTXTFile()
        {
            StreamWriter sr = new StreamWriter(@"../../HighScores.txt");

            for (int i = 0; i < highScoreArr.Length; i++)
            {
                sr.WriteLine(highScoreArr[i]);
            }
            sr.Close();
        }

        // This sets the winner and calls the other methods
        public void WhoWon(bool playerWin)
        {
            //winnerIsPlayer = playerWin;
            FillArrayFromFile();
            SaveToTXTFile();
        }

        // This creates a new string for the current finished game
        /*
        private string makeNewHighScore()
        {
            string winnerName = winnerIsPlayer ? playerScore.GetName : cpuScore.GetName;
            return $"{playerScore.GetName}: {playerScore.GetScore} || {cpuScore.GetName}: {cpuScore.GetScore} || Winner is {winnerName}";
        }
        */
    }
}
