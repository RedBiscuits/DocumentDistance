using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentDistance
{



    /*
    class DocDistance
    {

        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            string document1 = File.ReadAllText(doc1FilePath).ToLower();
            string document2 = File.ReadAllText(doc2FilePath).ToLower();

            double distance = CalculateDocumentDistance(document1, document2);

            return distance;
        }

        static double CalculateDocumentDistance(string document1, string document2)
        {
            Dictionary<string, double> wordFrequency1 = GetWordFrequency(document1);
            Dictionary<string, double> wordFrequency2 = GetWordFrequency(document2);

            IEnumerable<string> uniqueWords = wordFrequency1.Keys.Union(wordFrequency2.Keys);

            double[] vector1 = new double[uniqueWords.Count()];
            double[] vector2 = new double[uniqueWords.Count()];

            int index = 0;
            foreach (string word in uniqueWords)
            {
                vector1[index] = wordFrequency1.ContainsKey(word) ? Math.Log10(1 + wordFrequency1[word]) : 0;
                vector2[index] = wordFrequency2.ContainsKey(word) ? Math.Log10(1 + wordFrequency2[word]) : 0;
                index++;
            }

            double dotProduct = 0.0;
            double magnitude1 = 0.0;
            double magnitude2 = 0.0;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                magnitude1 += vector1[i] * vector1[i];
                magnitude2 += vector2[i] * vector2[i];
            }

            magnitude1 = Math.Sqrt(magnitude1);
            magnitude2 = Math.Sqrt(magnitude2);

            double cosineSimilarity = dotProduct / (magnitude1 * magnitude2) + 0.0;
            double radians = Math.Acos(cosineSimilarity);
            double degrees = radians * 180 / Math.PI;

            return degrees;
        }

        static Dictionary<string, double> GetWordFrequency(string document)
        {
            document = document.ToLower();
            document = Regex.Replace(document, @"[^\w\s\]", "");

            string[] words = document.Split(' ');

           

            Dictionary<string, double> wordFrequency = new Dictionary<string, double>();
            foreach (string word in words)
            {

                if (wordFrequency.ContainsKey(word))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }

            return wordFrequency;
        }
    }
    */

    
    class DocDistance
    {
        private static object objectlock = new object();

        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            string document1 = File.ReadAllText(doc1FilePath).ToLower();
            string document2 = File.ReadAllText(doc2FilePath).ToLower();

            double distance = CalculateDocumentDistance(document1, document2);

            document1 = "";
            document2 = "";


            return distance;
        }

        static double CalculateDocumentDistance(string document1, string document2)
        {
            Dictionary<string, double> wordFrequency1 = GetWordFrequency(document1);
            Dictionary<string, double> wordFrequency2 = GetWordFrequency(document2);

            List<string> uniqueWordsList = new List<string>();
            uniqueWordsList.AddRange(wordFrequency1.Keys);
            uniqueWordsList.AddRange(wordFrequency2.Keys);
            uniqueWordsList = uniqueWordsList.Distinct().ToList();

            double[] vector1 = new double[uniqueWordsList.Count];
            double[] vector2 = new double[uniqueWordsList.Count];

            for (int i = 0; i < uniqueWordsList.Count; i++)
            {
                if (wordFrequency1.TryGetValue(uniqueWordsList[i], out double count1))
                {
                    vector1[i] = count1;
                }
                if (wordFrequency2.TryGetValue(uniqueWordsList[i], out double count2))
                {
                    vector2[i] = count2;
                }
            }

            double dotProduct = 0.0;
            double magnitude1 = 0.0;
            double magnitude2 = 0.0;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                magnitude1 += vector1[i] * vector1[i];
                magnitude2 += vector2[i] * vector2[i];
            }

            magnitude1 = Math.Sqrt(magnitude1 * magnitude2);
           

            const double epsilon = 0.0;
            double cosineSimilarity = dotProduct / ((magnitude1 ) + epsilon);
            double radians = Math.Acos(cosineSimilarity);
            double degrees = radians * 180 / Math.PI;

            wordFrequency1.Clear();
            wordFrequency2.Clear();
            return degrees;
        }

        static Dictionary<string, double> GetWordFrequency(string document)
        {
            document = document.ToLower();
            var words = new List<string>();
            var currentWord = "";

            foreach (char c in document)
            {
                if (char.IsLetterOrDigit(c))
                {
                    currentWord += c;
                }
                else
                {
                    if (currentWord != "")
                    {
                        words.Add(currentWord);
                        currentWord = "";
                    }
                }
            }

            if (currentWord != "")
            {
                words.Add(currentWord);
            }

            var wordFrequency = new Dictionary<string, double>();

            foreach (string word in words)
            {
                if (wordFrequency.TryGetValue(word, out double count))
                {
                    wordFrequency[word] = count + 1;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }

            return wordFrequency;
        }
    }


}


