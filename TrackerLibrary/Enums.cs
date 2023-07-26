using System.ComponentModel;
using System.Reflection;


namespace TrackerLibrary
{
    /// <summary>
    /// This Class contains extension Methods helping to convert Enums or get Descriptions etc;
    /// </summary>
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// Get the Description of Enum;
        /// </summary>
        /// <param name="GenericEnum">Enum</param>
        /// <returns>Description as String;</returns>
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

        /// <summary>
        /// Converts a string describing Card (like "Jh", "8d") to Uint(Id)
        /// </summary>
        /// <param name="card"></param>
        /// <returns>ID related to the Card;</returns>
        public static uint ConvertCardStringToUint(this string card)
        {
            try
            {
                if (card != "")
                {
                    if (card[1] == 'c')
                    {
                        return card[0].ConvertCardFacestringToUint(0);
                    }
                    else if (card[1] == 'd')
                    {
                        return card[0].ConvertCardFacestringToUint(1);
                    }
                    else if (card[1] == 'h')
                    {
                        return card[0].ConvertCardFacestringToUint(2);
                    }
                    else if (card[1] == 's')
                    {
                        return card[0].ConvertCardFacestringToUint(3);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                return 0;
            }

        }

        /// <summary>
        /// Converts a string describing CardFace (like "J", "8") to Uint(Id)
        /// </summary>
        /// <param name="cardFace">Card Face as Char</param>
        /// <param name="colorOffset">Color Offset: 
        ///                 -Clubs      - 0
        ///                 - Diamonds  - 1
        ///                 - Hearts    - 2
        ///                 - Spades    - 3</param>
        /// <returns>ID related to the Card;</returns>
        private static uint ConvertCardFacestringToUint(this char cardFace, uint colorOffset)
        {
            switch (cardFace)
            {
                case 'A':
                    return 1 + colorOffset;
                case 'K':
                    return 5 + colorOffset;
                case 'Q':
                    return 9 + colorOffset;
                case 'J':
                    return 13 + colorOffset;
                case 'T':
                    return 17 + colorOffset;
                case '9':
                    return 21 + colorOffset;
                case '8':
                    return 25 + colorOffset;
                case '7':
                    return 29 + colorOffset;
                case '6':
                    return 33 + colorOffset;
                case '5':
                    return 37 + colorOffset;
                case '4':
                    return 41 + colorOffset;
                case '3':
                    return 45 + colorOffset;
                case '2':
                    return 49 + colorOffset;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Converts 2 CardsAsString To (HoleCardsSimple)AllCardsSimple Id;
        /// </summary>
        /// <param name="hc1">1. HoleCard as String;</param>
        /// <param name="hc2">2. HoleCard as String;</param>
        /// <returns>(HoleCardsSimple)AllCardsSimple Id;</returns>
        public static string CalculateHoleCardsSimple(string hc1, string hc2)
        {
            string output = "";
            string temp = "";
            //Check which CardFace is higher, as the higher CardFace is always first to reduce the combos;
            //Switch cardStrings if necessary;
            //Check if CardFaces are different, Add "o" for offSuit in case the suit is different or "s" for suited otherways;
            if (hc1[0].ConvertCardFacestringToUint(0) > hc2[0].ConvertCardFacestringToUint(0))
            {
                temp = hc1;
                hc1 = hc2;
                hc2 = temp;
            }
            if (hc1 != "")
            {
                output += hc1[0];
                output += hc2[0];
                if (hc1[0] != hc2[0])
                {

                    if (hc1[1] == hc2[1])
                    {
                        output += "s";
                    }
                    else
                    {
                        output += "o";
                    }
                }
            }

            return output;
        }


        /// <summary>
        /// Converts a HoleCardsSimple String to Enum-CardAllSimple(id);
        /// </summary>
        /// <param name="hcsSimple">HoleCardsSimple as String</param>
        /// <returns>Id</returns>
        public static int ConvertHoleCardsSimpleToEnum(string hcsSimple)
        {
            int output = 0;
            if (hcsSimple != "XX")
            {
                output = (int)Enum.Parse<CardAllSimple>("_" + hcsSimple);
            }
            return output;
        }
    }

    /// <summary>
    /// Positions as Enum, currently not used;
    /// </summary>
    public enum Position
    {
        BTN = 0,
        SB = 8,
        BB = 9
    }

    /// <summary>
    /// Levels as Enum, currently not used;
    /// </summary>
    public enum Level
    {
        [Description("10/20")] Level_I,
        [Description("15/30")] Level_II,
        [Description("20/40")] Level_III,
        [Description("30/60")] Level_IV,
        [Description("40/80")] Level_V,
        [Description("50/100")] Level_VI,
        [Description("60/120")] Level_VII
    }
    /// <summary>
    /// CardFaces as Enum, currently not used;
    /// </summary>
    public enum CardFace
    {
        [Description("E")] Empty = 1,
        [Description("2")] Two = 2,
        [Description("3")] Three = 3,
        [Description("4")] Four = 4,
        [Description("5")] Five = 5,
        [Description("6")] Six = 6,
        [Description("7")] Seven = 7,
        [Description("8")] Eight = 8,
        [Description("9")] Nine = 9,
        [Description("T")] Ten = 10,
        [Description("J")] Jack = 11,
        [Description("Q")] Queen = 12,
        [Description("K")] King = 13,
        [Description("A")] Ace = 14
    }

    /// <summary>
    /// CardSuits As Enum, currently not used;
    /// </summary>
    public enum CardSuit
    {
        [Description("c")] Clubs = 1,
        [Description("d")] Diamonds = 2,
        [Description("h")] Hearts = 3,
        [Description("s")] Spades = 4,
        [Description("e")] Empty = 5
    };


    /// <summary>
    /// Street as Enum, currently not used;
    /// </summary>
    public enum Street
    {
        [Description("PF")] PreFlop = 1,
        [Description("F")] Flop = 2,
        [Description("T")] Turn = 3,
        [Description("R")] River = 4,
    }

    /// <summary>
    /// Enum - CardAllSimple, used to get Id of HoleCards and to power up HeatMap;
    /// </summary>
    public enum CardAllSimple
    {
        [Description("UNKNOWN")] _UC = 0,
        [Description("AA")] _AA = 1,
        [Description("AKo")] _AKo = 2,
        [Description("AKs")] _AKs = 3,
        [Description("AQo")] _AQo = 4,
        [Description("AQs")] _AQs = 5,
        [Description("AJo")] _AJo = 6,
        [Description("AJs")] _AJs = 7,
        [Description("ATo")] _ATo = 8,
        [Description("ATs")] _ATs = 9,
        [Description("A9o")] _A9o = 10,
        [Description("A9s")] _A9s = 11,
        [Description("A8o")] _A8o = 12,
        [Description("A8s")] _A8s = 13,
        [Description("A7o")] _A7o = 14,
        [Description("A7s")] _A7s = 15,
        [Description("A6o")] _A6o = 16,
        [Description("A6s")] _A6s = 17,
        [Description("A5o")] _A5o = 18,
        [Description("A5s")] _A5s = 19,
        [Description("A4o")] _A4o = 20,
        [Description("A4s")] _A4s = 21,
        [Description("A3o")] _A3o = 22,
        [Description("A3s")] _A3s = 23,
        [Description("A2o")] _A2o = 24,
        [Description("A2s")] _A2s = 25,
        [Description("KK")] _KK = 26,
        [Description("KQo")] _KQo = 27,
        [Description("KQs")] _KQs = 28,
        [Description("KJo")] _KJo = 29,
        [Description("KJs")] _KJs = 30,
        [Description("KTo")] _KTo = 31,
        [Description("KTs")] _KTs = 32,
        [Description("K9o")] _K9o = 33,
        [Description("K9s")] _K9s = 34,
        [Description("K8o")] _K8o = 35,
        [Description("K8s")] _K8s = 36,
        [Description("K7o")] _K7o = 37,
        [Description("K7s")] _K7s = 38,
        [Description("K6o")] _K6o = 39,
        [Description("K6s")] _K6s = 40,
        [Description("K5o")] _K5o = 41,
        [Description("K5s")] _K5s = 42,
        [Description("K4o")] _K4o = 43,
        [Description("K4s")] _K4s = 44,
        [Description("K3o")] _K3o = 45,
        [Description("K3s")] _K3s = 46,
        [Description("K2o")] _K2o = 47,
        [Description("K2s")] _K2s = 48,
        [Description("QQ")] _QQ = 49,
        [Description("QJo")] _QJo = 50,
        [Description("QJs")] _QJs = 51,
        [Description("QTo")] _QTo = 52,
        [Description("QTs")] _QTs = 53,
        [Description("Q9o")] _Q9o = 54,
        [Description("Q9s")] _Q9s = 55,
        [Description("Q8o")] _Q8o = 56,
        [Description("Q8s")] _Q8s = 57,
        [Description("Q7o")] _Q7o = 58,
        [Description("Q7s")] _Q7s = 59,
        [Description("Q6o")] _Q6o = 60,
        [Description("Q6s")] _Q6s = 61,
        [Description("Q5o")] _Q5o = 62,
        [Description("Q5s")] _Q5s = 63,
        [Description("Q4o")] _Q4o = 64,
        [Description("Q4s")] _Q4s = 65,
        [Description("Q3o")] _Q3o = 66,
        [Description("Q3s")] _Q3s = 67,
        [Description("Q2o")] _Q2o = 68,
        [Description("Q2s")] _Q2s = 69,
        [Description("JJ")] _JJ = 70,
        [Description("JTo")] _JTo = 71,
        [Description("JTs")] _JTs = 72,
        [Description("J9o")] _J9o = 73,
        [Description("J9s")] _J9s = 74,
        [Description("J8o")] _J8o = 75,
        [Description("J8s")] _J8s = 76,
        [Description("J7o")] _J7o = 77,
        [Description("J7s")] _J7s = 78,
        [Description("J6o")] _J6o = 79,
        [Description("J6s")] _J6s = 80,
        [Description("J5o")] _J5o = 81,
        [Description("J5s")] _J5s = 82,
        [Description("J4o")] _J4o = 83,
        [Description("J4s")] _J4s = 84,
        //--
        [Description("J3o")] _J3o = 85,
        [Description("J3s")] _J3s = 86,
        [Description("J2o")] _J2o = 87,
        [Description("J2s")] _J2s = 88,
        [Description("TT")] _TT = 89,
        [Description("T9o")] _T9o = 90,
        [Description("T9s")] _T9s = 91,
        [Description("T8o")] _T8o = 92,
        [Description("T8s")] _T8s = 93,
        [Description("T7o")] _T7o = 94,
        [Description("T7s")] _T7s = 95,
        [Description("T6o")] _T6o = 96,
        [Description("T6s")] _T6s = 97,
        [Description("T5o")] _T5o = 98,
        [Description("T5s")] _T5s = 99,
        [Description("T4o")] _T4o = 100,
        [Description("T4s")] _T4s = 101,
        [Description("T3o")] _T3o = 102,
        [Description("T3s")] _T3s = 103,
        [Description("T2o")] _T2o = 104,
        [Description("T2s")] _T2s = 105,
        [Description("99")] _99 = 106,
        [Description("98o")] _98o = 107,
        [Description("98s")] _98s = 108,
        [Description("97o")] _97o = 109,
        [Description("97s")] _97s = 110,
        [Description("96o")] _96o = 111,
        [Description("96s")] _96s = 112,
        [Description("95o")] _95o = 113,
        [Description("95s")] _95s = 114,
        [Description("94o")] _94o = 115,
        [Description("94s")] _94s = 116,
        [Description("93o")] _93o = 117,
        [Description("93s")] _93s = 118,
        [Description("92o")] _92o = 119,
        [Description("92s")] _92s = 120,
        [Description("88")] _88 = 121,
        [Description("87o")] _87o = 122,
        [Description("87s")] _87s = 123,
        [Description("86o")] _86o = 124,
        [Description("86s")] _86s = 125,
        [Description("85o")] _85o = 126,
        [Description("85s")] _85s = 127,
        [Description("84o")] _84o = 128,
        [Description("84s")] _84s = 129,
        [Description("83o")] _83o = 130,
        [Description("83s")] _83s = 131,
        [Description("82o")] _82o = 132,
        [Description("82s")] _82s = 133,
        [Description("77")] _77 = 134,
        [Description("76o")] _76o = 135,
        [Description("76s")] _76s = 136,
        [Description("75o")] _75o = 137,
        [Description("75s")] _75s = 138,
        [Description("74o")] _74o = 139,
        [Description("74s")] _74s = 140,
        [Description("73o")] _73o = 141,
        [Description("73s")] _73s = 142,
        [Description("72o")] _72o = 143,
        [Description("72s")] _72s = 144,
        [Description("66")] _66 = 145,
        [Description("65o")] _65o = 146,
        [Description("65s")] _65s = 147,
        [Description("64o")] _64o = 148,
        [Description("64s")] _64s = 149,
        [Description("63o")] _63o = 150,
        [Description("63s")] _63s = 151,
        [Description("62s")] _62s = 152,
        [Description("62o")] _62o = 153,
        [Description("55")] _55 = 154,
        [Description("54o")] _54o = 155,
        [Description("54s")] _54s = 156,
        [Description("53o")] _53o = 157,
        [Description("53s")] _53s = 158,
        [Description("52o")] _52o = 159,
        [Description("52s")] _52s = 160,
        [Description("44")] _44 = 161,
        [Description("43o")] _43o = 162,
        [Description("43s")] _43s = 163,
        [Description("42o")] _42o = 164,
        [Description("42s")] _42s = 165,
        [Description("33")] _33 = 166,
        [Description("32o")] _32o = 167,
        [Description("32s")] _32s = 168,
        [Description("22")] _22 = 169,

    }

    public enum CardEnum
    {
        [Description("UNKNOWN CARD")] _UC = 0,
        [Description("Ac")] _Ac = 1,
        [Description("Ad")] _Ad = 2,
        [Description("Ah")] _Ah = 3,
        [Description("As")] _As = 4,
        [Description("Kc")] _Kc = 5,
        [Description("Kd")] _Kd = 6,
        [Description("Kh")] _Kh = 7,
        [Description("Ks")] _Ks = 8,
        [Description("Qc")] _Qc = 9,
        [Description("Qd")] _Qd = 10,
        [Description("Qh")] _Qh = 11,
        [Description("Qs")] _Qs = 12,
        [Description("Jc")] _Jc = 13,
        [Description("Jd")] _Jd = 14,
        [Description("Jh")] _Jh = 15,
        [Description("Js")] _Js = 16,
        [Description("Tc")] _Tc = 17,
        [Description("Td")] _Td = 18,
        [Description("Th")] _Th = 19,
        [Description("Ts")] _Ts = 20,
        [Description("9c")] _9c = 21,
        [Description("9d")] _9d = 22,
        [Description("9h")] _9h = 23,
        [Description("9s")] _9s = 24,
        [Description("8c")] _8c = 25,
        [Description("8d")] _8d = 26,
        [Description("8h")] _8h = 27,
        [Description("8s")] _8s = 28,
        [Description("7c")] _7c = 29,
        [Description("7d")] _7d = 30,
        [Description("7h")] _7h = 31,
        [Description("7s")] _7s = 32,
        [Description("6c")] _6c = 33,
        [Description("6d")] _6d = 34,
        [Description("6h")] _6h = 35,
        [Description("6s")] _6s = 36,
        [Description("5c")] _5c = 37,
        [Description("5d")] _5d = 38,
        [Description("5h")] _5h = 39,
        [Description("5s")] _5s = 40,
        [Description("4c")] _4c = 41,
        [Description("4d")] _4d = 42,
        [Description("4h")] _4h = 43,
        [Description("4s")] _4s = 44,
        [Description("3c")] _3c = 45,
        [Description("3d")] _3d = 46,
        [Description("3h")] _3h = 47,
        [Description("3s")] _3s = 48,
        [Description("2c")] _2c = 49,
        [Description("2d")] _2d = 50,
        [Description("2h")] _2h = 51,
        [Description("2s")] _2s = 52
    }

    /// <summary>
    /// DataBase Type as Enum;
    /// </summary>
    public enum DataBaseType
    {
        [Description("NoSQL")] NoSQL = 1,
        [Description("SQL")] SQL = 2,

    }
}
