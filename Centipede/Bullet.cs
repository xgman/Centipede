using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    class Bullet : Sprite
    {
        float yelocity;
        public Bullet(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {
            yelocity = -1 * GameConstants.PlayerBulletSpeed;
        }

        public void Update(GameTime gameTime)
        {
            rectangle.Y += (int)(yelocity * gameTime.ElapsedGameTime.Milliseconds);
        }
    }
}
