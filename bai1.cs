using System;

class MatrixProgram
{
    // Nhập ma trận
    static int[,] InputMatrix(out int rows, out int cols, string name)
    {
        Console.WriteLine($"Nhập ma trận {name}:");
        Console.Write("Nhập số dòng: ");
        rows = int.Parse(Console.ReadLine());
        Console.Write("Nhập số cột: ");
        cols = int.Parse(Console.ReadLine());

        int[,] matrix = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"[{i},{j}] = ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }
        return matrix;
    }

    // Hiển thị ma trận
    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Cộng ma trận
    static int[,] AddMatrix(int[,] A, int[,] B)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = A[i, j] + B[i, j];
        return result;
    }

    // Nhân ma trận
    static int[,] MultiplyMatrix(int[,] A, int[,] B)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int rowsB = B.GetLength(0);
        int colsB = B.GetLength(1);

        if (colsA != rowsB)
        {
            Console.WriteLine("Không thể nhân: số cột A khác số dòng B!");
            return null;
        }

        int[,] result = new int[rowsA, colsB];
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < colsA; k++)
                    result[i, j] += A[i, k] * B[k, j];
            }
        }
        return result;
    }

    // Chuyển vị ma trận
    static int[,] TransposeMatrix(int[,] A)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        int[,] result = new int[cols, rows];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[j, i] = A[i, j];
        return result;
    }

    // Tìm giá trị lớn nhất và nhỏ nhất
    static void FindMinMax(int[,] A)
    {
        int min = A[0, 0], max = A[0, 0];
        foreach (int val in A)
        {
            if (val < min) min = val;
            if (val > max) max = val;
        }
        Console.WriteLine($"Giá trị nhỏ nhất = {min}, lớn nhất = {max}");
    }

    // Tính định thức (sử dụng đệ quy, chỉ cho ma trận vuông)
    static int Determinant(int[,] A)
    {
        int n = A.GetLength(0);
        if (n == 1) return A[0, 0];
        if (n == 2) return A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];

        int det = 0;
        for (int col = 0; col < n; col++)
        {
            int[,] minor = GetMinor(A, 0, col);
            det += (col % 2 == 0 ? 1 : -1) * A[0, col] * Determinant(minor);
        }
        return det;
    }

    // Lấy ma trận con khi bỏ đi dòng row và cột col
    static int[,] GetMinor(int[,] A, int row, int col)
    {
        int n = A.GetLength(0);
        int[,] minor = new int[n - 1, n - 1];
        int r = 0, c;
        for (int i = 0; i < n; i++)
        {
            if (i == row) continue;
            c = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == col) continue;
                minor[r, c++] = A[i, j];
            }
            r++;
        }
        return minor;
    }

    // Kiểm tra ma trận đối xứng
    static bool IsSymmetric(int[,] A)
    {
        int n = A.GetLength(0);
        if (A.GetLength(0) != A.GetLength(1)) return false;

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (A[i, j] != A[j, i]) return false;

        return true;
    }

    static void Main()
    {
        int[,] A = InputMatrix(out int rowsA, out int colsA, "A");
        Console.WriteLine("Ma trận A:");
        PrintMatrix(A);

        int choice;
        do
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1. Cộng hai ma trận");
            Console.WriteLine("2. Nhân hai ma trận");
            Console.WriteLine("3. Chuyển vị ma trận A");
            Console.WriteLine("4. Tìm giá trị lớn nhất và nhỏ nhất của A");
            Console.WriteLine("5. Tính định thức của A (nếu vuông)");
            Console.WriteLine("6. Kiểm tra A có đối xứng không");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn chức năng: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        int[,] B = InputMatrix(out int rowsB, out int colsB, "B");
                        if (rowsA == rowsB && colsA == colsB)
                        {
                            int[,] C = AddMatrix(A, B);
                            Console.WriteLine("Kết quả A + B:");
                            PrintMatrix(C);
                        }
                        else Console.WriteLine("Kích thước không phù hợp để cộng!");
                        break;
                    }
                case 2:
                    {
                        int[,] B = InputMatrix(out int rowsB, out int colsB, "B");
                        int[,] C = MultiplyMatrix(A, B);
                        if (C != null)
                        {
                            Console.WriteLine("Kết quả A * B:");
                            PrintMatrix(C);
                        }
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Ma trận chuyển vị A:");
                        PrintMatrix(TransposeMatrix(A));
                        break;
                    }
                case 4:
                    {
                        FindMinMax(A);
                        break;
                    }
                case 5:
                    {
                        if (rowsA == colsA)
                            Console.WriteLine($"Định thức = {Determinant(A)}");
                        else
                            Console.WriteLine("A không phải ma trận vuông!");
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine(IsSymmetric(A) ? "A là ma trận đối xứng" : "A không đối xứng");
                        break;
                    }
            }
        } while (choice != 0);
    }
}
