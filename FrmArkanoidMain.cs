using ArkanoidGame.Engine;
using ArkanoidGame.GameGraphics;
using System;
using System.Windows.Forms;

namespace ArkanoidGame
{
    public partial class FrmArkanoidMain : Form
    {
        private GameEngine gameEngine;
        private GameObjectsRenderer gameObjectsRenderer;

        public FrmArkanoidMain()
        {
            InitializeComponent();
        }

        private void FrmArkanoidMain_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            gameEngine = new GameEngine(GameIterationTimer, Width, Height);
            gameObjectsRenderer = new GameObjectsRenderer(gameEngine);
            gameEngine.StartGame();
            Invalidate();
        }

        private void FrmArkanoidMain_Paint(object sender, PaintEventArgs e)
        {
            gameObjectsRenderer.RenderGameObjects(e.Graphics);
        }

        private void FrmArkanoidMain_MouseMove(object sender, MouseEventArgs e)
        {
            gameEngine.HandleMouseMove(e.Location);
        }

        private void FrmArkanoidMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gameEngine.ToggleGamePauseMode(() => Invalidate());
            }
            if (e.KeyCode == Keys.Escape)
            {
                gameEngine.PauseGame(() => Invalidate());

                DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    gameEngine.UnpauseGame();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                gameEngine.ResetGame();
            }
            if (e.KeyCode == Keys.S)
            {
                gameEngine.IsShowStats = !gameEngine.IsShowStats;
            }
        }

        private void FrmArkanoidMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gameEngine.ToggleGamePauseMode(() => Invalidate());
            }
            if (e.Button == MouseButtons.Middle)
            {
                gameEngine.IsShowStats = !gameEngine.IsShowStats;
            }
        }

        private void GameIterationTimer_Tick(object sender, EventArgs e)
        {
            Func<bool> funcIsNeedToContinueWhenGameIsOver = () =>
            {
                DialogResult dlgResult = MessageBox.Show("You lose... Start again?\r\nYou can cancel now and start the game later by pressing Enter",
                    "Losing :(",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dlgResult == DialogResult.Yes)
                {
                    return true;
                }
                return false;
            };

            Func<int, bool> funcIsNeedToRepeatAfterWin = (currentGameLevel) =>
            {
                DialogResult dlgResult = MessageBox.Show(
                    "Congratulations, you have won by reaching the last level " + currentGameLevel + "!\r\nShall we play again?\r\n",
                    "Victory!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dlgResult == DialogResult.Yes)
                {
                    return true;
                }
                return false;
            };

            gameEngine.HandleGameCycle(
                () => Invalidate(),
                funcIsNeedToContinueWhenGameIsOver,
                funcIsNeedToRepeatAfterWin
            );

            Invalidate();
        }
    }
}
