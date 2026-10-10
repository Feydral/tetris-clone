public static class Blocks
{
    public static readonly byte[][,] Shapes =
    {
        // I
        new byte[,]
        {
            { 1, 1, 1, 1 }
        },

        // O
        new byte[,]
        {
            { 1, 1 },
            { 1, 1 }
        },

        // T
        new byte[,]
        {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        },

        // L
        new byte[,]
        {
            { 0, 0, 1 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        },

        // J
        new byte[,]
        {
            { 1, 0, 0 },
            { 1, 1, 1 },
            { 0, 0, 0 }
        },

        // S
        new byte[,]
        {
            { 0, 1, 1 },
            { 1, 1, 0 },
            { 0, 0, 0 }
        },

        // Z
        new byte[,]
        {
            { 1, 1, 0 },
            { 0, 1, 1 },
            { 0, 0, 0 }
        }
    };

    public static byte[,] Rotate(byte[,] block)
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
}