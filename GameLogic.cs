/* Program name: project-2-space-invaders-legin8
Project file name: GameLogic.cs
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
using System.Media;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    // This Class holds the logic that runs the game
    public class GameLogic
    {
        // Class Variables
        private const int RESETCOUNTER = 0, MAXSHOTS = 15;

        private Form form;
        private SpriteMaker spriteMaker;
        private Controller controller;
        private Random random;

        private Sprite player;
        private List<Sprite> enemies;
        private List<Sprite> shots;
        private List<Sprite> bombs;

        private SoundPlayer bombSound;
        private int spriteSize, enemyDownCounter;
        private bool isSideOfScreen, goRight;

        // Class constructor
        public GameLogic(Form form, Random random, Controller controller, Sprite player, SpriteMaker spriteMaker, List<Sprite> enemies,
            List<Sprite> shots, List<Sprite> bombs, int scaleOfSprite)
        {
            this.form = form;
            this.random = random;
            this.controller = controller;
            this.player = player;
            this.spriteMaker = spriteMaker;
            this.enemies = enemies;
            this.shots = shots;
            this.bombs = bombs;
            spriteSize = form.ClientRectangle.Width / scaleOfSprite;
            isSideOfScreen = false;
            goRight = true;
            bombSound = new SoundPlayer(@"..\..\Resources\bomb.wav");
        }


        // This calls the logic for moving and checking the enemy sprites
        public void GameSpriteLogic(int speed)
        {
            // Moves the enemies
            moveEnemies(speed);
            movementConditionsChecker();

            // Moves the shots and bombs, if they exist
            if (shots.Count > 0) foreach (Shot shot in shots) if (shot.SpriteBox != null) shot.MoveSprite();
            if (bombs.Count > 0) foreach (Bomb bomb in bombs) if (bomb.SpriteBox != null) bomb.MoveSprite();

            // May or may not drop a bomb from the bottom enemy of each column
            if (enemyDownCounter == RESETCOUNTER) bombLogic();
            colisionDetection();

            // This removes any sprite from the list that has been hit to save on memory, runs last.
            removeSprites();
            if (enemies.Count == 0)
            {
                controller.PlayGame = false;
                controller.PlayerWin = true;
            }
        }


        // Removes the sprites that are not on the screen
        private void removeSprites()
        {
            if (enemies != null) removeFromList(ref enemies);
            if (shots != null) removeFromList(ref shots);
            if (bombs != null) removeFromList(ref bombs);
        }


        // This moves the enemies either left, right or down each time.
        private void moveEnemies(int speed)
        {
            // This will move each enemy Left or Right if not at the side of the screen
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (goRight)
                    {
                        enemy.SpriteEDirection = EDirection.RIGHT;
                        enemy.MoveSprite();
                    }
                    if (!goRight)
                    {
                        enemy.SpriteEDirection = EDirection.LEFT;
                        enemy.MoveSprite();
                    }
                }

            // This moves the enemies down, then increases the down counter
            // If any enemy is at the side of the screen and down counter less than the sprite size
            if (isSideOfScreen && enemyDownCounter <= spriteSize)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.SpriteEDirection = EDirection.DOWN;
                    enemy.MoveSprite();
                }
                enemyDownCounter += speed;
            }
        }


        // This checks any enemy is at the side of the screen or needs to stop going down.
        private void movementConditionsChecker()
        {
            // This runs only if not at the side of the screen
            if (!isSideOfScreen)
            {
                // This checks the last enemy in the list against the side of the screen, if goRight true
                if (goRight &&
                    enemies[enemies.Count - 1].SpriteBox.Right >= form.ClientRectangle.Right)
                {
                    isSideOfScreen = true;
                }

                // This checks the first enemy in the list against the side of the screen, if goRight false
                if (!goRight &&
                    enemies[0].SpriteBox.Left <= form.ClientRectangle.Left)
                {
                    isSideOfScreen = true;
                }
            }


            // Stops the enemies from moving down once they have gone their own size and inverts goRight and isSideOfScreen
            if (isSideOfScreen &&
                enemyDownCounter >= spriteSize)
            {
                enemyDownCounter = RESETCOUNTER;
                goRight = !goRight;
                isSideOfScreen = !isSideOfScreen;
            }
        }

        // Checks if should creates a new shot unless there is already the max shots on the screen.
        // Fires the shot from the players position
        public void MakeShot()
        {
            if (shots.Count < MAXSHOTS) shots.Add(spriteMaker.MakeShot(player));
        }



        // Drops Bombs on player randomly
        private void bombLogic()
        {
            // Calls makeEnemyList method that, returns a list of enemies that are at the bottom of their columns
            List<Sprite> enemyBombList = makeEnemyList();

            // This uses the above list to Create the bomb sprits if random number is 99
            for (int i = 0; i < enemyBombList.Count; i++)
            {
                if (random.Next(100) == 99) bombs.Add(spriteMaker.MakeBomb(enemyBombList[i]));
            }
        }

        // Returns a list of Enemies that have no other enemy sprites under them.
        private List<Sprite> makeEnemyList()
        {
            // This creates a list of the enemies on the bottom of each column
            List<Sprite> tempEnemies = new List<Sprite>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (i == enemies.Count - 1 && enemies[i].SpriteBox != null)
                {
                    tempEnemies.Add(enemies[i]);
                    break;
                }
                else if (enemies[i].SpriteBox != null && enemies[i + 1].SpriteBox != null && enemies[i].SpriteBox.Bottom >= enemies[i + 1].SpriteBox.Bottom)
                {
                    tempEnemies.Add(enemies[i]);
                }
            }
            return tempEnemies;
        }


        // Uses other reusable methods that checks for collisions, doing it this way save on code.
        private void colisionDetection()
        {
            // These only run if Shots and or Bombs exist
            if (shots.Count > 0) colisionChecker(ref shots, ref enemies);
            if (shots.Count > 0 && bombs.Count > 0) colisionChecker(ref shots, ref bombs);
            if (bombs.Count > 0) colisionChecker(ref bombs, ref player);
        }


        // This checks 2 sprites against each other and removes both if they touch, takes 2 lists
        private void colisionChecker(ref List<Sprite> spritesListA, ref List<Sprite> spritesListB)
        {
            // This checks each sprite again every other sprite in the 2 lists and calls the RemoveSprite method if true
            // Sprite list 1
            foreach (Sprite spriteA in spritesListA)
            {
                // Sprite list 2
                foreach (Sprite spriteB in spritesListB)
                {
                    if (spriteA.SpriteBox != null && spriteB.SpriteBox != null &&
                        spriteA.SpriteBox.Top <= spriteB.SpriteBox.Bottom &&
                        spriteA.SpriteBox.Top >= spriteB.SpriteBox.Top &&
                        spriteA.SpriteBox.Left <= spriteB.SpriteBox.Right &&
                        spriteA.SpriteBox.Right >= spriteB.SpriteBox.Left)
                    {
                        spriteA.RemoveSprite(spriteA);
                        spriteB.RemoveSprite(spriteB);
                    }
                }
            }
        }

        // This checks 2 sprites against each other and removes both if they touch, takes 1 list and 1 sprite
        // It takes 2 lists, size doesn't matter.
        private void colisionChecker(ref List<Sprite> spritesListA, ref Sprite sprite)
        {
            // This checks all the sprites in the list against the single sprite, aka the player
            foreach (Sprite spriteList in spritesListA)
            {
                if (spriteList.SpriteBox != null &&
                    sprite.SpriteBox != null &&
                    spriteList.SpriteBox.Bottom >= sprite.SpriteBox.Top &&
                    spriteList.SpriteBox.Left <= sprite.SpriteBox.Right &&
                    spriteList.SpriteBox.Right >= sprite.SpriteBox.Left)
                {
                    spriteList.RemoveSprite(spriteList);
                    form.Controls.Remove(sprite.SpriteBox);
                    sprite = null;
                    bombSound.Play();
                    controller.PlayGame = false;
                    break;
                }
            }
        }


        // This takes a list and removes any entry from the list, where the spriteBox is null.
        // This makes the list smaller over time and uses less resources.
        private void removeFromList(ref List<Sprite> list)
        {
            // Loops with the list given.
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SpriteBox == null)
                {
                    list.RemoveAt(i);
                    // This changes the index the for loop is using because the list just got smaller by 1.
                    i--;
                }
            }
        }
    }
}
