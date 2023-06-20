
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using TrackerLibrary.Models;




namespace TrackerLibrary
{
    public static partial class HHReader
    {
        public static List<string> ReadFileReturnListOfString(this string filePath)
        {
            string[] text = System.IO.File.ReadAllLines(filePath);
            List<string> output = text.ToList();
            //Add empty line to read the last Hand
            output.Add("");
            return output;

        }

        public static string ReadFileReturnString(this string filePath)
        {
            string text = System.IO.File.ReadAllText(filePath);

            return text;

        }

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

        private static string CleanHandHistory(this string handHistory)
        {
            string output = "";

            string patternDeleteEntireRows = @"([\s\r\n]+.+ will be allowed .+)" +
                                    @"|(([\s\r\n]+(Seat .+)|([\s\r\n]+(?!(Seat\b)).*)) (has returned|is disconnected|is connected|has timed out|is sitting out))" +
                                    @"|([\s\r\n]+(.+) collected (\d+) from( side pot| main pot| pot))" +
                                    @"|([\s\r\n]+(.+) mucks hand)" +
                                    @"|([\s\r\n]+(.+) out of hand .+)" +
                                    @"|([\s\r\n]+^.+: doesn't show hand)" +
                                    @"|([\s\r\n]+(.+) folded (.+))" +
                                    @"|([\s\r\n]+^.+ said, (.+))";

            string patternDeleteEnding = @"([\s\r\n]+(Seat .+)(has returned|is disconnected|is connected|has timed out|is sitting out|out of hand (moved from another table into small blind)))";

            string patternAllActions = @"(?:[\s\r\n]+(?<Action_Player>.+): (?:(?<Action_Action>raises|bets|folds|calls|checks)(?: \d+ to)?(?: )?(?<Action_Size>\d+)?)(?<AI> and is all-in)?)";

            string patternUncalledBet = @"(?:[\s\r\n]+Uncalled bet \((?<UncallectedBet>\d+)\) returned to (?<ReturnedTo>[^\r\n]+)(?: .+)?)";

            string patternSummaryShowdown = @"([\s\r\n]+.+: (?<Summary_Player>(?:(?!\((?:button|small blind|big blind|)\)).)+)(?: \((?:button|small blind|big blind|button\) \(small blind)\))? (?:showed|mucked)(?: (\[(?<Summary_HC1>[a-zA-Z2-9]+) (?<Summary_HC2>[a-zA-Z2-9]+)\]( and won \((?<Summary_AmountCollected>\d+)\))?(?:.+)?)))";

            string patternSummaryNoShowdown = @"(?:[\s\r\n]+Seat (?:\d+): (?<Summary_Player>(?:(?!\((?:button|small blind|big blind|button\) \(small blind)\)).)+)(?: \((?:button|small blind|big blind|button\) \(small blind)\))? collected \((?<Summary_AmountCollected>\d+)\))";
            string hhDeletedEndigs = Regex.Replace(handHistory, patternDeleteEnding, match =>
            {
                return "\r\n" + match.Groups[2].ToString();
            });

            string hhActionEdited = Regex.Replace(hhDeletedEndigs, patternAllActions, match =>
            {
                return "\r\n" + match.Groups["Action_Player"].ToString() + ": " + match.Groups["Action_Action"].ToString() + (match.Groups["Action_Size"].ToString() != "" ? " " + match.Groups["Action_Size"].ToString() : " 0") + (match.Groups["AI"].ToString() != "" ? " 1" : " 0");
            });

            string hhUncalledBetEdited = Regex.Replace(hhActionEdited, patternUncalledBet, match =>
            {
                return "\r\n" + match.Groups["ReturnedTo"].ToString() + ": uBet -" + match.Groups["UncallectedBet"].ToString() + " 0";
            });

            string hhSummaryShowDownEdited = Regex.Replace(hhUncalledBetEdited, patternSummaryShowdown, match =>
            {
                return "\r\n" + match.Groups["Summary_Player"].ToString() + ": [" + (match.Groups["Summary_HC1"].ToString() != "" ? match.Groups["Summary_HC1"].ToString() : "Xx") + " " + (match.Groups["Summary_HC2"].ToString() != "" ? match.Groups["Summary_HC2"].ToString() : "Xx") + "] " + (match.Groups["Summary_AmountCollected"].ToString() != "" ? match.Groups["Summary_AmountCollected"].ToString() : "0");
            });

            string hhSummaryEdited = Regex.Replace(hhSummaryShowDownEdited, patternSummaryNoShowdown, match =>
            {
                return "\r\n" + match.Groups["Summary_Player"].ToString() + ": [Xx Xx] " + match.Groups["Summary_AmountCollected"].ToString();
            });


            output = Regex.Replace(hhSummaryEdited, patternDeleteEntireRows, "", RegexOptions.Multiline);

            return output;
        }
        private static HandInfo ReadHandInfo(this Match regexHand)
        {
            if (long.Parse(regexHand.Groups["HandId"].Value) == 225658452321)
            {
                int ff = 1;
            }


            float pfPot = regexHand.Groups["PreHCsAction_Chips"].Captures.Sum(capture => float.Parse(capture.Value));

            float amtBefore = pfPot;

            List<float> sidePots = new();
            for (int i = 0; i < regexHand.Groups["SidePot"].Captures.Count; i++)
            {
                sidePots.Add(regexHand.Groups["SidePot"].Captures[i].Value != "" ? float.Parse(regexHand.Groups["SidePot"].Captures[i].Value) : 0);
            }

            List<(string, HoleCards)> sawShowdown_Players = new();
            for (int i = 0; i < regexHand.Groups["ShowDown_Player"].Captures.Count; i++)
            {
                sawShowdown_Players.Add((regexHand.Groups["ShowDown_Player"].Captures[i].Value, new HoleCards { HC1 = new Card { Id = regexHand.Groups["ShowDown_HC1"].Captures[i].Value.ConvertCardstringToUint(), Name = regexHand.Groups["ShowDown_HC1"].Captures[i].Value }, HC2 = new Card { Id = regexHand.Groups["ShowDown_HC2"].Captures[i].Value.ConvertCardstringToUint(), Name = regexHand.Groups["ShowDown_HC2"].Captures[i].Value } }));
            }

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
                FullHandAsString = regexHand.Groups["0"].Value,
                CntPlayers = Convert.ToUInt16(regexHand.Groups["Seat_Player"].Captures.Count()),
                CntPlayers_Flop = Convert.ToUInt16(regexHand.Groups["FlopAction_Player"].Captures.Count()),
                CntPlayers_Turn = Convert.ToUInt16(regexHand.Groups["TurnAction_Player"].Captures.Count()),
                CntPlayers_River = Convert.ToUInt16(regexHand.Groups["RiverAction_Player"].Captures.Count()),
                CntPlayers_Showdown = Convert.ToUInt16(regexHand.Groups["RC"].Value != "" ? regexHand.Groups["ShowDown_Player"].Captures.Count() : 0),
                PfPot = pfPot,
                HC1 = new Card { Id = regexHand.Groups["HC1"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["HC1"].Value },
                HC2 = new Card { Id = regexHand.Groups["HC2"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["HC2"].Value },
                FC1 = new Card { Id = regexHand.Groups["FC1"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["FC1"].Value },
                FC2 = new Card { Id = regexHand.Groups["FC2"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["FC2"].Value },
                FC3 = new Card { Id = regexHand.Groups["FC3"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["FC3"].Value },
                TC = new Card { Id = regexHand.Groups["TC"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["TC"].Value },
                RC = new Card { Id = regexHand.Groups["RC"].Value.ConvertCardstringToUint(), Name = regexHand.Groups["RC"].Value },
                TotalPot = (regexHand.Groups["TotalPot"].Value != "" ? float.Parse(regexHand.Groups["TotalPot"].Value) : 0),
                SidePots = sidePots,
                SawShowdown_Players = sawShowdown_Players,
                Players = players
            };

            return handInfo;
        }
        private static void ReadFlopTurnOrRiverActions(this Match regexHand, ref (StreetAction,Dictionary<string, SeatAction>) actions, string street)
        {
            string currPlayer;
            string currAction;
            string currSize;
            string currAI;

            for (int i = 0; i < regexHand.Groups[street + "Action_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups[street + "Action_Player"].Captures[i].ToString();
                currAction = regexHand.Groups[street + "Action_Action"].Captures[i].ToString();
                currSize = regexHand.Groups[street + "Action_Size"].Captures[i].ToString();
                currAI = regexHand.Groups[street + "Action_AI"].Captures[i].ToString();

                if (street == "Flop")
                {
                    actions.Item2[currPlayer].saw_flop = true;
                    actions.Item2[currPlayer].Actions.Flop.Add(new Models.Action() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });
                    actions.Item1.FlopActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });

                    if (currAI == "1")
                    {
                        actions.Item2[currPlayer].StreetAI = "F";
                        actions.Item2[currPlayer].saw_turn = true;
                        actions.Item2[currPlayer].saw_river = true;
                        actions.Item2[currPlayer].saw_showdown = true;
                    }
                }
                else if (street == "Turn")
                {
                    actions.Item2[currPlayer].saw_turn = true;

                    actions.Item2[currPlayer].Actions.Turn.Add(new Models.Action() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });
                    actions.Item1.TurnActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });

                    if (currAI == "1")
                    {
                        actions.Item2[currPlayer].StreetAI = "T";
                        actions.Item2[currPlayer].saw_river = true;
                        actions.Item2[currPlayer].saw_showdown = true;
                    }
                }
                else if (street == "River")
                {
                    actions.Item2[currPlayer].saw_river = true;
                    actions.Item2[currPlayer].Actions.River.Add(new Models.Action() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });
                    actions.Item1.RiverActions.Add(new StreetAction.FullAction() { Player = currPlayer, Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });

                    if (currAI == "1")
                    {
                        actions.Item2[currPlayer].StreetAI = "R";
                        actions.Item2[currPlayer].saw_showdown = true;
                    }
                }
            }
        }
        private static void ReadSummaryActions(this Match regexHand, ref (StreetAction, Dictionary<string, SeatAction>) actions)
        {
            string currPlayer;

            for (int i = 0; i < regexHand.Groups["ShowDown_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups["ShowDown_Player"].Captures[i].ToString();

                actions.Item2[currPlayer].saw_showdown = true;

            }

            for (int i = 0; i < regexHand.Groups["Summary_Player"].Captures.Count; i++)
            {
                currPlayer = regexHand.Groups["Summary_Player"].Captures[i].ToString();
                if (regexHand.Groups["Summary_HC1"].Captures.Count > 0)
                {
                    actions.Item2[currPlayer].HC1 = regexHand.Groups["Summary_HC1"].Captures[i].Value.ConvertCardstringToUint();
                    actions.Item2[currPlayer].HC2 = regexHand.Groups["Summary_HC2"].Captures[i].Value.ConvertCardstringToUint();

                    actions.Item2[currPlayer].ChipsWon = float.Parse(regexHand.Groups["Summary_AmountCollected"].Captures[i].ToString());
                    if (float.Parse(regexHand.Groups["Summary_AmountCollected"].Captures[i].ToString()) > 0)
                    {
                        actions.Item2[currPlayer].IsWinner = true;
                    }
                }
            }
        }
        private static (StreetAction , Dictionary<string, SeatAction>) CreatePreFlopStreetActionAndActionDictionary(this Match regexHand)
        {
            string currPlayer;
            string currAction;
            string currSize;
            string currAI;

            string contributionType;

            Dictionary<string, SeatAction> actionDict = new();
            StreetAction streetAction = new();


            for (int i = 0; i < regexHand.Groups["Seat_Player"].Captures.Count; i++)
            {
                SeatAction currSeatAction = new SeatAction();
                currSeatAction.SeatNumber = sbyte.Parse(regexHand.Groups["Seat_Num"].Captures[i].ToString());
                currSeatAction.StartingStack = float.Parse(regexHand.Groups["Seat_Stack"].Captures[i].ToString());
                actionDict.Add(regexHand.Groups["Seat_Player"].Captures[i].ToString(), currSeatAction);
            }
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
                actionDict[currPlayer].Actions.PreFlop.Add(new Models.Action() { Act = currAction, Size = float.Parse(currSize), AI = sbyte.Parse(currAI) });
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

        private static (StreetAction , Dictionary<string, SeatAction>) ReadAllActions (this Match regexHand)
        {
            if (long.Parse(regexHand.Groups["HandId"].Value) == 225658452321)
            {
                int ff = 1;
            }

            (StreetAction,Dictionary<string, SeatAction>) actions = regexHand.CreatePreFlopStreetActionAndActionDictionary();

            regexHand.ReadFlopTurnOrRiverActions(ref actions, "Flop");
            regexHand.ReadFlopTurnOrRiverActions(ref actions, "Turn");
            regexHand.ReadFlopTurnOrRiverActions(ref actions, "River");
            regexHand.ReadSummaryActions(ref actions);

            return actions;
        }

        public static List<Hand> ReadHands(string hh)
        {
            List<Hand> hands = new List<Hand>();

            //https://regex101.com/r/5ATdi6/1 - Regex Full hands
            //https://regex101.com/r/XzHC8k/1 - Regex Edited hands


            //(?: ^Dealt to(?< Hero >.+) \[(?< HC1 >[a - zA - Z2 - 9] +) (?< HC2 >[a - zA - Z2 - 9] +)\](?:.+) ?)?(?:[\s\r\n] + ^(?< PreFlopAction_Player >.+): (?< PreFlopAction_Action > raises | bets | folds | calls | checks | uBet)(?< PreFlopAction_Size > -?\d +)(?< PreFlopAction_AI > 1 | 0)(?:.+) ?)*(?:[\s\r\n] + ^\*\*\*FLOP \*\*\*(?:\[(?< FC1 >[a - zA - Z2 - 9] +) (?< FC2 >[a - zA - Z2 - 9] +)(?< FC3 >[a - zA - Z2 - 9] +)\])(?:.+) ?)?(?:[\s\r\n] + ^(?< FlopAction_Player >.+): (?< FlopAction_Action > raises | bets | folds | calls | checks | uBet)(?< FlopAction_Size > -?\d +)(?< FlopAction_AI > 1 | 0)(?:.+) ?)*(?:[\s\r\n] + ^\*\*\*TURN \*\*\*(?:.+\[(?< TC >[a - zA - Z2 - 9] +)\])(?:.+) ?)?(?:[\s\r\n] + ^(?< TurnAction_Player >.+): (?< TurnAction_Action > raises | bets | folds | calls | checks | uBet)(?< TurnAction_Size > -?\d +)(?< TurnAction_AI > 1 | 0)(?:.+) ?)*(?:[\s\r\n] + ^\*\*\*RIVER \*\*\*(?:.+\[(?< RC >[a - zA - Z2 - 9] +)\])(?:.+) ?)?(?:[\s\r\n] + ^(?< RiverAction_Player >.+): (?< RiverAction_Action > raises | bets | folds | calls | checks | uBet)(?< RiverAction_Size > -?\d +)(?< RiverAction_AI > 1 | 0)(?:.+) ?)*(?:[\s\r\n] + ^\*\*\*SHOW DOWN \*\*\*(?:.+) ?)?(?:[\s\r\n] + ^(?< ShowDown_Player >.+): shows \[(?< ShowDown_HC1 >[a - zA - Z2 - 9] +) (?< ShowDown_HC2 >[a - zA - Z2 - 9] +)\] .+)*(?:[\s\r\n] + ^(?< ShowDown_PlayerFinished >.+) finished the tournament in (?< ShowDown_AtPlace >.+)(?:nd | rd | th) place(?: and received[$€](?< Prize >[\.\d] +).) ?)*(?:[\s\r\n] + ^(?< ShowDown_PlayerWon >.+) wins the tournament and receives[$€](?< PrizeWinner >[\.\d] +) - congratulations!)*(?:[\s\r\n] + ^\*\*\*SUMMARY \*\*\*(?:.+) ?)?(?:[\s\r\n] + ^Total pot(?< TotalPot >\d +)(?: Main pot(?< SidePot >\d +)\.) ? (?: Side pot(?:[-\d] +)? (?< SidePot >\d +)\.)* \| Rake(?< Rake >\d +))?(?:[\s\r\n] + ^Board.+) ? (?:[\s\r\n] + ^(?< Summary_Player > (.+)): \[(?< Summary_HC1 >[a - zA - Z2 - 9] +) (?< Summary_HC2 >[a - zA - Z2 - 9] +)\] (?< Summary_AmountCollected > -?\d +))*

            Console.WriteLine("start");
            Stopwatch watch = new();
            watch.Start();

            string pattern = @"^(?<Room>.+) Hand #(?<HandId>\d+): Tournament #(?<TournamentId>\d+), (?<Currency>[$€])(?<BuyIn>[\.\d]+)(?:\+[$€](?<Fee>[\.\d]+))*.+(?<Level>\d+/\d+)\) - (?<Date>[\d/]+) (?<Time>[\d:]+) (.+)" +
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


            string cleanHH = hh.CleanHandHistory();

            //var splitString = cleanHH.SplitStringBySize(20000);

            MatchCollection matches = Regex.Matches(cleanHH, pattern, RegexOptions.Multiline);

            Console.WriteLine("Clean HH Readed: " + watch.ElapsedMilliseconds / 1000 + "s");
            Console.WriteLine("Hands Readed: " + matches.Count());

            watch.Restart();

            List<Hand> allHands = new List<Hand>();

            foreach (Match match in matches)
            {
                HandInfo handInfo = match.ReadHandInfo();
                (StreetAction , Dictionary<string, SeatAction>) actions = match.ReadAllActions();

                allHands.Add(new Hand(handInfo, actions.Item1, actions.Item2));

            }




            Console.WriteLine("---");
            Console.WriteLine("allHands CalcTime: " + watch.ElapsedMilliseconds / 1000.0 + "s");
            Console.WriteLine("allHands: Hands Number: " + allHands.Count());
            watch.Restart();

            allHands.CalculateProperties();

            Console.WriteLine("---");
            Console.WriteLine("Properties CalcTime: " + watch.ElapsedMilliseconds / 1000.0 + "s");


            //for (int i = 0; i < allHands.Count; i++)
            //{
            //    Console.WriteLine(allHands[i].Info.HandIdBySite + ": ");
            //    foreach (var sA in allHands[i].SeatActions)
            //    {
            //        Console.WriteLine(sA.Key + ": CEV: " + sA.Value.CevWon + " // AmtWon: " + sA.Value.ChipsWon);
            //    }
            //    Console.WriteLine("---");

            //}

            return allHands;
        }
    }
}