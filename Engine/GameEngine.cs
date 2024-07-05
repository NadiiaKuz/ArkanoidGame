using ArkanoidGame.GameObjects;
using ArkanoidGame.GameObjects.Instances;
using ArkanoidGame.GameObjects.Positioning;
using ArkanoidGame.Statistics;
using System;
using System.Collections.Generic;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;

namespace ArkanoidGame.Engine
{
    public class GameEngine
    {
        private const int platformWidth = 120;
        private const int platformHeight = 15;
        private const int ballRadius = 15;
        private const int bottomMargin = 45;
        
        public const int TOTAL_GAME_LEVELS_TO_WIN = 7;

        private const int BALLS_TO_PUSH_AWAY_FOR_NEXT_LEVEL = 5;
        private const int BALL_STARTING_SPEED = 4;

        private readonly GameObject platform = new RectangularGameObject("Player platform", platformWidth, platformHeight);
        private readonly GameObject ball = new BouncingBall("Ball", ballRadius);
        private readonly GameStats gameStats = new GameStats();
        private readonly List<StaticBlock> blocks = new List<StaticBlock>();

        public static readonly string GAME_STATS_PUSHED_AWAY_BALLS_TOTAL = "Bounced balls for all games";
        public static readonly string GAME_STATS_PUSHED_AWAY_BALLS_CURRENT = "Bounced balls in the current game";
        public static readonly string GAME_STATS_CURRENT_BALL_SPEED = "Current ball speed";
        public static readonly string GAME_STATS_LEVEL = "Current level";

        private bool isGamePaused = false;

        public bool IsGamePaused { get { return isGamePaused; } }

        public int GameFieldWidth { get; set; }
        public int GameFieldHeight { get; set; }

        public GameStats GameStatistics { get {  return gameStats; } }

        public bool IsShowStats { get; set; } = true;

        private Timer timer;

        public GameObject PlayerPlatform { get { return platform; } }
        public GameObject BouncingBall { get { return ball; } }
        public List<StaticBlock> Blocks { get { return blocks; } }

        public GameEngine(Timer gameTimer, int gameFieldWidth, int gameFieldHeight)
        {
            timer = gameTimer;
            GameFieldWidth = gameFieldWidth;
            GameFieldHeight = gameFieldHeight;
        }

        public void StartGame()
        {
            InitGameObjectsPositionsAndState();
            timer.Start();
        }

        private void GenerateBlocksForCurrentLevel()
        {
            int blockWidth = 60;
            int blockHeight = 15;

            blocks.Clear();

            int currentGameLevel = gameStats.GetGameCounterValue(GAME_STATS_LEVEL);

            switch (currentGameLevel)
            {
                case 1:
                    for (int blockLayer = 1; blockLayer <= 2; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Block #" + n + ", row #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            block.BodyColor = Color.Purple;
                            block.HitsToDestroy = 1;
                            blocks.Add(block);
                        }
                    }
                    break;
                case 2:
                    for (int blockLayer = 1; blockLayer <= 3; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Block #" + n + ", row #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 3:
                    for (int blockLayer = 1; blockLayer <= 4; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Block #" + n + ", row #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1 || blockLayer == 2)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 4:
                    for (int blockLayer = 1; blockLayer <= 4; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Block #" + n + ", row #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1)
                            {
                                block.BodyColor = Color.Brown; 
                                block.HitsToDestroy = 3;
                            }
                            if (blockLayer == 2)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 5:
                    for (int blockLayer = 1; blockLayer <= 4; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Block #" + n + ", row #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1)
                            {
                                block.BodyColor = Color.Brown;
                                block.HitsToDestroy = 3;
                            }
                            if (blockLayer == 2 || blockLayer == 3)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 6:
                    for (int blockLayer = 1; blockLayer <= 5; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Block #" + n + ", row #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1 || blockLayer == 2)
                            {
                                block.BodyColor = Color.Brown;
                                block.HitsToDestroy = 3;
                            }
                            if (blockLayer == 3 || blockLayer == 4)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
            }
        }

        public void InitGameObjectsPositionsAndState()
        {
            ResetObjectsPositions();

            gameStats.AddGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_TOTAL, 0);
            gameStats.AddGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_CURRENT, 0);
            gameStats.AddGameCounter(GAME_STATS_LEVEL, 1);
            gameStats.AddGameCounter(GAME_STATS_CURRENT_BALL_SPEED, BALL_STARTING_SPEED);

            ball.CollapsedWithOtherObjects += Ball_CollapsedWithOtherObjects;
            ball.InitIncrementNumberOfFailures += Ball_InitIncrementNumberOfFailures;
            ball.InitPositiveGameAction += Ball_InitPositiveGameAction;

            IBouncingDiagonalMovingGameObject bouncingBall = ball as IBouncingDiagonalMovingGameObject;

            bouncingBall.SetBounceFromObject(platform);

            GenerateBlocksAndSetThemAsDestroyingObjects(bouncingBall);

            InitializeBallMovement(bouncingBall);

            bouncingBall.SetWallFailureConstraint(WallPosition.WallFromTheBottom);
        }

        private void Ball_InitPositiveGameAction(object sender, EventArgs e)
        {
            gameStats.IncrementGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_TOTAL);
            gameStats.IncrementGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_CURRENT);
        }

        private void Ball_InitIncrementNumberOfFailures(object sender, EventArgs e)
        {
            gameStats.InctementNumberOfFailures();
        }

        private void Ball_CollapsedWithOtherObjects(object sender, ICollection<GameObject> destroyedBlocks)
        {
            Blocks.RemoveAll(block => destroyedBlocks.Contains(block));
        }

        private void GenerateBlocksAndSetThemAsDestroyingObjects(IBouncingDiagonalMovingGameObject bouncingBall)
        {
            GenerateBlocksForCurrentLevel();
            List<GameObject> destroyingBlocks = new List<GameObject>(blocks);
            bouncingBall.SetBounceFromDestroyingObjects(destroyingBlocks);
        }

        public void ResetObjectsPositions()
        {
            platform.SetPosition(GameFieldWidth / 2 - platformWidth / 2, GameFieldHeight - platformHeight - bottomMargin);
            ball.SetPosition(GameFieldWidth / 2 - ballRadius, GameFieldHeight / 2 - ballRadius);
        }

        public void InitializeBallMovement(IBouncingDiagonalMovingGameObject bouncingBall)
        {
            bouncingBall.InitRandomSafeDiagonalMovingDirection();
            bouncingBall.SetMovingSpeed(BALL_STARTING_SPEED);
        }
    }
}
