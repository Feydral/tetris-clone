public static class Blocks
{
    public static readonly byte[][,] Shapes =
    {
        new byte[,]
        {
            { 1, 1, 1, 1 }
        },

        new byte[,]
        {
            { 1, 1 },
            { 1, 1 }
        },

        new byte[,]
        {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        },

        new byte[,]
        {
            { 0, 0, 1 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        },

        new byte[,]
        {
            { 1, 0, 0 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        },

        new byte[,]
        {
            { 0, 1, 1 },
            { 1, 1, 0 },
            { 0, 0, 0 }
        },

        new byte[,]
        {
            { 1, 1, 0 },
            { 0, 1, 1 },
            { 0, 0, 0 }
        }
    };

    public static byte[,] RotateCW(byte[,] block)
    {
        int rows = block.GetLength(0);
        int cols = block.GetLength(1);

        byte[,] rotated = new byte[cols, rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                rotated[x, rows - 1 - y] = block[y, x];
            }
        }

        return rotated;
    }

    public static byte[,] RotateCCW(byte[,] block)
    {
        int rows = block.GetLength(0);
        int cols = block.GetLength(1);

        byte[,] rotated = new byte[cols, rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                rotated[cols - 1 - x, y] = block[y, x];
            }
        }

        return rotated;
    }
}