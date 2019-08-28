using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class LeftStoneBuilder : IBuilder
    {
        GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject(position);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "side", 0));
            gameObject.AddComponent(new LeftStone(gameObject));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
