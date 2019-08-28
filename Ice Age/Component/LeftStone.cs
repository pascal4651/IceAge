using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class LeftStone : Component, IUpdateable
    {
        int speed;
        Vector2 translation;
      
        public LeftStone(GameObject gameObject) : base(gameObject)
        {
            speed = 150;
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            translation = Vector2.Zero;
            translation += new Vector2(0, 1);
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
            if (GameObject.Transform.Position.Y >= 800)
                GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, -1700);
        }
    }
}
