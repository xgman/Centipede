using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    public class Point : Sprite
    {
        public Point(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {
        }
    }
}
