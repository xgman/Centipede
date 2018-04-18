using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Centipede
{
    class MushroomGrid
    {
        List<Mushroom> mushrooms;
        Random random = new Random();

        public MushroomGrid(SpriteBatch spriteBatch, Texture2D texture)
        {
            mushrooms = new List<Mushroom>();

            for (int count = 0; count < 50; count++)
            {
                int x, y;
                x = random.Next(30);
                y = random.Next(2, 29);
                mushrooms.Add(new Mushroom(spriteBatch, texture, new Vector2(x * GameConstants.SpriteSize, y * GameConstants.SpriteSize)));
            }
        }
        public void Draw()
        {
            for (int count = 0; count < mushrooms.Count; count++)
                mushrooms[count].Draw();
        }
        public int MushroomCount { get { return mushrooms.Count; } }
        public List<Mushroom> Mushrooms { get { return mushrooms; } }
        public Mushroom Mushroom(int i)
        {
            return mushrooms[i];
        }
    }
}
