using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    public class Bullet : Sprite
    {
        bool active = true;
        float yelocity;
        public Bullet(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {
            yelocity = -1 * GameConstants.PlayerBulletSpeed;
        }

        public void Update(GameTime gameTime)
        {
            // move bullet
            rectangle.Y += (int)(yelocity * gameTime.ElapsedGameTime.Milliseconds);

            // check for outside game window
            if (rectangle.Bottom < 0)
            {
                active = false;
            }
        }

        public bool Active { get { return active; } set { active = value; } }
    }
}
