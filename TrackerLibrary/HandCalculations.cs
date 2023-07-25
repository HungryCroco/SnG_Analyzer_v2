using TrackerLibrary.Models;


namespace TrackerLibrary
{
    /// <summary>
    /// This Class contains all Methods, necessary to Calculate Properties of fully readed Hands;
    /// </summary>
    public static class HandCalculations
    {
        // An 4 Dimensional Array containing the preFlop equities  for each 2 Hand-combinations;
        // current RAM ussage ~= 200MB; However, the array could be refactured to jagged Array and use the half RAM;
        private static float[,,,] ea = EVCalculator.ReadEAFromFileAsFloatArray(GlobalConfig.GetMainFolderPath() + GlobalConfig.pfEA);

        /// <summary>
        /// Calculates the expected Value [Chips] and updated the hand's property;
        /// </summary>
        /// <param name="myHand">Hand;</param>
        private static void CalculateCev(this Hand myHand)
        {
            float resP1 = 0;
            float resP2 = 0;
            float resP3 = 0;

            //Check if there is an ALLIN;
            //  - If not - Set cEV to ChipsWon - ChipsInvested;
            //  - If yes - Check on which sreet did happen the ALL IN, how many players were involved, if 3+ players check for side Pots; Call the relevant Equity calculation Method and find the equity of each player;
            //  Set the cEV to Pot*Equity for each player involved in the ALL IN;
            //  - Set the cEV of all players that did fold to -chipsInvested;
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
                        EVCalculator.CalculateOdds_2Hands_PF(myHand.Info.SawShowdown_Players[0].Item2.HC1.Id, myHand.Info.SawShowdown_Players[0].Item2.HC2.Id, myHand.Info.SawShowdown_Players[1].Item2.HC1.Id, myHand.Info.SawShowdown_Players[1].Item2.HC2.Id, ref resP1, ref resP2, ref resP3, ref ea);
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
                        EVCalculator.CalculateOdds_2Hands_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, ref resP1, ref resP2, ref resP3);
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
                        EVCalculator.CalculateOdds_2Hands_ReturnFloat(HCs_P1 + flopPlusTurn, HCs_P2 + flopPlusTurn, ref resP1, ref resP2, ref resP3);
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

                //  - Set the cEV of all players that did fold to -chipsInvested;
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

        /// <summary>
        /// Calculates the cEV(expected Chips) in 3players-AllIns and Updates the related properties of the Hand;
        /// </summary>
        /// <param name="myHand">Hand</param>
        /// <param name="resP1">Equity of the 1.Player</param>
        /// <param name="resP2">Equity of the 2.Player</param>
        /// <param name="resP3">Equity of the 3.Player</param>
        /// <param name="HCs_P1">HoleCards of 1.Player</param>
        /// <param name="HCs_P2">HoleCards of 2.Player</param>
        /// <param name="HCs_P3">HoleCards of 3.Player</param>
        /// <param name="flopTurnRiver">string, containing the common Cards in format: Flop - "Jh8d6c"/Turn - "Jh8d6c3c"/River - "Jh8d6c3c2c" dealth before the last all-in happened</param>
        private static void SetCev_3Players(this Hand myHand, ref float resP1, ref float resP2, ref float resP3, ref string HCs_P1, ref string HCs_P2, ref string HCs_P3, ref string flopTurnRiver)
        {
            // Calculate the equities of all players:
            EVCalculator.CalculateOdds_3Hands_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, HCs_P3 + flopTurnRiver, ref resP1, ref resP2, ref resP3);


            // Set cEV to mainPot*Equity - ChipsInvested;
            // Check for sidePots;
            //  - If none - calc finished;
            //  - If yes - calc the new equities of the remaining 2 players using the 3.player's cards as dead cards;
            //  Calculate the extra cEV from the sidePot and add to the related cEVs from the main pot;

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
                        EVCalculator.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, HCs_P3, ref resP1, ref resP2, ref resP3);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon += myHand.Info.SidePots[1] * resP1 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += myHand.Info.SidePots[1] * resP2 / 100;
                    }
                    else
                    {
                        // main Pot: P1,P3
                        EVCalculator.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P3 + flopTurnRiver, HCs_P2, ref resP1, ref resP3, ref resP2);

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
                        EVCalculator.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P1 + flopTurnRiver, HCs_P2 + flopTurnRiver, HCs_P3, ref resP1, ref resP2, ref resP3);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[0].Item1].CevWon += myHand.Info.SidePots[1] * resP1 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += myHand.Info.SidePots[1] * resP2 / 100;
                    }
                    else
                    {
                        // main Pot: P2,P3
                        // main Pot: P1,P2
                        EVCalculator.CalculateOdds_2Hands_WithDeathCards_ReturnFloat(HCs_P2 + flopTurnRiver, HCs_P3 + flopTurnRiver, HCs_P1, ref resP2, ref resP3, ref resP1);

                        myHand.SeatActions[myHand.Info.SawShowdown_Players[1].Item1].CevWon += myHand.Info.SidePots[1] * resP2 / 100;
                        myHand.SeatActions[myHand.Info.SawShowdown_Players[2].Item1].CevWon += myHand.Info.SidePots[1] * resP3 / 100;
                    }
                }
            }
              
        }

        /// <summary>
        /// Calculates the cEV(expected Chips) in 2players-AllIns and Updates the related properties of the Hand;
        /// </summary>
        /// <param name="myHand">Hand</param>
        /// <param name="resP1">Equity of the 1.Player</param>
        /// <param name="resP2">Equity of the 2.Player</param>
        private static void SetCev_2Players(this Hand myHand, ref float resP1, ref float resP2)
        {
            // Check for side pots; 
            // - If none, set the cEV of each player = player's Equity * main Pot;
            // - If any , check which player did win the side pot(side Pots in 2players all in happen when a player goes AI on earlier street and 2+ other players continue; Then all but one folds, so the left player takes 100% of the side pot;
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

        /// <summary>
        /// 4+ players EquityCalcs are currently not supported; So we just set the cEV to the actual chips won; 
        /// It could be improved in the future, but cEV queries generally are valuable in cases of all ins with only 2 players; 
        /// As 3 players all-ins aren't very rare and could affect signiffically the entire EV / period we support them as well;
        /// 4+ players all ins are very rare and complicated, so just skipping them is totally fine;
        /// </summary>
        /// <param name="myHand">Hand</param>
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

        /// <summary>
        /// Calculates the chips invested from all players in the Hand and updates the related Property;
        /// </summary>
        /// <param name="myHand">Hand</param>
        private static void CalculateChipsInvested(this Hand myHand)
        {
            foreach (var seatAction in myHand.SeatActions)
            {
                string currPlayer = seatAction.Key;

                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Ante + seatAction.Value.Blind;

                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.PreFlop.CalculateChipsInvestedPerStreet(amtBlind: seatAction.Value.Blind); // PreFlop;
                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.Flop.CalculateChipsInvestedPerStreet(); // Flop;
                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.Turn.CalculateChipsInvestedPerStreet(); // Turn;
                myHand.SeatActions[currPlayer].ChipsInvested += seatAction.Value.Actions.River.CalculateChipsInvestedPerStreet(); // River;
            }
        }

        /// <summary>
        /// Calculates the real amount of chips won/lost in the Hand and updates the related Property;
        /// </summary>
        /// <param name="myHand">Hand</param>
        private static void CalculateChipsWon(this Hand myHand)
        {
            foreach (var seatAction in myHand.SeatActions)
            {
                string currPlayer = seatAction.Key;

                    myHand.SeatActions[currPlayer].ChipsWon -= myHand.SeatActions[currPlayer].ChipsInvested;
            }
        }

        /// <summary>
        /// Calculates the Amount of Chips invested by a player on 1 Street, defined by the choosen PlayerActionList;
        /// </summary>
        /// <param name="actions">All SeatActions done by the player on 1 Street;</param>
        /// <param name="amtBlind">Blind + Ante payed from the player in the Hand;</param>
        /// <returns>The Amount of chips that the player did invest on the Street;</returns>
        private static float CalculateChipsInvestedPerStreet(this PlayerActionList  actions, float amtBlind = 0) // Set amtBlinds only if seatAction.Value.Actions.PreFlop !!!
        {
            // If the player "raises" , we need to substract the amt of Blinds of the Action's size;
            // All other Actions doesn't include the Blinds, so we just add them to the output's value;
            float output = 0;

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

        /// <summary>
        /// Updates the Street on which the all in did happen;
        /// </summary>
        /// <param name="myHand">Hand</param>
        private static void CalculateStreetAI(this Hand myHand)
        {
            // Check how many players did reach a Showdown, update only if >0;
            // Check the Amt of players(Actions) on all PostFlop streets; If we have a showdown, but no Action on some street, then it means that ai happened on the previous one;
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

        /// <summary>
        /// Checks if the players could open(Raise First) PreFlop and updates the related property;
        /// </summary>
        /// <param name="myHand">Hand</param>
        private static void Calculate_pf_open_opp(this Hand myHand)
        {
            // Check if any player did raise before the current one, if not => openOpp = true, if yes openOpp = false;
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

        /// <summary>
        /// Calculates a string containing the position of all players that did an aggressive action ( bet, raise) ordered by the Action and updates the related property;
        /// </summary>
        /// <param name="myHand">Hand</param>
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

        /// <summary>
        /// Calculates a string containing the position of all players that did an passive action but didn't fold ( bet, raise, checks, calls) ordered by the Action and updates the related property;
        /// </summary>
        /// <param name="myHand">Hand</param>
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

        /// <summary>
        /// Calculates all Properties and updates them;
        /// </summary>
        /// <param name="myHandsList">A List of Hands which properties will be updated;</param>
        public static void CalculateProperties(this List<Hand> myHandsList)
        {
            for (int i = 0; i < myHandsList.Count; i++)
            {

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
