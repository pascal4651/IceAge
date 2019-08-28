﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_Age
{
    class SpriteRenderer : Component, IDrawable, ILoadable
    {
        Rectangle rectangle;
        Texture2D sprite;
        string spriteName;
        float layerDepth;
        Vector2 offset;
        Color color;
        float rotation;
        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
        }
        public Vector2 Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth) : base(gameObject)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
            color = Color.White;
            rotation = 0;
        }

        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spriteName);
            rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameObject.GetComponent("Bat") is Bat)
            {
                spriteBatch.DrawString(GameWorld.Instance.CFont, ((int)(GameObject.GetComponent("Bat") as Bat).LifeTimer).ToString(), new Vector2(GameObject.Transform.Position.X + 30, GameObject.Transform.Position.Y - 10), Color.Gray);
            }
            if (GameObject.GetComponent("MiniBat") is MiniBat)
            {
                spriteBatch.DrawString(GameWorld.Instance.CFont, ((int)(GameObject.GetComponent("MiniBat") as MiniBat).LifeTimer).ToString(), new Vector2(GameObject.Transform.Position.X + 25, GameObject.Transform.Position.Y - 10), Color.Gray);
            }
            //spriteBatch.Draw(sprite, GameObject.Transform.Position, Color.White);
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, rectangle, Color, rotation, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
        }
    }
}
