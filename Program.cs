using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;


namespace SFModule136
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "D:/Text1.txt";

            if (File.Exists(filePath)) // Проверим, что файл существует
            {
                // Читаем содержимое файла в строку текста, убираем пунктуацию и приводим все буквы к нижнему регистру
                string text = File.ReadAllText(filePath);
                var noPunctuationText = new string(text.ToLower().Where(c => !char.IsPunctuation(c)).ToArray());

                // Сохраняем символы-разделители в массив
                char[] delimiters = new char[] { ' ', '\r', '\n' };

                // разбиваем строку текста, используя ранее перечисленные символы-разделители
                var words = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // сложим элементы в обычный List
                var wordsList = new List<string>();

                foreach (string word in words)
                    wordsList.Add(word);

                // сложим элементы в List со ссылками
                var wordsLinkedList = new LinkedList<string>();

                foreach (string word in words)
                    wordsLinkedList.AddLast(word);

                // выведем количество элементов в каждой коллекции
                Console.WriteLine("Размер коллекции List: {0} слов.", wordsList.Count);
                Console.WriteLine("Размер коллекции LinkedList: {0} слов.", wordsLinkedList.Count);
                Console.WriteLine();

                // добавим элемент внутрь коллекции List и выведем время добавления к консоль
                var stopWatch1 = Stopwatch.StartNew();
                wordsList.Insert(2, "тест");
                Console.WriteLine("Время добавления элемента в List заняло {0} мс.", stopWatch1.Elapsed.TotalMilliseconds);

                // добавим элемент внутрь коллекции LinkedList и выведем время добавления к консоль
                var stopWatch2 = Stopwatch.StartNew();
                wordsLinkedList.AddAfter(wordsLinkedList.First, "тест");
                Console.WriteLine("Время добавления элемента в LinkedList заняло {0} мс.", stopWatch2.Elapsed.TotalMilliseconds);

                // выведем количество элементов в коллекции после добавления элемента
                Console.WriteLine();
                Console.WriteLine("Размер обновленной коллекции List: {0} слов.", wordsList.Count);
                Console.WriteLine("Размер обновленной коллекции LinkedList: {0} слов.", wordsLinkedList.Count);

                // выведем топ-10 уникальных слов
                Console.WriteLine();
                Console.WriteLine("Топ-10 самых повторяющихся слов:");
                Top10UniqueWords(wordsList);
            }
            else
            {
                Console.WriteLine("Передан некорректный путь к файлу для работы.");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static void Top10UniqueWords(List<string> wordsList)
        {
            var counter = new Dictionary<string, int>();

            foreach (string word in wordsList)
            {
                if (word.Length > 3) // уберем слова менее 4 символов, чтобы было интересно
                {
                    if (!counter.ContainsKey(word))
                    {
                        counter.Add(word, 1); // добавляем слово в словарь
                    }
                    else
                    {
                        counter[word] += 1; // добавляем повторение слова в словарь
                    }
                }
            }

            var sortedCounter = counter.OrderByDescending(x => x.Value);

            var top10 = sortedCounter.Take(10);

            foreach (KeyValuePair<string, int> x in top10)
                Console.WriteLine($"{x.Key} = {x.Value}");
        }
    }
}