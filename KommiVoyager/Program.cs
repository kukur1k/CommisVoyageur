using KommiVoyager;

KommiVoyagerClass kv = new KommiVoyagerClass();

int[,] matrix = {
    { -1, 4, 5, 7, 5},
    { 8, -1, 5, 6, 6},
    { 3, 5, -1, 9, 6},
    { 3, 5, 6, -1, 2},
    { 6, 2, 3, 8, -1}
};

int res = kv.KommiVoyagerMethod(matrix);

Console.WriteLine(res);


int[,] matrixD =
{
    {-1, 7, 9, -1, -1, 14 },
    {7, -1, 10, 15, -1, -1 },
    {9, 10, -1, 11, -1, 2 },
    {-1, 15, 11, -1, 6, -1},
    {-1, -1, -1, 6, -1, 9},
    {14, -1, 2, -1, 9, -1}
};

Deykstra deykstra = new Deykstra();

int[] result = deykstra.DeykstraMethod(matrixD, 0);


foreach (var item in result)
{
    Console.WriteLine(item);
}
