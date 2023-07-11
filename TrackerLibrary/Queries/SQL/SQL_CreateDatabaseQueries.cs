using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.SQL
{
    public static class SQL_CreateDatabaseQueries
    {
        public static string sql_CreateTable_HandAsString =
            @"CREATE SEQUENCE IF NOT EXISTS public.handasstring_handid_seq
	            INCREMENT 1
	            START 1
	            MINVALUE 1
	            MAXVALUE 2147483647
	            CACHE 1;

            CREATE TABLE IF NOT EXISTS public.handasstring
            (
	            id integer NOT NULL DEFAULT nextval('handasstring_handid_seq'::regclass),
	            fullhand text COLLATE pg_catalog.default,
	            CONSTRAINT handasstring_pkey PRIMARY KEY (id)
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.handasstring
	            OWNER to postgres;";

        public static string sql_CreateTable_Hands =
            @"--CREATE SEQUENCE IF NOT EXISTS public.hands_id_seq
                --INCREMENT 1
                --START 1
                --MINVALUE 1
                --MAXVALUE 2147483647
                --CACHE 1;



            CREATE TABLE IF NOT EXISTS public.hands
            (
                handid integer NOT NULL,
                handidbysite bigint NOT NULL,
                tournamentid integer NOT NULL,
                levelhand character varying(10) COLLATE pg_catalog.""default"" NOT NULL,
                amt_bb integer NOT NULL,
                datetimehand timestamp without time zone NOT NULL,
                tableidbysite character varying(20) COLLATE pg_catalog.""default"" NOT NULL,
                tournamenttype character(10) COLLATE pg_catalog.""default"",
                heroid integer NOT NULL,
                holecard1 character varying(2) COLLATE pg_catalog.""default"",
                holecard2 character varying(2) COLLATE pg_catalog.""default"",
                seat1actionid integer,
                seat2actionid integer,
                seat3actionid integer,
                seat4actionid integer,
                seat5actionid integer,
                seat6actionid integer,
                seat7actionid integer,
                seat8actionid integer,
                seat9actionid integer,
                seat10actionid integer,
                flopcard1 character varying(2) COLLATE pg_catalog.""default"",
                flopcard2 character varying(2) COLLATE pg_catalog.""default"",
                flopcard3 character varying(2) COLLATE pg_catalog.""default"",
                turncard character varying(2) COLLATE pg_catalog.""default"",
                rivercard character varying(2) COLLATE pg_catalog.""default"",
                totalpot integer NOT NULL,
                timeimported timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
                heroseatactionid integer,
                btnseatactionid integer,
                sbseatactionid integer,
                bbseatactionid integer,
                CONSTRAINT ""fk_hands_handasstring_handId"" FOREIGN KEY (handid)
                    REFERENCES public.handasstring (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_hands_player_heroId"" FOREIGN KEY (heroid)
                    REFERENCES public.player (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_hands_seataction_seat1ActionId"" FOREIGN KEY (seat1actionid)
                    REFERENCES public.seataction (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_hands_seataction_seat2ActionId"" FOREIGN KEY (seat2actionid)
                    REFERENCES public.seataction (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_hands_seataction_seat3ActionId"" FOREIGN KEY (seat3actionid)
                    REFERENCES public.seataction (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_hands_tournament_tournamentId"" FOREIGN KEY (tournamentid)
                    REFERENCES public.tournament (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.hands
                OWNER to postgres;
            -- Index: IDX_BB_SA

            -- DROP INDEX IF EXISTS public.""IDX_BB_SA"";

            CREATE INDEX IF NOT EXISTS ""IDX_BB_SA""
                ON public.hands USING btree
                (bbseatactionid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: IDX_BTN_SA

            -- DROP INDEX IF EXISTS public.""IDX_BTN_SA"";

            CREATE INDEX IF NOT EXISTS ""IDX_BTN_SA""
                ON public.hands USING btree
                (btnseatactionid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: IDX_HERO_SA

            -- DROP INDEX IF EXISTS public.""IDX_HERO_SA"";

            CREATE INDEX IF NOT EXISTS ""IDX_HERO_SA""
                ON public.hands USING btree
                (heroseatactionid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: IDX_SB_SA

            -- DROP INDEX IF EXISTS public.""IDX_SB_SA"";

            CREATE INDEX IF NOT EXISTS ""IDX_SB_SA""
                ON public.hands USING btree
                (sbseatactionid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: handId

            -- DROP INDEX IF EXISTS public.""handId"";

            CREATE INDEX IF NOT EXISTS ""handId""
                ON public.hands USING btree
                (handid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: heroId

            -- DROP INDEX IF EXISTS public.""heroId"";

            CREATE INDEX IF NOT EXISTS ""heroId""
                ON public.hands USING btree
                (heroid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: seat1ActionId_index

            -- DROP INDEX IF EXISTS public.""seat1ActionId_index"";

            CREATE INDEX IF NOT EXISTS ""seat1ActionId_index""
                ON public.hands USING hash
                (seat1actionid)
                TABLESPACE pg_default;
            -- Index: seat2ActionId_index

            -- DROP INDEX IF EXISTS public.""seat2ActionId_index"";

            CREATE INDEX IF NOT EXISTS ""seat2ActionId_index""
                ON public.hands USING hash
                (seat2actionid)
                TABLESPACE pg_default;
            -- Index: seat3ActionId_index

            -- DROP INDEX IF EXISTS public.""seat3ActionId_index"";

            CREATE INDEX IF NOT EXISTS ""seat3ActionId_index""
                ON public.hands USING hash
                (seat3actionid)
                TABLESPACE pg_default;
            -- Index: tournamentId

            -- DROP INDEX IF EXISTS public.""tournamentId"";

            CREATE INDEX IF NOT EXISTS ""tournamentId""
                ON public.hands USING btree
                (tournamentid ASC NULLS LAST)
                TABLESPACE pg_default;";

        public static string sql_CreateTable_HoleCardsSimple =
            @"--CREATE SEQUENCE IF NOT EXISTS public.holecardssimple_id_seq
              --  INCREMENT 1
              --  START 1
              --  MINVALUE 1
              --  MAXVALUE 2147483647
              --  CACHE 1;

            CREATE TABLE IF NOT EXISTS public.holecardssimple
            (
                id integer NOT NULL,
                holecardsidbycs integer,
                holecardsasstring character varying(10) COLLATE pg_catalog.""default"",
                CONSTRAINT holecardssimple_pkey PRIMARY KEY (id)
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.holecardssimple
                OWNER to postgres;";

        public static string sql_CreateTable_Player =
            @"CREATE SEQUENCE IF NOT EXISTS public.player_id_seq
                INCREMENT 1
                START 1
                MINVALUE 1
                MAXVALUE 2147483647
                CACHE 1;

            CREATE TABLE IF NOT EXISTS public.player
            (
                id integer NOT NULL DEFAULT nextval('player_id_seq'::regclass),
                playernickname character varying(30) COLLATE pg_catalog.""default"" NOT NULL,
                roomid integer,
                CONSTRAINT player_pkey PRIMARY KEY (id),
                CONSTRAINT fk_player_room_roomid FOREIGN KEY (roomid)
                    REFERENCES public.room (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.player
                OWNER to postgres;";

        public static string sql_CreateTable_Room =
            @"CREATE SEQUENCE IF NOT EXISTS public.room_id_seq
                INCREMENT 1
                START 1
                MINVALUE 1
                MAXVALUE 2147483647
                CACHE 1;

            CREATE TABLE IF NOT EXISTS public.room
            (
                id integer NOT NULL DEFAULT nextval('room_id_seq'::regclass),
                room character varying(30) COLLATE pg_catalog.""default"",
                CONSTRAINT room_pkey PRIMARY KEY (id)
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.room
                OWNER to postgres;";

        public static string sql_CreateTable_SeatAction =
            @"CREATE SEQUENCE IF NOT EXISTS public.seataction_id_seq
                INCREMENT 1
                START 1
                MINVALUE 1
                MAXVALUE 2147483647
                CACHE 1;

            CREATE TABLE IF NOT EXISTS public.seataction
            (
                id integer NOT NULL DEFAULT nextval('seataction_id_seq'::regclass),
                playerid integer,
                seatnumber integer,
                ""position"" character varying(6) COLLATE pg_catalog.""default"",
                startingstack double precision,
                preflopaction character varying(30) COLLATE pg_catalog.""default"",
                flopaction character varying(30) COLLATE pg_catalog.""default"",
                turnaction character varying(30) COLLATE pg_catalog.""default"",
                riveraction character varying(30) COLLATE pg_catalog.""default"",
                holecard1 character varying(2) COLLATE pg_catalog.""default"",
                holecard2 character varying(2) COLLATE pg_catalog.""default"",
                hcssimpleid smallint,
                flg_open_opp boolean,
                flg_open_shove_p boolean DEFAULT false,
                flg_limp_p boolean DEFAULT false,
                preflopactionsimple character varying(30) COLLATE pg_catalog.""default"",
                p_act_1 character varying(2) COLLATE pg_catalog.""default"",
                p_act_1_size integer,
                p_act_2 character varying(2) COLLATE pg_catalog.""default"",
                p_act_2_size integer,
                p_act_3 character varying(2) COLLATE pg_catalog.""default"",
                p_act_3_size integer,
                p_act_4 character varying(2) COLLATE pg_catalog.""default"",
                p_act_4_size integer,
                p_act_5 character varying(2) COLLATE pg_catalog.""default"",
                p_act_5_size integer,
                p_act_6 character varying(2) COLLATE pg_catalog.""default"",
                p_act_6_size integer,
                chips_won integer,
                cev_won double precision,
                CONSTRAINT seataction_pkey PRIMARY KEY (id),
                CONSTRAINT fk_seataction_holecardssimple_id FOREIGN KEY (hcssimpleid)
                    REFERENCES public.holecardssimple (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_seataction_player_playerId"" FOREIGN KEY (playerid)
                    REFERENCES public.player (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.seataction
                OWNER to postgres;
            -- Index: hcssimpleid

            -- DROP INDEX IF EXISTS public.hcssimpleid;

            CREATE INDEX IF NOT EXISTS hcssimpleid
                ON public.seataction USING btree
                (hcssimpleid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: playerId

            -- DROP INDEX IF EXISTS public.""playerId"";

            CREATE INDEX IF NOT EXISTS ""playerId""
                ON public.seataction USING btree
                (playerid ASC NULLS LAST)
                TABLESPACE pg_default;";

        public static string sql_CreateTable_Tournament =
            @"CREATE SEQUENCE IF NOT EXISTS public.tournament_id_seq
                INCREMENT 1
                START 1
                MINVALUE 1
                MAXVALUE 2147483647
                CACHE 1;

            CREATE TABLE IF NOT EXISTS public.tournament
            (
                id integer NOT NULL DEFAULT nextval('tournament_id_seq'::regclass),
                roomid integer,
                tournamentidbysite bigint,
                tournamenttype character varying(15) COLLATE pg_catalog.""default"",
                tournamentdate timestamp without time zone,
                currency character varying(5) COLLATE pg_catalog.""default"",
                amt_buyin numeric,
                amt_fee numeric,
                amt_prize_pool_real numeric,
                amt_prize_pool_ev numeric,
                cnt_players numeric,
                seat1playerid integer,
                seat2playerid integer,
                seat3playerid integer,
                seat4playerid integer,
                seat5playerid integer,
                seat6playerid integer,
                seat7playerid integer,
                seat8playerid integer,
                seat9playerid integer,
                seat10playerid integer,
                tournamentwinnerid integer,
                CONSTRAINT tournament_pkey PRIMARY KEY (id),
                CONSTRAINT fk_tournament_player_seat1playerid FOREIGN KEY (seat1playerid)
                    REFERENCES public.player (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT fk_tournament_player_seat2playerid FOREIGN KEY (seat2playerid)
                    REFERENCES public.player (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT fk_tournament_player_seat3playerid FOREIGN KEY (seat3playerid)
                    REFERENCES public.player (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT fk_tournament_player_tournamentwinnerid FOREIGN KEY (tournamentwinnerid)
                    REFERENCES public.player (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_tournament_roomId"" FOREIGN KEY (roomid)
                    REFERENCES public.room (id) MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.tournament
                OWNER to postgres;
            -- Index: roomId

            -- DROP INDEX IF EXISTS public.""roomId"";

            CREATE INDEX IF NOT EXISTS ""roomId""
                ON public.tournament USING btree
                (roomid ASC NULLS LAST)
                TABLESPACE pg_default;
            -- Index: tournamentWinnerId

            -- DROP INDEX IF EXISTS public.""tournamentWinnerId"";

            CREATE INDEX IF NOT EXISTS ""tournamentWinnerId""
                ON public.tournament USING btree
                (tournamentwinnerid ASC NULLS LAST)
                TABLESPACE pg_default;";



    }
}
