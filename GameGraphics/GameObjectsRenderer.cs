using ArkanoidGame.Engine;
using ArkanoidGame.GameObjects;
using ArkanoidGame.GameObjects.Instances;
using ArkanoidGame.Statistics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ArkanoidGame.GameGraphics
{
    public class GameObjectsRenderer
    {
        private readonly GameEngine gameEngine;

        public GameObjectsRenderer(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        private void RenderGameStats(Graphics g)
        {
            if (gameEngine.IsShowStats)
            {
                GameStats gameStats = gameEngine.GameStatistics;
                Font statsFont = new Font("Ubuntu Mono", 12);
                string pushedAwayBallsStats = string.Format("{0}: {1}\r\n{2}: {3}\r\n{4}: {5} / {6}\r\n{7}: {8}\r\n{9}: {10}\r\n{11}: {12}\r\n\r\n[S] or middle mouse button\r\n - hide/show statistics",
                    GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_TOTAL,
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_TOTAL),
                    GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_CURRENT,
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_CURRENT),
                    GameEngine.GAME_STATS_LEVEL,
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_LEVEL),
                    GameEngine.TOTAL_GAME_LEVELS_TO_WIN,
                    GameEngine.GAME_STATS_CURRENT_BALL_SPEED,
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_CURRENT_BALL_SPEED),
                    "Games lost",
                    gameStats.NumberOfFailures,
                    "Games won",
                    gameStats.NumberOfWins);

                g.DrawString(pushedAwayBallsStats, statsFont, Brushes.Lime, new PointF(0, 150));

                statsFont.Dispose();
            }

        }

        private void RenderPlayerPlatform(Graphics g)
        {
            GameObject platform = gameEngine.PlayerPlatform;
            Rectangle platformRect = platform.GetObjectRectangle();
            g.FillRectangle(Brushes.Crimson, platformRect);
        }

        private void RenderBouncingBall(Graphics g)
        {
            GameObject ball = gameEngine.BouncingBall;
            Rectangle ballRect = ball.GetObjectRectangle();
            Rectangle ballOutherRect = new Rectangle(ballRect.X + 4, ballRect.Y + 4, ballRect.Width - 8, ballRect.Height - 8);
            g.FillEllipse(Brushes.Goldenrod, ballRect);
            g.FillEllipse(Brushes.DarkGoldenrod, ballOutherRect);
        }

        private void RenderStaticBlocks(Graphics g)
        {
            foreach (StaticBlock block in gameEngine.Blocks)
            {
                Rectangle blockBorderRect = block.GetObjectRectangle();
                Pen blockBorderPen = new Pen(block.BorderColor, 2);
                Brush blockBodyBrush = new SolidBrush(block.BodyColor);

                g.DrawRectangle(blockBorderPen, blockBorderRect);

                Rectangle blockBodyRect = new Rectangle(blockBorderRect.X + 1, blockBorderRect.Y + 1, blockBorderRect.Width - 1, blockBorderRect.Height - 1);
                g.FillRectangle(blockBodyBrush, blockBodyRect);

                Font fontHits = new Font("Arial", 8, FontStyle.Bold);
                g.DrawString(block.CurrentHits + " / " + block.HitsToDestroy, fontHits, Brushes.White, new PointF(blockBodyRect.X + 2, blockBodyRect.Y));

                blockBodyBrush.Dispose();
                blockBorderPen.Dispose();
                fontHits.Dispose();
            }
        }

        private void RenderGamePausedMessage(Graphics g)
        {
            if (gameEngine.IsGamePaused)
            {
                Font pausedFont = new Font("Arial", 16);
                string pausedMessage = "     Game on pause...\r\n[Space] - Continue\r\n[Esc] - Quitting the game";
                SizeF pausedMessageSize = g.MeasureString(pausedMessage, pausedFont);

                g.DrawString(pausedMessage, pausedFont, Brushes.Yellow,
                    new PointF(
                        gameEngine.GameFieldWidth / 2 - pausedMessageSize.Width / 2,
                        gameEngine.GameFieldHeight / 2 - pausedMessageSize.Height / 2 + 100
                    )
                );

                pausedFont.Dispose();
            }
        }

        public void RenderGameObjects(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            RenderGameStats(g);
            RenderPlayerPlatform(g);
            RenderBouncingBall(g);
            RenderStaticBlocks(g);
            RenderGamePausedMessage(g);
        }
    }
}
