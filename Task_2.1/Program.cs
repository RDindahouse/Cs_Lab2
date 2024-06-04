// Программа берёт 3 последние строки из файла
// т.к. не указано что файл должен быть ограничен тремя строками
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // .txt file path
        string filePath = "test.txt";

        try
        {
            // Reading the contents of a file
            string[] lines = File.ReadAllLines(filePath);

            // Check that the file has > 3 lines
            if (lines.Length < 3)
            {
                Console.WriteLine("The file contains insufficient lines.");
                return;
            }
            //debug

            //for (int i = lines.Length - 3; i < lines.Length; i++)
            //{
            //   Console.WriteLine(lines[i]);
            //}
            //Console.WriteLine();

            // Output last 3 lines in reverse order
            for (int i = lines.Length - 1; i >= lines.Length - 3; i--)
            {
                Console.WriteLine(lines[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}