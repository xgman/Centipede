using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Centipede
{
    class Mushroom : Sprite
    {
        public Mushroom(SpriteBatch spriteBatch, Texture2D texture, Vector2 position) : base(spriteBatch, texture, position)
        {
        }

        public void Shoot()
        {
            animationState++;
        }
    }
}