public class Game
{
    bool isRunning;
    byte[,] map;

    public Game((int, int) mapSize)
    {
        isRunning = true;
        map = new byte[mapSize.Item1, mapSize.Item2];
    }

    public void Run()
    {
        while (!isRunning)
        {
            Start();
            Update();
            Print();
        }
    }

    void Start()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Random rng = new Random();
                
                if (rng.Next() % 2 == 0)
                {
                    map[x, y] = 1;
                }

            }
        }
    }

    void Update()
    {
        
    }

    void Print()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == 1)
                    Console.Write("[]");
                else
                    Console.Write("  ");
            }

            Console.WriteLine();
        }
    }
}

public class Progam
{
    public static void Main()
    {
        Game game = new Game((20, 40));
        game.Run();
    }
}
