using System.Text;

public class Game
{
    private const int Width = 20;
    private const int Height = 40;

    private readonly byte[,] map = new byte[Height, Width];
    private readonly Random random = new Random();

    private byte[,] block = null!;
    private int blockX;
    private int blockY;

    private bool isRunning = true;
    private int score;
    private int lines;
    private int level = 1;

    private long lastFall;
    private long FallInterval => Math.Max(80, 800 - (level - 1) * 50);

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();

        try
        {
            SpawnBlock();
            lastFall = Environment.TickCount64;

            while (isRunning)
            {
                ReadInput();

                if (!isRunning) break;

                long now = Environment.TickCount64;

                if (now - lastFall >= FallInterval)
                {
                    if (CanMove(blockX, blockY + 1, block))
                    {
                        blockY++;
                    }
                    else
                    {
                        LockBlock();
                    }

                    lastFall = now;
                }

                Print();
                Thread.Sleep(16);
            }

            Print();
            Console.WriteLine();
            Console.WriteLine("Game Over!");
            Console.ReadKey(true);
        }
        finally
        {
            Console.CursorVisible = true;
        }
    }

    private void ReadInput()
    {
        while (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A:
                    if (CanMove(blockX - 1, blockY, block)) blockX--;
                    break;

                case ConsoleKey.D:
                    if (CanMove(blockX + 1, blockY, block)) blockX++;
                    break;

                case ConsoleKey.S:
                    if (CanMove(blockX, blockY + 1, block))
                    {
                        blockY++;
                        score++;
                    }
                    else
                    {
                        LockBlock();
                    }

                    lastFall = Environment.TickCount64;
                    break;

                case ConsoleKey.LeftArrow:
                    byte[,] rotatedCCW = Blocks.RotateCCW(block);

                    if (CanMove(blockX, blockY, rotatedCCW)) block = rotatedCCW;
                    break;

                case ConsoleKey.RightArrow:
                    byte[,] rotatedCW = Blocks.RotateCW(block);

                    if (CanMove(blockX, blockY, rotatedCW)) block = rotatedCW;
                    break;

                case ConsoleKey.Escape:
                    isRunning = false;
                    break;
            }
        }
    }

    private bool CanMove(int x, int y, byte[,] shape)
    {
        for (int sy = 0; sy < shape.GetLength(0); sy++)
        for (int sx = 0; sx < shape.GetLength(1); sx++)
        {
            if (shape[sy, sx] == 0) continue;

            int px = x + sx;
            int py = y + sy;

            if (px < 0 || px >= Width || py < 0 || py >= Height) return false;
            if (map[py, px] != 0) return false;
        }

        return true;
    }

    private void SpawnBlock()
    {
        block = Blocks.Shapes[random.Next(Blocks.Shapes.Length)];
        blockX = (Width - block.GetLength(1)) / 2;
        blockY = 0;

        if (!CanMove(blockX, blockY, block)) isRunning = false;
    }

    private void LockBlock()
    {
        for (int y = 0; y < block.GetLength(0); y++)
        for (int x = 0; x < block.GetLength(1); x++)
        {
            if (block[y, x] != 0) map[blockY + y, blockX + x] = 1;
        }

        ClearLines();

        if (isRunning) SpawnBlock();
    }

    private void ClearLines()
    {
        int cleared = 0;

        for (int y = Height - 1; y >= 0; y--)
        {
            bool full = true;

            for (int x = 0; x < Width; x++)
            {
                if (map[y, x] == 0)
                {
                    full = false;
                    break;
                }
            }

            if (!full) continue;

            cleared++;

            for (int row = y; row > 0; row--)
            {
                for (int x = 0; x < Width; x++) {
                    map[row, x] = map[row - 1, x];
                }
            }
                

            for (int x = 0; x < Width; x++) {
                map[0, x] = 0; 
            }

            y++; 
        }

        lines += cleared;
        level = lines / 10 + 1;

        score += cleared switch
        {
            1 => 100 * level,
            2 => 300 * level,
            3 => 500 * level,
            4 => 800 * level,
            _ => 0
        };
    }

    private void Print()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("+" + new string('-', Width * 2) + "+");

        for (int y = 0; y < Height; y++)
        {
            sb.Append('|');

            for (int x = 0; x < Width; x++)
            {
                bool active = false;

                for (int sy = 0; sy < block.GetLength(0); sy++)
                {
                    for (int sx = 0; sx < block.GetLength(1); sx++)
                    {
                        if (block[sy, sx] != 0 &&
                            blockX + sx == x &&
                            blockY + sy == y)
                        {
                            active = true;
                        }
                    }
                }

                sb.Append(map[y, x] != 0 || active ? "[]" : ". ");
            }

            sb.AppendLine("|");
        }

        sb.AppendLine("+" + new string('-', Width * 2) + "+");
        sb.AppendLine($"Score: {score}   Lines: {lines}   Level: {level}");
        sb.AppendLine("A/D Move | ←/→ Rotate | S Drop");

        Console.SetCursorPosition(0, 0);
        Console.Write(sb.ToString());
    }
}

public class Program
{
    public static void Main()
    {
        new Game().Run();
    }
}
 