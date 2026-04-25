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