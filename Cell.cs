using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HW2
{
    class Cell {
        public Point Position { get; private set;}
        public Rectangle Bounds { get; private set; }
        public Life life { get; set; }
    
        public Cell(Point position) {
            Position = position;
            Bounds = new Rectangle(Position.X * Game1.CellSize, Position.Y * Game1.CellSize, Game1.CellSize, Game1.CellSize);
            life = Life.Dead;
        }
        
        public void Update(MouseState mouseState) {
            if ( Bounds.Contains(new Point(mouseState.X, mouseState.Y)) ) {
                // Cells come alive with a left click, and die with a right click
                if (mouseState.LeftButton == ButtonState.Pressed)
                    life = Life.Alive;
                else if (mouseState.RightButton == ButtonState.Pressed)
                    life = Life.Dead;
            }
        }
        public void Draw(SpriteBatch spriteBatch) {
            if(life == Life.Alive)
                spriteBatch.Draw(Game1.Pixel, Bounds, Color.Yellow); 
        }
    }
}
