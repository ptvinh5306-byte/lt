using System;
using System.Diagnostics.Metrics;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("===== Change Base Number System (2, 10, 16) =====");
            Console.WriteLine("Choose input base: 1 = Binary, 2 = Decimal, 3 = Hexadecimal");
            Console.Write("Input choice: ");
            int inputChoice = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose output base: 1 = Binary, 2 = Decimal, 3 = Hexadecimal");
            Console.Write("Output choice: ");
            int outputChoice = int.Parse(Console.ReadLine());

            Console.Write("Enter the input value: ");
            string inputValue = Console.ReadLine();

            try
            {
                // Bước 1: Chuyển về DECIMAL trước
                int decimalValue = 0;
                switch (inputChoice)
                {
                    case 1: // Binary
                        decimalValue = Convert.ToInt32(inputValue, 2);
                        break;
                    case 2: // Decimal
                        decimalValue = int.Parse(inputValue);
                        break;
                    case 3: // Hexadecimal
                        decimalValue = Convert.ToInt32(inputValue, 16);
                        break;
                    default:
                        Console.WriteLine("Invalid input choice!");
                        continue;
                }

                // Bước 2: Chuyển từ DECIMAL sang output base
                string outputValue = "";
                switch (outputChoice)
                {
                    case 1: // Binary
                        outputValue = Convert.ToString(decimalValue, 2);
                        break;
                    case 2: // Decimal
                        outputValue = decimalValue.ToString();
                        break;
                    case 3: // Hexadecimal
                        outputValue = Convert.ToString(decimalValue, 16).ToUpper();
                        break;
                    default:
                        Console.WriteLine("Invalid output choice!");
                        continue;
                }

                Console.WriteLine($"Result: {outputValue}");
            }
            catch
            {
                Console.WriteLine("Invalid input value! Please try again.");
            }

            Console.Write("Do you want to continue? (y/n): ");
            string cont = Console.ReadLine();
            if (cont.ToLower() != "y")
                break;
        }
    }
}

===== Change Base Number System (2, 10, 16) =====
Choose input base: 1 = Binary, 2 = Decimal, 3 = Hexadecimal
Input choice: 2
Choose output base: 1 = Binary, 2 = Decimal, 3 = Hexadecimal
Output choice: 3
Enter the input value: 535
Result: 217
Do you want to continue? (y/n): y

