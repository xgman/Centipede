using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Centipede
{
    public class MushroomGrid
    {
        public static List<Mushroom> mushrooms;
        Random random = new Random();
        public MushroomGrid(SpriteBatch spriteBatch, Mushroom mushroom)
        {
            mushrooms = new List<Mushroom>();

            for (int count = 0; count < 50; count++)
            {
                int x, y;
                x = random.Next(30);
                y = random.Next(2, 28);
                Mushroom m = new Mushroom(spriteBatch, mushroom.Texture, mushroom.SpriteWidth, mushroom.SpriteHeight,
                    new Vector2(x * mushroom.SpriteWidth, y * mushroom.SpriteHeight));
                mushrooms.Add(m);
            }
        }
        public void Draw()
        {
            for (int count = 0; count < mushrooms.Count; count++)
            {
                mushrooms[count].Draw();
            }
        }
        public int MushroomCount { get { return mushrooms.Count; } }
        public List<Mushroom> Mushrooms { get { return mushrooms; } }
        public Mushroom Mushroom(int i)
        {
            return mushrooms[i];
        }
    }
}
