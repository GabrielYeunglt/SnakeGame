using System;
using System.Drawing;
using System.Windows.Forms;

public partial class GameForm : Form
{
    private Snake snake;
    private System.Windows.Forms.Timer gameTimer;
    private Point food;
    private Random random;
    private bool isGameOver;

    public GameForm()
    {
        InitializeComponent();
        this.KeyDown += new KeyEventHandler(OnKeyDown);
        random = new Random();
        InitializeGame();
    }

    private void InitializeComponent()
    {
        // placeholder
    }

    private void InitializeGame()
    {
        snake = new Snake(10, 10);
        isGameOver = false;
        food = GenerateFood();

        gameTimer = new System.Windows.Forms.Timer();
        gameTimer.Interval = 100; // Game speed (100 ms)
        gameTimer.Tick += new EventHandler(GameTick);
        gameTimer.Start();
    }

    private void GameTick(object sender, EventArgs e)
    {
        if (isGameOver) return;

        snake.Move();

        if (snake.Body[0] == food)
        {
            snake.Grow();
            food = GenerateFood(); // Generate new food
        }

        if (CheckCollision())
        {
            gameTimer.Stop();
            isGameOver = true;
            MessageBox.Show("Game Over!");
        }

        Invalidate(); // Trigger the form to be redrawn
    }

    private bool CheckCollision()
    {
        // Check if snake hits the boundaries or itself
        var head = snake.Body[0];

        if (head.X < 0 || head.X >= this.ClientSize.Width / 10 || head.Y < 0 || head.Y >= this.ClientSize.Height / 10)
            return true;

        for (int i = 1; i < snake.Body.Count; i++)
        {
            if (snake.Body[i] == head)
                return true;
        }

        return false;
    }

    private Point GenerateFood()
    {
        int x = random.Next(0, this.ClientSize.Width / 10) * 10;
        int y = random.Next(0, this.ClientSize.Height / 10) * 10;
        return new Point(x, y);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Up && snake.CurrentDirection != Snake.Direction.Down)
            snake.CurrentDirection = Snake.Direction.Up;
        else if (e.KeyCode == Keys.Down && snake.CurrentDirection != Snake.Direction.Up)
            snake.CurrentDirection = Snake.Direction.Down;
        else if (e.KeyCode == Keys.Left && snake.CurrentDirection != Snake.Direction.Right)
            snake.CurrentDirection = Snake.Direction.Left;
        else if (e.KeyCode == Keys.Right && snake.CurrentDirection != Snake.Direction.Left)
            snake.CurrentDirection = Snake.Direction.Right;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (isGameOver)
            return;

        // Draw Snake
        foreach (var segment in snake.Body)
        {
            e.Graphics.FillRectangle(Brushes.Green, segment.X * 10, segment.Y * 10, 10, 10);
        }

        // Draw Food
        e.Graphics.FillRectangle(Brushes.Red, food.X, food.Y, 10, 10);
    }
}
