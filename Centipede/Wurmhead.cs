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
        bool active = true;
        int elapsedShotMilliseconds = 0;
        int direction = 1;
        bool dY = false;


        public Wurmhead(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {

        }
        public void Update(GameTime gameTime)
        {
            elapsedShotMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (GameConstants.WurmMoveDelay < elapsedShotMilliseconds)
	        {


                if (rectangle.X == 696)
                {
                    direction *= -1;
                    rectangle.Y += 24;
                    rectangle.X -= 24;
                    dY = true;
                }

                if (rectangle.X == 0)
                {
                    direction *= -1;
                    rectangle.Y += 24;
                    rectangle.X += 24;
                    dY = true;
                }

                if (direction == -1)
                {
                    elapsedShotMilliseconds = 0;
                    if (dY == false)
                    {
                        rectangle.X -= 24;
                    }
                }

                if (direction == 1)
                {
                    elapsedShotMilliseconds = 0;
                    if (dY == false)
                    {
                        rectangle.X += 24;
                    }
                }

                

                foreach (Mushroom mushroom in MushroomGrid.mushrooms)
	            {
                    if(rectangle.Intersects(mushroom.Rectangle))
                    {
                        direction *= -1;
                        rectangle.Y += 24;
                    }
	            }

                dY = false;

	        }
        }
    }
}

