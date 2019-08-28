using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ice_Age
{
    class SnowFlake : Component, IUpdateable, ILoadable, IAnimateable
    {
        int speed;
        Vector2 translation;
        Animator animator;
        bool onMove;

        public bool OnMove
        {
            get
            {
                return onMove;
            }

            set
            {
                onMove = value;
            }
        }

        public SnowFlake(GameObject gameObject) : base(gameObject)
        {
            speed = GameWorld.Instance.Rnd.Next(60, 200);
            OnMove = true;
        }

        public void Update()
        {
            if (GameWorld.Instance.RestartGame)
                Relocate();
           else Move();
        }

        public void Move()
        {
            translation = Vector2.Zero;
            if (OnMove)
            {
                translation += new Vector2(0, -1);
                GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
                animator.PlayAnimation("Move");
                if (GameObject.Transform.Position.Y <= -100)
                {
                    GameObject.Transform.Position = new Vector2(GameWorld.Instance.Rnd.Next(450, 700), GameWorld.Instance.Rnd.Next(650, 1000));
                    speed = GameWorld.Instance.Rnd.Next(60, 200);
                }
            }
            else
            {
                translation += new Vector2(0, 0);
                GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
                animator.PlayAnimation("Melt");
            }
        }

        public void Relocate()
        {
            translation = Vector2.Zero;       
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Rnd.Next(450, 700), GameWorld.Instance.Rnd.Next(650, 1000));
            speed = GameWorld.Instance.Rnd.Next(60, 200);
            translation += new Vector2(0, -1);
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * speed);
            animator.PlayAnimation("Move");
        }


        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");
            CreateAnimation();
        }

        public void CreateAnimation()
        {
            animator.CreateAnimation("Move", new Animation(3, 0, 0, 110, 60, 8, Vector2.Zero));
            animator.CreateAnimation("Melt", new Animation(4, 60, 0, 110, 60, 8, Vector2.Zero));
            animator.PlayAnimation("Move");
        }

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("Melt"))
            {
                GameObject.Transform.Position = new Vector2(GameWorld.Instance.Rnd.Next(450, 700), GameWorld.Instance.Rnd.Next(700, 1000));
                speed = GameWorld.Instance.Rnd.Next(60, 200);
                OnMove = true;
            }
        }
    }
}
