﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    interface IBuilder
    {
        GameObject GetResult();
        void BuildGameObject(Vector2 position);
    }
}
