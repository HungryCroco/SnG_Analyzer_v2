using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;



namespace EVCalculator
{
    public class ImportDLL
    {
        public const string CppPokerOddsCalculatorDLL = @"C:\Users\tatsi\source\repos\Poker\PokerOddsCalculator\x64\Release\PokerOddsCalculator_v4.dll";
        public const string pfEA = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\eaPF_a.txt";

        [DllImport(CppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_3Hands(string myHand1_param, string myHand2_param, string myHand3_param, ref double res1, ref double res2, ref double res3);

        [DllImport(CppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_2Hands(string myHand1_param, string myHand2_param, ref double res1, ref double res2, ref double res3);

        [DllImport(CppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_2Hands_WithDeathCards(string myHand1_param, string myHand2_param, string deathCards_param, ref float res1, ref float res2, ref float res3);

        [DllImport(CppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_3Hands_ReturnFloat(string myHand1_param, string myHand2_param, string myHand3_param, ref float res1, ref float res2, ref float res3);

        [DllImport(CppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_2Hands_ReturnFloat(string myHand1_param, string myHand2_param, ref float res1, ref float res2, ref float res3);

        [DllImport(CppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_2Hands_WithDeathCards_ReturnFloat(string myHand1_param, string myHand2_param, string deathCards_param, ref float res1, ref float res2, ref float res3);

        public static void CalculateOdds_2Hands_PF(uint Player1_HC1, uint Player1_HC2, uint Player2_HC1, uint Player2_HC2, ref float res1, ref float res2, ref float res3, float[,,,] ea)
        {

            res1 = ea[(Player1_HC1 < Player1_HC2) ? Player1_HC1-1 : Player1_HC2-1, (Player1_HC1 < Player1_HC2) ? Player1_HC2-1 : Player1_HC1-1, (Player2_HC1 < Player2_HC2) ? Player2_HC1-1 : Player2_HC2-1, (Player2_HC1 < Player2_HC2) ? Player2_HC2-1 : Player2_HC1-1];

            
            res2 = 100 - res1;
        }

        static void Main(string[] args)
        {
            {
                Stopwatch _watch = new();

                double res1 = 0;
                double res2 = 0;
                double res3 = 0;
                float res1f = 0;
                float res2f = 0;
                float res3f = 0;

                //CalculateOdds_3Hands("AhQh", "7c7d", "Ad4s", ref res1, ref res2, ref res3);
                //CalculateOdds_3Hands("AhKc", "3h3c", "5h5c", ref res1, ref res2, ref res3);
                _watch.Start();

                CalculateOdds_2Hands("AhQh", "7c7d", ref res1, ref res2, ref res3);
                _watch.Stop();

                Console.WriteLine(_watch.ElapsedTicks);
                Console.WriteLine(res1);
                Console.WriteLine(res2);

                Console.WriteLine("-----");

                float[,,,] ea = ReadEAFromFileAsFloatArray(pfEA);

                _watch.Restart();

                //CalculateOdds_2Hands_PF("8d8h" , "KdKs", ref res1f, ref res2f, ref res3f, ea);
                _watch.Stop();

                Console.WriteLine(_watch.ElapsedTicks);
                Console.WriteLine(res1f);
                Console.WriteLine(res2f);






                Console.WriteLine("-----");
                Console.ReadLine();

                
            }
        }

        public static float[,,,] ReadEAFromFileAsFloatArray(string _fileName)
        {
            var data = File.ReadAllText(_fileName);

            var output = JsonConvert.DeserializeObject<float[,,,]>(data);

            return output;
        }
    }
}
