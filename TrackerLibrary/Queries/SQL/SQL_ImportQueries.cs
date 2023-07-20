

namespace TrackerLibrary.Queries.SQL
{
    /// <summary>
    /// Class containing all Queries necessary to import to SQL-relational DB;
    /// p.s.: NoSQL Connector doesn't really need such class as NoSQL DB's shema is very simple;
    /// p.s.2: As these queries are related, it's important to run them in correct order to access the FOREIGN Key's after creating the related Column;
	/// Correct order is:
	///		1) Import To HoleCardsSimple (only while creating DB);
	///		2) Import To Cards (only while creating DB);
	///		3) Import To Room;
	///		4) Import To Player;
	///		5) Import To Tournament;
	///		6) Import To HandAsString;
	///		7) Import To SeatAction;
	///		8) Import To Hand
    /// </summary>
    public static class SQL_ImportQueries
    {
		/// <summary>
		/// Import to Table Tournament;
		/// </summary>
        public static string sql_ImportTournament =
            @"INSERT INTO public.Tournament 
				(roomId, tournamentidbysite, tournamenttype, tournamentdate, currency, amt_buyin, amt_fee, amt_prize_pool_real, cnt_players,
				 seat1playerId, seat2playerId, seat3playerId, seat4playerId, seat5playerId, seat6playerId, seat7playerId, seat8playerId, seat9playerId, seat10playerId) 
				
				 
					select @roomId::int, @tournamentidbysite::bigint, @tournamenttype::varchar, @tournamentdate::timestamp, @currency::varchar,
						@amt_buyin::float, @amt_fee::float, @amt_prize_pool_real::float, @cnt_players::smallint,
						@seat1playerId::int, @seat2playerId::int, @seat3playerId::int, @seat4playerId::int, @seat5playerId::int, 
						@seat6playerId::int, @seat7playerId::int, @seat8playerId::int, @seat9playerId::int, @seat10playerId::int
						
					where @tournamentidbysite::bigint not in (select tournamentidbysite from Tournament);

					(select Id from public.Tournament
						where tournament.tournamentidbysite = @tournamentidbysite::bigint);";

        /// <summary>
        /// Import to Table Room;
        /// </summary>
        public static string sql_ImportRoom =
            @"INSERT INTO public.room 
            (room) 
                select @room::varchar
	            where @room::varchar not in (select room from public.room);   

	            (select Id from public.room
	            where room.room = @room::varchar);";

        /// <summary>
        /// Import to Table Player;
        /// </summary>
        public static string sql_ImportPlayer =
            //params: @activePlayer::varchar, @room::varchar
            @"INSERT INTO public.Player 
            (PlayerNickName, roomId) 
                select @activePlayer::varchar, @roomId::int
	            where @activePlayer::varchar not in (select PlayerNickName from Player);   

	            (select Id from public.Player
	            where player.PlayerNickName = @activePlayer::varchar);";

        /// <summary>
        /// Import to Table HandAsAtring;
        /// </summary>
        public static string sql_ImportHandAsAtring =
            @"INSERT INTO public.handAsString
				(fullHand) 
				 
					select @fullHand::text;

			select currval('handasstring_handid_seq'::regclass)";

        /// <summary>
        /// Import to Table SeatAction;
        /// </summary>
        public static string sql_ImportSeatAction =
            @"INSERT INTO public.SeatAction 
				(playerid, seatnumber, position, startingstack,
				preflopaction, flopaction, turnaction, riveraction,
				holecard1, holecard2, HCsSimpleId,
				flg_open_opp, preflopactionsimple, p_act_1, p_act_1_size,
				p_act_2, p_act_2_size, p_act_3, p_act_3_size, p_act_4, p_act_4_size,
				p_act_5, p_act_5_size, p_act_6, p_act_6_size, chips_won, cev_won)

					select @PlayerId::int, @SeatNumber::int, @Position::varchar,
					@StartingStack::int, @PreflopAction::varchar, @FlopAction::varchar,
					@TurnAction::varchar, @RiverAction::varchar, @HoleCard1::varchar, @HoleCard2::varchar, @HCsSimpleId::smallint,
					@flg_open_opp::bool, @PreflopActionSimple::varchar, @p_act_1::varchar, @p_act_1_size::int,
					@p_act_2::varchar, @p_act_2_size::int, @p_act_3::varchar, @p_act_3_size::int,
					@p_act_4::varchar, @p_act_4_size::int, @p_act_5::varchar, @p_act_5_size::int, @p_act_6::varchar, @p_act_6_size::int, 
					@chips_won::int, @cev_won::float;


					select currval('seataction_id_seq'::regclass);";

        /// <summary>
        /// Import to MAIN Table Hands;
        /// </summary>
        public static string sql_ImportToHand =
            @"INSERT INTO public.Hands 
				(handId, handidbysite, tournamentId, levelhand, amt_bb,
				datetimehand, tableidbysite, tournamenttype, heroid,
				holecard1, holecard2, flopcard1, flopcard2, flopcard3, turncard, rivercard,
				totalpot, pf_aggressors, pf_actors, seat1actionid, seat2actionid, seat3actionid, seat4actionid, seat5actionid,
				seat6actionid, seat7actionid, seat8actionid, seat9actionid, seat10actionid,
				heroseatactionid, btnseatactionid, sbseatactionid, bbseatactionid, 
				cnt_players, cnt_players_flop, cnt_players_turn, cnt_players_river, cnt_players_showdown) 
					select @handId::int, @handidbysite::bigint, @tournamentId::int, @levelhand::varchar,@amt_bb::int, 
					@datetimehand::timestamp, @tableidbysite::varchar, @tournamenttype::varchar,
					@heroid::int, @holecard1::int, @holecard2::int, 
					@flopcard1::int, @flopcard2::int, @flopcard3::int, @turncard::int, @rivercard::int,
					@totalpot::int, @pf_aggressors::varchar, @pf_actors::varchar, @seat1actionid::int, @seat2actionid::int, @seat3actionid::int, @seat4actionid::int, @seat5actionid::int,
					@seat6actionid::int, @seat7actionid::int, @seat8actionid::int, @seat9actionid::int, @seat10actionid::int,
					@heroseatactionid::int, @btnseatactionid::int, @sbseatactionid::int, @bbseatactionid::int,
					@cnt_players::int, @cnt_players_flop::int, @cnt_players_turn::int, @cnt_players_river::int, @cnt_players_showdown::int

					--where @handidbysite::bigint not in (select handidbysite from hands);";

        /// <summary>
        /// Import to Table HoleCardsSimpleIds;
        /// </summary>
        public static string sql_ImportHoleCardsSimpleIds =
            @"INSERT INTO public.holecardssimple
				(id, holecardsasstring) 
				 
					select @hcId::int, @hcAsString::text
					where @hcAsString::text not in (select holecardsasstring from public.holecardssimple);";

        /// <summary>
        /// Import to Table Card;
        /// </summary>
        public static string sql_ImportToCard =
            @"INSERT INTO public.card
				(id, card) 
				 
					select @hcId::int, @hcAsString::text
					where @hcAsString::text not in (select card from public.card);";

    }

  
}
