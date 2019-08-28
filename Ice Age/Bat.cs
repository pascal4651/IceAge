using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class Bat : Component, IUpdateable, ILoadable, IAnimateable
    {
        int speed;
        Vector2 translation;
        Animator animator;
        Random rnd;
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

        public Bat(GameObject gameObject) : base(gameObject)
        {
            rnd = new Random();
            speed = 10;
            lifeTimer = 6;
        }

        public void Update()
        {
                Move();
        }

        public void Move()
        {
            if (GameWorld.Instance.RestartGame)
                lifeTimer = 0;
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
                GameWorld.Instance.ObjectsToRemove.Add(GameObject);
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");
            CreateAnimation();
        }

        public void CreateAnimation()
        {
            animator.CreateAnimation("IdleFront", new Animation(4, 3, 0, 77, 33, 6, Vector2.Zero));
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
