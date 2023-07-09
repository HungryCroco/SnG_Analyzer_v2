using Microsoft.VisualBasic;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using EVCalculator;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace TrackerLibrary
{
    internal static class HandCalculations
    {
        public const string pfEA = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\eaPF_a.txt";
        private static float[,,,] ea = EVCalculator.ImportDLL.ReadEAFromFileAsFloatArray(pfEA);

        private static void CalculateCev(this Hand myHand)
        {
            float resP1 = 0;
            float resP2 = 0;
            float resP3 = 0;

            if (myHand.Info.HandIdBySite == 232798552085)
            {
                int ff = 1;
            }

            if (myHand.Info.StreetAI == null)
            {
                for (int i = 0; i < myHand.SeatActions.Count; i++)
                {
                    string currPlayer = myHand.Info.Players[i];
                    if (myHand.SeatActions[currPlayer].IsWinner == false)
                    {
                        myHand.SeatActions[currPlayer].CevWon = -myHand.SeatActions[currPlayer].ChipsInvested;
                    }
                    else
                    {
                        myHand.SeatActions[currPlayer].CevWon = myHand.SeatActions[currPlayer].ChipsWon - myHand.SeatActions[currPlayer].ChipsInvested;
                    }
                }
            }
            else
            {
                string HCs_P1 = myHand.Info.SawShowdown_Players.Count > 0 ? myHand.Info.SawShowdown_Players[0].Item2.HC1.Name + myHand.Info.SawShowdown_Players[0].Item2.HC2.Name : "";
                string HCs_P2 = myHand.Info.SawShowdown_Players.Count > 1 ? myHand.Info.SawShowdown_Players[1].Item2.HC1.Name + myHand.Info.SawShowdown_Players[1].Item2.HC2.Name : "";
                string HCs_P3 = myHand.Info.SawShowdown_Players.Count > 2 ? myHand.Info.SawShowdown_Players[2].Item2.HC1.Name + myHand.Info.SawShowdown_Players[2].Item2.HC2.Name : "";
                string flopTurnRiver = "";

                if (myHand.Info.StreetAI == "PF")
                {
                    if (myHand.Info.CntPlayers_Showdown == 2)
                    {
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_PF(myHand.Info.SawShowdown_Players[0].Item2.HC1.Id, myHand.Info.SawShowdown_Players[0].Item2.HC2.Id, myHand.Info.SawShowdown_Players[1].Item2.HC1.Id, myHand.Info.SawShowdown_Players[1].Item2.HC2.Id, ref resP1, ref resP2, ref resP3, ea);
                        myHand.SetCev_2Players(ref resP1, ref resP2);
                    }
                    else if (myHand.Info.CntPlayers_Showdown == 3)
                    {
                        myHand.SetCev_3Players(ref resP1, ref resP2, ref resP3, ref HCs_P1, ref HCs_P2, ref HCs_P3, ref flopTurnRiver);
                    }
                    else
                    {
                        myHand.SetCev_4PlayersPlus();
                    }
                }
                else if (myHand.Info.StreetAI == "F")
                {
                    flopTurnRiver = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name;

                    if (myHand.Info.CntPlayers_Showdown == 2)
                    { 
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, ref resP1, ref resP2, ref resP3);
                        myHand.SetCev_2Players(ref resP1, ref resP2);
                    }
                    else if (myHand.Info.CntPlayers_Showdown == 3)
                    {
                        myHand.SetCev_3Players(ref resP1, ref resP2, ref resP3, ref HCs_P1, ref HCs_P2, ref HCs_P3, ref flopTurnRiver);
                    }
                    else
                    {
                        myHand.SetCev_4PlayersPlus();
                    }
                }
                else if (myHand.Info.StreetAI == "T")
                {
                    if (myHand.Info.CntPlayers_Showdown == 2)
                    {
                        string flopPlusTurn = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name + myHand.Info.TC.Name;
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_ReturnFloat(HCs_P1 + flopPlusTurn, HCs_P2 + flopPlusTurn, ref resP1, ref resP2, ref resP3);
                        myHand.SetCev_2Players(ref resP1, ref resP2);
                    }
                    else if (myHand.Info.CntPlayers_Showdown == 3)
                    {
                        flopTurnRiver = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name + myHand.Info.TC.Name;
                        myHand.SetCev_3Players(ref resP1, ref resP2, ref resP3, ref HCs_P1, ref HCs_P2, ref HCs_P3, ref flopTurnRiver);
                    }
                    else
                    {
                        myHand.SetCev_4PlayersPlus();
                    }
                }

                for (int i = 0; i < myHand.SeatActions.Count; i++)
                {
                    string currPlayer = myHand.Info.Players[i];
                    if (!myHand.Info.SawShowdown_Players.Any(item => item.Item1 == currPlayer))
                    {
                        myHand.SeatActions[currPlayer].CevWon = -myHand.SeatActions[currPlayer].ChipsInvested;
                    }
                }

            } 
        }
        private static void SetCev_3Players(this Hand myHand, ref float resP1, ref float resP2, ref float resP3, ref string HCs_P1, ref string HCs_P2, ref string HCs_P3, ref string flopTurnRiver)
        {
            EVCalculator.ImportDLL.CalculateOdds_3Hands_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, HCs_P3 + flopTurnRiver, ref resP1, ref resP2, ref resP3);
            if (myHand.Info.SidePots.Count == 0)
            {
                myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon = (myHand.Info.TotalPot * resP1 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested;
                myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon = (myHand.Info.TotalPot * resP2 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested;
                myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].CevWon = (myHand.Info.TotalPot * resP3 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].ChipsInvested;
            }
            else
            {
                myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon += (myHand.Info.SidePots[0] * resP1 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested;
                myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += (myHand.Info.SidePots[0] * resP2 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested;
                myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].CevWon += (myHand.Info.SidePots[0] * resP3 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].ChipsInvested;

                if (myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested > myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested)
                {
                    if (myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested > myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].ChipsInvested)
                    {
                        // main Pot: P1,P2
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, HCs_P3, ref resP1, ref resP2, ref resP3);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon += myHand.Info.SidePots[1] * resP1 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += myHand.Info.SidePots[1] * resP2 / 100;
                    }
                    else
                    {
                        // main Pot: P1,P3
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P3 + flopTurnRiver, HCs_P2, ref resP1, ref resP3, ref resP2);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon += myHand.Info.SidePots[1] * resP1 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].CevWon += myHand.Info.SidePots[1] * resP3 / 100;
                    }
                }
                else
                {
                    if (myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested > myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].ChipsInvested)
                    {
                        // main Pot: P1,P2
                        // main Pot: P1,P2
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, HCs_P3, ref resP1, ref resP2, ref resP3);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon += myHand.Info.SidePots[1] * resP1 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += myHand.Info.SidePots[1] * resP2 / 100;
                    }
                    else
                    {
                        // main Pot: P2,P3
                        // main Pot: P1,P2
                        EVCalculator.ImportDLL.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P2 + flopTurnRiver, HCs_P3 + flopTurnRiver, HCs_P1, ref resP2, ref resP3, ref resP1);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += myHand.Info.SidePots[1] * resP2 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].CevWon += myHand.Info.SidePots[1] * resP3 / 100;
                    }
                }
            }
              
        }

        private static void SetCev_2Players(this Hand myHand, ref float resP1, ref float resP2)
        {
            if (myHand.Info.SidePots.Count == 0)
            {
                myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon = (myHand.Info.TotalPot * resP1 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested;
                myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon = (myHand.Info.TotalPot * resP2 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested;
            }
            else
            {
                if (myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested > myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested)
                {
                    myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon = (myHand.Info.SidePots[0] * resP1 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested + myHand.Info.SidePots[1];
                    myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon = (myHand.Info.SidePots[0] * resP2 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested;
                }
                else
                {
                    myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon = (myHand.Info.SidePots[0] * resP1 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].ChipsInvested;
                    myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon = (myHand.Info.SidePots[0] * resP2 / 100) - myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].ChipsInvested + myHand.Info.SidePots[1];
                }
            }
        }

        private static void SetCev_4PlayersPlus(this Hand myHand)
        {
            for (int i = 0; i < myHand.SeatActions.Count; i++)
            {
                string currPlayer = myHand.Info.Players[i];
                if (myHand.Info.SawShowdown_Players.Any(item => item.Item1 == currPlayer))
                {
                    myHand.SeatActions[currPlayer].CevWon = myHand.SeatActions[currPlayer].ChipsWon - myHand.SeatActions[currPlayer].ChipsInvested;
                }
            }
        }

        private static void CalculateChipsInvested(this Hand myHand)
        {
            foreach (var seatAction in myHand.SeatActions)
            {
                string currPlayer = seatAction.Key;

                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Ante + seatAction.Value.Blind;

                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.PreFlop.CalculateChipsInvestedPerStreet(amtBlind: seatAction.Value.Blind);
                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.Flop.CalculateChipsInvestedPerStreet();
                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.Turn.CalculateChipsInvestedPerStreet();
                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.River.CalculateChipsInvestedPerStreet();
            }
        }

        private static void CalculateChipsWon(this Hand myHand)
        {
            foreach (var seatAction in myHand.SeatActions)
            {
                string currPlayer = seatAction.Key;

                    myHand.SeatActions[currPlayer].ChipsWon -= myHand.SeatActions[currPlayer].ChipsInvested;
            }
        }

        private static float CalculateChipsInvestedPerStreet(this PlayerActionList  actions, float amtBlind = 0) // Set amtBlinds only if seatAction.Value.Actions.PreFlop !!!
        {
            float output = 0;
            if (true)
            {

            }
            foreach (var action in actions)
            {
                if (action.Act == "raises")
                {
                    output = action.Size - amtBlind;
                }
                else
                {
                    output += action.Size;
                }
            }

            return output;
        }

        private static void CalculateStreetAI(this Hand myHand)
        {
            if (myHand.Info.CntPlayers_Showdown > 0)
            {
                if (myHand.Info.CntPlayers_Flop == 0)
                {
                    myHand.Info.StreetAI = "PF";
                }
                else if (myHand.Info.CntPlayers_Turn == 0)
                {
                    myHand.Info.StreetAI = "F";
                }
                else if (myHand.Info.CntPlayers_River == 0)
                {
                    myHand.Info.StreetAI = "T";
                }
            }        
        }
        private static void Calculate_pf_open_opp(this Hand myHand)
        {
            bool openOpp = true;
            for (int i = 0; i < (myHand.Info.CntPlayers < myHand.StreetActions.PreflopActions.Count ? myHand.Info.CntPlayers : myHand.StreetActions.PreflopActions.Count); i++)
            {
                myHand.SeatActions[myHand.StreetActions.PreflopActions[i].Player].pf_open_opp = openOpp;
                if (myHand.StreetActions.PreflopActions[i].Act == "raises")
                {
                    openOpp = false;
                }
            }
        }

        private static void Calculate_pf_agressors(this Hand myHand)
        {
            for (int i = 0; i < myHand.StreetActions.PreflopActions.Count; i++)
            {
                if (myHand.StreetActions.PreflopActions[i].Act == "raises" || myHand.StreetActions.PreflopActions[i].Act == "bets")
                {
                    myHand.Info.pf_aggressors += myHand.SeatActions[myHand.StreetActions.PreflopActions[i].Player].SeatPosition.ToString();
                }   
            }
        }

        private static void Calculate_pf_actors(this Hand myHand)
        {
            for (int i = 0; i < myHand.StreetActions.PreflopActions.Count; i++)
            {
                if (myHand.StreetActions.PreflopActions[i].Act == "raises" || myHand.StreetActions.PreflopActions[i].Act == "bets" || myHand.StreetActions.PreflopActions[i].Act == "calls" || myHand.StreetActions.PreflopActions[i].Act == "checks")
                {
                    myHand.Info.pf_actors += myHand.SeatActions[myHand.StreetActions.PreflopActions[i].Player].SeatPosition.ToString();
                }
            }
        }

        public static void CalculateProperties(this List<Hand> myHandsList)
        {
            for (int i = 0; i < myHandsList.Count; i++)
            {
                if (myHandsList[i].Info.HandIdBySite == 225658452321)
                {
                    int debug1 = 0;
                }
                myHandsList[i].CalculateStreetAI();
                myHandsList[i].CalculateChipsInvested();
                myHandsList[i].CalculateCev();
                myHandsList[i].CalculateChipsWon();
                myHandsList[i].Calculate_pf_open_opp();
                myHandsList[i].Calculate_pf_agressors();
                myHandsList[i].Calculate_pf_actors();

            }
        }
    }
}


//private static void CalculateCev(this Hand myHand)
//{
//    float resP1 = 0;
//    float resP2 = 0;
//    float resP3 = 0;

//    if (myHand.Info.HandIdBySite == 225658452321)
//    {
//        int ff = 1;
//    }

//    if (myHand.Info.StreetAI == null)
//    {
//        for (int i = 0; i < myHand.SeatActions.Count; i++)
//        {
//            string currPlayer = myHand.Info.Players[i];
//            if (myHand.SeatActions[currPlayer].IsWinner == false)
//            {
//                myHand.SeatActions[currPlayer].CevWon = -myHand.SeatActions[currPlayer].ChipsInvested;
//            }
//            else
//            {
//                myHand.SeatActions[currPlayer].CevWon = myHand.SeatActions[currPlayer].ChipsWon - myHand.SeatActions[currPlayer].ChipsInvested;
//            }
//        }
//    }
//    else
//    {
//        if (myHand.Info.StreetAI == "PF")
//        {
//            if (myHand.Info.CntPlayers_Showdown == 2)
//            {
//                EVCalculator.ImportDLL.CalculateOdds_2Hands_PF(myHand.Info.SawShowdown_Players[0].Item2.HC1.Id, myHand.Info.SawShowdown_Players[0].Item2.HC2.Id, myHand.Info.SawShowdown_Players[1].Item2.HC1.Id, myHand.Info.SawShowdown_Players[1].Item2.HC2.Id, ref resP1, ref resP2, ref resP3, ea);

//                myHand.SetCev_2Players(ref resP1, ref resP2);
//            }
//            else if (myHand.Info.CntPlayers_Showdown == 3)
//            {
//                string HCs_P1 = myHand.Info.SawShowdown_Players[0].Item2.HC1.Name + myHand.Info.SawShowdown_Players[0].Item2.HC2.Name;
//                string HCs_P2 = myHand.Info.SawShowdown_Players[1].Item2.HC1.Name + myHand.Info.SawShowdown_Players[1].Item2.HC2.Name;
//                string HCs_P3 = myHand.Info.SawShowdown_Players[2].Item2.HC1.Name + myHand.Info.SawShowdown_Players[2].Item2.HC2.Name;
//                string flopTurnRiver = "";
//                myHand.SetCev_3Players(ref resP1, ref resP2, ref resP3, ref HCs_P1, ref HCs_P2, ref HCs_P3, ref flopTurnRiver);
//            }
//            else
//            {

//            }
//        }
//        else if (myHand.Info.StreetAI == "F")
//        {
//            if (myHand.Info.CntPlayers_Showdown == 2)
//            {
//                string HCs_P1 = myHand.Info.SawShowdown_Players[0].Item2.HC1.Name + myHand.Info.SawShowdown_Players[0].Item2.HC2.Name;
//                string HCs_P2 = myHand.Info.SawShowdown_Players[1].Item2.HC1.Name + myHand.Info.SawShowdown_Players[1].Item2.HC2.Name;
//                string flop = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name;
//                EVCalculator.ImportDLL.CalculateOdds_2Hands_ReturnFloat(HCs_P1 + flop, HCs_P2 + flop, ref resP1, ref resP2, ref resP3);

//                myHand.SetCev_2Players(ref resP1, ref resP2);
//            }
//            else if (myHand.Info.CntPlayers_Showdown == 3)
//            {
//                string HCs_P1 = myHand.Info.SawShowdown_Players[0].Item2.HC1.Name + myHand.Info.SawShowdown_Players[0].Item2.HC2.Name;
//                string HCs_P2 = myHand.Info.SawShowdown_Players[1].Item2.HC1.Name + myHand.Info.SawShowdown_Players[1].Item2.HC2.Name;
//                string HCs_P3 = myHand.Info.SawShowdown_Players[2].Item2.HC1.Name + myHand.Info.SawShowdown_Players[2].Item2.HC2.Name;
//                string flopTurnRiver = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name;
//                myHand.SetCev_3Players(ref resP1, ref resP2, ref resP3, ref HCs_P1, ref HCs_P2, ref HCs_P3, ref flopTurnRiver);
//            }
//            else
//            {

//            }
//        }
//        else if (myHand.Info.StreetAI == "T")
//        {
//            if (myHand.Info.CntPlayers_Showdown == 2)
//            {
//                string HCs_P1 = myHand.Info.SawShowdown_Players[0].Item2.HC1.Name + myHand.Info.SawShowdown_Players[0].Item2.HC2.Name;
//                string HCs_P2 = myHand.Info.SawShowdown_Players[1].Item2.HC1.Name + myHand.Info.SawShowdown_Players[1].Item2.HC2.Name;
//                string flopPlusTurn = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name + myHand.Info.TC.Name;
//                EVCalculator.ImportDLL.CalculateOdds_2Hands_ReturnFloat(HCs_P1 + flopPlusTurn, HCs_P2 + flopPlusTurn, ref resP1, ref resP2, ref resP3);
//                myHand.SetCev_2Players(ref resP1, ref resP2);
//            }
//            else if (myHand.Info.CntPlayers_Showdown == 3)
//            {
//                string HCs_P1 = myHand.Info.SawShowdown_Players[0].Item2.HC1.Name + myHand.Info.SawShowdown_Players[0].Item2.HC2.Name;
//                string HCs_P2 = myHand.Info.SawShowdown_Players[1].Item2.HC1.Name + myHand.Info.SawShowdown_Players[1].Item2.HC2.Name;
//                string HCs_P3 = myHand.Info.SawShowdown_Players[2].Item2.HC1.Name + myHand.Info.SawShowdown_Players[2].Item2.HC2.Name;
//                string flopTurnRiver = myHand.Info.FC1.Name + myHand.Info.FC2.Name + myHand.Info.FC3.Name + myHand.Info.TC.Name;
//                myHand.SetCev_3Players(ref resP1, ref resP2, ref resP3, ref HCs_P1, ref HCs_P2, ref HCs_P3, ref flopTurnRiver);
//            }
//            else
//            {

//            }
//        }

//        for (int i = 0; i < myHand.SeatActions.Count; i++)
//        {
//            string currPlayer = myHand.Info.Players[i];
//            if (!myHand.Info.SawShowdown_Players.Any(item => item.Item1 == currPlayer))
//            {
//                myHand.SeatActions[currPlayer].CevWon = -myHand.SeatActions[currPlayer].ChipsInvested;
//            }
//        }

//    }





//}