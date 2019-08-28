﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    abstract class Component
    {
         private GameObject gameObject;

         public GameObject GameObject
         {
             get
             {
              return gameObject;
             }
         }
         public Component(GameObject gameObject)
         {
             this.gameObject = gameObject;
         }
         public Component()
         {

         }
     }
}
