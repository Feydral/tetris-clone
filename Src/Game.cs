public class Game
{
    bool isRunning;

    public Game()
    {
        isRunning = true;
    }

    public void Run()
    {
        while (isRunning)
        {
            Update();
        }
    }

    void Update()
    {
        
    }
}