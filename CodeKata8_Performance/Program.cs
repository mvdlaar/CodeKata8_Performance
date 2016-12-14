using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodeKata8_Performance
{
    class Program
    {
        private static Dictionary<string, List<string>> _fewerLetterWords;

        static void Main(string[] args)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\wordlist.txt";

            using (StreamReader wordListStreamReader = File.OpenText(path))
            {
                List<string> sixLetterWords = new List<string>();
                _fewerLetterWords = new Dictionary<string, List<string>>();

                string wordListLine = "";
                while ((wordListLine = wordListStreamReader.ReadLine()) != null)
                {
                    if (wordListLine.Length == 6)
                    {
                        sixLetterWords.Add(wordListLine.ToLower());
                    }
                    else
                    {
                        if (wordListLine.Length < 6)
                        {
                            wordListLine = wordListLine.ToLower();
                            if (!_fewerLetterWords.ContainsKey(wordListLine[0] + wordListLine.Length.ToString()))
                            {
                                _fewerLetterWords[wordListLine[0] + wordListLine.Length.ToString()] = new List<string>();
                            }
                            _fewerLetterWords[wordListLine[0] + wordListLine.Length.ToString()].Add(wordListLine);
                        }
                    }
                }


                Parallel.ForEach(sixLetterWords, (word) =>
                {
                    for (int i = 1; i < 6 - 1; i++)
                    {
                        string leftWordPart = word.Substring(0, i);

                        bool leftWordPartFound =
                            _fewerLetterWords.ContainsKey(leftWordPart[0] + leftWordPart.Length.ToString()) &&
                            _fewerLetterWords[leftWordPart[0] + leftWordPart.Length.ToString()].Contains(leftWordPart);

                        if (leftWordPartFound)
                        {
                            string rightWordPart = word.Substring(i);
                            if (_fewerLetterWords.ContainsKey(rightWordPart[0] + rightWordPart.Length.ToString()) &&
                                _fewerLetterWords[rightWordPart[0] + rightWordPart.Length.ToString()].Contains(rightWordPart))
                            {
                                StringBuilder resultBuilder = new StringBuilder();
                                resultBuilder.Append(leftWordPart);
                                resultBuilder.Append(" + ");
                                resultBuilder.Append(rightWordPart);
                                resultBuilder.Append(" => ");
                                resultBuilder.Append(word);
                                Console.WriteLine(resultBuilder.ToString());
                            }
                        }
                    }
                });

                Console.ReadKey();
            }
        }
    }
}
