using ArkanoidGame.GameObjects;
using ArkanoidGame.GameObjects.Instances;
using ArkanoidGame.Statistics;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading;

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


    }
}
