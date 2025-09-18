using System;

class ArrayProcessor
{
    private int[] arr;

    // Nhập mảng từ bàn phím
    public void Input()
    {
        Console.Write("Nhập số phần tử của mảng: ");
        int n = int.Parse(Console.ReadLine());
        arr = new int[n];

        Console.WriteLine("Nhập các phần tử của mảng:");
        for (int i = 0; i < n; i++)
        {
            Console.Write("arr[{0}] = ", i);
            arr[i] = int.Parse(Console.ReadLine());
        }
    }

    // Hiển thị mảng
    public void Display()
    {
        foreach (int x in arr)
            Console.Write(x + " ");
        Console.WriteLine();
    }

    // Bubble Sort (tăng dần)
    public void BubbleSort()
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    // Quick Sort
    public void QuickSort(int left, int right)
    {
        int i = left, j = right;
        int pivot = arr[(left + right) / 2];

        while (i <= j)
        {
            while (arr[i] < pivot) i++;
            while (arr[j] > pivot) j--;
            if (i <= j)
            {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
                j--;
            }
        }

        if (left < j)
            QuickSort(left, j);
        if (i < right)
            QuickSort(i, right);
    }

    // Linear Search
    public int LinearSearch(int key)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == key)
                return i;
        }
        return -1; // không tìm thấy
    }

    // Binary Search (yêu cầu mảng đã sắp xếp)
    public int BinarySearch(int key)
    {
        int left = 0, right = arr.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (arr[mid] == key)
                return mid;
            else if (arr[mid] < key)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1; // không tìm thấy
    }

    public int Length()
    {
        return arr.Length;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArrayProcessor ap = new ArrayProcessor();

        // Nhập mảng
        ap.Input();

        Console.WriteLine("\nMảng ban đầu:");
        ap.Display();

        // Sắp xếp bằng Bubble Sort
        ap.BubbleSort();
        Console.WriteLine("\nMảng sau khi sắp xếp bằng Bubble Sort:");
        ap.Display();

        // Sắp xếp lại bằng Quick Sort
        Console.WriteLine("\nNhập lại mảng để thử Quick Sort:");
        ap.Input();
        Console.WriteLine("Mảng ban đầu:");
        ap.Display();

        ap.QuickSort(0, ap.Length() - 1);
        Console.WriteLine("\nMảng sau khi sắp xếp bằng Quick Sort:");
        ap.Display();

        // Tìm kiếm
        Console.Write("\nNhập số cần tìm: ");
        int key = int.Parse(Console.ReadLine());

        int posLinear = ap.LinearSearch(key);
        if (posLinear != -1)
            Console.WriteLine("Linear Search: tìm thấy {0} tại vị trí {1}", key, posLinear);
        else
            Console.WriteLine("Linear Search: không tìm thấy {0}", key);

        int posBinary = ap.BinarySearch(key);
        if (posBinary != -1)
            Console.WriteLine("Binary Search: tìm thấy {0} tại vị trí {1}", key, posBinary);
        else
            Console.WriteLine("Binary Search: không tìm thấy {0}", key);

        Console.ReadKey();
    }
}
