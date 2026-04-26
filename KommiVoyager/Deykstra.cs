namespace KommiVoyager;

public class Deykstra
{
    public int[] DeykstraMethod(int[,] matrix, int start)
    {
        int countPoint = matrix.GetLength(0); // количество вкршин
        int[] distance = new int[countPoint]; // расстояния от А до вершин
        bool[] active = new bool[countPoint]; // проверена ли точка

        // ЗАБИЛИ РАССТОЯНИЯ бесконечностью
        for (int i = 0; i < countPoint; i++)
        {
            distance[i] = int.MaxValue;
        }

        // ставим 0 у первой клетке
        distance[start] = 0;

        for (int i = 0; i < countPoint - 1; i++)
        {
            // ищем вершины с минимальными значениями расстояния и непройденные (проходим по distance)
            int minDistance = int.MaxValue;
            int minIdx = -1;

            for (int j = 0; j < countPoint; j++)
            {
                // если точка не закрыта и меньше
                if (!active[j] && distance[j] <= minDistance)
                {
                    minDistance = distance[j];
                    minIdx = j;
                }
            }

            if (minIdx == -1) // если нет открытых вершин то закончили
            {
                break;
            }
            // найденную минимальную отмечаем
            active[minIdx] = true;

            // рассчитываем расстояние до соседних вершин
            // (нашли индекс с минимальным расстоянием (допустим 2) и смотрим в соответствующей строке матрицы (строка 2)
            // расстояния от нее до других вершин)
            for (int j = 0; j < countPoint; j++)
            {
                // проходим по столбцам (по вершинам) ряда с найденным минимальным расстоянием и смотрим,
                // если не закрыта
                // и существует путь из найденной с минимальным расстоянием (не -1)
                // и расстояние до найденной минимальной не бесконечность 
                if (!active[j] && matrix[minIdx, j] != -1 && distance[minIdx] != int.MaxValue)
                {
                    // тогда считаем новое расстояние 
                    // расстояние от А до найденной с минимальным расстоянгием + расстояние от минимальной до j (найденной в ряду)
                    int newDistance = distance[minIdx] + matrix[minIdx, j];
                    // если новое меньше, то записываем расстояние до j придавляя найденное до минимальной
                    if (newDistance < distance[j])
                    {
                        distance[j] = newDistance;
                    }
                }
            }
        }
        
        return distance;
    }
    
    
}