using IrrKlang;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Ice_Age
{
    /// <summary>
    /// Represents GameWorld
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<GameObject> gameObjects;
        List<GameObject> objectsToRemove;
        List<Collider> colliders;
        List<Collider> collidersToRemove;
        List<Score> scores;
        List<Score> scoresToRemove;
        static GameWorld instance;
        Director director;
        Random rnd;
        float batSpownTimer;
        Texture2D skySprite;
        Rectangle skyRectangle;
        Texture2D dragonSprite;
        bool playGame;
        SpriteFont aFont;
        SpriteFont bFont;
        SpriteFont cFont;
        bool restartGame;
        bool firstStart;
        ISoundEngine engine;
        byte currentLevel;
        byte previousLevel;
        bool playSound;
        string soundMode;
        float hour, min, sec;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameWorld();
                return instance;
            }
        }
        public float DeltaTime { get; private set; }
        internal List<Collider> Colliders
        {
            get
            {
                return colliders;
            }
        }
        internal List<GameObject> ObjectsToRemove
        {
            get
            {
                return objectsToRemove;
            }

            set
            {
                objectsToRemove = value;
            }
        }
        public SpriteFont AFont
        {
            get
            {
                return aFont;
            }
        }
        public Texture2D DragonSprite
        {
            get
            {
                return dragonSprite;
            }
        }
        public bool PlayGame
        {
            get
            {
                return playGame;
            }

            set
            {
                playGame = value;
            }
        }
        public bool RestartGame
        {
            get
            {
                return restartGame;
            }

            set
            {
                restartGame = value;
            }
        }
        public Random Rnd
        {
            get
            {
                return rnd;
            }

            set
            {
                rnd = value;
            }
        }
        internal List<Score> Scores
        {
            get
            {
                return scores;
            }

            set
            {
                scores = value;
            }
        }
        internal List<Score> ScoresToRemove
        {
            get
            {
                return scoresToRemove;
            }

            set
            {
                scoresToRemove = value;
            }
        }
        public SpriteFont CFont
        {
            get
            {
                return cFont;
            }
        }
        public ISoundEngine Engine
        {
            get
            {
                return engine;
            }
        }
        public bool PlaySound
        {
            get
            {
                return playSound;
            }
        }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 650;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            batSpownTimer = 0;
            rnd = new Random(DateTime.Now.Millisecond);
            playGame = true;
            restartGame = false;
            firstStart = true;
            engine = new ISoundEngine();
            currentLevel = previousLevel = 1;
            playSound = true;
            soundMode = "on";
            hour = min = sec = 0;
            gameObjects = new List<GameObject>();
            colliders = new List<Collider>();
            objectsToRemove = new List<GameObject>();
            collidersToRemove = new List<Collider>();
            scores = new List<Score>();
            scoresToRemove = new List<Score>();

            director = new Director(new LeftStoneBuilder());
            gameObjects.Add(director.Construct(new Vector2(300, -500)));
            director = new Director(new LeftStoneBuilder());
            gameObjects.Add(director.Construct(new Vector2(300, -1700)));
            director = new Director(new RightStoneBuilder());
            gameObjects.Add(director.Construct(new Vector2(880, -500)));
            director = new Director(new RightStoneBuilder());
            gameObjects.Add(director.Construct(new Vector2(880, -1700)));
            director = new Director(new PlayerBuilder());
            gameObjects.Add(director.Construct(new Vector2(310, 0)));

            //gameObjects.Add(SnowFlakePool.Create(new Vector2(600, 700), Content));
            //gameObjects.Add(SnowFlakePool.Create(new Vector2(600, 700), Content));
            //gameObjects.Add(SnowFlakePool.Create(new Vector2(600, 700), Content));
            //director = new Director(new SnowFlakeBuilder());
            //gameObjects.Add(director.Construct(new Vector2(650, 700)));
            //director = new Director(new SnowFlakeBuilder());
            //gameObjects.Add(director.Construct(new Vector2(450, 900)));
            //director = new Director(new SnowFlakeBuilder());
            //gameObjects.Add(director.Construct(new Vector2(700, 1200)));
            //director = new Director(new BatBuilder());
            //gameObjects.Add(director.Construct(new Vector2(rnd.Next(450, 750), rnd.Next(50, 500))));
            director = new Director(new MiniBatBuilder());
            gameObjects.Add(director.Construct(new Vector2(rnd.Next(450, 750), rnd.Next(50, 500))));
            director = new Director(new IceBuilder());
            gameObjects.Add(director.Construct(new Vector2(280, 570)));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            skySprite = Content.Load<Texture2D>("sky");
            skyRectangle = new Rectangle(400, 0, skySprite.Width, 650);
            dragonSprite = Content.Load<Texture2D>("1life");
            aFont = Content.Load<SpriteFont>("AFont");
            bFont = Content.Load<SpriteFont>("BFont");
            cFont = Content.Load<SpriteFont>("CFont");
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                previousLevel = currentLevel;
                currentLevel = 1;
                SetupLevel();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                previousLevel = currentLevel;
                currentLevel = 2;
                SetupLevel();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                previousLevel = currentLevel;
                currentLevel = 3;
                SetupLevel();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                playSound = !playSound;
                soundMode = playSound ? "on" : "off";
                if (!playSound) engine.StopAllSounds();
                else if (playGame && !firstStart) engine.Play2D("Content/iceMove.wav", true);
            }

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (playGame && !firstStart)
            {
                batSpownTimer += DeltaTime;
                UpdateTimer();
                if (batSpownTimer >= 8)
                {
                    director = new Director(new BatBuilder());
                    GameObject bat = director.Construct(new Vector2(rnd.Next(450, 750), rnd.Next(50, 500)));
                    bat.LoadContent(Content);
                    gameObjects.Add(bat);
                    batSpownTimer = 0;
                    if (playSound)
                        engine.Play2D("Content/bat1.wav", false);
                }

                ClearLists();

                foreach (GameObject go in gameObjects)
                {
                    go.Update();
                }

                if (restartGame == true) restartGame = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(skySprite, skyRectangle, Color.White);
            spriteBatch.DrawString(aFont, "Current level: "+ currentLevel, new Vector2(10, 30), Color.Ivory);
            spriteBatch.DrawString(aFont, string.Format("Current time: {0:00}:{1:00}:{2:00}", hour, min, sec), new Vector2(10, 70), Color.LightGray);
            spriteBatch.DrawString(aFont, "[space] - press to jump", new Vector2(10, 300), Color.WhiteSmoke);
            spriteBatch.DrawString(aFont, "[R] - hold to run", new Vector2(10, 330), Color.WhiteSmoke);
            spriteBatch.DrawString(aFont, "[1] - start game Level 1", new Vector2(10, 400), Color.GreenYellow);
            spriteBatch.DrawString(aFont, "[2] - start game Level 2", new Vector2(10, 430), Color.GreenYellow);
            spriteBatch.DrawString(aFont, "[3] - start game Level 3", new Vector2(10, 460), Color.GreenYellow);
            spriteBatch.DrawString(aFont, "[M] - sound: " + soundMode, new Vector2(10, 520), Color.Cyan);
            spriteBatch.DrawString(aFont, "[esc] - quit the game", new Vector2(10, 560), Color.Orange);

            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }
            if (scores.Count > 0)
            {
                foreach (Score s in scores)
                {
                    s.Draw(spriteBatch);
                }
            }
            if (firstStart)
            {
                spriteBatch.DrawString(bFont, "Catch the bats and", new Vector2(530, 300), Color.DarkGreen);
                spriteBatch.DrawString(bFont, "avoid the ice shards", new Vector2(520, 350), Color.DarkGreen);
            }
            if (!playGame)
            {
                spriteBatch.DrawString(bFont, "GAME OVER!", new Vector2(570, 200), Color.Red);
                engine.StopAllSounds();
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ClearLists()
        {
            if (objectsToRemove.Count > 0)
            {
                foreach (GameObject go in objectsToRemove)
                {
                    collidersToRemove.Add(go.GetComponent("Collider") as Collider);
                }
                foreach (GameObject go in objectsToRemove)
                {
                    if (go.GetComponent("SnowFlake") is SnowFlake)
                        SnowFlakePool.ReleaseObject(go);
                    gameObjects.Remove(go);
                }
                objectsToRemove.Clear();
            }
            if (collidersToRemove.Count > 0)
            {
                foreach (Collider c in collidersToRemove)
                {
                    colliders.Remove(c);
                }
                collidersToRemove.Clear();
            }
            if (scores.Count > 0)
            {
                foreach (Score s in scores)
                {
                    s.Update();
                }
            }
            if (scoresToRemove.Count > 0)
            {
                foreach (Score s in scoresToRemove)
                {
                    scores.Remove(s);
                }
            }
        }
        public void SetupLevel()
        {
            if (firstStart)
            {
                gameObjects.Insert(gameObjects.Count - 4, SnowFlakePool.Create(new Vector2(600, 700), Content));
                gameObjects.Insert(gameObjects.Count - 4, SnowFlakePool.Create(new Vector2(600, 700), Content));
                gameObjects.Insert(gameObjects.Count - 4, SnowFlakePool.Create(new Vector2(600, 700), Content));
            }
            hour = min = sec = 0;
            firstStart = false;
            engine.StopAllSounds();
            if (playSound) engine.Play2D("Content/iceMove.wav", true);
            restartGame = true;
            playGame = true;
            batSpownTimer = 0;
            if (currentLevel < previousLevel)
            {
                int i = 0;
                foreach (GameObject go in gameObjects)
                {
                   if (go.GetComponent("SnowFlake") is SnowFlake)
                   {
                        objectsToRemove.Add(go);
                        i++;
                        if (i==(previousLevel - currentLevel)) break;
                   }
                }
            }
            else if (currentLevel > previousLevel)
            {
                for (int i = 0; i < currentLevel - previousLevel; i++)
                {
                    gameObjects.Insert(gameObjects.Count - 4, SnowFlakePool.Create(new Vector2(rnd.Next(450, 700), rnd.Next(650, 1000)), Content));
                }
            }
            UpdateTimer();
        }

        public void UpdateTimer()
        {
            sec += DeltaTime;
            if (sec>=60)
            {
                min++;
                sec = 0;
            }
            if (min>=60)
            {
                hour++;
                min = 0;
            }
        }
    }
}
