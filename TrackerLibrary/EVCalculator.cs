using Newtonsoft.Json;
using System.Runtime.InteropServices;


namespace TrackerLibrary
{
    /// <summary>
    /// Contains all Methods related to Equity Calculations; Calculation Equity of 2 hands doesnt require ref float res3, could be refactured;
    /// </summary>
    public static class EVCalculator
    {
        /// <summary>
        /// Calculates the Equity of 3 Hands using cpp DLL;
        /// </summary>
        /// <param name="myHand1_param">1. Hand Format "Jh";</param>
        /// <param name="myHand2_param">2. Hand Format "Jh";</param>
        /// <param name="myHand3_param">3. Hand Format "Jh";</param>
        /// <param name="res1">Reference to Float, which will save the Equity of the 1. Hand;</param>
        /// <param name="res2">Reference to Float, which will save the Equity of the 2. Hand;</param>
        /// <param name="res3">Reference to Float, which will save the Equity of the 3. Hand;</param>
        [DllImport(GlobalConfig.cppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_3Hands_ReturnFloat(string myHand1_param, string myHand2_param, string myHand3_param, ref float res1, ref float res2, ref float res3);

        /// <summary>
        /// Calculates the Equity of 2 Hands using cpp DLL;
        /// </summary>
        /// <param name="myHand1_param">1. Hand Format "Jh"</param>
        /// <param name="myHand2_param">2. Hand Format "Jh"</param>
        /// <param name="res1">Reference to Float, which will save the Equity of the 1. Hand;</param>
        /// <param name="res2">Reference to Float, which will save the Equity of the 2. Hand;</param>
        /// <param name="res3">Reference to Float. This value is not needed; Could be refactured;</param>
        [DllImport(GlobalConfig.cppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_2Hands_ReturnFloat(string myHand1_param, string myHand2_param, ref float res1, ref float res2, ref float res3);

        /// <summary>
        /// Calculates the Equity of 2 Hands + 1 Hand pharsed as DeathCards using cpp DLL;
        /// </summary>
        /// <param name="myHand1_param">1. Hand Format "Jh"</param>
        /// <param name="myHand2_param">2. Hand Format "Jh"</param>
        /// <param name="deathCards_param">Death Cards Format "Jh"</param>
        /// <param name="res1">Reference to Float, which will save the Equity of the 1. Hand;</param>
        /// <param name="res2">Reference to Float, which will save the Equity of the 2. Hand;</param>
        /// <param name="res3">Reference to Float. This value is not needed; Could be refactured;</param>
        [DllImport(GlobalConfig.cppPokerOddsCalculatorDLL, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CalculateOdds_2Hands_WithDeathCards_ReturnFloat(string myHand1_param, string myHand2_param, string deathCards_param, ref float res1, ref float res2, ref float res3);

        /// <summary>
        /// Calculates the Equity of 2 Hands using PreFlop Equity Array;
        /// </summary>
        /// <param name="Player1_HC1">1. Hand - 1. HoleCard, Format Id "1";</param>
        /// <param name="Player1_HC2">1. Hand - 2. HoleCard, Format Id "1";</param>
        /// <param name="Player2_HC1">2. Hand - 1. HoleCard, Format Id "1";</param>
        /// <param name="Player2_HC2">2. Hand - 2. HoleCard, Format Id "1";</param>
        /// <param name="res1">Reference to Float, which will save the Equity of the 1. Hand;</param>
        /// <param name="res2">Reference to Float, which will save the Equity of the 2. Hand;</param>
        /// <param name="res3">Reference to Float. This value is not needed; Could be refactured;</param>
        /// <param name="ea">Equity Array</param>
        public static void CalculateOdds_2Hands_PF(uint Player1_HC1, uint Player1_HC2, uint Player2_HC1, uint Player2_HC2, ref float res1, ref float res2, ref float res3, ref float[,,,] ea)
        {
            // Cards needs to be placed in the right Order, first the higher CardFace, if equal the higher Suit; 
            // The Equity Array is full just in the upper half; Could be refactured with jagged array to reduce the RAM ussage;
            res1 = ea[(Player1_HC1 < Player1_HC2) ? Player1_HC1 - 1 : Player1_HC2 - 1, (Player1_HC1 < Player1_HC2) ? Player1_HC2 - 1 : Player1_HC1 - 1, (Player2_HC1 < Player2_HC2) ? Player2_HC1 - 1 : Player2_HC2 - 1, (Player2_HC1 < Player2_HC2) ? Player2_HC2 - 1 : Player2_HC1 - 1];


            res2 = 100 - res1;
        }

        /// <summary>
        /// Opens and Reads a File converting the string to Equity Array;
        /// </summary>
        /// <param name="fileName">The full Path of the .txt Equity File;</param>
        /// <returns>Equity Array</returns>
        public static float[,,,] ReadEAFromFileAsFloatArray(string fileName)
        {
            var data = File.ReadAllText(fileName);

            var output = JsonConvert.DeserializeObject<float[,,,]>(data);

            return output;
        }
    }
}
