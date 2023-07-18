
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using TrackerLibrary.Models;




namespace TrackerLibrary
{
    /// <summary>
    /// This Class is responsible for reading a string/.txt file containing Hands in the format delivered by the game-provider and converting them to a List of Hand via REGEX expressions;
    /// p.s.: For now the Reader is working only with Pokerstars NL-Holdem Tournament-Hands; However, the format of all famous rooms is very similar, the same is valid for formats of Tournament and Cash Games and could be implemented easily with small corrections to this code; 
    /// Different Formats of Poker like OMAHA, ShortDEck etc could require much more time to be implemented, especially the calculation of Expected Value;
    /// </summary>
    public static partial class HHReader
    {
        //public static List<string> ReadFileReturnListOfString(this string filePath)
        //{
        //    string[] text = System.IO.File.ReadAllLines(filePath);
        //    List<string> output = text.ToList();
        //    //Add empty line to read the last Hand
        //    output.Add("");
        //    return output;
        //}

        /// <summary>
        /// Open and reads a .txt file;
        /// </summary>
        /// <param name="filePath">The full file path including .txt extension;</param>
        /// <returns>A single string containing the content of the entire .txt file;</returns>
        public static string ReadFileReturnString(this string filePath)
        {
            string text = System.IO.File.ReadAllText(filePath);

            return text;

        }

        /// <summary>
        /// Splits a String containing Hands to few smaller strings; Used to control the RAM ussage;
        /// </summary>
        /// <param name="inputString">String, containing all Hands that needs to pe pharsed;</param>
        /// <param name="splitSize">Maximum Hands possible to be contained after splitting;</param>
        /// <returns>An array of strings with Hand's size <,= splitSize;</returns>
        public static string[] SplitStringBySize(this string inputString, int splitSize)
        {
            List<string> splitStrings = new();

            string[] hands = inputString.Split("\n\r\n");

            for (int i = 0; i < hands.Length; i+=splitSize)
            {
                string splitString = "";
                if (i + splitSize >= hands.Length)
                {
                    splitSize = hands.Length - i;
                } 

                splitString = string.Join("\n\r\n", hands, i, splitSize);

                splitStrings.Add(splitString);
            }

            return splitStrings.ToArray();
        }

        /// <summary>
        /// Deletes unnecesary information from the string of Hands; Formatting parts of the string to make them easier for processing via multiline REDEX;
        /// </summary>
        /// <param name="handHistory">A string containg the Hands;</param>
        /// <returns>Edited formated string, ready to be pharsed by multiline REGEX</returns>
        private static string CleanHandHistory(this string handHistory)
        {
            string output = "";

            // A pattern, that finds rare rows(contained by very small amount of the Hands) with information that is not needed like disconnections, comments etc; This lines can break a multiline REGEX and cause reading less Hands or Hands with missing or wrong info;
            string patternDeleteEntireRows = @"([\s\r\n]+.+ will be allowed .+)" +
                                    @"|(([\s\r\n]+(Seat .+)|([\s\r\n]+(?!(Seat\b)).*)) (has returned|is disconnected|is connected|has timed out|is sitting out))" +
                                    @"|([\s\r\n]+(.+) collected (\d+) from( side pot| main pot| pot))" +
                                    @"|([\s\r\n]+(.+) mucks hand)" +
                                    @"|([\s\r\n]+(.+) out of hand .+)" +
                                    @"|([\s\r\n]+^.+: doesn't show hand)" +
                                    @"|([\s\r\n]+(.+) folded (.+))" +
                                    @"|([\s\r\n]+^.+ said, (.+))";

            // A pattern deleting only endings containing information that is not needed like disconnections, comments etc; Used when the previous part of the line contains valuable info and needs to be kept;
            string patternDeleteEnding = @"([\s\r\n]+(Seat .+)(has returned|is disconnected|is connected|has timed out|is sitting out|out of hand (moved from another table into small blind)))";

            // A pattern that is used to make all seatactions with similar format( like checks/folds doesnt have Size, but bets/raises has size after it; Same for AI, it's written only when a player goes ai); Simplifieng the Hand pharsing;
            string patternAllActions = @"(?:[\s\r\n]+(?<Action_Player>.+): (?:(?<Action_Action>raises|bets|folds|calls|checks)(?: \d+ to)?(?: )?(?<Action_Size>\d+)?)(?<AI> and is all-in)?)";

            // A pattern that is used to make all seatactions with similar format( like checks/folds doesnt have Size, but bets/raises has size after it; Same for AI, it's written only when a player goes ai); Simplifieng the Hand pharsing;
            string patternUncalledBet = @"(?:[\s\r\n]+Uncalled bet \((?<UncallectedBet>\d+)\) returned to (?<ReturnedTo>[^\r\n]+)(?: .+)?)";

            // Rewriting Uncalled Bet Action in a format similar to other Actions;
            string patternSummaryShowdown = @"([\s\r\n]+.+: (?<Summary_Player>(?:(?!\((?:button|small blind|big blind|)\)).)+)(?: \((?:button|small blind|big blind|button\) \(small blind)\))? (?:showed|mucked)(?: (\[(?<Summary_HC1>[a-zA-Z2-9]+) (?<Summary_HC2>[a-zA-Z2-9]+)\]( and won \((?<Summary_AmountCollected>\d+)\))?(?:.+)?)))";

            // Editing Adding Empty Cards to NoShowDown Hands to simplify HoleCards reading;
            string patternSummaryNoShowdown = @"(?:[\s\r\n]+Seat (?:\d+): (?<Summary_Player>(?:(?!\((?:button|small blind|big blind|button\) \(small blind)\)).)+)(?: \((?:button|small blind|big blind|button\) \(small blind)\))? collected \((?<Summary_AmountCollected>\d+)\))";
            
            // Replacing the unneded parts of the pattern with empty string;
            string hhDeletedEndigs = Regex.Replace(handHistory, patternDeleteEnding, match =>
            {
                return "\r\n" + match.Groups[2].ToString();
            });

            // Adding 0 if there is no size ( calls/checks/folds) after the action and 0(if no AI) / 1(if AI) at the end of the string;
            string hhActionEdited = Regex.Replace(hhDeletedEndigs, patternAllActions, match =>
            {
                return "\r\n" + match.Groups["Action_Player"].ToString() + ": " + match.Groups["Action_Action"].ToString() + (match.Groups["Action_Size"].ToString() != "" ? " " + match.Groups["Action_Size"].ToString() : " 0") + (match.Groups["AI"].ToString() != "" ? " 1" : " 0");
            });

            //Changing the format of "Uncalled Bet" line to Action-format;
            string hhUncalledBetEdited = Regex.Replace(hhActionEdited, patternUncalledBet, match =>
            {
                return "\r\n" + match.Groups["ReturnedTo"].ToString() + ": uBet -" + match.Groups["UncallectedBet"].ToString() + " 0";
            });

            // Adding [Xx Xx] - Empty HoleCards to players that didnt see ShowDown in ShowDown Hands;
            string hhSummaryShowDownEdited = Regex.Replace(hhUncalledBetEdited, patternSummaryShowdown, match =>
            {
                return "\r\n" + match.Groups["Summary_Player"].ToString() + ": [" + (match.Groups["Summary_HC1"].ToString() != "" ? match.Groups["Summary_HC1"].ToString() : "Xx") + " " + (match.Groups["Summary_HC2"].ToString() != "" ? match.Groups["Summary_HC2"].ToString() : "Xx") + "] " + (match.Groups["Summary_AmountCollected"].ToString() != "" ? match.Groups["Summary_AmountCollected"].ToString() : "0");
            });

            // Adding [Xx Xx] - Empty HoleCards to all players in No-ShowDown Hands;
            string hhSummaryEdited = Regex.Replace(hhSummaryShowDownEdited, patternSummaryNoShowdown, match =>
            {
                return "\r\n" + match.Groups["Summary_Player"].ToString() + ": [Xx Xx] " + match.Groups["Summary_AmountCollected"].ToString();
            });

            // Delete all lines that contain rare non informative lines like disconnections, comments etc;
            output = Regex.Replace(hhSummaryEdited, patternDeleteEntireRows, "", RegexOptions.Multiline);

            return output;
        }

        /// <summary>
        /// Reads Groups from REGEX-Match and pharses them to Class-Properties;
        /// </summary>
        /// <param name="regexHand">REGEX-Match</param>
        /// <returns>HandInfo Class, containing all general Hand Info;</returns>
        private static HandInfo ReadHandInfo(this Match regexHand)
        {
            //PreFlop Pot, after all PreFlop Actions;
            float pfPot = regexHand.Groups["PreHCsAction_Chips"].Captures.Sum(capture => float.Parse(capture.Value));

            //float amtBefore = pfPot;

            // [0] - Main Pot, [Optional 1,2 etc] - Side Pots;
            List<float> sidePots = new();
            for (int i = 0; i < regexHand.Groups["SidePot"].Captures.Count; i++)
            {
                sidePots.Add(regexHand.Groups["SidePot"].Captures[i].Value != "" ? float.Parse(regexHand.Groups["SidePot"].Captures[i].Value) : 0);
            }

            //Players, that did see a showDown;
            List<(string, HoleCards)> sawShowdown_Players = new();
            for (int i = 0; i < regexHand.Groups["ShowDown_Player"].Captures.Count; i++)
            {
                sawShowdown_Players.Add((regexHand.Groups["ShowDown_Player"].Captures[i].Value, new HoleCards { HC1 = new Card { Id = regexHand.Groups["ShowDown_HC1"].Captures[i].Value.ConvertCardStringToUint(), Name = regexHand.Groups["ShowDown_HC1"].Captures[i].Value }, HC2 = new Card { Id = regexHand.Groups["ShowDown_HC2"].Captures[i].Value.ConvertCardStringToUint(), Name = regexHand.Groups["ShowDown_HC2"].Captures[i].Value } }));
            }

            // All players participating in the Hand;
            List<string> players = new();
            for (int i = 0; i < regexHand.Groups["Seat_Player"].Captures.Count; i++)
            {
                players.Add(regexHand.Groups["Seat_Player"].Captures[i].Value);
            }


            HandInfo handInfo = new()
            {
                Room = regexHand.Groups["Room"].Value,
                HandIdBySite = long.Parse(regexHand.Groups["HandId"].Value),
                TournamentIdBySite = long.Parse(regexHand.Groups["TournamentId"].Value),
                Currency = regexHand.Groups["Currency"].Value,
                BuyIn = double.Parse(regexHand.Groups["BuyIn"].Value),
                Fee = double.Parse(regexHand.Groups["Fee"].Value),
                Level = regexHand.Groups["Level"].Value,
                Date = DateTime.Parse(regexHand.Groups["Date"].Value),
                Time = TimeSpan.Parse(regexHand.Groups["Time"].Value),
                TableIdBySite = regexHand.Groups["TableId"].Value,
                TournamentType = regexHand.Groups["Type"].Value,
                SeatBtn = sbyte.Parse(regexHand.Groups["SeatBtn"].Value),
                Hero = regexHand.Groups["Hero"].Value,
                Amt_bb = float.Parse(regexHand.Groups["AmtBb"].Value),
                FullHandAsString = regexHand.Groups["0"].Value,
                CntPlayers = Convert.ToUInt16(regexHand.Groups["Seat_Player"].Captures.Count()),
                CntPlayers_Flop = Convert.ToUInt16(regexHand.Groups["FlopAction_Player"].Captures.Count()),
                CntPlayers_Turn = Convert.ToUInt16(regexHand.Groups["TurnAction_Player"].Captures.Count()),
                CntPlayers_River = Convert.ToUInt16(regexHand.Groups["RiverAction_Player"].Captures.Count()),
                CntPlayers_Showdown = Convert.ToUInt16(regexHand.Groups["RC"].Value != "" ? regexHand.Groups["ShowDown_Player"].Captures.Count() : 0),
                PfPot = pfPot,
                HC1 = new Card { Id = regexHand.Groups["HC1"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["HC1"].Value },
                HC2 = new Card { Id = regexHand.Groups["HC2"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["HC2"].Value },
                FC1 = new Card { Id = regexHand.Groups["FC1"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["FC1"].Value },
                FC2 = new Card { Id = regexHand.Groups["FC2"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["FC2"].Value },
                FC3 = new Card { Id = regexHand.Groups["FC3"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["FC3"].Value },
                TC = new Card { Id = regexHand.Groups["TC"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["TC"].Value },
                RC = new Card { Id = regexHand.Groups["RC"].Value.ConvertCardStringToUint(), Name = regexHand.Groups["RC"].Value },
                TotalPot = (regexHand.Groups["TotalPot"].Value != "" ? float.Parse(regexHand.Groups["TotalPot"].Value) : 0),
                SidePots = sidePots,
                SawShowdown_Players = sawShowdown_Players,
                Players = players
            };

            return handInfo;
        }

        /// <summary>
        /// Reads Groups from REGEX-Match and pharses them to Class-Properties; Updates the Street- and SeatActions;
        /// </summary>
        /// <param name="regexHand">REGEX-Match</param>
        /// <param name="actions">Reference to a Street- and SeatAction</param>
        /// <param name="street"> Street, on which the Action did happen; Possible: Flop , Turn , River;</param>
        private static void ReadFlopTurnOrRiverActions(this Match regexHand, ref (StreetAction,Dictionary<string, SeatAction>) actions, string street)
        {
            string currPlayer; // active Player, that did the Action;
            string currAction; // Action;
            string currSize; // Size of the Action;
            string currAI; // 0 - is not AI, 1 = is AI;

            // Loop Though All Actions for the choosen Street;
            for (int i = 0; i < regexHand.Groups[street + "Action_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups[street + "Action_Player"].Captures[i].ToString();
                currAction = regexHand.Groups[street + "Action_Action"].Captures[i].ToString();
                currSize = regexHand.Groups[street + "Action_Size"].Captures[i].ToString();
                currAI = regexHand.Groups[street + "Action_AI"].Captures[i].ToString();

                if (street == "Flop")
                {
                    actions.Item2[currPlayer].saw_flop = true;
                    actions.Item2[currPlayer].Actions.Flop.Add(new Models.PlayerAction() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) }); // Add a PlayerAction
                    actions.Item1.FlopActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) }); // Add a StreetAction;

                    if (currAI == "1")
                    {
                        actions.Item2[currPlayer].StreetAI = "F"; 
                        actions.Item2[currPlayer].saw_turn = true; // As the player is AI on a previous street he is seeing all later strets;
                        actions.Item2[currPlayer].saw_river = true; // - // -
                        actions.Item2[currPlayer].saw_showdown = true; // - // -
                    }
                }
                else if (street == "Turn")
                {
                    actions.Item2[currPlayer].saw_turn = true;

                    actions.Item2[currPlayer].Actions.Turn.Add(new Models.PlayerAction() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) }); // Add a PlayerAction;
                    actions.Item1.TurnActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) }); // Add a StreetAction;

                    if (currAI == "1")
                    {
                        actions.Item2[currPlayer].StreetAI = "T";
                        actions.Item2[currPlayer].saw_river = true; // As the player is AI on a previous street he is seeing all later strets;
                        actions.Item2[currPlayer].saw_showdown = true; // - // -
                    }
                }
                else if (street == "River")
                {
                    actions.Item2[currPlayer].saw_river = true;
                    actions.Item2[currPlayer].Actions.River.Add(new Models.PlayerAction() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) }); // Add a PlayerAction;
                    actions.Item1.RiverActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) }); // Add a StreetAction;

                    if (currAI == "1")
                    {
                        actions.Item2[currPlayer].StreetAI = "R";
                        actions.Item2[currPlayer].saw_showdown = true; // As the player is AI on a previous street he is seeing all later strets;
                    }
                }
            }
        }

        /// <summary>
        /// Reads Groups from REGEX-Match and pharses them to Class-Properties;  Updates the Street- and SeatActions;
        /// </summary>
        /// <param name="regexHand">REGEX-Match</param>
        /// <param name="actions">Reference to a Street- and SeatAction</param>
        private static void ReadSummaryActions(this Match regexHand, ref (StreetAction, Dictionary<string, SeatAction>) actions)
        {
            string currPlayer;

            // Loop though ShowDown;
            for (int i = 0; i < regexHand.Groups["ShowDown_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups["ShowDown_Player"].Captures[i].ToString();

                actions.Item2[currPlayer].saw_showdown = true;

            }

            // Loop though Summary;
            for (int i = 0; i < regexHand.Groups["Summary_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups["Summary_Player"].Captures[i].ToString();
                if (regexHand.Groups["Summary_HC1"].Captures.Count > 0)
                {
                    actions.Item2[currPlayer].HC1 = new Card { Id = regexHand.Groups["Summary_HC1"].Captures[i].Value.ConvertCardStringToUint(), Name = regexHand.Groups["Summary_HC1"].Captures[i].Value };
                    actions.Item2[currPlayer].HC2 = new Card { Id = regexHand.Groups["Summary_HC2"].Captures[i].Value.ConvertCardStringToUint(), Name = regexHand.Groups["Summary_HC2"].Captures[i].Value };
                    actions.Item2[currPlayer].HCsAsNumber = EnumExtensionMethods.ConvertHoleCardsSimpleToEnum(EnumExtensionMethods.CalculateHoleCardsSimple(regexHand.Groups["Summary_HC1"].Captures[i].Value, regexHand.Groups["Summary_HC2"].Captures[i].Value ));

                    actions.Item2[currPlayer].ChipsWon = float.Parse(regexHand.Groups["Summary_AmountCollected"].Captures[i].ToString());
                    if (float.Parse(regexHand.Groups["Summary_AmountCollected"].Captures[i].ToString()) > 0)
                    {
                        actions.Item2[currPlayer].IsWinner = true;
                    }
                }
            }
        }

        /// <summary>
        /// Creates all Steet- And SeatActions and updates the PreFlop Information related to them;
        /// </summary>
        /// <param name="regexHand">REGEX-Match</param>
        /// <returns>Self-Defined struct containing Street- and SeeatAction containing ONLY the PreFlop player-related Information;</returns>
        private static (StreetAction , Dictionary<string, SeatAction>) CreatePreFlopStreetActionAndActionDictionary(this Match regexHand)
        {
            string currPlayer;
            string currAction;
            string currSize;
            string currAI;

            string contributionType; // "ante", "small blind" or "big blind"; 

            Dictionary<string, SeatAction> actionDict = new();
            StreetAction streetAction = new();

            // Looping trough the "Seat #:" before dealing the Hands, creating all SeatActions and updating SeatNumbers and StartingStacks:
                //Seat 1: IPray2Buddha (16 in chips) 
                //Seat 2: Calm Kev(234 in chips) 
                //Seat 3: FernandoCtz(1250 in chips)
                //FernandoCtz: posts small blind 15
                //IPray2Buddha: posts big blind 16 and is all -in
            for (int i = 0; i < regexHand.Groups["Seat_Player"].Captures.Count; i++)
            {
                SeatAction currSeatAction = new SeatAction();
                currSeatAction.SeatNumber = sbyte.Parse(regexHand.Groups["Seat_Num"].Captures[i].ToString());
                currSeatAction.StartingStack = float.Parse(regexHand.Groups["Seat_Stack"].Captures[i].ToString());
                actionDict.Add(regexHand.Groups["Seat_Player"].Captures[i].ToString(), currSeatAction);
            }

            // Looping trough the SeatActions before dealing the Hands, updating some blinds, positions, and Chips invested in the Hand by default (antes, small blind, big blind) :
                //FernandoCtz: posts small blind 15
                //IPray2Buddha: posts big blind 16 and is all -in
            for (int i = 0; i < regexHand.Groups["PreHCsAction_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups["PreHCsAction_Player"].Captures[i].ToString();
                contributionType = regexHand.Groups["PreHCsAction_Post"].Captures[i].ToString();
                if (contributionType == " the ante ")
                {
                    actionDict[currPlayer].Ante = float.Parse(regexHand.Groups["PreHCsAction_Chips"].Captures[i].ToString());
                }
                else if (contributionType == " small blind ")
                {
                    actionDict[currPlayer].Blind = float.Parse(regexHand.Groups["PreHCsAction_Chips"].Captures[i].ToString());
                    actionDict[currPlayer].SeatPosition = 8;

                }
                else if (contributionType == " big blind ")
                {
                    actionDict[currPlayer].Blind = float.Parse(regexHand.Groups["PreHCsAction_Chips"].Captures[i].ToString());
                    actionDict[currPlayer].SeatPosition = 9;
                }
            }

            // Looping trough PreFlop Actions and updating them;
            for (int i = 0; i < regexHand.Groups["PreFlopAction_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups["PreFlopAction_Player"].Captures[i].ToString();
                currAction = regexHand.Groups["PreFlopAction_Action"].Captures[i].ToString();
                currSize = regexHand.Groups["PreFlopAction_Size"].Captures[i].ToString();
                currAI = regexHand.Groups["PreFlopAction_AI"].Captures[i].ToString();

                if (actionDict[currPlayer].SeatPosition == 0 && i < regexHand.Groups["Seat_Player"].Captures.Count() - 3)
                {
                    actionDict[currPlayer].SeatPosition = Convert.ToUInt16(regexHand.Groups["Seat_Player"].Captures.Count() - i - 3); //As the first layer to act pF is always the firthes one, his pos = cntPlayers - 3(btn=0, SB=8 and BB=9 are fixed POS); -- For every next pos until we reach the SB;  
                }
                actionDict[currPlayer].Actions.PreFlop.Add(new Models.PlayerAction() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });
                streetAction.PreflopActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });

                if (currAI == "1")
                {
                    actionDict[currPlayer].StreetAI = "PF";
                    actionDict[currPlayer].saw_flop = true;
                    actionDict[currPlayer].saw_turn = true;
                    actionDict[currPlayer].saw_river = true;
                    actionDict[currPlayer].saw_showdown = true;
                }
                
            }

            return (streetAction ,actionDict);
        }

        /// <summary>
        /// Convering all Player-related HandInformation to Street- and SeatAction;
        /// </summary>
        /// <param name="regexHand">REGEX-Match</param>
        /// <returns>Self-Defined struct containing Street- and SeeatAction containing the full player-related Information;</returns>
        private static (StreetAction , Dictionary<string, SeatAction>) ReadAllActions (this Match regexHand)
        {          
            (StreetAction,Dictionary<string, SeatAction>) actions = regexHand.CreatePreFlopStreetActionAndActionDictionary(); // Create Street- and SeatActions and update PreFlop;

            regexHand.ReadFlopTurnOrRiverActions(ref actions, "Flop"); // Update Flop;
            regexHand.ReadFlopTurnOrRiverActions(ref actions, "Turn"); // Update Turn;
            regexHand.ReadFlopTurnOrRiverActions(ref actions, "River"); // Update River;
            regexHand.ReadSummaryActions(ref actions); // Update ShowDown and Summary;

            return actions;
        }

        /// <summary>
        ///  - Opens and Read a string contating Hands in game-provider format; 
        ///  - Editting the String to multiline REGEX-readable format;
        ///  - Reads the edited string and pharses to REGEX-MatchCollection;
        ///  - Loops though the MatchCollection and pharses to List of Hands;
        ///  - Loops though the List of Hands and updates the Properties, that need Extra Calculations like expected Value, chipsInvested, Flags etc;
        /// </summary>
        /// <param name="hh">A string contating Hands in game-provider format; Restricted by size for controlling the RAM ussage;</param>
        /// <returns>List of Hands with updated Properties, ready to be inserted to DB;</returns>
        public static List<Hand> ReadHands(string hh)
        {
            List<Hand> hands = new List<Hand>();

            //https://regex101.com/r/5ATdi6/1 - Regex Full hands
            //https://regex101.com/r/XzHC8k/1 - Regex Edited hands



            Stopwatch watch = new();
            watch.Start();

            // A pattern that reads a Hand via multiline REGEX from edited HandHistory string;
            string pattern = @"^(?<Room>.+) Hand #(?<HandId>\d+): Tournament #(?<TournamentId>\d+), (?<Currency>[$€])(?<BuyIn>[\.\d]+)(?:\+[$€](?<Fee>[\.\d]+))*[^0-9]+(?<Level>(?:\d+)/(?<AmtBb>\d+))\) - (?<Date>[\d/]+) (?<Time>[\d:]+) (.+)" +
            @"[\s\r\n]+^Table '(?<TableId>[\s\d]+)' (?<Type>.+) Seat #(?<SeatBtn>\d+) is the button" +
            @"(?:[\s\r\n]+^Seat (?<Seat_Num>\d+): (?<Seat_Player>.+) \((?:(?<Seat_Stack>[\d\.]+) in chips)(?:, [$€](?<Seat_Bounty>[\d\.]+) bounty)?\))*" +
            @"(?:[\s\r\n]+^(?<PreHCsAction_Player>.+): posts(?<PreHCsAction_Post> the ante | small blind | big blind )(?<PreHCsAction_Chips>\d+)(?: and is all-in)?)*" +
            @"[\s\r\n]+^\*\*\* HOLE CARDS \*\*\*[\s\r\n]+" +
            //PRE FLOP
            @"(?:^Dealt to (?<Hero>.+) \[(?<HC1>[a-zA-Z2-9]+) (?<HC2>[a-zA-Z2-9]+)\](?:.+)?)?" +
            @"(?:[\s\r\n]+^(?<PreFlopAction_Player>.+): (?<PreFlopAction_Action>raises|bets|folds|calls|checks|uBet) (?<PreFlopAction_Size>-?\d+) (?<PreFlopAction_AI>1|0)(?:.+)?)*" +
            // FLOP
            @"(?:[\s\r\n]+^\*\*\* FLOP \*\*\* (?:\[(?<FC1>[a-zA-Z2-9]+) (?<FC2>[a-zA-Z2-9]+) (?<FC3>[a-zA-Z2-9]+)\])(?:.+)?)?" +
            @"(?:[\s\r\n]+^(?<FlopAction_Player>.+): (?<FlopAction_Action>raises|bets|folds|calls|checks|uBet) (?<FlopAction_Size>-?\d+) (?<FlopAction_AI>1|0)(?:.+)?)*" +
            // TURN
            @"(?:[\s\r\n]+^\*\*\* TURN \*\*\* (?:.+\[(?<TC>[a-zA-Z2-9]+)\])(?:.+)?)?" +
            @"(?:[\s\r\n]+^(?<TurnAction_Player>.+): (?<TurnAction_Action>raises|bets|folds|calls|checks|uBet) (?<TurnAction_Size>-?\d+) (?<TurnAction_AI>1|0)(?:.+)?)*" +
            // RIVER
            @"(?:[\s\r\n]+^\*\*\* RIVER \*\*\* (?:.+\[(?<RC>[a-zA-Z2-9]+)\])(?:.+)?)?" +
            @"(?:[\s\r\n]+^(?<RiverAction_Player>.+): (?<RiverAction_Action>raises|bets|folds|calls|checks|uBet) (?<RiverAction_Size>-?\d+) (?<RiverAction_AI>1|0)(?:.+)?)*" +
            // SHOW DOWN
            @"(?:[\s\r\n]+^\*\*\* SHOW DOWN \*\*\*(?:.+)?)?" +
            @"(?:[\s\r\n]+^(?<ShowDown_Player>.+): shows \[(?<ShowDown_HC1>[a-zA-Z2-9]+) (?<ShowDown_HC2>[a-zA-Z2-9]+)\] .+)*" +
            @"(?:[\s\r\n]+^(?<ShowDown_PlayerFinished>.+) finished the tournament in (?<ShowDown_AtPlace>.+)(?:nd|rd|th) place(?: and received [$€](?<Prize>[\.\d]+).)?)*" +
            @"(?:[\s\r\n]+^(?<ShowDown_PlayerWon>.+) wins the tournament and receives [$€](?<PrizeWinner>[\.\d]+) - congratulations!)*" +
            // SUMMARY
            @"(?:[\s\r\n]+^\*\*\* SUMMARY \*\*\*(?:.+)?)?" +
            @"(?:[\s\r\n]+^Total pot (?<TotalPot>\d+)(?: Main pot (?<SidePot>\d+)\.)?(?: Side pot(?:[-\d]+)? (?<SidePot>\d+)\.)* \| Rake (?<Rake>\d+))?" +
            @"(?:[\s\r\n]+^Board .+)?" +
            @"(?:[\s\r\n]+^(?<Summary_Player>(.+)): \[(?<Summary_HC1>[a-zA-Z2-9]+) (?<Summary_HC2>[a-zA-Z2-9]+)\] (?<Summary_AmountCollected>-?\d+))*";

            // Edit the HandHistory File;
            string cleanHH = hh.CleanHandHistory();

            // Read Regex;
            MatchCollection matches = Regex.Matches(cleanHH, pattern, RegexOptions.Multiline);

            Console.WriteLine($"{matches.Count()} Hands readed for: " + watch.ElapsedMilliseconds / 1000 + "s");

            watch.Restart();

            List<Hand> allHands = new List<Hand>();

            //Convert REGEX-Matches to List of Hands;
            foreach (Match match in matches)
            {
                HandInfo handInfo = match.ReadHandInfo();
                (StreetAction , Dictionary<string, SeatAction>) actions = match.ReadAllActions();

                allHands.Add(new Hand(handInfo, actions.Item1, actions.Item2));

            }



            Console.WriteLine("");
            Console.WriteLine("---");
            Console.WriteLine("current HandSet - CalcTime: " + watch.ElapsedMilliseconds / 1000.0 + "s");
            Console.WriteLine("current HandSet - Hands Number: " + allHands.Count());

            watch.Restart();

            //Calculate Properties
            allHands.CalculateProperties();

            Console.WriteLine("current HandSet - Properties CalcTime: " + watch.ElapsedMilliseconds / 1000.0 + "s");

            return allHands;
        }
    }
}