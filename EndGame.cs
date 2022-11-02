/* Program name: project-2-space-invaders-legin8
Project file name: Controller.cs
Author: Nigel Maynard
Date: 25/10/22
Language: C#
Platform: Microsoft Visual Studio 2022
Purpose: Class work
Description: Assessment game: Space Invaders
Known Bugs:
Additional Features:
*/

using System.Drawing;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    // This class only sets the background for the form, depending on if you won or not.
    // Only runs if the player dies or all the enemies are destroyed.
    public class EndGame
    {
        // Class variables
        private readonly Bitmap youWin, youLose;

        // Class constructor
        public EndGame(Form form, bool isWinner)
        {
            youWin = Properties.Resources.youwin; // You win picture
            youLose = Properties.Resources.youlose; // You Lose picture
            form.BackgroundImage = isWinner ? youWin : youLose; // Sets the background
        }


    }
}
