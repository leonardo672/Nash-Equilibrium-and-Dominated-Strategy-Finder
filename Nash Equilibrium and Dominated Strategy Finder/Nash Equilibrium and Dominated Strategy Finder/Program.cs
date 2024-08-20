using System;

class Program
{
    static void Main()
    {

        int[,] payoffMatrix = new int[,]
        {
            { 6, 5, 3, 4 },
            { 7, 4, 7, 8 },
            { 3, 3, 2, 4 }
        };


        FindOptimalStrategies(payoffMatrix);
    }

    static void FindOptimalStrategies(int[,] matrix)
    {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);


        bool[] dominatedRows = IdentifyDominatedRows(matrix);
        bool[] dominatedCols = IdentifyDominatedCols(matrix);

        Console.WriteLine("Сокращенная матрица:");
        for (int i = 0; i < numRows; i++)
        {
            if (dominatedRows[i]) continue;
            for (int j = 0; j < numCols; j++)
            {
                if (dominatedCols[j]) continue;
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }


        Console.WriteLine("\nРавновесия Нэша:");
        for (int i = 0; i < numRows; i++)
        {
            if (dominatedRows[i]) continue;
            for (int j = 0; j < numCols; j++)
            {
                if (dominatedCols[j]) continue;

                if (IsNashEquilibrium(matrix, i, j, dominatedRows, dominatedCols))
                {
                    Console.WriteLine($"A{i + 1}, B{j + 1} с выплатой {matrix[i, j]}");
                }
            }
        }
    }

    static bool[] IdentifyDominatedRows(int[,] matrix)
    {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);
        bool[] dominatedRows = new bool[numRows];

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                if (i != j && !dominatedRows[j])
                {
                    bool rowDominated = true;
                    for (int k = 0; k < numCols; k++)
                    {
                        if (matrix[i, k] > matrix[j, k])
                        {
                            rowDominated = false;
                            break;
                        }
                    }
                    if (rowDominated)
                    {
                        dominatedRows[i] = true;
                    }
                }
            }
        }
        return dominatedRows;
    }

    static bool[] IdentifyDominatedCols(int[,] matrix)
    {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);
        bool[] dominatedCols = new bool[numCols];

        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (i != j && !dominatedCols[j])
                {
                    bool colDominated = true;
                    for (int k = 0; k < numRows; k++)
                    {
                        if (matrix[k, i] < matrix[k, j])
                        {
                            colDominated = false;
                            break;
                        }
                    }
                    if (colDominated)
                    {
                        dominatedCols[i] = true;
                    }
                }
            }
        }
        return dominatedCols;
    }

    static bool IsNashEquilibrium(int[,] matrix, int row, int col, bool[] dominatedRows, bool[] dominatedCols)
    {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);


        for (int i = 0; i < numRows; i++)
        {
            if (!dominatedRows[i] && matrix[i, col] > matrix[row, col])
            {
                return false;
            }
        }


        for (int j = 0; j < numCols; j++)
        {
            if (!dominatedCols[j] && matrix[row, j] > matrix[row, col])
            {
                return false;
            }
        }

        return true;
    }
}
