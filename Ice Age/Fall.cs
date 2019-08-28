using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class Fall : IMotionStrategy
    {
        private float speed;
        private Transform transform;
        private Animator animator;
        SpriteRenderer spriteRenderer;
        Vector2 translation;

        public Fall(Transform transform, Animator animator, SpriteRenderer spriteRenderer)
        {
            this.transform = transform;
            this.animator = animator;
            this.spriteRenderer = spriteRenderer;
            speed = 180;
        }

        public void Execute()
        {
            translation = Vector2.Zero;
            translation += new Vector2(0.3f, 1);
            spriteRenderer.Rotation += GameWorld.Instance.DeltaTime;
            transform.Translate(translation * GameWorld.Instance.DeltaTime * speed * 2);
            animator.PlayAnimation("Fall");
        }
    }
}
