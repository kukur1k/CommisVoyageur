namespace KommiVoyager;

public class KommiVoyagerClass
{
    public int KommiVoyagerMethod(int[,] matrix)
    {
        
        //Коллекция для H
        List<int> listH = new List<int>();
        listH.Add(0);
        int idxH = 0;

        bool active = true;
        while (active)
        {
            int[] minElemRowArr = MinElemRow(matrix);
            ReductionMatrixRow(matrix, minElemRowArr);
        
            int[] minElemColumnArr = MinElemColumn(matrix);
            ReductionMatrixColumn(matrix, minElemColumnArr);


            int h = CalcBorder(minElemRowArr, minElemColumnArr) + listH[idxH];
            listH.Add(h);
            idxH++;

            int[,] newMatrix;
            if (matrix.GetLength(0) == 2 & matrix.GetLength(1) == 2) 
            {
                newMatrix = ProcessMaxMarkForTwo(matrix);
                minElemRowArr = MinElemRow(matrix);
                ReductionMatrixRow(matrix, minElemRowArr);

                minElemColumnArr = MinElemColumn(matrix);
                ReductionMatrixColumn(matrix, minElemColumnArr);


                h = CalcBorder(minElemRowArr, minElemColumnArr) + listH[idxH];
                listH.Add(h);
                idxH++;
                active = false;
            }
            else
            {
                newMatrix = ProcessMaxMark(matrix);
                matrix = newMatrix;
            }
        }

        return listH.Last();
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
                if (matrix[i, j] != -1)
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
                if (matrix[j, i] != -1)
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
            if (item != int.MaxValue)
                summ += item;
        }
        foreach (var item in dj)
        {
            if (item != int.MaxValue)
                summ += item;
        }
        return summ;
    }
    
    //==================Оценки нулевых ячеек================= 
    public int[,] ProcessMaxMark(int[,] matrix)
    {
        int maxMark = int.MinValue;

        int tempMark = 0;

        int IdxI = 0;
        int IdxJ = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 0)
                {

                    // минимум по строке
                    int minElemStr = int.MaxValue;
                    for(int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (c != j && matrix[i, c] < minElemStr && matrix[i, c] != -1)
                        {
                            minElemStr = matrix[i, c];
                        }
                    }

                    // минимум по столбцу
                    int minElemColumn = int.MaxValue;
                    for (int c = 0; c < matrix.GetLength(0); c++)
                    {
                        if (c != i && matrix[c, j] < minElemColumn && matrix[c, j] != -1)
                        {
                            minElemColumn = matrix[c, j];
                        }
                    }

                    if (minElemStr == int.MaxValue) minElemStr = 0;
                    if (minElemColumn == int.MaxValue) minElemColumn = 0;

                    tempMark = minElemStr + minElemColumn;
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
        for (int s = 0; s < matrix.GetLength(0); s++)
        {
            if (s == IdxI) continue;
            int m = 0;
            for (int l = 0; l < matrix.GetLength(1); l++)
            {
                if (l == IdxJ) continue;
                newMatrix[k, m] = matrix[s, l];
                m++;
            }
            k++;
        }

        //На обратный путь ставим заглушку -1 (М)
        if (IdxJ < newMatrix.GetLength(0) && IdxI < newMatrix.GetLength(1))
            newMatrix[IdxJ, IdxI] = -1;
        
        return newMatrix;
    }

    //==================Оценки нулевых ячеек для случая с двумя клетками================= 
    public int[,] ProcessMaxMarkForTwo(int[,] matrix)
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

                    // минимум по строке
                    int minElemStr = int.MaxValue;
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (c != j && matrix[i, c] < minElemStr && matrix[i, c] != -1)
                        {
                            minElemStr = matrix[i, c];
                        }
                    }

                    // минимум по столбцу
                    int minElemColumn = int.MaxValue;
                    for (int c = 0; c < matrix.GetLength(0); c++)
                    {
                        if (c != i && matrix[c, j] < minElemColumn && matrix[c, j] != -1)
                        {
                            minElemColumn = matrix[c, j];
                        }
                    }

                    if (minElemStr == int.MaxValue) minElemStr = 0;
                    if (minElemColumn == int.MaxValue) minElemColumn = 0;

                    int tempMark = minElemStr + minElemColumn;
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
        int[,] newMatrix = new int[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
        int k = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (i == IdxI) continue;
            int m = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (j == IdxJ) continue;
                newMatrix[k, m] = matrix[i, j];
                m++;
            }
            k++;
        }

        //На обратный путь ставим заглушку -1 (М)
        if (IdxJ < newMatrix.GetLength(0) && IdxI < newMatrix.GetLength(1))
            newMatrix[IdxJ, IdxI] = -1;

        return newMatrix;
    }
}