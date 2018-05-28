using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    public class Player : Sprite
    {
        #region Fields
        const int LEFT = 0;
        const int TOP = 1;
        const int RIGHT = 2;
        const int BOTTOM = 3;

        bool active = true;
        bool canShoot = false;

        Vector2[] collisionRadar = { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero };
        int collisionRadarDistance = 100;
        bool collisionRadarDisplay = false;
        #endregion

        #region Constructors
        public Player(SpriteBatch spriteBatch, Texture2D texture, int spriteWidth, int spriteHeight, Vector2 position) :
            base(spriteBatch, texture, spriteWidth, spriteHeight, position)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Public methods
        public void Update(GameTime gameTime, MouseState mouseState, MushroomGrid mushroomGrid)
        {
            if(active)
            {
                
            float dx = mouseState.X - rectangle.Center.X;
            float dy = mouseState.Y - rectangle.Center.Y;
            //Console.WriteLine(mouseState.Position + ";" + dx + ";" + dy);

            if (mouseState.LeftButton == ButtonState.Released)
            {
                canShoot = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && canShoot)
            {
                canShoot = false;
                Bullet bullet = new Bullet(spriteBatch, Game1.bulletSprite, 3, 15, new Vector2(rectangle.Center.X - 1, rectangle.Center.Y - 3));
                Game1.AddBullet(bullet);
            }

            CheckCollisionWithMushrooms(mushroomGrid);

            if (dx < 0 || dx > 0)
            {
                rectangle.X += (int)(dx * 0.3);
                if (collisionRadar[LEFT] != Vector2.Zero && rectangle.X < collisionRadar[LEFT].X)
                    rectangle.X = (int)collisionRadar[LEFT].X;
                if (collisionRadar[RIGHT] != Vector2.Zero && rectangle.Right > collisionRadar[RIGHT].X)
                    rectangle.X = (int)collisionRadar[RIGHT].X - rectangle.Width;
            }
            if (dy < 0 || dy > 0)
            {
                rectangle.Y += (int)(dy * 0.3);
                if (collisionRadar[TOP] != Vector2.Zero && rectangle.Y < collisionRadar[TOP].Y)
                    rectangle.Y = (int)collisionRadar[TOP].Y;
                if (collisionRadar[BOTTOM] != Vector2.Zero && rectangle.Bottom > collisionRadar[BOTTOM].Y)
                    rectangle.Y = (int)collisionRadar[BOTTOM].Y - rectangle.Height;
            }

            // clamp player in window
            if (rectangle.Left < 0)
                rectangle.X = 0;

            if (rectangle.Right > GameConstants.WindowWidth)
                rectangle.X = GameConstants.WindowWidth - rectangle.Width;

            if (rectangle.Y < GameConstants.PlayerClampHeight)
                rectangle.Y = GameConstants.PlayerClampHeight;

            if (rectangle.Bottom > GameConstants.WindowHeight)
                rectangle.Y = GameConstants.WindowHeight - rectangle.Height;
            }

        }
        public new void Draw()
        {
            // calls draw method in base class (Sprite) 
            base.Draw();
            // collisionRadar visibility can be turned on or off
            if (collisionRadarDisplay)
            {
                for (int i = 0; i < collisionRadar.Length; i++)
                {
                    if (collisionRadar[i] != Vector2.Zero)
                    {
                        DrawLine(spriteBatch, new Vector2(Rectangle.Center.X, Rectangle.Center.Y), collisionRadar[i], Color.Wheat, 1);
                        DrawCircle(spriteBatch, collisionRadar[i], 5, Color.White, 1);
                    }
                }
            }
        }
        #endregion

        #region Private methods
        private void CheckCollisionWithMushrooms(MushroomGrid mushroomGrid)
        {
            int[,] xy = new int[,] { { -1, 0 }, { 0, -1 }, { 1, 0 }, { 0, 1 } };

            for (int i = 0; i < collisionRadar.Length; i++)
                collisionRadar[i] = Vector2.Zero;

            for (int i = 0; i < mushroomGrid.MushroomCount; i++)
            {
                Rectangle mushroom = mushroomGrid.Mushroom(i).Rectangle;

                // unnecessary to check for mushroom collisions above PlayerClampHeight
                if (mushroom.Bottom < GameConstants.PlayerClampHeight)
                    continue;

                for (int j = 0; j < xy.GetLength(0); j++)
                {
                    bool hasCollision = LineRectangle(
                        new Vector2(rectangle.Center.X, rectangle.Center.Y),
                        new Vector2(rectangle.Center.X + xy[j, 0] * collisionRadarDistance, rectangle.Center.Y + xy[j, 1] * collisionRadarDistance),
                        mushroom,
                        out Vector2 intersection, out Vector2 dummy);
                    if (hasCollision)
                    {
                        if (collisionRadar[j] == Vector2.Zero ||
                            Vector2.Distance(new Vector2(rectangle.Center.X, rectangle.Center.Y), intersection) <
                            Vector2.Distance(new Vector2(rectangle.Center.X, rectangle.Center.Y), collisionRadar[j]))
                        {
                            collisionRadar[j].X = intersection.X;
                            collisionRadar[j].Y = intersection.Y;
                        }
                    }
                }
            }
        }

        // a1 is line1 start, a2 is line1 end, rec is colliding rectangle, b1 is clipped line start, b2 is clipped line end
        private bool LineRectangle(Vector2 ax, Vector2 ay, Rectangle rec, out Vector2 bx, out Vector2 by)
        {
            bx = Vector2.Zero;
            by = Vector2.Zero;
            float u1 = 0.0f; float u2 = 1.0f;
            float dx = ay.X - ax.X;
            float dy = ay.Y - ax.Y;
            float[] p = { -dx, dx, -dy, dy };
            float[] q = {
                ax.X - rec.Left,
                rec.Right - ax.X,
                ax.Y - rec.Top,
                rec.Bottom - ax.Y
            };

            // traverse through left, right, top, bottom edges
            for (int i = 0; i < 4; i++)
            {
                if (p[i] == 0)
                {
                    if (q[i] < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    float u = q[i] / p[i];
                    if (p[i] < 0)
                    {
                        u1 = Math.Max(u, u1);
                    }
                    else
                    {
                        u2 = Math.Min(u, u2);
                    }
                }
            }
            if (u1 > u2)
            {
                return false;
            }
            bx.X = ax.X + u1 * dx;
            bx.Y = ax.Y + u1 * dy;
            by.X = ax.X + u2 * dx;
            by.Y = ax.Y + u2 * dy;
            return true;
        }
        private void DrawCircle(SpriteBatch spriteBatch, Vector2 center, float radius, Color color, int lineWidth, int segments = 16)
        {
            Vector2[] vertex = new Vector2[segments];
            double increment = Math.PI * 2.0 / segments;
            double theta = 0.0;
            for (int i = 0; i < segments; i++)
            {
                vertex[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }
            for (int i = 0; i < segments - 1; i++)
            {
                DrawLine(spriteBatch, vertex[i], vertex[i + 1], color, lineWidth);
            }
            DrawLine(spriteBatch, vertex[segments - 1], vertex[0], color, lineWidth);
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, int lineWidth)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(Game1.pointSprite,
                new Rectangle(          // rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    lineWidth),         //width of line, change this to make thicker line
                null,
                color,                  //colour of line
                angle,                  //angle of line (calulated above)
                new Vector2(0, 0),      // point in line about which to rotate
                SpriteEffects.None,
                0);
        }
        #endregion

        public bool Active { get { return active; } set { active = value; } }
    }
}
