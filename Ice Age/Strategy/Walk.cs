using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class Walk : IMotionStrategy
    {
        private float speed;
        private Transform transform;
        private Animator animator;
        Vector2 translation;

        public Walk(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
            speed = 180;
        }

        public void Execute()
        {
            translation = Vector2.Zero;
            translation += new Vector2(0, 1);
            transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
            animator.PlayAnimation("Walk");
        }
    }
}
