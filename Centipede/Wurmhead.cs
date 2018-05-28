using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Centipede
{
    public class Wurmhead : Sprite
    {

        public bool active = true;
        int elapsedShotMilliseconds = 0;
        public int direction = 1;
        bool moved = false;

        public int wurmLength = 2;
        
        

        public List<Rectangle> oldCoordinates = new List<Rectangle>();

        


        public Wurmhead(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position, int wurmlength) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {
            wurmLength = wurmlength;
        }
        public void Update(GameTime gameTime)
        {
            if(oldCoordinates.Count > wurmLength) oldCoordinates.RemoveAt(oldCoordinates.Count - 1);

            if(rectangle.Y > GameConstants.WindowHeight) active = false;

            elapsedShotMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (GameConstants.WurmMoveDelay < elapsedShotMilliseconds)
	        {
                if(direction == 1) rectangle.X += 24;
                if(direction == -1) rectangle.X -= 24;
                oldCoordinates.Insert(0, rectangle);


                if (rectangle.X == 0 || rectangle.X == GameConstants.WindowWidth - SpriteWidth)
                {
                    Rectangle tempRectangle = rectangle;
                    tempRectangle.Y += 24;

                    if (MushroomCollsion(tempRectangle) == false)
                    {
                         direction *= -1;
                         rectangle.Y += 24;
                         oldCoordinates.Insert(0, rectangle);

                    }else{

                        direction *= -1;
                    }
                }


                elapsedShotMilliseconds = 0;
                
	        }

        }
        public bool MushroomCollsion(Rectangle r)
        {
            bool intersects = false;
            foreach(Mushroom m in MushroomGrid.mushrooms)
            {
                if (r.Intersects(m.Rectangle)) intersects = true;
            }

            return intersects;
        }
        

        public bool Active { get { return active; } set { active = value; } }
        public int Direction { get { return direction; } set { direction = value; } }
        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }
        public int WurmLength { get { return wurmLength; } set { wurmLength = value; } }

        public override void Draw()
        {
            foreach (Rectangle rectangle in oldCoordinates)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
            base.Draw();
        }
    }
}

