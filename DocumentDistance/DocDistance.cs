/*
 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
 ||||||||||||||||||             This code belongs to Yousef A. ELkammar (RedBiscuits@Github).   
 ||||||||||||||||||             Any access to this code is forbiden but for Algorithm AAD staff.
 ||||||||||||||||||                     Happy debugging :)
 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
 */


/*
 * 
 * 
 * 
                                        **********                                               **********                       
                                    ******************                                       ******************
                                  **********************                                   **********************
                                 *******  ******  *******                                 *******  ******  *******
                                **************************                               **************************
                                **************************                               **************************
                                **************************                               **************************
                                *****   **********   *****                               *****   **********   *****
                                 *****   ********   *****                                 *****   ********   *****
                                 ******            ******                                 ******            ******
                                  **********************                                   **********************
                                    ******************                                       ******************
                                        **********                                               **********
 * 
 * 
 * 
 * 
 * */



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DocumentDistance
{
    class DocDistance
    {
      // Tryed to make an Async function but couldn't   
    //    private static object objectlock = new object();

        // main func
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            //reading files from paths
            string kammar1 = File.ReadAllText(doc1FilePath).ToLower();
            string kammar2 = File.ReadAllText(doc2FilePath).ToLower();

            
            // caling another fun to seperate concerns
            double kammar3 = Calculatekammar16kammar3(kammar1, kammar2);

            
            
            // clearing docs for memory improvs
            kammar1 = "";
            kammar2 = "";


            // reslt
            return kammar3;
        }

        static double Calculatekammar16kammar3(string kammar1, string kammar2)
        {

            // calc frequencies
            // could be async funs using symaphores critical section protec.
            Dictionary<string, double> kammar4 = Getkammar19(kammar1);
            Dictionary<string, double> kammar5 = Getkammar19(kammar2);

            //list of distinguish kammar17
            List<string> kammar6 = new List<string>();

            //adding kammar17
            kammar6.AddRange(kammar4.Keys);
            kammar6.AddRange(kammar5.Keys);
            kammar6 = kammar6.Distinct().ToList();

            //vectors for represntation
            double[] kammar7 = new double[kammar6.Count];
            double[] kammar8 = new double[kammar6.Count];


            //looping to assign count
            for (int i = 0; i < kammar6.Count; i++)
            {
                if (kammar4.TryGetValue(kammar6[i], out double count1))
                {
                    kammar7[i] = count1;
                }
                if (kammar5.TryGetValue(kammar6[i], out double count2))
                {
                    kammar8[i] = count2;
                }
            }

            // dot and cross products
            double kammar9 = 0.0;
            double kammar10 = 0.0;
            double kammar11 = 0.0;

            // calcing what above
            for (int i = 0; i < kammar7.Length; i++)
            {
                kammar9 += kammar7[i] * kammar8[i];
                kammar10 += kammar7[i] * kammar7[i];
                kammar11 += kammar8[i] * kammar8[i];
            }

            //doneminator
            kammar10 = Math.Sqrt(kammar10 * kammar11);
           

            // angle calc
            const double kammar12 = 0.0;
            double kammar13 = kammar9 / ((kammar10 ) + kammar12);
            double kammar14 = Math.Acos(kammar13);
            double degrees = kammar14 * 180 / Math.PI;

            // clear dics for mem
            kammar4.Clear();
            kammar5.Clear();
            // ret res
            return degrees;
        }

        static Dictionary<string, double> Getkammar19(string kammar16)
        {
            //handling uppercase letters
            kammar16 = kammar16.ToLower();

            //list of kammar17
            // would be better if we use StringBuilder but
            // it gets me a NaN value as i can't access what is in it
            var kammar17 = new List<string>();
            var kammar18 = "";

            // looping at everychar
            foreach (char c in kammar16)
            {
                //if alhpanumeric
                if (char.IsLetterOrDigit(c))
                {
                    kammar18 += c;
                }
                else
                {
                    if (kammar18 != "")
                    {
                        kammar17.Add(kammar18);
                        kammar18 = "";
                    }
                }
            }
            //not empty str
            if (kammar18 != "")
            {
                kammar17.Add(kammar18);
            }

            // frequency
            var kammar19 = new Dictionary<string, double>();

            // calculating frequency arr
            foreach (string word in kammar17)
            {
                if (kammar19.TryGetValue(word, out double count))
                {
                    kammar19[word] = count + 1;
                }
                else
                {
                    kammar19[word] = 1;
                }
            }

            //ret res
            return kammar19;
        }
  

    }


}
