using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    public class Score : Sprite
    {
        public Score(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {
        }
        public void Draw(int score)
        {
            string s = score.ToString();
            //s = "012345678901234567890123456789";
            int scoreWidth = s.Length * SpriteWidth + (s.Length - 1) * 3;
            for (int i = 0; i < s.Length; i++)
            {   
                // convert score char ascii to index j, ascii '0' = 48, '1' = 49 etc.
                int j = (int)s[i] - 48;

                int x = (GameConstants.WindowWidth - scoreWidth) / 2 + rectangle.X + i * spriteWidth + i * 3;
                spriteBatch.Draw(
                    texture,
                    new Rectangle(x, rectangle.Y, spriteWidth, spriteHeight),
                    new Rectangle(j * spriteWidth, 0, spriteWidth, spriteHeight),
                    Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.0f);
            }
        }
    }
}
