using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class TextProcessor
{
    private string text;

    public TextProcessor(string input)
    {
        text = input;
    }

    // 1. Chuẩn hóa văn bản
    public string NormalizeText()
    {
        // Xóa khoảng trắng thừa
        text = Regex.Replace(text, @"\s+", " ").Trim();

        // Viết hoa ký tự đầu mỗi câu
        string[] sentences = Regex.Split(text, @"(?<=[\.!\?])\s+");
        for (int i = 0; i < sentences.Length; i++)
        {
            if (!string.IsNullOrEmpty(sentences[i]))
            {
                sentences[i] = sentences[i].Trim();
                sentences[i] = char.ToUpper(sentences[i][0]) + sentences[i].Substring(1);
            }
        }
        text = string.Join(" ", sentences);
        return text;
    }

    // 2. Thống kê văn bản
    public (int totalWords, int distinctWords, Dictionary<string, int> frequency) AnalyzeText()
    {
        string[] words = text
            .ToLower()
            .Split(new char[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

        int totalWords = words.Length;
        int distinctWords = words.Distinct().Count();

        Dictionary<string, int> frequency = new Dictionary<string, int>();
        foreach (var w in words)
        {
            if (frequency.ContainsKey(w))
                frequency[w]++;
            else
                frequency[w] = 1;
        }

        return (totalWords, distinctWords, frequency);
    }

    // 3. Hiển thị kết quả
    public void DisplayResult()
    {
        string normalized = NormalizeText();
        Console.WriteLine("Văn bản chuẩn hóa:");
        Console.WriteLine(normalized);

        var (total, distinct, freq) = AnalyzeText();
        Console.WriteLine("\nThống kê văn bản:");
        Console.WriteLine($"Tổng số từ: {total}");
        Console.WriteLine($"Số lượng từ khác nhau: {distinct}");
        Console.WriteLine("Tần suất xuất hiện các từ:");

        foreach (var pair in freq.OrderByDescending(p => p.Value))
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Nhập đoạn văn bản:");
        string input = Console.ReadLine();

        TextProcessor tp = new TextProcessor(input);
        tp.DisplayResult();

        Console.ReadKey();
    }
}

