using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class SnowFlakePool
    {
        private static List<GameObject> inactive = new List<GameObject>();

        private static List<GameObject> active = new List<GameObject>();

        private static Director director = new Director(new SnowFlakeBuilder());

        public static GameObject Create(Vector2 position, ContentManager content)
        {
            if (inactive.Count > 0)
            {
                GameObject snowFlake = inactive[0];
                active.Add(snowFlake);
                inactive.RemoveAt(0);

                return snowFlake;
            }
            else
            {
                GameObject snowFlake = director.Construct(position);
                //(snowFlake.GetComponent("SnowFlake") as SnowFlake).LoadContent(content);
                snowFlake.LoadContent(content);
                active.Add(snowFlake);
                return snowFlake;
            }
        }

        public static void ReleaseObject(GameObject snowFlake)
        {
            CleanUp();
            inactive.Add(snowFlake);
            active.Remove(snowFlake);
        }

        private static void CleanUp()
        {
            //Reset data, remove references etc.
        }
    }
}
