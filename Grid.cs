using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HW2
{
    class Grid {

        public Point Size { get; private set; }

        public Cell [,] cells;
        private Life [,] nextGen;

        private TimeSpan updateTimer;

        public Grid() {
            Size = new Point(Game1.CellsX, Game1.CellsY-6);

            nextGen = new Life[Size.X, Size.Y]; 
            cells = new Cell[Size.X, Size.Y];

            for (int i = 0; i < Size.X; i++) {
                for (int j = 0; j < Size.Y; j++) {
                    cells[i, j] = new Cell(new Point(i, j));
                    nextGen[i, j] = Life.Dead;
                }
            }
            updateTimer = TimeSpan.Zero;
        }
        public void Update(GameTime gameTime) {

            MouseState mouseState = Mouse.GetState();

            foreach (Cell cell in cells)
                cell.Update(mouseState);
            if (Game1.Pause)
                return;

            updateTimer += gameTime.ElapsedGameTime;

            if ( updateTimer.TotalMilliseconds > 1000f / Game1.UPS) {
                updateTimer = TimeSpan.Zero;

                /*for(int i = 0; i < Size.X; i++) {
                    for(int j = 0; j < Size.Y; j++) {
                        bool amIAlive = (cells[i, j].life == Life.Alive);
                        int neighbors = checkAround(i, j);
                        Life nextLife = Life.Dead;

                        if (amIAlive && (neighbors == 2 || neighbors == 3))
                            nextLife = Life.Alive;
                        if (!(amIAlive) && (neighbors == 3))
                            nextLife = Life.Alive;
                        if (amIAlive && ((neighbors < 2) || (neighbors > 3)) )
                            nextLife = Life.Dead;
                            
                        nextGen[i, j] = nextLife;                        
                    }
                }*/
                OneGenerationPasses();
                nextGeneration();
            }
            
        }

        public void IncrementGeneration() {
            OneGenerationPasses();
            nextGeneration();
        }

        public void OneGenerationPasses() {
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    bool amIAlive = (cells[i, j].life == Life.Alive);
                    int neighbors = checkAround(i, j);
                    Life nextLife = Life.Dead;

                    if (amIAlive && (neighbors == 2 || neighbors == 3))
                        nextLife = Life.Alive;
                    if (!(amIAlive) && (neighbors == 3))
                        nextLife = Life.Alive;
                    if (amIAlive && ((neighbors < 2) || (neighbors > 3)))
                        nextLife = Life.Dead;

                    nextGen[i, j] = nextLife;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Cell cell in cells)
                cell.Draw(spriteBatch);

            for (int i = 0; i < Size.X; i++)
                spriteBatch.Draw(Game1.Pixel, new Rectangle(i * Game1.CellSize - 1, 0, 1, (Size.Y) * Game1.CellSize), Color.Black);
            for (int j = 0; j < Size.Y+1; j++)
                spriteBatch.Draw(Game1.Pixel, new Rectangle(0, j * Game1.CellSize - 1, (Size.X) * Game1.CellSize, 1), Color.Black);
            
        }
        public int checkAround(int x, int y) {
            int numOfNeighbors = 0;

            /* Check cells in clockwise motion */
            //Right
            if (x != Size.X-1)
                if (cells[x + 1, y].life == Life.Alive)
                    ++numOfNeighbors;
            //Bottom-Right
            if (x != Size.X-1 && y != Size.Y-1 )
                if (cells[x + 1, y + 1].life == Life.Alive)
                    ++numOfNeighbors;
            //Bottom
            if (y != Size.Y-1)
                if (cells[x, y + 1].life == Life.Alive)
                    ++numOfNeighbors;
            //Bottom Left
            if (x != 0 && y != Size.Y-1)
                if (cells[x - 1, y + 1].life == Life.Alive)
                    ++numOfNeighbors;
            //Left
            if (x != 0)
                if (cells[x - 1, y].life == Life.Alive)
                    ++numOfNeighbors;
            //Top Left
            if (x != 0 && y != 0)
                if (cells[x - 1, y - 1].life == Life.Alive)
                    ++numOfNeighbors;
            //Top
            if (y != 0)
                if (cells[x , y - 1].life == Life.Alive)
                    ++numOfNeighbors;
            //Top Right
            if (x != Size.X-1 && y != 0)
                if (cells[x + 1, y - 1].life == Life.Alive)
                    ++numOfNeighbors;

            return numOfNeighbors;
        }

        public void nextGeneration() {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    cells[i, j].life = nextGen[i, j];
        }

        public void Clear() {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    cells[i, j].life = Life.Dead;
        }
    }
}
