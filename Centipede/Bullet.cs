using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    class Bullet : Sprite
    {
        public Bullet(SpriteBatch spriteBatch, Texture2D texture, Vector2 position) : base(spriteBatch, texture, position)
        {
        }
    }
}
