using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class RightStoneBuilder : IBuilder
    {
        GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "side2", 0));
            gameObject.AddComponent(new RightStone(gameObject));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
