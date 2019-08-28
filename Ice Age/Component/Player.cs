using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrrKlang;

namespace Ice_Age
{
    class Player : Component, IUpdateable, ILoadable, IAnimateable, ICollisionStay, ICollisionEnter, ICollisionExit, IDrawable
    {
        Vector2 translation;
        int lifesAmount;
        Animator animator;
        KeyboardState keyState;
        bool isFrozen;
        float xPlus; // start position for drawing dragons row
        int score;
        IMotionStrategy strategy;

        public Player(GameObject gameObject) : base(gameObject)
        {
            strategy = new Walk(GameObject.Transform, animator);
            lifesAmount = 6;
            isFrozen = false;
            xPlus = 5;
            score = 0;
        }

        public void Update()
        {
            if (GameWorld.Instance.RestartGame)
            {
                lifesAmount = 6;
                score = 0;
                isFrozen = false;
                xPlus = 5;
                strategy = new Walk(GameObject.Transform, animator);
                GameObject.Transform.Position = new Vector2(310, 50);
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Rotation = 0;
                (GameObject.GetComponent("Collider") as Collider).DoCollisionCheck = true;
            }
            if (lifesAmount <= 0)
                GameWorld.Instance.PlayGame = false;
            Move();
        }

        public void Move()
        {
            translation = Vector2.Zero;
            keyState = Keyboard.GetState();

            if (isFrozen)
            {        
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Blue;
                (GameObject.GetComponent("Collider") as Collider).DoCollisionCheck = false;
                strategy = new Fall(GameObject.Transform, animator, GameObject.GetComponent("SpriteRenderer") as SpriteRenderer);
                isFrozen = false;
            }
            if ((strategy is Fall) && GameObject.Transform.Position.Y >= 750)
            { 
                GameObject.Transform.Position = new Vector2(310, 50);
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Rotation = 0;
                (GameObject.GetComponent("Collider") as Collider).DoCollisionCheck = true;
                strategy = new Walk(GameObject.Transform, animator);
                lifesAmount -= 1;
            }
            else if ((strategy is Run) && GameObject.Transform.Position.Y >= 750)
            {
                GameObject.Transform.Position = new Vector2(310, 50);
                strategy = new Walk(GameObject.Transform, animator);
                lifesAmount -= 1;
            }
            else if ((strategy is Walk) && GameObject.Transform.Position.Y >= 750)
            {
                GameObject.Transform.Position = new Vector2(310, 50);
                lifesAmount -= 1;
            }

            if ((strategy is Walk) && keyState.IsKeyDown(Keys.R))
            {
                strategy = new Run(GameObject.Transform, animator);

                if (GameObject.Transform.Position.Y >= 750)
                {
                    GameObject.Transform.Position = new Vector2(310, 50);
                    strategy = new Walk(GameObject.Transform, animator);
                    lifesAmount -= 1;
                }
            }
            else if ((strategy is Walk) && keyState.IsKeyDown(Keys.Space) && GameObject.Transform.Position.X <= 310)
            {
                strategy = new JumpRight(GameObject.Transform, animator);
                if (GameWorld.Instance.PlaySound)
                    GameWorld.Instance.Engine.Play2D("Content/flapping.wav", false);
            }
            else if ((strategy is Walk) && keyState.IsKeyDown(Keys.Space) && GameObject.Transform.Position.X >= 880)
            {
                strategy = new JumpLeft(GameObject.Transform, animator);
                if (GameWorld.Instance.PlaySound)
                    GameWorld.Instance.Engine.Play2D("Content/flapping.wav", false);
            }

            if ((strategy is Run) && keyState.IsKeyUp(Keys.R))
                strategy = new Walk(GameObject.Transform, animator);
            
            if (((strategy is JumpLeft) && GameObject.Transform.Position.X <= 310) || ((strategy is JumpRight) && GameObject.Transform.Position.X >= 880))
            {
                strategy = new Walk(GameObject.Transform, animator);
                if (GameWorld.Instance.PlaySound)
                    GameWorld.Instance.Engine.Play2D("Content/click.wav", false);
            }
                strategy.Execute();
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");
            CreateAnimation();
        }

        public void CreateAnimation()
        {
            animator.CreateAnimation("Walk", new Animation(4, 400, 0, 125, 90, 3, Vector2.Zero));
            animator.CreateAnimation("FlyRight", new Animation(4, 285, 0, 124, 80, 9, Vector2.Zero));
            animator.CreateAnimation("FlyLeft", new Animation(4, 165, 0, 123, 75, 8, Vector2.Zero));
            animator.CreateAnimation("Fall", new Animation(4, 5, 1, 120, 115, 0, Vector2.Zero));
            animator.CreateAnimation("Run", new Animation(4, 400, 0, 125, 90, 8, Vector2.Zero));
            animator.PlayAnimation("Walk");
        }

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("AttackRight"))
            {
                //canAttack = false;
            }
        }

        public void OnCollisionStay(Collider other)
        {
            // (other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;
        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("SnowFlake") is SnowFlake)
            {
                //(other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Blue;
                (other.GameObject.GetComponent("SnowFlake") as SnowFlake).OnMove = false;
                isFrozen = true;
                if (GameWorld.Instance.PlaySound)
                    GameWorld.Instance.Engine.Play2D("Content/pterodactyl.wav", false);
            }
            if (other.GameObject.GetComponent("Bat") is Bat)
            {
                (other.GameObject.GetComponent("Bat") as Bat).LifeTimer = 0;
                GameWorld.Instance.Scores.Add(new Score("+20", (other.GameObject.GetComponent("Transform") as Transform).Position));
                score += 20;
                if (GameWorld.Instance.PlaySound)
                    GameWorld.Instance.Engine.Play2D("Content/misc010.wav", false);
            }
            if (other.GameObject.GetComponent("MiniBat") is MiniBat)
            {
                (other.GameObject.GetComponent("MiniBat") as MiniBat).LifeTimer = 0;
                GameWorld.Instance.Scores.Add(new Score("+5", (other.GameObject.GetComponent("Transform") as Transform).Position));
                score += 5;
                if (GameWorld.Instance.PlaySound)
                GameWorld.Instance.Engine.Play2D("Content/misc010.wav", false);
            }
        }

        public void OnCollisionExit(Collider other)
        {
            //(other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Instance.AFont, "Score: " + score, new Vector2(10, 100), Color.LightGreen);
            spriteBatch.DrawString(GameWorld.Instance.AFont, "Lifes left: "+lifesAmount, new Vector2(10, 130), Color.AliceBlue);
            for (int i = 0; i < lifesAmount; i++)
            {
                spriteBatch.Draw(GameWorld.Instance.DragonSprite, new Vector2(xPlus, 150), Color.White);
                xPlus += 27;
            }
            xPlus = 5;
        }
    }
}
