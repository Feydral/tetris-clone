public static class Blocks
{
    static readonly byte[,] tetrominoStraight = new byte[,]
    {
        { 0, 0, 0, 0 },
        { 1, 1, 1, 1 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
    };

    static readonly byte[,] tetrominoSquare = new byte[,]
    {
        { 1, 1 },
        { 1, 1 },
    };

    static readonly byte[,] tetrominoT = new byte[,]
    {
        { 0, 0, 0 },
        { 1, 1, 1 },
        { 0, 1, 0 },
    };

    static readonly byte[,] tetrominoL = new byte[,]
    {
        { 0, 1, 0 },
        { 0, 1, 0 },
        { 0, 1, 1 },
    };

    static readonly byte[,] tetrominoSkew = new byte[,]
    {
        { 0, 0, 0 },
        { 0, 1, 1 },
        { 1, 1, 0 },
    };

    public static bool[,] Rotate(bool[,] block)
    {
        int rows = block.GetLength(0);
        int cols = block.GetLength(1);
    
        bool[,] rotated = new bool[cols, rows];
    
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                rotated[x, rows - 1 - y] = block[y, x];
            }
        }
    
        return rotated;
    }
}