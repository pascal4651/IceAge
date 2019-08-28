using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class Score
    {
        float lifeTimer;
        string price;
        Vector2 position;
        float speed;
        Vector2 translation;

        public Score(string price, Vector2 position)
        {
            this.price = price;
            this.position = position;
            lifeTimer = 2;
            speed = 30;
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            translation = Vector2.Zero;
            translation += new Vector2(0, -1);
            position += translation*GameWorld.Instance.DeltaTime * speed;
            lifeTimer -= GameWorld.Instance.DeltaTime;

            if (lifeTimer <= 0)
                GameWorld.Instance.ScoresToRemove.Add(this);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Instance.AFont, price, position, Color.DarkRed);
        }
    }
}
