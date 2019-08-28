using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class JumpLeft : IMotionStrategy
    {
        private float speed;
        private Transform transform;
        private Animator animator;
        float correctJump; // used to correct Y-direction of motion during jump
        Vector2 translation;

        public JumpLeft(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
            speed = 180;
            correctJump = 0.5f;
        }

        public void Execute()
        {
            translation = Vector2.Zero;
            if (transform.Position.Y <= -50)
                translation += new Vector2(-1, 0);
            else translation += new Vector2(-1, -correctJump);

            transform.Translate(translation * GameWorld.Instance.DeltaTime * speed * 5);
            animator.PlayAnimation("FlyLeft");
            correctJump -= GameWorld.Instance.DeltaTime / 2;
        }
    }
}
