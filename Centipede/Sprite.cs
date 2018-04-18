using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Centipede
{
    public class Sprite
    {
        protected SpriteBatch spriteBatch;
        protected Rectangle rectangle;
        protected Texture2D texture;
        protected int animationState;

        public Sprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.animationState = 0;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y,
                GameConstants.SpriteSize, GameConstants.SpriteSize);
        }

        public Rectangle Rectangle { get { return rectangle; } set { rectangle = value; } }

        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }

        public int AnimationState { get { return animationState; } set { animationState = value; } }

        public void Draw()
        {
            spriteBatch.Draw(texture, rectangle, new Rectangle(AnimationState * 24, 0, GameConstants.SpriteSize, GameConstants.SpriteSize),
                Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }
    }
}
