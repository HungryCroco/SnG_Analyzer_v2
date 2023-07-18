using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.SQL
{
    /// <summary>
    /// Class containing all Queries necessary to request HeatMaps from SQL DB;
    /// </summary>
    public class SQL_HeatMapQueries
    {
        /// <summary>
        /// Get a JSON of StatsModel grouped by HoleCard's Ids;
        /// </summary>
        public static string SQL_ExportHeatMapAsJSON =
            @"SELECT array_to_json(array_agg(row_to_json(t)))
			FROM (
				SELECT 
					hc.id AS hcs_id, 
					CASE WHEN fullt.Id IS null THEN 0 ELSE fullt.cnt END AS amt_situations
				FROM public.holecardssimple hc
				LEFT JOIN (
					SELECT hc.id , count(*) cnt 
	   				FROM public.Hands ha
					INNER JOIN public.seataction sa_hero ON (sa_hero.Id=ha.@seatActionHero)
					INNER JOIN public.holecardssimple hc ON hc.Id=sa_hero.hcssimpleid
					INNER JOIN public.Player hero ON hero.Id=sa_hero.PlayerId
					INNER JOIN public.seataction sa_villain ON (sa_villain.Id=ha.@seatActionVillain)
					INNER JOIN public.Player villain ON villain.Id=sa_villain.PlayerId

					WHERE	@whereClauseHero	
						  
				   GROUP BY hc.id
						) fullt ON fullt.Id = hc.Id
				) t
			";

        /// <summary>
        /// Get a DataGridView filtered by HoleCard's Id;
        /// </summary>
        public static string SQL_ExportDataGridViewByHoleCardsSimple =
            @"SELECT  
			r.room as room,
			ha.HandIdBySite as handId,
			tr.amt_BuyIn, 
			tr.Currency as Currency, 

			DATE ( ha.DateTimeHand) as PlayedOn, 
			hero.PlayerNickName,
			sa_hero.position as Pos,
			sa_hero.PreflopAction as PFA,
			CONCAT(hc1.Card, hc2.Card) as HoleCards,
			CONCAT(fc1.Card, fc2.Card, fc3.Card) as Flop,
			sa_hero.FlopAction as FlopA,
			tc.Card as Turn,
			sa_hero.TurnAction as TurnA,
			rc.Card as River,
			sa_hero.RiverAction as RiverA

			FROM public.Hands ha
				INNER JOIN public.seataction sa_hero ON (sa_hero.Id=@seatActionHero)
				INNER JOIN public.holecardssimple hc ON hc.Id=sa_hero.hcssimpleid
				INNER JOIN public.Player hero ON hero.Id=sa_hero.PlayerId
				INNER JOIN public.seataction sa_villain ON (sa_villain.Id=ha.@seatActionVillain)
				INNER JOIN public.Player villain ON villain.Id=sa_villain.PlayerId
				INNER JOIN public.Tournament tr on tr.Id=ha.TournamentId
				INNER JOIN public.Room r on r.Id=tr.roomid
				INNER JOIN public.Card hc1 ON hc1.Id=sa_hero.holecard1::integer
				INNER JOIN public.Card hc2 ON hc2.Id=sa_hero.holecard2::integer
				INNER JOIN public.Card fc1 ON fc1.Id=ha.flopcard1::integer
				INNER JOIN public.Card fc2 ON fc2.Id=ha.flopcard2::integer
				INNER JOIN public.Card fc3 ON fc3.Id=ha.flopcard3::integer
				INNER JOIN public.Card tc ON tc.Id=ha.flopcard3::integer
				INNER JOIN public.Card rc ON rc.Id=ha.flopcard3::integer

			WHERE @whereClauseHero
				AND sa_hero.HCsSimpleId= @HCsId::smallint";

        /// <summary>
        /// Where conditions for requesting BlindvsBlind openLimp Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string SQL_WhereClauseHero_BvB_oL =
			@"hero.PlayerNickName = '@hero'::varchar 
			AND sa_hero.flg_open_opp 
			AND villain.PlayerNickName @regList 
			AND ha.pf_actors LIKE '8%' 
			AND ha.cnt_players = 3 
			AND sa_hero.PreFlopAction LIKE 'C%' 
			AND (sa_hero.StartingStack / ha.Amt_bb) @es
			AND (sa_villain.StartingStack / ha.Amt_bb) @es
			AND ha.TournamentType = '@tourneyType'
			AND ha.datetimehand::date > DATE '@date'";

        /// <summary>
        /// Where conditions for requesting BlindvsBlind openRaise Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string SQL_WhereClauseHero_BvB_oR =
           @"hero.PlayerNickName = '@hero'::varchar 
			AND sa_hero.flg_open_opp 
			AND villain.PlayerNickName @regList 
			AND ha.pf_actors LIKE '89%' 
			AND ha.pf_aggressors LIKE '8%'
			AND ha.cnt_players = 3 
			AND sa_hero.PreFlopAction LIKE 'R%' 
			AND (sa_hero.StartingStack / ha.Amt_bb) @es
			AND (sa_villain.StartingStack / ha.Amt_bb) @es
			AND (regexp_replace(substring(sa_hero.PreFlopAction, 'R([0-9]+)'), '[^0-9]', '', 'g')::numeric / ha.Amt_bb::numeric) @size
			AND sa_hero.PreFlopAction ~ '@AI' 
			AND ha.TournamentType = '@tourneyType'
			AND ha.datetimehand::date > DATE '@date'";

        /// <summary>
        /// Where conditions for requesting BlindvsBlind Iso Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string SQL_WhereClauseHero_BvB_Iso =
            @"hero.PlayerNickName = '@hero'::varchar 
			AND sa_hero.flg_open_opp 
			AND villain.PlayerNickName @regList
			AND ha.pf_aggressors LIKE '9%'
			AND ha.pf_actors LIKE '89%' 
			AND ha.cnt_players = 3 
			AND sa_hero.PreFlopAction LIKE 'R%' 
			AND (sa_hero.StartingStack / ha.Amt_bb) @es
			AND (sa_villain.StartingStack / ha.Amt_bb) @es
			AND (regexp_replace(substring(sa_hero.PreFlopAction, 'R([0-9]+)'), '[^0-9]', '', 'g')::numeric / ha.Amt_bb::numeric) @size
			AND sa_hero.PreFlopAction ~ '@AI' 
			AND ha.TournamentType = '@tourneyType'
			AND ha.datetimehand::date > DATE '@date'";
    }
}
