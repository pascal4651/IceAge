using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class Ice : Component, IUpdateable
    {
        int speed;
        Vector2 translation;
        bool moveRight;

        public Ice(GameObject gameObject) : base(gameObject)
        {
            speed = 8;
            moveRight = true;
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            if (moveRight)
            {
                translation = Vector2.Zero;
                translation += new Vector2(1, 0);
                GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
                if (GameObject.Transform.Position.X > 310)
                    moveRight = false;
            }
            else
            {
                translation = Vector2.Zero;
                translation += new Vector2(-1, 0);
                GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
                if (GameObject.Transform.Position.X < 200)
                    moveRight = true;
            }
        }
    }
}
