namespace KommiVoyager;



public class KommiVoyager
{
    public int[,] KommiVoyagerMethod(int[,] matrix)
    {
        int[] minElemRowArr = MinElemRow(matrix);
        ReductionMatrixRow(matrix, minElemRowArr);
        
        int[] minElemColumnArr = MinElemColumn(matrix);
        ReductionMatrixColumn(matrix, minElemColumnArr);
        
        int h = CalcBorder(minElemRowArr, minElemColumnArr);

        int[,] newMatrix = ProcessMaxMark(matrix, minElemRowArr, minElemColumnArr);
        
        return new int[,] { };
    }

    
    //================По строке====================
    
    public int[] MinElemRow(int[,] matrix)
    {
        int[] minArr = new int[matrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int minElem = int.MaxValue;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] < minElem && matrix[i, j] != -1)
                {
                    minElem = matrix[i, j];
                }
            }
            minArr[i] = minElem;
        }

        return minArr;
    }

    public void ReductionMatrixRow(int[,] matrix, int[] minRow)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] -= minRow[i];
            }
        }
    }
    
    
    //================По столбцу====================
    
    public int[] MinElemColumn(int[,] matrix)
    {
        int[] minArr = new int[matrix.GetLength(1)];
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            int minElem = int.MaxValue;
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                if (matrix[j, i] < minElem && matrix[j, i] != -1)
                {
                    minElem = matrix[j, i];
                }
            }
            minArr[i] = minElem;
        }

        return minArr;
    }
    
    
    public void ReductionMatrixColumn(int[,] matrix, int[] minRow)
    {
        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                matrix[j, i] -= minRow[i];
            }
        }
    }
    
    
    // Корневая нижняя граница
    public int CalcBorder(int[] di, int[] dj)
    {
        int summ = 0;
        foreach (var item in di)
        {
            summ += item;
        }
        foreach (var item in dj)
        {
            summ += item;
        }
        return summ;
    }
    
    //==================Оценки нулевых ячеек================= 
    public int[,] ProcessMaxMark(int[,] matrix, int[] di, int[] dj)
    {
        int maxMark = int.MinValue;
        int IdxI = 0;
        int IdxJ = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 0)
                {
                    int tempMark = di[i] - dj[j];
                    if (tempMark > maxMark)
                    {
                        maxMark = tempMark;
                        IdxI = i;
                        IdxJ = j;
                    }
                }
            }
        }

        
        
        
        //==========Удаляем лишние строки и столбцы по максимальной оценке
        int[,] newMatrix = new int[matrix.GetLength(0)-1, matrix.GetLength(1)-1];
        int k = 0;
        int m = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            k++;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (i != IdxI && j != IdxJ)
                {
                    newMatrix[k, m] = matrix[i, j];
                    m++;
                } 
            }
        }
        //На обрвтный путь ставим заглушку -1 (М)
        newMatrix[IdxJ, IdxI] = -1;
        
        return newMatrix;
    }
    
}
}