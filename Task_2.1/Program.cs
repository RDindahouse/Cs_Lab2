// Программа берёт 3 последние строки из файла
// т.к. не указано что файл должен быть ограничен тремя строками
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Путь к текстовому файлу
        string filePath = "test.txt";

        try
        {
            // Чтение содержимого файла
            string[] lines = File.ReadAllLines(filePath);

            // Проверка, что в файле > 3 строк
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

            // Вывод последних 3 строк в обратном порядке
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