using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class MiniBat : Component, IUpdateable, ILoadable, IAnimateable
    {
        int speed;
        Vector2 translation;
        Animator animator;
        float lifeTimer;

        public float LifeTimer
        {
            get
            {
                return lifeTimer;
            }

            set
            {
                lifeTimer = value;
            }
        }

        public MiniBat(GameObject gameObject) : base(gameObject)
        {
            speed = 10;
            lifeTimer = 5;
        }

        public void Update()
        {
            if (GameWorld.Instance.RestartGame)
                lifeTimer = 0;
            Move();
        }

        public void Move()
        {
            translation = Vector2.Zero;
            if (lifeTimer < 2)
                translation += new Vector2(-1, -1);
            else if (lifeTimer < 4)
                translation += new Vector2(0, 1);
            else translation += new Vector2(1, 1);
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
            animator.PlayAnimation("IdleFront");
            lifeTimer -= GameWorld.Instance.DeltaTime;

            if (lifeTimer <= 0)
            {
                GameObject.Transform.Position = new Vector2(GameWorld.Instance.Rnd.Next(450, 800), GameWorld.Instance.Rnd.Next(50, 500));
                lifeTimer = 5;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");
            CreateAnimation();
        }

        public void CreateAnimation()
        {
            animator.CreateAnimation("IdleFront", new Animation(4, 80, 0, 50, 20, 14, Vector2.Zero));
            animator.PlayAnimation("IdleFront");
        }

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("AttackRight"))
            {

            }
        }
    }
}
