


namespace TrackerLibrary.Models
{
    /// <summary>
    /// A Card is defined by Id and Name; Check the CardEnum;
    /// </summary>
    public struct Card
    {
        /// <summary>
        /// Id is used to simplify queries in the SQL DB;
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Name is used to simplify Reading of SQL-Queries and for NoSQL quering;
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// HoleCards is a Set of 2 Cards dealth to each player preflop; HoleCards are visible for Hero in each hand and for all villains that have reached ShowDown;
    /// </summary>
    public struct HoleCards
    {
        /// <summary>
        /// 1. HoleCard
        /// </summary>
        public Card HC1 { get; set; }

        /// <summary>
        /// 2. HoleCard
        /// </summary>
        public Card HC2 { get; set; }
    }

    /// <summary>
    /// Contains all Properties, that are defined by the Hand and are the same for all Players;
    /// </summary>
    public class HandInfo
    {
        /// <summary>
        /// Id of the Hand given by the game-provider; Should be unique for each game-provider;
        /// </summary>
        public long HandIdBySite { get; set; }

        /// <summary>
        /// Id of the Tournament given by the game-provider; Should be unique for each game-provider;
        /// </summary>
        public long TournamentIdBySite { get; set; }

        /// <summary>
        /// Currency used to BuyIn; Should be mainly $ - USD/ € - EURO;
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Amount of money that each player participating in the tournament did pay to form the PrizePool of the tournament; Sum of all BuyIns = PrizePool; 
        /// </summary>
        public double BuyIn { get; set; }

        /// <summary>
        /// Amount of money that each player participating in the tournament did pay to the game-provider(Rake); Sum of BuyIn+Fee = the entire amount of money / player needed to participate in the tournament;
        /// </summary>
        public double Fee { get; set; }

        /// <summary>
        /// Type of the tournament given by the game-provider; In this app we deal mainly with '3-max' -> SpinAndGo- and '2-max' HU-Tournaments; 
        /// </summary>
        public string TournamentType { get; set; }

        /// <summary>
        /// Current SmallBlind/BigBlind Ratio of the Hand; Format '20/40';
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Amount of the BigBlind;
        /// </summary>
        public float Amt_bb { get; set; }

        /// <summary>
        /// Date when the hand did started;
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Time when the hand did started;
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Id of the Table-Id given by the game-provider; Should NOT be unique;
        /// </summary>
        public string TableIdBySite { get; set; }

        /// <summary>
        /// Amount of the total Pot reached in the End-Point of the Hand;
        /// </summary>
        public float TotalPot { get; set; }

        /// <summary>
        /// List of Side-Pots in case 3+ players did reach ShowDown and some of them did invest different amounts in the hand. To happen, atleast 1 player needs to be ALL-IN; Sum of all Side-Pots = TotalPot;
        /// </summary>
        public List<float> SidePots { get; set; }

        /// <summary>
        /// The entire EDITED Hand as 1 string;
        /// TO DO: Would be much better to save the UNEDITED Hand;
        /// </summary>
        public string FullHandAsString { get; set; }

        /// <summary>
        /// The game-provider;
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// NickName of Hero;
        /// </summary>
        public string Hero { get; set; }

        /// <summary>
        /// The number of the Seat that is currently a BTN;
        /// </summary>
        public sbyte SeatBtn { get; set; }

        /// <summary>
        /// Amount of Players that play in the hand, doesnt matter if they did just openFold PreFlop;
        /// </summary>
        public uint CntPlayers { get; set; }

        /// <summary>
        /// Amount of Players that did reach Flop;
        /// </summary>
        public uint CntPlayers_Flop { get; set; }

        /// <summary>
        /// Amount of Players that did reach Turn;
        /// </summary>
        public uint CntPlayers_Turn { get; set; }

        /// <summary>
        /// Amount of Players that did reach River;
        /// </summary>
        public uint CntPlayers_River { get; set; }

        /// <summary>
        /// Amount of Players that did reach ShowDown;
        /// </summary>
        public uint CntPlayers_Showdown { get; set; }

        /// <summary>
        /// List of NickNames of all players involved in the Hand; Ordered By SeatNumber from 1 to 10, but some seatNumbers could be Null;
        /// </summary>
        public List<string> Players { get; set; }

        /// <summary>
        /// String that contains the position of each aggresive move's maker;
        /// Example: "898" = SmallBlind Raise, BigBlind 3Bet, SmallBlind 4-Bet, BigBlind Call OR Fold; There could be limp-Action before the SB, as limping/calling/folding are considered as passive action;
        /// Basically contains only "raises" and "bets" actions;
        /// </summary>
        public string pf_aggressors { get; set; }

        /// <summary>
        /// String that contains the position of each move's maker incase not folding;
        /// /// Basically contains "raises", "bets", "calls" and "checks" actions;
        /// </summary>
        public string pf_actors { get; set; }

        /// <summary>
        /// List of playerNickName, HoleCards for each player that did reach ShowDown;
        /// </summary>
        public List<(string, HoleCards)> SawShowdown_Players { get; set; }

        /// <summary>
        /// The last street when AI did happen; Empty string = no all in; Used to perform cEV cals;
        /// </summary>
        public string StreetAI { get; set; }

        /// <summary>
        /// Amount of total Pot after the end of the PreFlop Actions; 
        /// </summary>
        public float PfPot { get; set; }

        /// <summary>
        /// Amount of total Pot after the end of the Flop Actions; 
        /// Currently not implemented, but would be useful for some queries;
        /// </summary>
        public float FlopPot { get; set; }

        /// <summary>
        /// Amount of total Pot after the end of the Turn Actions; 
        /// Currently not implemented, but would be useful for some queries;
        /// </summary>
        public float TurnPot { get; set; }

        /// <summary>
        /// Amount of total Pot after the end of the River Actions; 
        /// Currently not implemented, but would be useful for some queries;
        /// </summary>
        public float RiverPot { get; set; }

        /// <summary>
        /// Hero's 1. HoleCard (Own Card);
        /// </summary>
        public Card HC1 { get; set; }

        /// <summary>
        /// Hero's 2. HoleCard (Own Card);
        /// </summary>
        public Card HC2 { get; set; }

        /// <summary>
        /// 1. Flop Card (Common Card);
        /// </summary>
        public Card FC1 { get; set; }

        /// <summary>
        /// 2. Flop Card (Common Card);
        /// </summary>
        public Card FC2 { get; set; }

        /// <summary>
        /// 3. Flop Card (Common Card);
        /// </summary>
        public Card FC3 { get; set; }

        /// <summary>
        /// Tlop Card (Common Card);
        /// </summary>
        public Card TC { get; set; }

        /// <summary>
        ///  River Card (Common Card);
        /// </summary>
        public Card RC { get; set; }

    }
}
